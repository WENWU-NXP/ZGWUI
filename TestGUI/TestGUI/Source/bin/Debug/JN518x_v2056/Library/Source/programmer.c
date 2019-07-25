
/****************************************************************************
 *
 * This software is owned by NXP B.V. and/or its supplier and is protected
 * under applicable copyright laws. All rights are reserved. We grant You,
 * and any third parties, a license to use this software solely and
 * exclusively on NXP products [NXP Microcontrollers such as JN5148, JN5142, JN5139].
 * You, and any third parties must reproduce the copyright and warranty notice
 * and any other legend of ownership on each copy or partial copy of the
 * software.
 *
 * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS"
 * AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE
 * IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE
 * ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE
 * LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR
 * CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF
 * SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS
 * INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN
 * CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE)
 * ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE
 * POSSIBILITY OF SUCH DAMAGE.

 * Copyright NXP B.V. 2012-2018. All rights reserved
 *
 ***************************************************************************/
/****************************************************************************/
/***        Include files                                                 ***/
/****************************************************************************/

#include <stdio.h>
#include <unistd.h>
#include <stdint.h>
#include <string.h>
#include <stdarg.h>
#include <stdlib.h>
#include <errno.h>
#include <sys/types.h>
#include <sys/stat.h>
#include <fcntl.h>

#if defined WIN32
#include <windows.h>
#include <malloc.h>
#else
//#include <arpa/inet.h>
#endif /* WIN32 */

#include <programmer.h>

#include "programmer_private.h"
#include "ChipID.h"
#include "protocol_jn51xx_v1.h"
#include "protocol_jn51xx_v2.h"
#include "uart.h"
#if defined WIN32
#include "pipe.h"
#endif
#include "dbg.h"
#include "portable_endian.h"

#ifdef USE_TOMCRYPT
#include <tomcrypt.h>
#endif

/****************************************************************************/
/***        Macro Definitions                                             ***/
/****************************************************************************/

#ifdef DEBUG_PROGRAMMER
#define TRACE_PROGRAMMER    TRUE
#else
#define TRACE_PROGRAMMER    FALSE
#endif

#ifndef O_BINARY
# ifdef _O_BINARY
# define O_BINARY _O_BINARY
# else
# define O_BINARY 0
# endif
#endif

#define BL_MAX_CHUNK_SIZE   512

#define IS_JN5189_ES2(psContext)     ePRG_IsJN5189(psContext,"pFlash")
#define IS_JN5189_ES1(psContext)     ePRG_IsJN5189(psContext,"pOTP")

/****************************************************************************/
/***        Type Definitions                                              ***/
/****************************************************************************/

/****************************************************************************/
/***        Local Function Prototypes                                     ***/
/****************************************************************************/
static teStatus ePRG_IsJN5189(tsPRG_Context *psContext, char* mem_name);
static teStatus ePRG_ChipGetChipName(tsPRG_Context *psContext);
static teStatus ePRG_ChipGetMemoryInfo(tsPRG_Context *psContext);
static teStatus ePRG_ChipGetMacAddress(tsPRG_Context *psContext);
static teStatus ePRG_SetUpImage(tsPRG_Context *psContext, tsFW_Info *psFWImage, tsChipDetails *psChipDetails);
static teStatus ePRG_ResetDevice(tsPRG_Context *psContext);


/****************************************************************************/
/***        Exported Variables                                            ***/
/****************************************************************************/

/** Version string for libprogrammer */
#ifndef VERSION_MAJOR
#error Major Version is not defined!
#else
#ifndef VERSION_MINOR
#error Minor Version is not defined!
#else
#ifndef VERSION_SVN
#error SVN Version is not defined!
#else
const char *pcPRG_Version = VERSION_MAJOR "." VERSION_MINOR " (r" VERSION_SVN ")";
#endif
#endif
#endif

/** Import binary data from FlashProgrammerExtension_JN5168.bin */
#if defined POSIX
extern int _binary_FlashProgrammerExtension_JN5168_bin_start;
extern int _binary_FlashProgrammerExtension_JN5168_bin_end;
#define FLASHPROGRAMMEREXTENSION_JN5168_BIN_START  ((uint8_t *)    &_binary_FlashProgrammerExtension_JN5168_bin_start)
#define FLASHPROGRAMMEREXTENSION_JN5168_BIN_END    ((uint8_t *)    &_binary_FlashProgrammerExtension_JN5168_bin_end)
#elif defined WIN32
extern int binary_FlashProgrammerExtension_JN5168_bin_start;
extern int binary_FlashProgrammerExtension_JN5168_bin_end;
#define FLASHPROGRAMMEREXTENSION_JN5168_BIN_START  ((uint8_t *)    &binary_FlashProgrammerExtension_JN5168_bin_start)
#define FLASHPROGRAMMEREXTENSION_JN5168_BIN_END    ((uint8_t *)    &binary_FlashProgrammerExtension_JN5168_bin_end)
#endif

/** Import binary data from FlashProgrammerExtension_JN5169.bin */
#if defined POSIX
extern int _binary_FlashProgrammerExtension_JN5169_bin_start;
extern int _binary_FlashProgrammerExtension_JN5169_bin_end;
#define FLASHPROGRAMMEREXTENSION_JN5169_BIN_START  ((uint8_t *)    &_binary_FlashProgrammerExtension_JN5169_bin_start)
#define FLASHPROGRAMMEREXTENSION_JN5169_BIN_END    ((uint8_t *)    &_binary_FlashProgrammerExtension_JN5169_bin_end)
#elif defined WIN32
extern int binary_FlashProgrammerExtension_JN5169_bin_start;
extern int binary_FlashProgrammerExtension_JN5169_bin_end;
#define FLASHPROGRAMMEREXTENSION_JN5169_BIN_START  ((uint8_t *)    &binary_FlashProgrammerExtension_JN5169_bin_start)
#define FLASHPROGRAMMEREXTENSION_JN5169_BIN_END    ((uint8_t *)    &binary_FlashProgrammerExtension_JN5169_bin_end)
#endif

/** Import binary data from FlashProgrammerExtension_JN5179.bin */
#if defined POSIX
extern int _binary_FlashProgrammerExtension_JN5179_bin_start;
extern int _binary_FlashProgrammerExtension_JN5179_bin_end;
#define FLASHPROGRAMMEREXTENSION_JN5179_BIN_START  ((uint8_t *)    &_binary_FlashProgrammerExtension_JN5179_bin_start)
#define FLASHPROGRAMMEREXTENSION_JN5179_BIN_END    ((uint8_t *)    &_binary_FlashProgrammerExtension_JN5179_bin_end)
#elif defined WIN32
extern int binary_FlashProgrammerExtension_JN5179_bin_start;
extern int binary_FlashProgrammerExtension_JN5179_bin_end;
#define FLASHPROGRAMMEREXTENSION_JN5179_BIN_START  ((uint8_t *)    &binary_FlashProgrammerExtension_JN5179_bin_start)
#define FLASHPROGRAMMEREXTENSION_JN5179_BIN_END    ((uint8_t *)    &binary_FlashProgrammerExtension_JN5179_bin_end)
#endif

/****************************************************************************/
/***        Local Variables                                               ***/
/****************************************************************************/

static const uint8_t au8UnlockKey[] = {
	0x11, 0x22, 0x33, 0x44, 0x55, 0x66, 0x77, 0x88,
	0x11, 0x22, 0x33, 0x44, 0x55, 0x66, 0x77, 0x88,
};



/****************************************************************************/
/***        Exported Functions                                            ***/
/****************************************************************************/

teStatus LIBPROGRAMMER ePRG_Init(tsPRG_Context *psContext)
{
	DBG_vPrintf(TRACE_PROGRAMMER, "ePRG_Init()\n");

    memset(psContext, 0, sizeof(tsPRG_Context));

    psContext->pvPrivate = malloc(sizeof(tsPRG_PrivateContext));

    if (!psContext->pvPrivate)
    {
        return E_PRG_OUT_OF_MEMORY;
    }

    memset(psContext->pvPrivate, 0, sizeof(tsPRG_PrivateContext));

    /* Default is to automatically strobe program / reset lines if possible */
    psContext->sFlags.bAutoProgramReset = 1;


    return ePRG_SetStatus(psContext, E_PRG_OK, "");
}

teStatus LIBPROGRAMMER ePRG_Destroy(tsPRG_Context *psContext)
{
    tsMemInfo *psCurrent, *psNext;

    DBG_vPrintf(TRACE_PROGRAMMER, "ePRG_Destroy()\n");

    ePRG_FwClose(psContext);

    ePRG_ConnectionClose(psContext);

    psCurrent = psContext->sChipDetails.psMemInfoList;

    while (psCurrent != NULL)
    {
        psNext = psCurrent->psNext;
        free(psCurrent);
        psCurrent = psNext;
    }

    free(psContext->pvPrivate);

    return E_PRG_OK;
}


char *LIBPROGRAMMER pcPRG_GetLastStatusMessage(tsPRG_Context *psContext)
{
    tsPRG_PrivateContext *psPriv = (tsPRG_PrivateContext *)psContext->pvPrivate;

    if (psPriv)
    {
        return psPriv->acStatusMessage;
    }
    return NULL;
}

tsProtocol* apsProtocols[] = { &sProtocol_v2, &sProtocol_v1 };

teStatus LIBPROGRAMMER ePRG_ConnectionOpen(tsPRG_Context *psContext, tsConnection *psConnection)
{
    tsPRG_PrivateContext *psPriv;
	teStatus eStatus;
	int i;

	DBG_vPrintf(TRACE_PROGRAMMER, "ePRG_ConnectionOpen()\n");
    if ((!psContext) || (!psConnection))
    {
        return E_PRG_NULL_PARAMETER;
    }

    psPriv = (tsPRG_PrivateContext *)psContext->pvPrivate;

    /* Close any existing connection */
    ePRG_ConnectionClose(psContext);

    DBG_vPrintf(TRACE_PROGRAMMER, "Opening connection %d\n", psConnection->eType);

    memcpy(&psPriv->sConnection, psConnection, sizeof(tsConnection));

    psPriv->sConnection.pcName = strdup(psConnection->pcName);
    if (!psPriv->sConnection.pcName)
    {
        return ePRG_SetStatus(psContext, E_PRG_OUT_OF_MEMORY, "");
    }

    switch(psConnection->eType)
    {
        case (E_CONNECT_SERIAL):
            if (psPriv->sConnection.uDetails.sSerial.cbOpen == NULL) {
                psPriv->sConnection.uDetails.sSerial.cbOpen   = ePRG_ConnectionUartOpen;
                psPriv->sConnection.uDetails.sSerial.cbClose  = ePRG_ConnectionUartClose;
                psPriv->sConnection.uDetails.sSerial.cbUpdate = ePRG_ConnectionUartUpdate;
                psPriv->sConnection.uDetails.sSerial.cbFlush  = eUART_Flush_Int;
                psPriv->sConnection.uDetails.sSerial.cbRead   = eUART_Read_Int;
                psPriv->sConnection.uDetails.sSerial.cbWrite  = eUART_Write_Int;
            }
			eStatus = psPriv->sConnection.uDetails.sSerial.cbOpen(psContext, psConnection);
        	break;

        case (E_CONNECT_PIPE):
			eStatus = ePRG_ConnectionPipeOpen(psContext, psConnection);
        	break;

        default:
            return E_PRG_INVALID_TRANSPORT;
    }

    psPriv = (tsPRG_PrivateContext *)psContext->pvPrivate;

    if (eStatus != E_PRG_OK)
    {
    	return eStatus;
    }

    memset(&psContext->sChipDetails, 0, sizeof(tsChipDetails));

    for (i = 0; i < sizeof(apsProtocols) / sizeof(apsProtocols[0]); i++)
    {
    	eStatus = apsProtocols[i]->eBL_Connect(psContext, &psPriv->sConnection);

    	if (eStatus == E_PRG_OK)
    	{
    		psPriv->psProtocol = apsProtocols[i];
    		break;
    	}
    }

    return eStatus;
}


teStatus LIBPROGRAMMER ePRG_ConnectionClose(tsPRG_Context *psContext)
{
    teStatus eStatus;
    tsPRG_PrivateContext *psPriv;

    DBG_vPrintf(TRACE_PROGRAMMER, "ePRG_ConnectionClose()\n");

    if (!psContext)
    {
        return E_PRG_NULL_PARAMETER;
    }

    psPriv = (tsPRG_PrivateContext *)psContext->pvPrivate;

    switch(psPriv->sConnection.eType)
    {
        case (E_CONNECT_NONE):
            eStatus = E_PRG_OK;
            break;

        case (E_CONNECT_SERIAL):
            if (psContext->sFlags.bAutoProgramReset)
            {
                ePRG_ResetDevice(psContext);
            }
            eStatus = psPriv->sConnection.uDetails.sSerial.cbClose(psContext);
            break;

        case (E_CONNECT_PIPE):
            if (psContext->sFlags.bAutoProgramReset)
            {
                ePRG_ResetDevice(psContext);
            }
            eStatus = ePRG_ConnectionPipeClose(psContext);
            break;

        default:
            eStatus = E_PRG_INVALID_TRANSPORT;
            break;
    }

    psPriv->sConnection.eType = E_CONNECT_NONE;
    return eStatus;
}

teStatus LIBPROGRAMMER ePRG_ConnectionGet(tsPRG_Context *psContext, tsConnection *psConnection)
{
    tsPRG_PrivateContext *psPriv;

    if ((psContext == NULL) || (psConnection == NULL))
    {
        return E_PRG_NULL_PARAMETER;
    }
    psPriv = (tsPRG_PrivateContext *)psContext->pvPrivate;

    psConnection->uDetails = psPriv->sConnection.uDetails;

    return E_PRG_OK;
}

teStatus ePRG_ConnectionUpdate(tsPRG_Context *psContext, tsConnection *psConnection)
{
    tsPRG_PrivateContext *psPriv;
    teStatus eStatus;

    DBG_vPrintf(TRACE_PROGRAMMER, "ePRG_ConnectionUpdate()\n");
    if ((psContext == NULL) || (psConnection == NULL))
    {
        return E_PRG_NULL_PARAMETER;
    }
    psPriv = (tsPRG_PrivateContext *)psContext->pvPrivate;

    switch(psPriv->sConnection.eType)
    {
        case (E_CONNECT_NONE):
            eStatus = E_PRG_OK;
            break;

        case (E_CONNECT_SERIAL):
            /* Change bootloader to new speed */
            if ((eStatus = psPriv->psProtocol->eBL_SetBaudrate(psContext, psConnection->uDetails.sSerial.u32BaudRate)) != E_PRG_OK)
            {
                switch (CHIP_ID_PART(psContext->sChipDetails.u32ChipId))
                {
                    /* These 4x bootloaders corrupt the CRC byte on the change baud response so are expected to fail */
                    case (CHIP_ID_PART(CHIP_ID_JN5148_REV2A)):
                    case (CHIP_ID_PART(CHIP_ID_JN5142_REV1A)):
                    case (CHIP_ID_PART(CHIP_ID_JN5142_REV1B)):
                    case (CHIP_ID_PART(CHIP_ID_JN5142_REV1C)):
                        DBG_vPrintf(TRACE_PROGRAMMER, "Expected CRC fail\n");
                        break;
                    default:
                        DBG_vPrintf(TRACE_PROGRAMMER, "Error selecting baud rate\n");
                        return ePRG_SetStatus(psContext, eStatus, "selecting baud rate");
                }
            }

            /* change local port to new speed */
            if ((eStatus = psPriv->sConnection.uDetails.sSerial.cbUpdate(psContext, psConnection)) != E_PRG_OK)
            {
                return ePRG_SetStatus(psContext, eStatus, "selecting baud rate");
            }

            if ((eStatus = eUART_Flush(psContext)) != E_PRG_OK)
            {
                return ePRG_SetStatus(psContext, eStatus, "flushing UART");
            }
            DBG_vPrintf(TRACE_PROGRAMMER, "Flushing UART OK\n");
            break;

        case (E_CONNECT_PIPE):
            eStatus = E_PRG_OK;
            break;

        default:
            eStatus = E_PRG_INVALID_TRANSPORT;
            break;
    }

    psPriv->sConnection.uDetails = psConnection->uDetails;

    return ePRG_SetStatus(psContext, E_PRG_OK, "");
}


teStatus ePRG_Unlock(tsPRG_Context *psContext, uint8_t u8Mode, const uint8_t *pu8UnlockKey, uint16_t u16KeyLen)
{
    int i;
    tsPRG_PrivateContext *psPrivate = psContext->pvPrivate;

    DBG_vPrintf(TRACE_PROGRAMMER, "Unlock %d %p %d\r\n", u8Mode, pu8UnlockKey, u16KeyLen);

    if (pu8UnlockKey == NULL)
    {
        pu8UnlockKey = au8UnlockKey;
    }

    for (i = 0; i < u16KeyLen; i++)
    {
        if (i % 32 == 0)
        {
            DBG_vPrintf(TRACE_PROGRAMMER, "\r\n");
        }
        DBG_vPrintf(TRACE_PROGRAMMER, "%02x ", pu8UnlockKey[i]);
    }

    if (psPrivate->psProtocol->eBL_Unlock == NULL)
    {
        return E_PRG_OK;
    }

    return psPrivate->psProtocol->eBL_Unlock(psContext, u8Mode, pu8UnlockKey, u16KeyLen);
}

teStatus ePRG_ChipGetDetails(tsPRG_Context *psContext)
{
    tsPRG_PrivateContext *psPriv;
    teStatus eStatus;

    DBG_vPrintf(TRACE_PROGRAMMER, "ePRG_ChipGetDetails()\n");

    if (!psContext)
    {
        return E_PRG_NULL_PARAMETER;
    }
    psPriv = (tsPRG_PrivateContext *)psContext->pvPrivate;

    if (psPriv->sConnection.eType == E_CONNECT_SERIAL)
    {
        if ((eStatus = psPriv->sConnection.uDetails.sSerial.cbFlush(psContext)) != E_PRG_OK)
        {
            return ePRG_SetStatus(psContext, eStatus, "flushing UART");
        }
    }

    if ((eStatus = ePRG_ChipGetMemoryInfo(psContext)) != E_PRG_OK)
    {
        return ePRG_SetStatus(psContext, eStatus, "reading memory info");
    }
    DBG_vPrintf(TRACE_PROGRAMMER, "Read Memory Info OK\n");

    if ((eStatus = ePRG_ChipGetChipName(psContext)) != E_PRG_OK)
    {
        return ePRG_SetStatus(psContext, eStatus, "reading chip ID");
    }
    DBG_vPrintf(TRACE_PROGRAMMER, "Read CHIP_ID OK\n");

    if ((eStatus = ePRG_ChipGetMacAddress(psContext)) != E_PRG_OK)
    {
        ePRG_SetStatus(psContext, eStatus, "reading MAC address");
        DBG_vPrintf(TRACE_PROGRAMMER, "Failed to read MAC %s\n", pcPRG_GetLastErrorMessage(psContext));
    }
    else
    {
        DBG_vPrintf(TRACE_PROGRAMMER, "Read MAC OK\n");
    }

    // Always select flash 0 to start off with.
    return E_PRG_OK; //ePRG_FlashSelectDevice(psContext, 0);
}


teStatus LIBPROGRAMMER ePRG_AddMemory(tsPRG_Context *psContext, const char *pcName, const uint32_t u32FlashSize,
                                      uint8_t u8ManufacturerID, const uint8_t u8DeviceID, const uint8_t u8ChipSelect)
{
    tsMemInfo *psMemInfo;
    tsMemInfo **psMemList;

    if ((!psContext) || (!pcName))
    {
        return E_PRG_NULL_PARAMETER;
    }

    /** Reallocate storage */
    psMemInfo = calloc(1, sizeof(tsMemInfo));

    if (!psMemInfo)
    {
        return ePRG_SetStatus(psContext, E_PRG_OUT_OF_MEMORY, "");
    }

    psMemList = &psContext->sChipDetails.psMemInfoList;

    while (*psMemList != NULL)
    {
    	psMemList = &((*psMemList)->psNext);
    }

    *psMemList = psMemInfo;

    psMemInfo->u8Index           = psContext->sChipDetails.u32NumMemories;
    psMemInfo->pcMemName         = (char *)pcName;
    psMemInfo->u32Size           = u32FlashSize;
    psMemInfo->u8ManufacturerID  = u8ManufacturerID;
    psMemInfo->u8DeviceID        = u8DeviceID;
    psMemInfo->u8ChipSelect      = u8ChipSelect;
    psMemInfo->u32BlockSize      = 32 * 1024;

    DBG_vPrintf(TRACE_PROGRAMMER, "Added memory %d: \"%s\" size %dk (0x%02X:0x%02X on CS %d)\n",
                psContext->sChipDetails.u32NumMemories, pcName, u32FlashSize / 1024,
                u8ManufacturerID, u8DeviceID, u8ChipSelect);

    psContext->sChipDetails.u32NumMemories++;

    return ePRG_SetStatus(psContext, E_PRG_OK, "");
}


teStatus LIBPROGRAMMER ePRG_MemoryOpen(tsPRG_Context *psContext, uint32_t u32FlashDevice, uint32_t u32AccessMode)
{
    tsPRG_PrivateContext *psPriv;
    tsMemInfo *psMemInfo;
    teStatus eStatus;

    if (!psContext)
    {
        return E_PRG_NULL_PARAMETER;
    }

    /* Select flash & enable write access */
    psPriv = (tsPRG_PrivateContext *)psContext->pvPrivate;

    if (u32FlashDevice >= psContext->sChipDetails.u32NumMemories)
    {
        /* Out of bounds flash device */
        DBG_vPrintf(TRACE_PROGRAMMER, "Flash %d out of bounds\n", u32FlashDevice);
        return ePRG_SetStatus(psContext, E_PRG_FLASH_DEVICE_UNAVAILABLE, "");
    }

    psMemInfo = psGetMemoryInfo(psContext, u32FlashDevice);

    if (psMemInfo == NULL)
    {
        DBG_vPrintf(TRACE_PROGRAMMER, "Flash %d not present\n", u32FlashDevice);
        return ePRG_SetStatus(psContext, E_PRG_FLASH_DEVICE_UNAVAILABLE, "selecting flash type");
    }

    /* Set the flash type */
    DBG_vPrintf(TRACE_PROGRAMMER, "Select Flash: %s\n", psMemInfo->pcMemName);

    if((eStatus = psPriv->psProtocol->eBL_MemOpen(psContext, u32FlashDevice, u32AccessMode, &psPriv->u32MemoryHandle)) != E_PRG_OK)
    {
        DBG_vPrintf(TRACE_PROGRAMMER, "Error selecting flash device\n");
        return ePRG_SetStatus(psContext, eStatus, "selecting flash type");
    }

    psPriv->u32SelectedMemory = u32FlashDevice;
    DBG_vPrintf(TRACE_PROGRAMMER, "Flash device %d selected\n", u32FlashDevice);
    return ePRG_SetStatus(psContext, E_PRG_OK, "");
}

teStatus ePRG_GetMemoryByName(tsPRG_Context *psContext, tsMemInfo *psMemInfo, const char *pcMemoryName)
{
	tsMemInfo *psMem = psGetMemByName(psContext, pcMemoryName);

	if (psMem != NULL)
	{
		memcpy(psMemInfo, psMem, sizeof(tsMemInfo));

		return ePRG_SetStatus(psContext, E_PRG_OK, "");
	}

	return ePRG_SetStatus(psContext, E_PRG_FLASH_DEVICE_UNAVAILABLE, "Memory %s not found", pcMemoryName);
}

teStatus ePRG_GetMemoryInfo(tsPRG_Context *psContext, tsMemInfo *psMemInfo, uint32_t u32MemoryIndex)
{
	memcpy(psMemInfo, psGetMemoryInfo(psContext, u32MemoryIndex), sizeof(tsMemInfo));

	return E_PRG_OK;
}

teStatus LIBPROGRAMMER ePRG_MemoryClose(tsPRG_Context *psContext)
{
	tsPRG_PrivateContext *psPriv;
    teStatus eStatus;

    if (!psContext)
    {
        return E_PRG_NULL_PARAMETER;
    }

    psPriv = psContext->pvPrivate;

    if (psPriv->psProtocol->eBL_MemClose != NULL)
    {
    	eStatus = psPriv->psProtocol->eBL_MemClose(psContext, 0);

		if(eStatus != E_PRG_OK)
		{
			DBG_vPrintf(TRACE_PROGRAMMER, "Error closing memory\n");
			return ePRG_SetStatus(psContext, eStatus, "closing memory");
		}
    }

    return ePRG_SetStatus(psContext, E_PRG_OK, "");
}

teStatus ePRG_Erase(tsPRG_Context *psContext, uint32_t u32DataLength, uint32_t u32Offset, tcbFW_Progress cbProgress, tcbFW_Confirm cbConfirm, void *pvUser)
{
    teStatus eStatus;
    uint32_t u32StartAddress;
    uint32_t u32EndAddress;

    tsPRG_PrivateContext *psPriv;
    char acOperationText[256];

    DBG_vPrintf(TRACE_PROGRAMMER, "ePRG_Erase()\n");
    if (!psContext)
    {
        return E_PRG_NULL_PARAMETER;
    }

    psPriv = psContext->pvPrivate;
    tsMemInfo *psMemInfo = psGetMemoryInfo(psContext, psPriv->u32SelectedMemory);

    u32StartAddress = psMemInfo->u32BaseAddress + u32Offset;
    u32EndAddress   = u32StartAddress + u32DataLength - 1;

    if ((u32StartAddress % psMemInfo->u32BlockSize != 0) || (u32DataLength % psMemInfo->u32BlockSize != 0))
    {
    	eStatus = E_PRG_ABORTED;

    	u32StartAddress -= u32StartAddress % psMemInfo->u32BlockSize;
    	u32EndAddress += psMemInfo->u32BlockSize - u32EndAddress % psMemInfo->u32BlockSize;

        if (cbConfirm)
        {
        	snprintf(acOperationText, sizeof(acOperationText), "The area to erase is not a multiple of the erase block size. Erase data from 0x%08x to 0x%08x?", u32StartAddress, u32EndAddress);
            eStatus = cbConfirm(pvUser, "Confirm Operation", acOperationText);
#if 1 // following code is moved from outside to inside 
			if (eStatus != E_PRG_OK)
			{
				return ePRG_SetStatus(psContext, E_PRG_ABORTED, "invalid erase range");
			}
#endif
        }
    }

    snprintf(acOperationText, sizeof(acOperationText), "Erasing %s", psMemInfo->pcMemName);

    /* Erase the flash memory */
    if (cbProgress)
    {
        if (cbProgress(pvUser, acOperationText, "Erasing", 1, 0) != E_PRG_OK)
        {
            return ePRG_SetStatus(psContext, E_PRG_ABORTED, "");
        }
    }

    if((eStatus = psPriv->psProtocol->eBL_MemErase(psContext, psPriv->u32MemoryHandle, psMemInfo->u32BaseAddress + u32Offset, u32DataLength)) != E_PRG_OK)
    {
        return ePRG_SetStatus(psContext, eStatus, "erasing flash");
    }

    if((eStatus = psPriv->psProtocol->eBL_MemBlank(psContext, psPriv->u32MemoryHandle, psMemInfo->u32BaseAddress + u32Offset, u32DataLength)) != E_PRG_OK)
    {
    	return ePRG_SetStatus(psContext, E_PRG_ERROR, "flash erase unsuccessful");
    }

    return ePRG_SetStatus(psContext, E_PRG_OK, "flash erased successfully");
}


teStatus ePRG_Program(tsPRG_Context *psContext, tcbFW_Progress cbProgress, tcbFW_Confirm cbConfirm, void *pvUser)
{
    teStatus eStatus;
    tsChipDetails *psChipDetails;
    tsFW_Info *psFWImage;

    DBG_vPrintf(TRACE_PROGRAMMER, "ePRG_Program()\n");

    if (!psContext)
    {
        return E_PRG_NULL_PARAMETER;
    }

    psChipDetails = &psContext->sChipDetails;
    psFWImage = &psContext->sFirmwareInfo;

    if (memcmp(&psFWImage->u32ROMVersion, &psChipDetails->u32SupportedFirmware, 4) != 0)
    {
        eStatus = E_PRG_INCOMPATIBLE;
        if (psFWImage->sFlags.bRawImage)
        {
            if (cbConfirm)
            {
                eStatus = cbConfirm(pvUser, "Confirm Operation", "The loaded file does not appear to be a valid image. Program raw?");
#if 1 // following code is moved from outside to inside 
            	if (eStatus != E_PRG_OK)
            	{
                	return ePRG_SetStatus(psContext, E_PRG_INCOMPATIBLE, "not a valid image");
            	}				
#endif
            }
        }
        else
        {
            if (cbConfirm)
            {
                eStatus = cbConfirm(pvUser, "Confirm Operation", "The loaded image does not appear to be compatible with the connected device. Continue?");
#if 1 // following code is moved from outside to inside  
				if (eStatus != E_PRG_OK)
				{
					return ePRG_SetStatus(psContext, E_PRG_INCOMPATIBLE, "image built for 0x%08X, connected device is 0x%08x", psFWImage->u32ROMVersion, psChipDetails->u32SupportedFirmware);
				}
#endif
            }
 
        }
        // User has given the go ahead to continue anyway. Here be dragons.
    }

    if ((eStatus = ePRG_SetUpImage(psContext, psFWImage, psChipDetails)) != E_PRG_OK)
    {
        return eStatus;
    }

    return ePRG_Write(psContext, psFWImage->pu8ImageData, psFWImage->u32ImageLength, psContext->u32FlashOffset, cbProgress, cbConfirm, pvUser);
}

teStatus ePRG_Write(tsPRG_Context *psContext, uint8_t *pu8Data, uint32_t u32DataLength, uint32_t u32Offset, tcbFW_Progress cbProgress, tcbFW_Confirm cbConfirm, void *pvUser)
{
    teStatus eStatus;
    int n;
    uint32_t u32ChunkSize;
    tsPRG_PrivateContext *psPriv;
    char acOperationText[256];

    DBG_vPrintf(TRACE_PROGRAMMER, "ePRG_Write()\n");
    if (!psContext)
    {
        return E_PRG_NULL_PARAMETER;
    }

    psPriv = psContext->pvPrivate;

    tsMemInfo *psMemInfo = psGetMemoryInfo(psContext, psPriv->u32SelectedMemory);

    DBG_vPrintf(TRACE_PROGRAMMER, "Programming data length %d into memory length %d at offset 0x%08x\n", u32DataLength, psMemInfo->u32Size, u32Offset);
    if ((u32Offset + u32DataLength) > psMemInfo->u32Size)
    {
        return ePRG_SetStatus(psContext, E_PRG_INCOMPATIBLE, "image is larger than selected memory.");
    }

    snprintf(acOperationText, sizeof(acOperationText), "Programming %s", psMemInfo->pcMemName);

    if (cbProgress)
    {
        if (cbProgress(pvUser, acOperationText, "Writing", u32DataLength, 0) != E_PRG_OK)
        {
            return ePRG_SetStatus(psContext, E_PRG_ABORTED, "");
        }
    }

    for(n = 0; n < u32DataLength; n += u32ChunkSize)
    {
        if((u32DataLength - n) > psPriv->psProtocol->u32ChunkSize)
        {
            u32ChunkSize = psPriv->psProtocol->u32ChunkSize;
        }
        else
        {
            u32ChunkSize = u32DataLength - n;
        }

        if((eStatus = psPriv->psProtocol->eBL_MemWrite(psContext, 0, psMemInfo->u32BaseAddress + u32Offset + n, u32ChunkSize, pu8Data + n)) != E_PRG_OK)
        {
            return ePRG_SetStatus(psContext, eStatus, "writing memory at address 0x%08X", psMemInfo->u32BaseAddress + u32Offset + n);
        }

        if (cbProgress)
        {
            if (cbProgress(pvUser, acOperationText, "Writing", u32DataLength, n) != E_PRG_OK)
            {
                return ePRG_SetStatus(psContext, E_PRG_ABORTED, "");
            }
        }
    }

    if (cbProgress)
    {
        if (cbProgress(pvUser, acOperationText, "Writing", u32DataLength, u32DataLength) != E_PRG_OK)
        {
            return ePRG_SetStatus(psContext, E_PRG_ABORTED, "");
        }
    }

    return ePRG_SetStatus(psContext, E_PRG_OK, "memory written succesfully");
}


teStatus ePRG_Verify(tsPRG_Context *psContext, tcbFW_Progress cbProgress, void *pvUser)
{
    teStatus eStatus;
    int n;
    uint32_t u32ChunkSize;
    uint8_t au8Buffer1[BL_MAX_CHUNK_SIZE + 1];
    tsChipDetails *psChipDetails;
    tsFW_Info *psFWImage;
    tsPRG_PrivateContext *psPriv;
    char acOperationText[256];

    DBG_vPrintf(TRACE_PROGRAMMER, "ePRG_Verify()\n");
    if (!psContext)
    {
        return E_PRG_NULL_PARAMETER;
    }
    if (psContext->sDeviceConfig.eCRP == E_DC_CRP_LEVEL1)
    {
        return ePRG_SetStatus(psContext, E_PRG_CRP_SET, "verifying device");
    }

    psPriv = psContext->pvPrivate;
    tsMemInfo *psMemInfo = psGetMemoryInfo(psContext, psPriv->u32SelectedMemory);
    snprintf(acOperationText, sizeof(acOperationText), "Verifying %s", psMemInfo->pcMemName);

    psChipDetails = &psContext->sChipDetails;
    psFWImage = &psContext->sFirmwareInfo;

    DBG_vPrintf(TRACE_PROGRAMMER, "Verifying image length %d into flash length %d at offset 0x%08x\n", psFWImage->u32ImageLength, psMemInfo->u32Size, psContext->u32FlashOffset);
    if ((psContext->u32FlashOffset + psFWImage->u32ImageLength) > psMemInfo->u32Size)
    {
        return ePRG_SetStatus(psContext, E_PRG_INCOMPATIBLE, "image is larger than selected flash device.");
    }

    if (cbProgress)
    {
        if (cbProgress(pvUser, acOperationText, "Verifying", psFWImage->u32ImageLength, 0) != E_PRG_OK)
        {
            return ePRG_SetStatus(psContext, E_PRG_ABORTED, "");
        }
    }

    if ((eStatus = ePRG_SetUpImage(psContext, psFWImage, psChipDetails)) != E_PRG_OK)
    {
        return eStatus;
    }

    for(n = 0; n < psFWImage->u32ImageLength; n += u32ChunkSize)
    {
        if((psFWImage->u32ImageLength - n) > BL_MAX_CHUNK_SIZE)
        {
            u32ChunkSize = BL_MAX_CHUNK_SIZE;
        }
        else
        {
            u32ChunkSize = psFWImage->u32ImageLength - n;
        }

        if ((eStatus = eBL2_MemRead(psContext, 0, psContext->u32FlashOffset + n, &u32ChunkSize, au8Buffer1)) != E_PRG_OK)
        {
            return ePRG_SetStatus(psContext, eStatus, "reading Flash at address 0x%08X", psContext->u32FlashOffset + n);
        }
        else
        {
            if (memcmp(psFWImage->pu8ImageData + n, au8Buffer1, u32ChunkSize))
            {
                return ePRG_SetStatus(psContext, E_PRG_VERIFICATION_FAILED, "at address 0x%08X", psContext->u32FlashOffset + n);
            }
        }

        if (cbProgress)
        {
            if (cbProgress(pvUser, acOperationText, "Verifying", psFWImage->u32ImageLength, n) != E_PRG_OK)
            {
                return ePRG_SetStatus(psContext, E_PRG_ABORTED, "");
            }
        }
    }

    if (cbProgress)
    {
        if (cbProgress(pvUser, acOperationText, "Verifying", psFWImage->u32ImageLength, psFWImage->u32ImageLength) != E_PRG_OK)
        {
            return ePRG_SetStatus(psContext, E_PRG_ABORTED, "");
        }
    }

    return ePRG_SetStatus(psContext, E_PRG_OK, "flash verified succesfully");
}


teStatus LIBPROGRAMMER ePRG_Dump(tsPRG_Context *psContext, char *pcDumpFile, tcbFW_Progress cbProgress, void *pvUser)
{
    int iFd;
    int n;
    uint32_t u32ChunkSize = BL_MAX_CHUNK_SIZE;
    tsPRG_PrivateContext *psPriv;
    uint32_t u32MemSize;
    uint32_t u32ReadSize = 0;

    char acOperationText[256];

    DBG_vPrintf(TRACE_PROGRAMMER, "ePRG_Dump()\n");
    if (!psContext || !pcDumpFile)
    {
        return E_PRG_NULL_PARAMETER;
    }

    if (psContext->sDeviceConfig.eCRP == E_DC_CRP_LEVEL1)
    {
        return ePRG_SetStatus(psContext, E_PRG_CRP_SET, "dumping flash");
    }

    psPriv = (tsPRG_PrivateContext *)psContext->pvPrivate;
    tsMemInfo *psMemInfo = psGetMemoryInfo(psContext, psPriv->u32SelectedMemory);
    u32MemSize = psMemInfo->u32Size;

    snprintf(acOperationText, sizeof(acOperationText), "Dumping %s", psMemInfo->pcMemName);

    iFd = open(pcDumpFile, O_WRONLY | O_CREAT | O_TRUNC | O_BINARY, S_IRUSR | S_IWUSR);

    if (iFd < 0)
    {
        return ePRG_SetStatus(psContext, E_PRG_FAILED_TO_OPEN_FILE, "\"%s\" (%s)", pcDumpFile, pcPRG_GetLastErrorMessage(psContext));
    }

    for(n = psContext->u32FlashOffset; n < u32MemSize ; n += u32ReadSize)
    {
        uint8_t au8Buffer[u32ChunkSize];
        teStatus eStatus;
        uint32_t u32Address = n + psMemInfo->u32BaseAddress;

        if ((n + u32ChunkSize) > u32MemSize)
        {
            // Don't run off the end of flash
            u32ChunkSize = u32MemSize - n;
        }

        u32ReadSize = u32ChunkSize;

        if ((eStatus = eBL2_MemRead(psContext, 0, u32Address, &u32ReadSize, au8Buffer)) != E_PRG_OK)
        {
            close(iFd);
            return ePRG_SetStatus(psContext, eStatus, "reading memory at address 0x%08X", u32Address);
        }

        if (write(iFd, au8Buffer, u32ReadSize) < 0)
        {
            close(iFd);
            return ePRG_SetStatus(psContext, E_PRG_ERROR_WRITING, "file at address 0x%08X", n);
        }

        if (cbProgress)
        {
            if (cbProgress(pvUser, acOperationText, "Dumping Memory", u32MemSize, n) != E_PRG_OK)
            {
                close(iFd);
                return ePRG_SetStatus(psContext, E_PRG_ABORTED, "");
            }
        }
    }
    if (cbProgress)
    {
        if (cbProgress(pvUser, acOperationText, "Dumping Memory", u32MemSize, u32MemSize) != E_PRG_OK)
        {
            close(iFd);
            return ePRG_SetStatus(psContext, E_PRG_ABORTED, "");
        }
    }

    close(iFd);
    return ePRG_SetStatus(psContext, E_PRG_OK, "Memory dumped successfully");
}

teStatus LIBPROGRAMMER ePRG_Read(tsPRG_Context *psContext, uint8_t *pu8Data, uint32_t u32DataLength, uint32_t u32Offset, tcbFW_Progress cbProgress, tcbFW_Confirm cbConfirm, void *pvUser)
{
    int n;
    uint32_t u32ChunkSize = BL_MAX_CHUNK_SIZE;
    tsPRG_PrivateContext *psPriv;
    uint32_t u32MemSize;
    uint32_t u32ReadSize = 0;

    char acOperationText[256];

    DBG_vPrintf(TRACE_PROGRAMMER, "ePRG_Read()\n");
    if (!psContext || !pu8Data)
    {
        return E_PRG_NULL_PARAMETER;
    }

    psPriv = (tsPRG_PrivateContext *)psContext->pvPrivate;
    tsMemInfo *psMemInfo = psGetMemoryInfo(psContext, psPriv->u32SelectedMemory);
    u32MemSize = psMemInfo->u32Size;

    snprintf(acOperationText, sizeof(acOperationText), "Dumping %s", psMemInfo->pcMemName);

    for(n = 0; n < u32DataLength ; n += u32ReadSize)
    {
        teStatus eStatus;
        uint32_t u32Address = n + u32Offset + psMemInfo->u32BaseAddress;

        if ((n + u32ChunkSize) > u32DataLength)
        {
            // Don't run off the end of memory
            u32ChunkSize = u32DataLength - n;
        }

        u32ReadSize = u32ChunkSize;

        if ((eStatus = eBL2_MemRead(psContext, 0, u32Address, &u32ReadSize, &pu8Data[n])) != E_PRG_OK)
        {
            return ePRG_SetStatus(psContext, eStatus, "reading memory at address 0x%08X", u32Address);
        }

        if (cbProgress)
        {
            if (cbProgress(pvUser, acOperationText, "Reading Memory", u32MemSize, n) != E_PRG_OK)
            {
                return ePRG_SetStatus(psContext, E_PRG_ABORTED, "");
            }
        }
    }
    if (cbProgress)
    {
        if (cbProgress(pvUser, acOperationText, "Reading Memory", u32MemSize, u32MemSize) != E_PRG_OK)
        {
            return ePRG_SetStatus(psContext, E_PRG_ABORTED, "");
        }
    }

    return ePRG_SetStatus(psContext, E_PRG_OK, "Memory read successfully");
}

void dbg_vPrintfImpl(const char *pcFile, uint32_t u32Line, const char *pcFormat, ...)
{
    va_list argp;
    va_start(argp, pcFormat);
#ifdef DBG_VERBOSE
    printf("%s:%d ", pcFile, u32Line);
#endif /* DBG_VERBOSE */
    vprintf(pcFormat, argp);
    fflush(stdout);
    return;
}


teStatus ePRG_SetStatus(tsPRG_Context *psContext, teStatus eStatus, const char *pcAdditionalInfoFmt, ...)
{
    tsPRG_PrivateContext *psPriv = (tsPRG_PrivateContext *)psContext->pvPrivate;
    const char *pcStatus;
    uint32_t u32Length = 0;
    va_list ap;
    va_start(ap, pcAdditionalInfoFmt);

    switch (eStatus)
    {
        case(E_PRG_OK):                         pcStatus = "Success"; break;
        case(E_PRG_ERROR):                      pcStatus = ""; break;
        case(E_PRG_OUT_OF_MEMORY):              pcStatus = "Low on memory"; break;
        case(E_PRG_ERROR_WRITING):              pcStatus = "Write error"; break;
        case(E_PRG_ERROR_READING):              pcStatus = "Read error"; break;
        case(E_PRG_FAILED_TO_OPEN_FILE):        pcStatus = "Failed to open file"; break;
        case(E_PRG_BAD_PARAMETER):              pcStatus = "Bad parameter"; break;
        case(E_PRG_NULL_PARAMETER):             pcStatus = "NULL parameter"; break;
        case(E_PRG_INCOMPATIBLE):               pcStatus = "Image is not compatible with chip,"; break;
        case(E_PRG_INVALID_FILE):               pcStatus = "Invalid image file"; break;
        case(E_PRG_UNSUPPORED_CHIP):            pcStatus = "Unsupported chip"; break;
        case(E_PRG_ABORTED):                    pcStatus = "Aborted"; break;
        case(E_PRG_VERIFICATION_FAILED):        pcStatus = "Verification failed"; break;
        case(E_PRG_INVALID_TRANSPORT):          pcStatus = "Invalid transport"; break;
        case(E_PRG_COMMS_FAILED):               pcStatus = "Communication failure"; break;
        case(E_PRG_UNSUPPORTED_OPERATION):      pcStatus = "Bootloader doesn't support"; break;
        case(E_PRG_FLASH_DEVICE_UNAVAILABLE):   pcStatus = "Could not select flash device"; break;
        case(E_PRG_CRP_SET):                    pcStatus = "Code protection prevents"; break;
        default:                                pcStatus = "Unknown"; break;
    }

    u32Length = snprintf(psPriv->acStatusMessage, PRG_MAX_STATUS_LENGTH, "%s ", pcStatus);

    vsnprintf(&psPriv->acStatusMessage[u32Length], PRG_MAX_STATUS_LENGTH - u32Length, pcAdditionalInfoFmt, ap);

    va_end(ap);
    return eStatus;
}

#if defined POSIX
char *pcPRG_GetLastErrorMessage(tsPRG_Context *psContext)
{
    tsPRG_PrivateContext *psPriv = (tsPRG_PrivateContext *)psContext->pvPrivate;
    strerror_r(errno, psPriv->acErrorMsgBuffer, PRG_MAX_STATUS_LENGTH);
    return psPriv->acErrorMsgBuffer;
}
#elif defined WIN32
char *pcPRG_GetLastErrorMessage(tsPRG_Context *psContext)
{
    tsPRG_PrivateContext *psPriv = (tsPRG_PrivateContext *)psContext->pvPrivate;

    DWORD dwLastError = GetLastError();
    DWORD dwNumChars;

    dwNumChars = FormatMessage(
        FORMAT_MESSAGE_FROM_SYSTEM,
        NULL,
        dwLastError,
        MAKELANGID(LANG_NEUTRAL, SUBLANG_DEFAULT),
        (LPTSTR)&psPriv->acErrorMsgBuffer,
        PRG_MAX_STATUS_LENGTH,
        NULL);

    // Remove \n\r from end of message.
    psPriv->acErrorMsgBuffer[dwNumChars-2] = '\0';

    return psPriv->acErrorMsgBuffer;
}
#endif /* WIN32 */


void vPRG_WaitMs(uint32_t u32TimeoutMs)
{
    DBG_vPrintf(TRACE_PROGRAMMER, "Delay %dms...", u32TimeoutMs);
#if defined POSIX
    usleep(u32TimeoutMs * 1000);
#elif defined WIN32
    Sleep(u32TimeoutMs);
#endif
    DBG_vPrintf(TRACE_PROGRAMMER, "waited\n");
}

/****************************************************************************/
/***        Local Functions                                               ***/
/****************************************************************************/
static teStatus ePRG_IsJN5189(tsPRG_Context *psContext, char* mem_name)
{
    teStatus eStatus = E_PRG_ERROR;
    uint8_t entry_id = 1;
    tsChipDetails *psChipDetails = &psContext->sChipDetails;

    //Below step is done because JN518x ES1 and JN5189 ES2 have the same chip id.
    //This could be solved:
    //1. by doing a patch of the chip_id function (so that it returns a meaningful id) but this is maybe not necessary.
    //2. by using the name of the 3rd memory section to distinguish between ES1 and ES2 (approach used here)
    tsMemInfo* entry = psChipDetails->psMemInfoList;
    while ((entry) && (entry_id < 3))
    {
        entry = entry->psNext;
        entry_id++;
    }
    //On ES1, the third memory section is called "pOTP"
    //On ES2, the third memory section is called "pFlash"
    if (!strcmp(entry->pcMemName, mem_name))
    {
        eStatus = E_PRG_OK;
    }
    return eStatus;
}
static teStatus ePRG_ChipGetChipName(tsPRG_Context *psContext)
{
    tsChipDetails *psChipDetails = &psContext->sChipDetails;
    tsMemInfo *psMemInfo;

    DBG_vPrintf(TRACE_PROGRAMMER, "Get Chip ID\n");

    if(psChipDetails == NULL)
    {
        return E_PRG_NULL_PARAMETER;
    }

    DBG_vPrintf(TRACE_PROGRAMMER, "Chip ID: 0x%08X\n", psContext->sChipDetails.u32ChipId);

    switch (CHIP_ID(psChipDetails->u32ChipId))
    {
        /* Handle JN5148 variants */
        case (CHIP_ID(CHIP_ID_JN5148_REV2A)):
            switch(CHIP_ID_WITH_METAL_MASK(psChipDetails->u32ChipId))
            {
                case (CHIP_ID_WITH_METAL_MASK(CHIP_ID_JN5148_REV2A)):
                case (CHIP_ID_WITH_METAL_MASK(CHIP_ID_JN5148_REV2B)):
                case (CHIP_ID_WITH_METAL_MASK(CHIP_ID_JN5148_REV2C)):
                    psChipDetails->pcChipName = "JN5148-001";
                    break;
                case (CHIP_ID_WITH_METAL_MASK(CHIP_ID_JN5148_REV2D)):
                    psChipDetails->pcChipName = "JN5148-J01";
                    break;
                case (CHIP_ID_WITH_METAL_MASK(CHIP_ID_JN5148_REV2E)):
                    psChipDetails->pcChipName = "JN5148-Z01";
                    break;
            }
            break;

        /* JN5142 is special and has different chip part numbers for each revision */
        case (CHIP_ID(CHIP_ID_JN5142_REV1A)):
        case (CHIP_ID(CHIP_ID_JN5142_REV1B)):
            psChipDetails->pcChipName = "JN5142";
            break;
        case (CHIP_ID(CHIP_ID_JN5142_REV1C)):
            psChipDetails->pcChipName = "JN5142-J01";
            break;

        /* Handle JN5168 variants */
        case (CHIP_ID(CHIP_ID_JN5168)):
        	psMemInfo = psGetMemoryInfo(psContext, 0);
            if (psMemInfo->u32Size == (64 * 1024))
            {
                psChipDetails->pcChipName = "JN5161";
            }
            else if (psMemInfo->u32Size == (160 * 1024))
            {
                psChipDetails->pcChipName = "JN5164";
            }
            else if (psMemInfo->u32Size == (256 * 1024))
            {
                psChipDetails->pcChipName = "JN5168";
            }
            else
            {
               psChipDetails->pcChipName = "JN516x";
            }
            break;

        /* Handle JN5169 variants */
        case (CHIP_ID(CHIP_ID_JN5169)):
            psChipDetails->pcChipName = "JN5169";
            break;

        /* Handle JN5179 variants */
        case (CHIP_ID(CHIP_ID_JN5172)):
			psMemInfo = psGetMemoryInfo(psContext, 0);
            switch(psChipDetails->u32ChipId)
            {
                case(CHIP_ID_JN5172_D):
                    psChipDetails->pcChipName = "JN5172_D";
                    break;
                case(CHIP_ID_JN5172_LR):
                    psChipDetails->pcChipName = "JN5172_LR";
                    break;
                case(CHIP_ID_JN5172_HR):
                    psChipDetails->pcChipName = "JN5172_HR";
                    break;
                case(CHIP_ID_JN5179):
                    if (psMemInfo->u32Size == (160 * 1024))
                    {
                        psChipDetails->pcChipName = "JN5174";      break;
                    }
                    else if (psMemInfo->u32Size == (256 * 1024))
                    {
                        psChipDetails->pcChipName = "JN5178";      break;
                    }
                    else if (psMemInfo->u32Size == (512 * 1024))
                    {
                        psChipDetails->pcChipName = "JN5179";      break;
                    }
                    else
                    {
                        psChipDetails->pcChipName = "JN517x";
                    }
                    break;

                default:
                    psChipDetails->pcChipName = "Unknown";
                    break;
            }
            break;

        case (CHIP_ID(CHIP_ID_JN5180_ESx)):
            /* JN518x ES1 reports 0x88888888, ES2 should report 0x89898989 but
              actually also reports 0x88888888. So we use the reported name of
              one of the memories ("pOTP" on ES1, "pFlash" on ES2) in order to
              differentiate between them */
            if (IS_JN5189_ES2(psContext) == E_PRG_OK)
            {
                /* ES2; is it JN5188/JN5189/QN9030/QN9090? N-2 page will tell us */
                uint32_t u32Length = sizeof(uint32_t);
                uint32_t u32DeviceType;
                uint32_t u32Handle;

                (void)eBL2_MemOpen(psContext, 3, 1, &u32Handle);
                (void)eBL2_MemRead(psContext, 0, 0x9fc60, &u32Length, (uint8_t *)&u32DeviceType);
                (void)eBL2_MemClose(psContext, 3);

                /* Remove 'T' and reserved fields */
                u32DeviceType &= 0x0fffffff;

                switch (u32DeviceType)
                {
                case 5188:
                    psChipDetails->pcChipName = "JN5188 ES2";
                    psContext->sChipDetails.u32ChipId = CHIP_ID_JN5188_ES2;
                    break;

                case 5189:
                    psChipDetails->pcChipName = "JN5189 ES2";
                    psContext->sChipDetails.u32ChipId = CHIP_ID_JN5189_ES2;
                    break;

                case 9030:
                    psChipDetails->pcChipName = "QN9030";
                    psContext->sChipDetails.u32ChipId = CHIP_ID_QN9030;
                    break;

                case 9090:
                    psChipDetails->pcChipName = "QN9090";
                    psContext->sChipDetails.u32ChipId = CHIP_ID_QN9090;
                    break;

                case 32041:
                    psChipDetails->pcChipName = "K32W041";
                    psContext->sChipDetails.u32ChipId = CHIP_ID_K32W041;
                    break;

                case 32061:
                    psChipDetails->pcChipName = "K32W061";
                    psContext->sChipDetails.u32ChipId = CHIP_ID_K32W061;
                    break;

                default:
                    psChipDetails->pcChipName = "Unknown JN518x";
                    psContext->sChipDetails.u32ChipId = CHIP_ID_JN5189_ES2;
                    break;
                }
            }
            else
            {
                psChipDetails->pcChipName = "JN518x ES1";
                psContext->sChipDetails.u32ChipId = CHIP_ID_JN5189_ES1;
            }
            break;

        default:
            psChipDetails->pcChipName = "Unknown";
            break;
    }

    DBG_vPrintf(TRACE_PROGRAMMER, "Chip ID: 0x%08X\n", psContext->sChipDetails.u32ChipId);
    DBG_vPrintf(TRACE_PROGRAMMER, "Chip Name: %s\n", psContext->sChipDetails.pcChipName);

    return E_PRG_OK;
}

static teStatus ePRG_ChipGetMemoryInfo(tsPRG_Context *psContext)
{
    teStatus eStatus = E_PRG_ERROR;
    tsPRG_PrivateContext *psPriv = (tsPRG_PrivateContext *)psContext->pvPrivate;

    DBG_vPrintf(TRACE_PROGRAMMER, "ePRG_ChipGetMemoryInfo()\n");
    eStatus = psPriv->psProtocol->eBL_MemInfo(psContext);

    if (IS_JN5189_ES1(psContext) == E_PRG_OK)
    {
        tsMemInfo* entry = psContext->sChipDetails.psMemInfoList;
        /* Fixup incorrect size of 8x flash */
        if ((strcmp(entry->pcMemName, "FLASH") == 0) && (entry->u32Size > 632 * 1024))
        {
            entry->u32Size = 632 * 1024;
            DBG_vPrintf(TRACE_PROGRAMMER, "ePRG_ChipGetMemoryInfo() ES1 flash fixup\n");
        }
    }

    return eStatus;
}

static teStatus ePRG_ChipGetMacAddress(tsPRG_Context *psContext)
{
    teStatus eStatus = E_PRG_OK;
    uint32_t au32InvalidMac[2] = {0xffffffff, 0xffffffff};
    tsChipDetails *psChipDetails = &psContext->sChipDetails;

    DBG_vPrintf(TRACE_PROGRAMMER, "ePRG_ChipGetMacAddress()\n");
    if(psChipDetails == NULL)
    {
        return E_PRG_NULL_PARAMETER;
    }

    switch(CHIP_ID_PART(psChipDetails->u32ChipId))
    {
        case CHIP_ID_PART(CHIP_ID_JN5168):
        case CHIP_ID_PART(CHIP_ID_JN5169):
        case CHIP_ID_PART(CHIP_ID_JN5172):
            /* First we read the customer specific MAC address, and if its not all F's, we use that */
        {
            uint32_t u32Words[2] = {0x0, 0x0};

            eStatus = eBL_MemoryRead(psContext, JN516X_INDEX_SECTOR_MAC_CUSTOMER_ADDRESS, sizeof(u32Words), sizeof(uint32_t), (uint8_t*)&u32Words);

            DBG_vPrintf(TRACE_PROGRAMMER, "Customer MAC Address: %08X%08X\n", u32Words[0], u32Words[1]);

            /* If its all F's, read factory assigned MAC */
            if(memcmp(u32Words, au32InvalidMac, sizeof(au32InvalidMac)) == 0)
            {
                DBG_vPrintf(TRACE_PROGRAMMER, "No customer MAC address - using factory\n");
                eStatus = eBL_MemoryRead(psContext, JN516X_INDEX_SECTOR_MAC_FACTORY_ADDRESS, sizeof(u32Words), sizeof(uint32_t), (uint8_t*)&u32Words);

                DBG_vPrintf(TRACE_PROGRAMMER, "Factory MAC Address: %08X%08X\n", u32Words[0], u32Words[1]);
            }
            // Convert into byte array.
            psChipDetails->au8MacAddress[0] = u32Words[0] >> 24;
            psChipDetails->au8MacAddress[1] = u32Words[0] >> 16;
            psChipDetails->au8MacAddress[2] = u32Words[0] >> 8;
            psChipDetails->au8MacAddress[3] = u32Words[0] >> 0;
            psChipDetails->au8MacAddress[4] = u32Words[1] >> 24;
            psChipDetails->au8MacAddress[5] = u32Words[1] >> 16;
            psChipDetails->au8MacAddress[6] = u32Words[1] >> 8;
            psChipDetails->au8MacAddress[7] = u32Words[1] >> 0;
            break;
        }

        case CHIP_ID_PART(CHIP_ID_JN518x):
        {
        	tsMemInfo *psMemInfo;

        	/*
        	 * JN518x flash defaults to 0's instead of 1's so we expected an
        	 * invalid MAC to be all 0 instead of all F.
        	 */
        	uint64_t u64Mac = 0x0;

        	uint32_t u32ReadSize = sizeof(u64Mac);

        	uint32_t u32MemHandle;

        	/* Customer MAC is stored in protected sector */
        	uint32_t address = 0;
        	if (psChipDetails->u32ChipId != CHIP_ID_JN5189_ES1)
        	{
        	    psMemInfo = psGetMemByName(psContext, "pFlash");
        	    address = 64;
        	}
        	else
        	{
        	    psMemInfo = psGetMemByName(psContext, "PSECT");
        	    address = 96;
        	}

            if (psMemInfo != NULL)
            {
                eStatus = eBL2_MemOpen(psContext, psMemInfo->u8Index, 1, &u32MemHandle);

                eStatus = eBL2_MemRead(psContext, u32MemHandle, address, &u32ReadSize, (uint8_t*)&u64Mac);

                eStatus = eBL2_MemClose(psContext, u32MemHandle);

                u64Mac = le64toh(u64Mac);

                DBG_vPrintf(TRACE_PROGRAMMER, "Customer MAC Address: %016llX\n", u64Mac);
            }

			/* If its 0, read factory assigned MAC */
			if(u64Mac == 0)
			{
				DBG_vPrintf(TRACE_PROGRAMMER, "No customer MAC address - using factory\n");

				/* Factory MAC is stored in the config page */
				psMemInfo = psGetMemByName(psContext, "Config");

				if (psMemInfo != NULL)
				{
					eStatus = eBL2_MemOpen(psContext, psMemInfo->u8Index, 1, &u32MemHandle);

					eStatus = eBL2_MemRead(psContext, u32MemHandle, psMemInfo->u32BaseAddress + 0x70, &u32ReadSize, (uint8_t*)&u64Mac);

					eStatus = eBL2_MemClose(psContext, u32MemHandle);

					u64Mac = le64toh(u64Mac);

					DBG_vPrintf(TRACE_PROGRAMMER, "Factory MAC Address: %016llX\n", u64Mac);
				}
			}

			// Convert into byte array.
			int i;
			for (i = 0; i < 8; i++)
			{
				psChipDetails->au8MacAddress[i] = u64Mac >> (7 - i) * 8;
			}
			break;
        }

        default:
            return E_PRG_ERROR;
    }

    return eStatus;
}


static teStatus ePRG_SetUpImage(tsPRG_Context *psContext, tsFW_Info *psFWImage, tsChipDetails *psChipDetails)
{
    /* Depending on chip type, we may need to copy the MAC address into the firmware image
     * Also, some images have a 4 byte header in the binary that needs stripping off, so do that here
     * by adjusting start point and length to suit */
    DBG_vPrintf(TRACE_PROGRAMMER, "ePRG_SetUpImage()\n");
    switch(CHIP_ID_PART(psChipDetails->u32ChipId))
    {
#if JN514X_SUPPORT
        case CHIP_ID_PART(CHIP_ID_JN5142_REV1A):
        case CHIP_ID_PART(CHIP_ID_JN5142_REV1B):
        case CHIP_ID_PART(CHIP_ID_JN5142_REV1C):
            DBG_vPrintf(TRACE_PROGRAMMER, "multi-image bootloader\n");
            memcpy(&psFWImage->pu8ImageData[JN514X_MIB_MAC_ADDRESS_LOCATION], psChipDetails->au8MacAddress, 8);
            break;

        case CHIP_ID_PART(CHIP_ID_JN5148_REV2A):
            DBG_vPrintf(TRACE_PROGRAMMER, "JN5148 ");
            switch (CHIP_ID_VESION(psChipDetails->u32ChipId))
            {
                case CHIP_ID_VESION(CHIP_ID_JN5148_REV2D):
                case CHIP_ID_VESION(CHIP_ID_JN5148_REV2E):
                    DBG_vPrintf(TRACE_PROGRAMMER, "multi-image bootloader\n");
                    memcpy(&psFWImage->pu8ImageData[JN514X_MIB_MAC_ADDRESS_LOCATION], psChipDetails->au8MacAddress, 8);
                    break;

                default:
                    DBG_vPrintf(TRACE_PROGRAMMER, "single image bootloader\n");
                    memcpy(&psFWImage->pu8ImageData[JN514X_MAC_ADDRESS_LOCATION], psChipDetails->au8MacAddress, 8);
                    break;
            }
            break;
#endif
        case CHIP_ID_PART(CHIP_ID_JN5168):
        case CHIP_ID_PART(CHIP_ID_JN5169):
        case CHIP_ID_PART(CHIP_ID_JN5172):
        case CHIP_ID_PART(CHIP_ID_JN518x):
            break;

        default:
            return ePRG_SetStatus(psContext, E_PRG_UNSUPPORED_CHIP, "");
            break;
    }
    return ePRG_SetStatus(psContext, E_PRG_OK, "");
}


static teStatus ePRG_ResetDevice(tsPRG_Context *psContext)
{
    tsPRG_PrivateContext *psPriv = (tsPRG_PrivateContext *)psContext->pvPrivate;

    DBG_vPrintf(TRACE_PROGRAMMER, "ePRG_ResetDevice()\n");

    if (psPriv->psProtocol != NULL)
    {
        if (psPriv->psProtocol->eBL_Reset != NULL)
        {
            return psPriv->psProtocol->eBL_Reset(psContext);
        }
    }

    return E_PRG_OK;
}

tsMemInfo *psGetMemByName(tsPRG_Context *psContext, const char *pcMemoryName)
{
	tsMemInfo *psMemInfo;

	psMemInfo = psContext->sChipDetails.psMemInfoList;

    while (psMemInfo != NULL)
    {
#ifdef POSIX
    	if (strcasecmp(psMemInfo->pcMemName, pcMemoryName) == 0)
#else
    	if (stricmp(psMemInfo->pcMemName, pcMemoryName) == 0)
#endif
    	{
    		break;
    	}
    	psMemInfo = psMemInfo->psNext;
    }

    return psMemInfo;
}

tsMemInfo *psGetMemoryInfo(tsPRG_Context *psContext, uint32_t u32MemoryIndex)
{
	tsMemInfo *psMemInfo;

	psMemInfo = psContext->sChipDetails.psMemInfoList;

    while (psMemInfo != NULL)
    {
    	if (psMemInfo->u8Index == u32MemoryIndex)
    	{
    		break;
    	}
    	psMemInfo = psMemInfo->psNext;
    }

    return psMemInfo;
}

/****************************************************************************/
/***        END OF FILE                                                   ***/
/****************************************************************************/
