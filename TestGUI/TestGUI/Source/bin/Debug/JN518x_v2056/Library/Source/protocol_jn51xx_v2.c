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
#include <math.h>
#include <sys/time.h>

#include <programmer.h>
#include "programmer_private.h"

#include "isp_comms.h"
#include "uart.h"
#include "pipe.h"
#include "dbg.h"
#include "crc.h"
#include "protocol.h"
#include "protocol_jn51xx_v2.h"
#include "portable_endian.h"

#ifdef USE_TOMCRYPT
#include <tomcrypt.h>
#endif

/****************************************************************************/
/***        Macro Definitions                                             ***/
/****************************************************************************/

#ifdef DEBUG_PROTOCOL
#define TRACE_PROTOCOL      TRUE
#else
#define TRACE_PROTOCOL      FALSE
#endif

#ifdef DEBUG_BOOTLOADER
#define TRACE_BOOTLOADER    TRUE
#else
#define TRACE_BOOTLOADER    FALSE
#endif

#define BL_TIMEOUT_250MS    250000
#define BL_TIMEOUT_500MS    500000
#define BL_TIMEOUT_1S       1000000
#define BL_TIMEOUT_10S      10000000

#define BOOTLOADER_MAX_MESSAGE_LENGTH 2048

/****************************************************************************/
/***        Type Definitions                                              ***/
/****************************************************************************/

typedef enum
{
    E_BL_MSG_TYPE_RESET_REQUEST                         = 0x14,
    E_BL_MSG_TYPE_RESET_RESPONSE                        = 0x15,

    E_BL_MSG_TYPE_RAM_RUN_REQUEST                       = 0x21,
    E_BL_MSG_TYPE_RAM_RUN_RESPONSE                      = 0x22,

    E_BL_MSG_TYPE_SET_BAUD_REQUEST                      = 0x27,
    E_BL_MSG_TYPE_SET_BAUD_RESPONSE                     = 0x28,

    E_BL_MSG_TYPE_GET_CHIPID_REQUEST                    = 0x32,
    E_BL_MSG_TYPE_GET_CHIPID_RESPONSE                   = 0x33,

	TYPE_MEM_OPEN_REQUEST = 0x40,
    TYPE_MEM_OPEN_RESPONSE,
	TYPE_MEM_ERASE_REQUEST,
    TYPE_MEM_ERASE_RESPONSE,
	TYPE_MEM_BLANK_CHECK_REQUEST,
    TYPE_MEM_BLANK_CHECK_RESPONSE,
	TYPE_MEM_READ_REQUEST,
	TYPE_MEM_READ_RESPONSE,
	TYPE_MEM_WRITE_REQUEST,
	TYPE_MEM_WRITE_RESPONSE,
	TYPE_MEM_CLOSE_REQUEST,
	TYPE_MEM_CLOSE_RESPONSE,
    TYPE_MEM_GET_INFO_REQUEST,
    TYPE_MEM_GET_INFO_RESPONSE,
    
    TYPE_UNLOCK_ISP_REQUEST,
	TYPE_UNLOCK_ISP_RESPONSE,

	TYPE_START_AUTHENTICATION_REQUEST,
	TYPE_START_AUTHENTICATION_RESPONSE,

	TYPE_USE_CERTIFICATE_REQUEST,
	TYPE_USE_CERTIFICATE_RESPONSE,

	TYPE_SET_ENCRYPTION_REQUEST,
	TYPE_SET_ENCRYPTION_RESPONSE,
    
} __attribute ((packed)) teBL2_MessageType;


typedef enum
{
    E_BL_RESPONSE_OK                                    = 0x00,
    E_BL_RESPONSE_NOT_SUPPORTED                         = 0xff,
    E_BL_RESPONSE_WRITE_FAIL                            = 0xfe,
    E_BL_RESPONSE_INVALID_RESPONSE                      = 0xfd,
    E_BL_RESPONSE_CRC_ERROR                             = 0xfc,
    E_BL_RESPONSE_ASSERT_FAIL                           = 0xfb,
    E_BL_RESPONSE_USER_INTERRUPT                        = 0xfa,
    E_BL_RESPONSE_READ_FAIL                             = 0xf9,
    E_BL_RESPONSE_TST_ERROR                             = 0xf8,
    E_BL_RESPONSE_AUTH_ERROR                            = 0xf7,
    E_BL_RESPONSE_NO_RESPONSE                           = 0xf6,
    E_BL_RESPONSE_ERROR                                 = 0xf0,
} __attribute__ ((packed)) teBL2_Response;

/****************************************************************************/
/***        Local Function Prototypes                                     ***/
/****************************************************************************/

static teStatus eBL_Connect(tsPRG_Context *psContext, tsConnection *psConnection);
static teStatus eBL_MemInfo(tsPRG_Context *psContext);

teStatus eBL2_SetBaudrate(tsPRG_Context *psContext, uint32_t u32Baudrate);

teStatus eBL2_ChipIdRead(tsPRG_Context *psContext, uint32_t *pu32ChipId, uint32_t *pu32BootloaderVersion);

teStatus eBL2_Execute(tsPRG_Context *psContext, uint32_t u32Address);

teStatus eBL2_Reset(tsPRG_Context *psContext);

static teBL2_Response eBL2_Request(tsPRG_Context *psContext, int iTimeoutMicroseconds, teBL2_MessageType eTxType, uint8_t u8HeaderLen, const uint8_t *pu8Header, uint16_t u16TxLength, const uint8_t *pu8TxData, teBL2_MessageType *peRxType, uint16_t *pu16RxLength, uint8_t *pu8RxData);

static teStatus eBL2_WriteMessage(tsPRG_Context *psContext, teBL2_MessageType eType, uint8_t u8HeaderLength, const uint8_t *pu8Header, uint16_t u16DataLength, const uint8_t *pu8Data);
static teBL2_Response eBL2_ReadMessage(tsPRG_Context *psContext, int iTimeoutMicroseconds, teBL2_MessageType *peType, uint16_t *pu16Length, uint8_t *pu8Data);

static teStatus eBL2_CheckResponse(const char *pcFunction, teBL2_Response eResponse, teBL2_MessageType eRxType, teBL2_MessageType eExpectedRxType);

/****************************************************************************/
/***        Exported Variables                                            ***/
/****************************************************************************/

tsProtocol sProtocol_v2 =
{
        .u32ChunkSize    = 512,

        .eBL_Connect     = eBL_Connect,
        .eBL_Unlock      = eBL2_Unlock,
        .eBL_MemInfo     = eBL_MemInfo,
        .eBL_SetBaudrate = eBL2_SetBaudrate,
        .eBL_Reset       = eBL2_Reset,

        .eBL_MemOpen     = eBL2_MemOpen,
        .eBL_MemErase    = eBL2_MemErase,
        .eBL_MemBlank    = eBL2_MemBlank,
        .eBL_MemWrite    = eBL2_MemWrite,
        .eBL_MemClose    = eBL2_MemClose,
};

/****************************************************************************/
/***        Local Variables                                               ***/
/****************************************************************************/

/****************************************************************************/
/***        Exported Functions                                            ***/
/****************************************************************************/

static teStatus eBL_Connect(tsPRG_Context *psContext, tsConnection *psConnection)
{
    tsChipDetails *psChipDetails = &psContext->sChipDetails;
    teStatus eStatus;
    uint32_t u32BaudRate = 0;

    DBG_vPrintf(TRACE_BOOTLOADER, "Connect Protocol v2\n");

    if (psConnection->eType == E_CONNECT_SERIAL)
    {
        u32BaudRate = psConnection->uDetails.sSerial.u32BaudRate;

        if (u32BaudRate == 0)
        {
            psConnection->uDetails.sSerial.u32BaudRate = 115200;
        }

        psConnection->uDetails.sSerial.cbUpdate(psContext, psConnection);
    }

    eStatus = eBL2_Unlock(psContext, 0, NULL, 0);

    if (eStatus == E_PRG_OK)
    {
        eStatus = eBL2_ChipIdRead(psContext, &psChipDetails->u32ChipId, &psChipDetails->u32BootloaderVersion);

        DBG_vPrintf(TRACE_BOOTLOADER, "Chip ID: 0x%08X\n", psContext->sChipDetails.u32ChipId);
    }
    else
    {
        DBG_vPrintf(TRACE_BOOTLOADER, "ERROR: eBL2_Unlock() failed\n");
        if (psConnection->eType == E_CONNECT_SERIAL)
        {
            psConnection->uDetails.sSerial.u32BaudRate = u32BaudRate;
        }
    }

    return eStatus;
}

static teStatus eBL_MemInfo(tsPRG_Context *psContext)
{
    teStatus eStatus;
    tsMemInfo *psMemInfo;
    tsMemInfo **ppsMemInfoListTail = &psContext->sChipDetails.psMemInfoList;
    uint8_t index = 0;

    do {
        psMemInfo = calloc(1, sizeof(tsMemInfo));

        if((eStatus = eBL2_MemInfo(psContext, index, psMemInfo)) == E_PRG_OK)
        {
            *ppsMemInfoListTail = psMemInfo;
            ppsMemInfoListTail = &psMemInfo->psNext;
            index = psMemInfo->u8Index + 1;
        }
        else
        {
            free(psMemInfo);
        }
    } while (eStatus == E_PRG_OK);

    if (index > 0)
    {
        DBG_vPrintf(TRACE_PROTOCOL, "MemInfo: 0x%08X\n", psContext->sChipDetails.psMemInfoList);

        psContext->sChipDetails.u32NumMemories = index;

        return E_PRG_OK;
    }
    return eStatus;
}

teStatus eBL2_SetBaudrate(tsPRG_Context *psContext, uint32_t u32Baudrate)
{
    teBL2_Response eResponse = 0;
    teBL2_MessageType eRxType = 0;
    uint8_t au8Buffer[6];
    uint32_t u32Divisor;

    DBG_vPrintf(TRACE_BOOTLOADER, "Set BL Baud rate to %d\n", u32Baudrate);

    // Divide 1MHz clock by baudrate to get the divisor
    u32Divisor = (uint32_t)roundf(1000000.0 / (float)u32Baudrate);

    DBG_vPrintf(TRACE_BOOTLOADER, "Set divisor %d\n", u32Divisor);

    au8Buffer[0] = (uint8_t)u32Divisor;
    au8Buffer[1] = (u32Baudrate >>  0) & 0xFF;
    au8Buffer[2] = (u32Baudrate >>  8) & 0xFF;
    au8Buffer[3] = (u32Baudrate >> 16) & 0xFF;
    au8Buffer[4] = (u32Baudrate >> 24) & 0xFF;

    eResponse = eBL2_Request(psContext, BL_TIMEOUT_1S, E_BL_MSG_TYPE_SET_BAUD_REQUEST, 5, au8Buffer, 0, NULL, &eRxType, NULL, NULL);
    return eBL2_CheckResponse(__FUNCTION__, eResponse, eRxType, E_BL_MSG_TYPE_SET_BAUD_RESPONSE);
}

teStatus eBL2_Unlock(tsPRG_Context *psContext, uint8_t u8Mode, const uint8_t *pu8Key, uint16_t u16KeyLen)
{
    teBL2_Response eResponse = 0;
    teBL2_MessageType eRxType = 0;
    uint8_t au8Buffer[1];

    DBG_vPrintf(TRACE_BOOTLOADER, "eBL2_Unlock()\n");

    au8Buffer[0] = u8Mode;

    eResponse = eBL2_Request(psContext, BL_TIMEOUT_250MS, TYPE_UNLOCK_ISP_REQUEST, 1, au8Buffer, u16KeyLen, pu8Key, &eRxType, NULL, NULL);
    return eBL2_CheckResponse(__FUNCTION__, eResponse, eRxType, TYPE_UNLOCK_ISP_RESPONSE);
}

/****************************************************************************
 *
 * NAME: iBL_ReadChipId
 *
 * DESCRIPTION:
 *
 * PARAMETERS: Name        RW  Usage
 *
 * RETURNS:
 * int          0 if success
 *              -1 if an error occurred
 *
 ****************************************************************************/
teStatus eBL2_ChipIdRead(tsPRG_Context *psContext, uint32_t *pu32ChipId, uint32_t *pu32BootloaderVersion)
{

    teBL2_Response eResponse = 0;
    teBL2_MessageType eRxType = 0;
    uint16_t u16RxDataLen = 0;
    uint8_t au8Buffer[BOOTLOADER_MAX_MESSAGE_LENGTH];

    DBG_vPrintf(TRACE_BOOTLOADER, "eBL2_ChipIdRead()\n");

    if(pu32ChipId == NULL)
    {
        return E_PRG_NULL_PARAMETER;
    }

    if(pu32BootloaderVersion == NULL)
    {
        return E_PRG_NULL_PARAMETER;
    }
    
    eResponse = eBL2_Request(psContext, BL_TIMEOUT_1S, E_BL_MSG_TYPE_GET_CHIPID_REQUEST, 0, NULL, 0, NULL, &eRxType, &u16RxDataLen, au8Buffer);

    if ((u16RxDataLen != 4) && (u16RxDataLen != 8))
    {
        return E_PRG_COMMS_FAILED;
    }

    *pu32ChipId  = au8Buffer[0] << 24;
    *pu32ChipId |= au8Buffer[1] << 16;
    *pu32ChipId |= au8Buffer[2] << 8;
    *pu32ChipId |= au8Buffer[3] << 0;

    if(u16RxDataLen == 8)
    {
        // Bootloader version is included
        *pu32BootloaderVersion  = au8Buffer[4] << 24;
        *pu32BootloaderVersion |= au8Buffer[5] << 16;
        *pu32BootloaderVersion |= au8Buffer[6] << 8;
        *pu32BootloaderVersion |= au8Buffer[7] << 0;

        DBG_vPrintf(TRACE_BOOTLOADER, "Bootloader Id Detected : u32BootloaderVersion = %x \n", *pu32BootloaderVersion);
    } else {
        *pu32BootloaderVersion = 0;
    }

    return eBL2_CheckResponse(__FUNCTION__, eResponse, eRxType, E_BL_MSG_TYPE_GET_CHIPID_RESPONSE);
}


static inline uint32_t le32btoh(uint8_t *pu8Store)
{
   uint32_t u32Le;
   memcpy(&u32Le, pu8Store, sizeof(uint32_t));
   return le32toh(u32Le);
}

teStatus eBL2_MemInfo(tsPRG_Context *psContext, uint8_t u8Index, tsMemInfo *psMemInfo)
{
    teStatus status;
    uint8_t au8CmdBuffer[18];
    teBL2_Response eResponse = 0;
    teBL2_MessageType eRxType = 0;
    uint16_t u16RxLength;
    uint16_t u16NameLen;
    uint8_t u8RxBuffer[BOOTLOADER_MAX_MESSAGE_LENGTH];

    DBG_vPrintf(TRACE_BOOTLOADER, "Get memory info\n");

    au8CmdBuffer[0] = u8Index;

    eResponse = eBL2_Request(psContext, BL_TIMEOUT_1S, TYPE_MEM_GET_INFO_REQUEST, 1, au8CmdBuffer, 0, NULL, &eRxType, &u16RxLength, u8RxBuffer);
    
    status = eBL2_CheckResponse(__FUNCTION__, eResponse, eRxType, TYPE_MEM_GET_INFO_RESPONSE);
    
    if (status == E_PRG_OK)
    {
    	psMemInfo->u8Index = u8RxBuffer[0];
        psMemInfo->u32BaseAddress = le32btoh(&u8RxBuffer[1]);
        psMemInfo->u32Size = le32btoh(&u8RxBuffer[5]);
        psMemInfo->u32BlockSize = le32btoh(&u8RxBuffer[9]);
        /* Memory type - unused */
        psMemInfo->u8Access = u8RxBuffer[14];

        u16NameLen = u16RxLength - 15;

        if (u16NameLen > 0)
        {
        	psMemInfo->pcMemName = malloc(u16NameLen + 1);
        	memcpy(psMemInfo->pcMemName, &u8RxBuffer[15], u16NameLen);
        	psMemInfo->pcMemName[u16NameLen] = '\0';
        }
        
        uint32_t u32Size = psMemInfo->u32Size;
        char *pcSizeUnit = "b";

        if (u32Size > 1024)
        {
        	u32Size = psMemInfo->u32Size  / 1024;
        	pcSizeUnit = "Kb";
        }
        DBG_vPrintf(TRACE_BOOTLOADER, "%d '%s': Base: 0x%08x, Length: %3d%s, Block: %3db\n", psMemInfo->u8Index, psMemInfo->pcMemName, psMemInfo->u32BaseAddress, u32Size, pcSizeUnit, psMemInfo->u32BlockSize);
    }
    
    return status;
}

teStatus eBL2_MemOpen(tsPRG_Context *psContext, uint8_t u8Index, uint8_t u8AccessMode, uint32_t *pu32Handle)
{
    uint8_t au8CmdBuffer[18];
    teBL2_Response eResponse = 0;
    teBL2_MessageType eRxType = 0;
    uint16_t u16RxLength;
    uint8_t u8RxBuffer[BOOTLOADER_MAX_MESSAGE_LENGTH];

    DBG_vPrintf(TRACE_BOOTLOADER, "Memory Open\n");

    au8CmdBuffer[0] = u8Index;
    au8CmdBuffer[1] = u8AccessMode;

    eResponse = eBL2_Request(psContext, BL_TIMEOUT_1S, TYPE_MEM_OPEN_REQUEST, 2, au8CmdBuffer, 0, NULL, &eRxType, &u16RxLength, u8RxBuffer);
        
    *pu32Handle = 0;

    return eBL2_CheckResponse(__FUNCTION__, eResponse, eRxType, TYPE_MEM_OPEN_RESPONSE);
}

teStatus eBL2_MemErase(tsPRG_Context *psContext, uint8_t u8Index, uint32_t u32Address, uint32_t u32Length)
{
    uint8_t au8CmdBuffer[10];
    teBL2_Response eResponse = 0;
    teBL2_MessageType eRxType = 0;
    uint16_t u16RxLength;
    uint8_t u8RxBuffer[BOOTLOADER_MAX_MESSAGE_LENGTH];

    DBG_vPrintf(TRACE_BOOTLOADER, "Memory Erase\n");

    au8CmdBuffer[0] = u8Index;
    
    au8CmdBuffer[1] = 0; /* Mode */

    au8CmdBuffer[2] = (uint8_t)(u32Address >> 0)  & 0xff;
    au8CmdBuffer[3] = (uint8_t)(u32Address >> 8)  & 0xff;
    au8CmdBuffer[4] = (uint8_t)(u32Address >> 16) & 0xff;
    au8CmdBuffer[5] = (uint8_t)(u32Address >> 24) & 0xff;
    
    au8CmdBuffer[6] = (uint8_t)(u32Length >> 0)  & 0xff;
    au8CmdBuffer[7] = (uint8_t)(u32Length >> 8)  & 0xff;
    au8CmdBuffer[8] = (uint8_t)(u32Length >> 16) & 0xff;
    au8CmdBuffer[9] = (uint8_t)(u32Length >> 24) & 0xff;

    eResponse = eBL2_Request(psContext, BL_TIMEOUT_1S, TYPE_MEM_ERASE_REQUEST, 10, au8CmdBuffer, 0, NULL, &eRxType, &u16RxLength, u8RxBuffer);
        
    return eBL2_CheckResponse(__FUNCTION__, eResponse, eRxType, TYPE_MEM_ERASE_RESPONSE);
}

teStatus eBL2_MemBlank(tsPRG_Context *psContext, uint8_t u8Index, uint32_t u32Address, uint32_t u32Length)
{
    uint8_t au8CmdBuffer[10];
    teBL2_Response eResponse = 0;
    teBL2_MessageType eRxType = 0;
    uint16_t u16RxLength;
    uint8_t u8RxBuffer[BOOTLOADER_MAX_MESSAGE_LENGTH];

    DBG_vPrintf(TRACE_BOOTLOADER, "Memory Blank Check\n");

    au8CmdBuffer[0] = u8Index;
    
    au8CmdBuffer[1] = 0; /* Mode */

    au8CmdBuffer[2] = (uint8_t)(u32Address >> 0)  & 0xff;
    au8CmdBuffer[3] = (uint8_t)(u32Address >> 8)  & 0xff;
    au8CmdBuffer[4] = (uint8_t)(u32Address >> 16) & 0xff;
    au8CmdBuffer[5] = (uint8_t)(u32Address >> 24) & 0xff;
    
    au8CmdBuffer[6] = (uint8_t)(u32Length >> 0)  & 0xff;
    au8CmdBuffer[7] = (uint8_t)(u32Length >> 8)  & 0xff;
    au8CmdBuffer[8] = (uint8_t)(u32Length >> 16) & 0xff;
    au8CmdBuffer[9] = (uint8_t)(u32Length >> 24) & 0xff;

    eResponse = eBL2_Request(psContext, BL_TIMEOUT_1S, TYPE_MEM_BLANK_CHECK_REQUEST, 10, au8CmdBuffer, 0, NULL, &eRxType, &u16RxLength, u8RxBuffer);
        
    return eBL2_CheckResponse(__FUNCTION__, eResponse, eRxType, TYPE_MEM_BLANK_CHECK_RESPONSE);
}

/****************************************************************************
 *
 * NAME: iBL_ReadRAM
 *
 * DESCRIPTION:
 *
 * PARAMETERS: Name        RW  Usage
 *
 * RETURNS:
 * int          0 if success
 *              -1 if an error occurred
 *
 ****************************************************************************/
teStatus eBL2_MemRead(tsPRG_Context *psContext, uint8_t u8Index, uint32_t u32Address, uint32_t *pu32Length, uint8_t *pu8Buffer)
{
    uint16_t u16RxDataLen = 0;
    uint8_t au8CmdBuffer[10];
    teBL2_Response eResponse = 0;
    teBL2_MessageType eRxType = 0;

    DBG_vPrintf(TRACE_BOOTLOADER, "eBL2_MemRead()\n");
    if(pu8Buffer == NULL)
    {
        return E_PRG_BAD_PARAMETER;
    }

    au8CmdBuffer[0] = u8Index;
    
    au8CmdBuffer[1] = 0; /* Mode */

    au8CmdBuffer[2] = (uint8_t)(u32Address >> 0)  & 0xff;
    au8CmdBuffer[3] = (uint8_t)(u32Address >> 8)  & 0xff;
    au8CmdBuffer[4] = (uint8_t)(u32Address >> 16) & 0xff;
    au8CmdBuffer[5] = (uint8_t)(u32Address >> 24) & 0xff;
    
    au8CmdBuffer[6] = (uint8_t)(*pu32Length >> 0)  & 0xff;
    au8CmdBuffer[7] = (uint8_t)(*pu32Length >> 8)  & 0xff;
    au8CmdBuffer[8] = (uint8_t)(*pu32Length >> 16) & 0xff;
    au8CmdBuffer[9] = (uint8_t)(*pu32Length >> 24) & 0xff;
    
    eResponse = eBL2_Request(psContext, BL_TIMEOUT_1S, TYPE_MEM_READ_REQUEST, 10, au8CmdBuffer, 0, NULL, &eRxType, &u16RxDataLen, pu8Buffer);

    if (u16RxDataLen == 0)
    {
        DBG_vPrintf(TRACE_BOOTLOADER, "Requested %d bytes, got %d\n", *pu32Length, u16RxDataLen);
        return E_PRG_ERROR_READING;
    }

    *pu32Length = u16RxDataLen;

    return eBL2_CheckResponse(__FUNCTION__, eResponse, eRxType, TYPE_MEM_READ_RESPONSE);
}

/****************************************************************************
 *
 * NAME: iBL_WriteRAM
 *
 * DESCRIPTION:
 *
 * PARAMETERS: Name        RW  Usage
 *
 * RETURNS:
 * int          0 if success
 *              -1 if an error occurred
 *
 ****************************************************************************/
teStatus eBL2_MemWrite(tsPRG_Context *psContext, uint8_t u8Index, uint32_t u32Address, uint32_t u32Length, uint8_t *pu8Buffer)
{
    uint8_t au8CmdBuffer[10];
    teBL2_Response eResponse = 0;
    teBL2_MessageType eRxType = 0;

    if(pu8Buffer == NULL)
    {
        return E_PRG_BAD_PARAMETER;
    }

    au8CmdBuffer[0] = u8Index;
    
    au8CmdBuffer[1] = 0; /* Mode */

    au8CmdBuffer[2] = (uint8_t)(u32Address >> 0)  & 0xff;
    au8CmdBuffer[3] = (uint8_t)(u32Address >> 8)  & 0xff;
    au8CmdBuffer[4] = (uint8_t)(u32Address >> 16) & 0xff;
    au8CmdBuffer[5] = (uint8_t)(u32Address >> 24) & 0xff;
    
    au8CmdBuffer[6] = (uint8_t)(u32Length >> 0)  & 0xff;
    au8CmdBuffer[7] = (uint8_t)(u32Length >> 8)  & 0xff;
    au8CmdBuffer[8] = (uint8_t)(u32Length >> 16) & 0xff;
    au8CmdBuffer[9] = (uint8_t)(u32Length >> 24) & 0xff;
    
    eResponse = eBL2_Request(psContext, BL_TIMEOUT_250MS, TYPE_MEM_WRITE_REQUEST, 10, au8CmdBuffer, u32Length, pu8Buffer, &eRxType, NULL, NULL);
    
    return eBL2_CheckResponse(__FUNCTION__, eResponse, eRxType, TYPE_MEM_WRITE_RESPONSE);
}

teStatus eBL2_MemClose(tsPRG_Context *psContext, uint8_t u8Index)
{
    uint8_t au8CmdBuffer[18];
    teBL2_Response eResponse = 0;
    teBL2_MessageType eRxType = 0;
    uint16_t u16RxLength;
    uint8_t u8RxBuffer[BOOTLOADER_MAX_MESSAGE_LENGTH];

    DBG_vPrintf(TRACE_BOOTLOADER, "Memory Close\n");

    au8CmdBuffer[0] = u8Index;

    eResponse = eBL2_Request(psContext, BL_TIMEOUT_1S, TYPE_MEM_CLOSE_REQUEST, 1, au8CmdBuffer, 0, NULL, &eRxType, &u16RxLength, u8RxBuffer);
        
    return eBL2_CheckResponse(__FUNCTION__, eResponse, eRxType, TYPE_MEM_CLOSE_RESPONSE);
}


teStatus eBL2_Reset(tsPRG_Context *psContext)
{
    teBL2_Response eResponse = 0;
    teBL2_MessageType eRxType = 0;

    eResponse = eBL2_Request(psContext, BL_TIMEOUT_1S, E_BL_MSG_TYPE_RESET_REQUEST, 0, NULL, 0, NULL, &eRxType, NULL, NULL);
    return eBL2_CheckResponse(__FUNCTION__, eResponse, eRxType, E_BL_MSG_TYPE_RESET_RESPONSE);
}

/****************************************************************************/
/***        Local Functions                                               ***/
/****************************************************************************/

/****************************************************************************
 *
 * NAME: eBL2_Request
 *
 * DESCRIPTION:
 *
 * PARAMETERS: Name        RW  Usage
 *
 * RETURNS:
 * void
 ****************************************************************************/
static teBL2_Response eBL2_Request(tsPRG_Context *psContext, int iTimeoutMicroseconds, teBL2_MessageType eTxType, uint8_t u8HeaderLen, const uint8_t *pu8Header, uint16_t u16TxLength, const uint8_t *pu8TxData,
                          teBL2_MessageType *peRxType, uint16_t *pu16RxLength, uint8_t *pu8RxData)
{
    /* Check data is not too long */
    if(u16TxLength > 65530)
    {
        DBG_vPrintf(TRACE_BOOTLOADER, "Data too long\n");
        return E_BL_RESPONSE_ERROR;
    }

    /* Send message */
    if(eBL2_WriteMessage(psContext, eTxType, u8HeaderLen, pu8Header, u16TxLength, pu8TxData) != E_PRG_OK)
    {
        return E_BL_RESPONSE_ERROR;
    }

    /* Get the response to the request */
    return eBL2_ReadMessage(psContext, iTimeoutMicroseconds, peRxType, pu16RxLength, pu8RxData);
}


/****************************************************************************
 *
 * NAME: iBL_WriteMessage
 *
 * DESCRIPTION:
 *
 * PARAMETERS: Name        RW  Usage
 *
 * RETURNS:
 * int          0 if success
 *              -1 if an error occured
 *
 ****************************************************************************/
static teStatus eBL2_WriteMessage(tsPRG_Context *psContext, teBL2_MessageType eType, uint8_t u8HeaderLength, const uint8_t *pu8Header, uint16_t u16DataLength, const uint8_t *pu8Data)
{
    int n;
    tsPRG_PrivateContext *psPriv;

    uint8_t *pu8NextHash = NULL;
    
    uint8_t au8Msg[BOOTLOADER_MAX_MESSAGE_LENGTH];
    uint8_t *pu8Msg = au8Msg;
    
    uint8_t u8Flags = 0;
    uint16_t u16Length;

    if (!psContext)
    {
        return E_PRG_NULL_PARAMETER;
    }
    
    u16Length = u8HeaderLength + u16DataLength + 8;

    /* total message length cannot be > 1024 bytes */
    if(u16Length >= BOOTLOADER_MAX_MESSAGE_LENGTH)
    {
        DBG_vPrintf(TRACE_BOOTLOADER, "Length too big\n");
        return E_PRG_BAD_PARAMETER;
    }
    
    if (pu8NextHash != NULL)
    {
        u16Length += 32;
        u8Flags |= (1 << 2);
    }

	
    /* Message flags */
    put8(u8Flags, &pu8Msg);
    
    /* Message length */
    put16b(u16Length, &pu8Msg);

    /* Message type */
    put8((uint8_t)eType, &pu8Msg);

    /* Message header */
    putBuffer(pu8Header, u8HeaderLength, &pu8Msg);

    /* Message payload */
    putBuffer(pu8Data, u16DataLength, &pu8Msg);

    if (pu8NextHash != NULL)
    {
        /* Message hash */
        for (n = 0; n < 32; n++)
        {
            *pu8Msg++ = n;
        }
    }


    /* Message checksum */
    crc_t crc = crc_init();
    crc = crc_update(crc, au8Msg, (pu8Msg - au8Msg));
    crc = crc_finalize(crc);

    put32b(crc, &pu8Msg);

    DBG_vPrintf(TRACE_BOOTLOADER, "\r\nTx: ");
    for(n = 0; n < (pu8Msg - au8Msg); n++)
    {
        if (n % 32 == 0)
        {
            DBG_vPrintf(TRACE_BOOTLOADER, "\r\n");
        }
        DBG_vPrintf(TRACE_BOOTLOADER, "%02x ", au8Msg[n]);
    }
    DBG_vPrintf(TRACE_BOOTLOADER, "\r\n");
    
    /* Write whole message to connection */
    psPriv = (tsPRG_PrivateContext *)psContext->pvPrivate;

    switch(psPriv->sConnection.eType)
    {
    case E_CONNECT_SERIAL:
        return eUART_Write(psContext, au8Msg, pu8Msg - au8Msg);
        break;
        
    case E_CONNECT_PIPE:
        return ePIPE_Write(psContext, au8Msg, pu8Msg - au8Msg);
        break;
        
    default:
        return E_PRG_INVALID_TRANSPORT;
        break;
    }
}

/****************************************************************************
 *
 * NAME: eBL2_ReadMessage
 *
 * DESCRIPTION:
 *
 * PARAMETERS: Name        RW  Usage
 *
 * RETURNS:
 * void
 ****************************************************************************/
static teBL2_Response eBL2_ReadMessage(tsPRG_Context *psContext, int iTimeoutMicroseconds, teBL2_MessageType *peType, uint16_t *pu16Length, uint8_t *pu8Data)
{

    int n;
    teStatus eStatus;
    uint8_t au8Msg[BOOTLOADER_MAX_MESSAGE_LENGTH];
    uint16_t u16Length = 0;
    teBL2_Response eResponse = E_BL_RESPONSE_OK;
    int iTotalBytesRead = 0;

    struct timeval sNow;
    struct timeval sTimeout;

    if (!psContext)
    {
        return E_PRG_NULL_PARAMETER;
    }

    if (gettimeofday(&sNow, NULL) < 0)
    {
        return ePRG_SetStatus(psContext, E_PRG_ERROR, "gettimeofday");
    }

    sTimeout.tv_sec  = sNow.tv_sec  + iTimeoutMicroseconds / 1000000;
    sTimeout.tv_usec = sNow.tv_usec + iTimeoutMicroseconds % 1000000;

    if (sTimeout.tv_usec >= 1000000)
    {
        sTimeout.tv_sec++;
        sTimeout.tv_usec -= 1000000;
    }

    eStatus = eBL_ReadData(psContext, &sTimeout, au8Msg, 3, &iTotalBytesRead);

    if((eStatus != E_PRG_OK) || (iTotalBytesRead != 3) )
    {
        DBG_vPrintf(TRACE_BOOTLOADER, "Error getting length\n");
    }
    else
    {
        /* Get the length */
        u16Length = be16btoh(au8Msg + 1);

        DBG_vPrintf(TRACE_BOOTLOADER, "Incoming message length %d\n", u16Length);

        /* Try and receive the rest of the message */
        if (u16Length < BOOTLOADER_MAX_MESSAGE_LENGTH)
        {
            eStatus = eBL_ReadData(psContext, &sTimeout, au8Msg, u16Length, &iTotalBytesRead);
        }
    }

    /* Print out everything received */
    DBG_vPrintf(TRACE_BOOTLOADER, "\r\nRx: ");
    for(n = 0; n < iTotalBytesRead; n++)
    {
        DBG_vPrintf(TRACE_BOOTLOADER, "%02x ", au8Msg[n]);
    }
    DBG_vPrintf(TRACE_BOOTLOADER, "\n");

    if((eStatus != E_PRG_OK) || (iTotalBytesRead != u16Length) || (u16Length < 9))
    {
        DBG_vPrintf(TRACE_BOOTLOADER, "Got %d / %d bytes (Status %d)\n", iTotalBytesRead, u16Length, eStatus);
        return E_BL_RESPONSE_NO_RESPONSE;
    }

    /* Calculate CRC of message */
    crc_t crc = crc_init();
    crc = crc_update(crc, au8Msg, u16Length - 4);
    crc = crc_finalize(crc);

    uint32_t received_crc = be32btoh(&au8Msg[u16Length - 4]);

    if(crc != received_crc)
    {
        DBG_vPrintf(TRACE_BOOTLOADER, "CRC bad, got %08x expected %08x\n", received_crc, crc);
        return E_BL_RESPONSE_CRC_ERROR;
    }
    
    *peType = au8Msg[3];
    eResponse = au8Msg[4];
    if (pu16Length)
    {
        *pu16Length = u16Length - 9;

        if (pu8Data)
        {
            memcpy(pu8Data, &au8Msg[5], *pu16Length);
        }
    }

    DBG_vPrintf(TRACE_BOOTLOADER, "Got response 0x%02x\n", eResponse);

    return eResponse;
}


static teStatus eBL2_CheckResponse(const char *pcFunction, teBL2_Response eResponse, teBL2_MessageType eRxType, teBL2_MessageType eExpectedRxType)
{
    DBG_vPrintf(TRACE_BOOTLOADER, "%s: Response %02x\n", pcFunction, eResponse);
    switch (eResponse)
    {
        case(E_BL_RESPONSE_OK):
            break;
        case (E_BL_RESPONSE_NOT_SUPPORTED):
            return E_PRG_UNSUPPORTED_OPERATION;
        case (E_BL_RESPONSE_WRITE_FAIL):
            return E_PRG_ERROR_WRITING;
        case (E_BL_RESPONSE_READ_FAIL):
            return E_PRG_ERROR_READING;
        default:
            return E_PRG_COMMS_FAILED;
    }

    if (eRxType != eExpectedRxType)
    {
        DBG_vPrintf(TRACE_BOOTLOADER, "%s: Got type 0x%02x, expected 0x%02x\n", pcFunction, eRxType, eExpectedRxType);
        return E_PRG_COMMS_FAILED;
    }
    return E_PRG_OK;
}


/****************************************************************************/
/***        END OF FILE                                                   ***/
/****************************************************************************/

