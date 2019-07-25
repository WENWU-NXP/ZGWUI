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

#include <programmer.h>
#include "programmer_private.h"

#include "ChipID.h"
#include "uart.h"
#include "dbg.h"
#include "protocol.h"
#include "protocol_jn51xx_v1.h"
#include "portable_endian.h"

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

#define BOOTLOADER_MAX_MESSAGE_LENGTH 255

/* JN513x / JN514x definitions */
#define JN514X_ROM_ID_ADDR                      0x00000004
#define JN514X_MAC_ADDRESS_LOCATION             0x00000030
#define JN514X_MIB_MAC_ADDRESS_LOCATION         0x00000010

/* JN516x definitions */
/* Location of bootloader information in memory map */
#define JN516X_BOOTLOADER_VERSION_ADDRESS               0x00000062
#define JN516X_BOOTLOADER_ENTRY                         0x00000066

/* JN517x definitions */
/* Location of bootloader information in memory map */
#define JN517X_BOOTLOADER_VERSION_ADDRESS               0x00000044
// this address not needed for this implementation
#define JN517X_BOOTLOADER_ENTRY                         0xFFFFFFFF

#define RSTCTRL_REGISTER_ADDRESS                0x0200104C
#define RSTCTRL_CPU_REBOOT_MASK                 (1 << 1)


/****************************************************************************/
/***        Type Definitions                                              ***/
/****************************************************************************/

typedef enum
{
    E_BL_MSG_TYPE_SET_CS_REQUEST                        = 0x05,
    E_BL_MSG_TYPE_SET_CS_RESPONSE                       = 0x06,
    E_BL_MSG_TYPE_FLASH_ERASE_REQUEST                   = 0x07,
    E_BL_MSG_TYPE_FLASH_ERASE_RESPONSE                  = 0x08,
    E_BL_MSG_TYPE_FLASH_PROGRAM_REQUEST                 = 0x09,
    E_BL_MSG_TYPE_FLASH_PROGRAM_RESPONSE                = 0x0a,
    E_BL_MSG_TYPE_FLASH_READ_REQUEST                    = 0x0b,
    E_BL_MSG_TYPE_FLASH_READ_RESPONSE                   = 0x0c,
    E_BL_MSG_TYPE_FLASH_SECTOR_ERASE_REQUEST            = 0x0d,
    E_BL_MSG_TYPE_FLASH_SECTOR_ERASE_RESPONSE           = 0x0e,
    E_BL_MSG_TYPE_FLASH_WRITE_PRG_REGISTER_REQUEST      = 0x0f,
    E_BL_MSG_TYPE_FLASH_WRITE_PRG_REGISTER_RESPONSE     = 0x10,
    E_BL_MSG_TYPE_RESET_REQUEST                         = 0x14,
    E_BL_MSG_TYPE_RESET_RESPONSE                        = 0x15,
    E_BL_MSG_TYPE_RAM_WRITE_REQUEST                     = 0x1d,
    E_BL_MSG_TYPE_RAM_WRITE_RESPONSE                    = 0x1e,
    E_BL_MSG_TYPE_RAM_READ_REQUEST                      = 0x1f,
    E_BL_MSG_TYPE_RAM_READ_RESPONSE                     = 0x20,
    E_BL_MSG_TYPE_RAM_RUN_REQUEST                       = 0x21,
    E_BL_MSG_TYPE_RAM_RUN_RESPONSE                      = 0x22,
    E_BL_MSG_TYPE_FLASH_READ_ID_REQUEST                 = 0x25,
    E_BL_MSG_TYPE_FLASH_READ_ID_RESPONSE                = 0x26,
    E_BL_MSG_TYPE_SET_BAUD_REQUEST                      = 0x27,
    E_BL_MSG_TYPE_SET_BAUD_RESPONSE                     = 0x28,
    E_BL_MSG_TYPE_FLASH_SELECT_TYPE_REQUEST             = 0x2c,
    E_BL_MSG_TYPE_FLASH_SELECT_TYPE_RESPONSE            = 0x2d,

    E_BL_MSG_TYPE_GET_CHIPID_REQUEST                    = 0x32,
    E_BL_MSG_TYPE_GET_CHIPID_RESPONSE                   = 0x33,

    /* Flash programmer extension commands */
    E_BL_MSG_TYPE_PDM_ERASE_REQUEST                     = 0x36,
    E_BL_MSG_TYPE_PDM_ERASE_RESPONSE                    = 0x37,
    E_BL_MSG_TYPE_PROGRAM_INDEX_SECTOR_REQUEST          = 0x38,
    E_BL_MSG_TYPE_PROGRAM_INDEX_SECTOR_RESPONSE         = 0x39,
    E_BL_MSG_TYPE_EEPROM_READ_REQUEST                   = 0x3A,
    E_BL_MSG_TYPE_EEPROM_READ_RESPONSE                  = 0x3B,
    E_BL_MSG_TYPE_EEPROM_WRITE_REQUEST                  = 0x3C,
    E_BL_MSG_TYPE_EEPROM_WRITE_RESPONSE                 = 0x3D,

} __attribute ((packed)) teBL_MessageType;


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
} __attribute__ ((packed)) teBL_Response;


typedef struct
{
    uint8_t                 u8ManufacturerID;
    uint8_t                 u8DeviceID;
    uint8_t                 u8FlashType;
    const char             *pcFlashName;
    uint32_t                u32FlashSize;
} tsBL_FlashDevice;

/****************************************************************************/
/***        Local Function Prototypes                                     ***/
/****************************************************************************/

static teStatus eBL_Connect(tsPRG_Context *psContext, tsConnection *psConnection);
static teStatus eBL_MemInfo(tsPRG_Context *psContext);
static teStatus ePL_Reset(tsPRG_Context *psContext);
static teStatus ePL_MemOpen(tsPRG_Context *psContext, uint8_t u8Index, uint8_t u8AccessMode, uint32_t *pu32Handle);
static teStatus ePL_MemErase(tsPRG_Context *psContext, uint8_t u8Index, uint32_t u32Address, uint32_t u32Length);
static teStatus ePL_MemBlank(tsPRG_Context *psContext, uint8_t u8Index, uint32_t u32Address, uint32_t u32Length);
static teStatus ePL_MemWrite(tsPRG_Context *psContext, uint8_t u8Index, uint32_t u32Address, uint32_t u32Length, uint8_t *pu8Buffer);

static teBL_Response eBL_Request(tsPRG_Context *psContext, int iTimeoutMicroseconds, teBL_MessageType eTxType, uint8_t u8HeaderLen, uint8_t *pu8Header, uint8_t u8TxLength, uint8_t *pu8TxData, teBL_MessageType *peRxType, uint8_t *pu8RxLength, uint8_t *pu8RxData);

static teStatus eBL_WriteMessage(tsPRG_Context *psContext, teBL_MessageType eType, uint8_t u8HeaderLength, uint8_t *pu8Header, uint8_t u8Length, uint8_t *pu8Data);
static teBL_Response eBL_ReadMessage(tsPRG_Context *psContext, int iTimeoutMicroseconds, teBL_MessageType *peType, uint8_t *pu8Length, uint8_t *pu8Data);

static teStatus eBL_CheckResponse(const char *pcFunction, teBL_Response eResponse, teBL_MessageType eRxType, teBL_MessageType eExpectedRxType);

/****************************************************************************/
/***        Exported Variables                                            ***/
/****************************************************************************/

tsProtocol sProtocol_v1 =
{
		.u32ChunkSize    = 128,

		.eBL_Connect     = eBL_Connect,
		.eBL_MemInfo     = eBL_MemInfo,
		.eBL_SetBaudrate = eBL_SetBaudrate,
		.eBL_Reset       = ePL_Reset,

		.eBL_MemOpen     = ePL_MemOpen,
		.eBL_MemErase    = ePL_MemErase,
		.eBL_MemBlank    = ePL_MemBlank,
		.eBL_MemWrite    = ePL_MemWrite,
};

/****************************************************************************/
/***        Local Variables                                               ***/
/****************************************************************************/

/** Array of flash devices that the bootloader knows about */
static tsBL_FlashDevice asFlashDevices[] =
{
    {
        .u8ManufacturerID   = 0x05,
        .u8DeviceID         = 0x05,
        .u8FlashType        = 4,
        .pcFlashName        = "SPI flash ST M25P05-A",
        .u32FlashSize       = 64 * 1024,
    },
    {
        .u8ManufacturerID   = 0x10,
        .u8DeviceID         = 0x10,
        .u8FlashType        = 0,
        .pcFlashName        = "SPI flash ST M25P10-A",
        .u32FlashSize       = 128 * 1024,
    },
    {
        .u8ManufacturerID   = 0x11,
        .u8DeviceID         = 0x11,
        .u8FlashType        = 5,
        .pcFlashName        = "SPI flash ST M25P20-A",
        .u32FlashSize       = 256 * 1024,
    },

    {
        .u8ManufacturerID   = 0x12,
        .u8DeviceID         = 0x12,
        .u8FlashType        = 3,
        .pcFlashName        = "SPI flash ST M25P40",
        .u32FlashSize       = 512 * 1024,
    },

    {
        .u8ManufacturerID   = 0xBF,
        .u8DeviceID         = 0x49,
        .u8FlashType        = 1,
        .pcFlashName        = "SPI flash SST 25VF010A",
        .u32FlashSize       = 128 * 1024,
    },

    {
        .u8ManufacturerID   = 0x1F,
        .u8DeviceID         = 0x60,
        .u8FlashType        = 2,
        .pcFlashName        = "SPI flash Atmel 25F512",
        .u32FlashSize       = 512 * 1024,
    },

    {
        /* JN516x Internal flash - don't need the name / size as it is determined by ePRG_ChipGetChipId */
        .u8ManufacturerID   = 0xCC,
        .u8DeviceID         = 0xEE,
        .u8FlashType        = 8,
    },
};


/****************************************************************************/
/***        Exported Functions                                            ***/
/****************************************************************************/

static teStatus eBL_Connect(tsPRG_Context *psContext, tsConnection *psConnection)
{
	uint8_t au8Buffer[BOOTLOADER_MAX_MESSAGE_LENGTH]; //Size changed from 16 to BOOTLOADER_MAX_MESSAGE_LENGTH to account
                                                      //for the case where the call to eBL_MemoryRead() fails and 255 bytes have been read
	tsChipDetails *psChipDetails = &psContext->sChipDetails;
	teStatus eStatus;
	uint32_t u32BaudRate = 0;

	DBG_vPrintf(TRACE_BOOTLOADER, "Connect Protocol v1\n");

	if (psConnection->eType == E_CONNECT_SERIAL)
	{
		u32BaudRate = psConnection->uDetails.sSerial.u32BaudRate;

		if (u32BaudRate == 0)
		{
			psConnection->uDetails.sSerial.u32BaudRate = 38400;
		}

		psConnection->uDetails.sSerial.cbUpdate(psContext, psConnection);
	}

	eBL_Flush(psContext);

	eStatus = eBL_ChipIdRead(psContext, &psChipDetails->u32ChipId, &psChipDetails->u32BootloaderVersion);

	if (eStatus == E_PRG_OK)
	{
		DBG_vPrintf(TRACE_BOOTLOADER, "Chip ID: 0x%08X\n", psContext->sChipDetails.u32ChipId);
	} else {
        /* That failed so it might be an old device that doesn't support the command, try reading it directly */
		eStatus = eBL_MemoryRead(psContext, 0x100000FC, sizeof(uint32_t), sizeof(uint32_t), au8Buffer);

		if (eStatus == E_PRG_OK)
        {
            memcpy(&psChipDetails->u32ChipId, au8Buffer, sizeof(uint32_t));
        }
	}

	if (eStatus == E_PRG_OK)
	{
		if(CHIP_ID_PART(psChipDetails->u32ChipId) == CHIP_ID_PART(CHIP_ID_JN5172))
		{
			psChipDetails->eEndianness = E_CHIP_LITTLE_ENDIAN;
		}

		if(
			(CHIP_ID_PART(psChipDetails->u32ChipId) == CHIP_ID_PART(CHIP_ID_JN5168)) ||
			(CHIP_ID_PART(psChipDetails->u32ChipId) == CHIP_ID_PART(CHIP_ID_JN5169)) ||
			(CHIP_ID_PART(psChipDetails->u32ChipId) == CHIP_ID_PART(CHIP_ID_JN5172))
		)
		{
			if (psChipDetails->u32BootloaderVersion == 0)
			{
				if(CHIP_ID_PART(psChipDetails->u32ChipId) == CHIP_ID_PART(CHIP_ID_JN5172))
				{
					if (eBL_MemoryRead(psContext, JN517X_BOOTLOADER_VERSION_ADDRESS, sizeof(uint32_t), sizeof(uint32_t), au8Buffer) != E_PRG_OK)
					{
						DBG_vPrintf(TRACE_PROTOCOL, "Error Reading bootloader version\n");
						return E_PRG_ERROR;
					}
				}
				else // 6x
				{
					if (eBL_MemoryRead(psContext, JN516X_BOOTLOADER_VERSION_ADDRESS, sizeof(uint32_t), sizeof(uint32_t), au8Buffer) != E_PRG_OK)
					{
						DBG_vPrintf(TRACE_PROTOCOL, "Error Reading bootloader version\n");
						return E_PRG_ERROR;
					}
				}

				// MemoryRead converts the word buffer into native endianness
				memcpy(&psChipDetails->u32BootloaderVersion, au8Buffer, sizeof(uint32_t));
			}

			DBG_vPrintf(TRACE_PROTOCOL, "Bootloader version 0x%08x\n", psChipDetails->u32BootloaderVersion);
		}
		else
		{
			if (eBL_MemoryRead(psContext, JN514X_ROM_ID_ADDR, sizeof(uint32_t), sizeof(uint32_t), au8Buffer) != E_PRG_OK)
			{
				DBG_vPrintf(TRACE_PROTOCOL, "Error Reading ROM ID\n");
				return E_PRG_ERROR;
			}
			else
			{
				memcpy(&psChipDetails->u32SupportedFirmware, au8Buffer, sizeof(uint32_t));
			}
		}
	}
	else
	{
		/* Restore original baudrate on failure */
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
	uint32_t au32Buffer[4];
	uint32_t u32DeviceConfig;
	tsChipDetails *psChipDetails = &psContext->sChipDetails;
	uint32_t u32FlashSize;

    uint8_t u8ManufacturerID;
    uint8_t u8DeviceID;
    const char *pcName;

    /* Internal flash */

	if(eBL_MemoryRead(psContext, JN516X_INDEX_SECTOR_ATE_SETTINGS_ADDRESS, sizeof(au32Buffer), sizeof(uint32_t), (uint8_t*)au32Buffer) == E_PRG_OK)
	{

		// Majority voting
		u32DeviceConfig = (au32Buffer[0] & au32Buffer[1]) | (au32Buffer[0] & au32Buffer[2]) | (au32Buffer[1] & au32Buffer[2]);

		DBG_vPrintf(TRACE_PROTOCOL, "ATE Settings (read from 0x%08X): 0x%08X (0x", JN516X_INDEX_SECTOR_ATE_SETTINGS_ADDRESS, u32DeviceConfig);
		{
			int i;
			for (i = 0; i < sizeof(au32Buffer); i++)
			{
				DBG_vPrintf(TRACE_PROTOCOL, "%02X", ((uint8_t*)au32Buffer)[i]);
			}
			DBG_vPrintf(TRACE_PROTOCOL, ")\n");
		}

		if (CHIP_ID_PART(psChipDetails->u32ChipId) == CHIP_ID_PART(CHIP_ID_JN5168))
		{
			psChipDetails->u32EepromSize    = (4 * 1024) - 64; /* Final segment not usable */
			u32FlashSize                    = (u32DeviceConfig & 0x07) >> 0;
			psChipDetails->u32RamSize       = (u32DeviceConfig & 0x30) >> 4;
		}
		else if (CHIP_ID_PART(psChipDetails->u32ChipId) == CHIP_ID_PART(CHIP_ID_JN5169))
		{
			psChipDetails->u32EepromSize    = (16 * 1024) - 64; /* Final segment not usable */
			u32FlashSize                    = (u32DeviceConfig & 0x0F) >> 0;
			psChipDetails->u32RamSize       = (u32DeviceConfig & 0x30) >> 4;
		}
		else // JN5172 will enter here
		{
			psChipDetails->u32EepromSize    = (4 * 1024) - 64; /* Final segment not usable */
			u32FlashSize                    = (u32DeviceConfig & 0x0F) >> 0;
			psChipDetails->u32RamSize       = (u32DeviceConfig & 0x30) >> 4;
		}
	} else {
		psContext->sDeviceConfig.eCRP = E_DC_CRP_LEVEL1;

		if (CHIP_ID_PART(psChipDetails->u32ChipId) == CHIP_ID_PART(CHIP_ID_JN5168))
		{
			psChipDetails->u32EepromSize    = (4 * 1024) - 64; /* Final segment not usable */
			u32FlashSize                    = 7;
			psChipDetails->u32RamSize       = 3;
		}
		else if (CHIP_ID_PART(psChipDetails->u32ChipId) == CHIP_ID_PART(CHIP_ID_JN5169))
		{
			psChipDetails->u32EepromSize    = (16 * 1024) - 64; /* Final segment not usable */
			u32FlashSize                    = 0xF;
			psChipDetails->u32RamSize       = 3;
		}
		else // JN5172 will enter here
		{
			psChipDetails->u32EepromSize    = (4 * 1024) - 64; /* Final segment not usable */
			u32FlashSize                    = 0xF;
			psChipDetails->u32RamSize       = 3;
		}
	}

	psChipDetails->u32SupportedFirmware = (
		(psChipDetails->u32RamSize              << 16) |
		(u32FlashSize                           << 24) |
		(CHIP_ID_PART(psChipDetails->u32ChipId) >> 12));

	psChipDetails->u32RamSize       = ((psChipDetails->u32RamSize + 1) * 8) * 1024;
	u32FlashSize                    = ((u32FlashSize + 1) * 32) * 1024;

	DBG_vPrintf(TRACE_PROTOCOL, "Internal Flash\n");

	if ((eStatus = ePRG_AddMemory(psContext, "FLASH", u32FlashSize, FLASH_MANUFACTURER_JN516X, FLASH_DEVICE_JN516X, 0)) != E_PRG_OK)
	{
		return eStatus;
	}

	/* SPI Flash */

    DBG_vPrintf(TRACE_PROTOCOL, "Get Flash ID\n");

    if(psChipDetails == NULL)
    {
        return E_PRG_NULL_PARAMETER;
    }

    if ((eStatus = eBL_DiscoverFlash(psContext, 0, &u8ManufacturerID, &u8DeviceID, &pcName, &u32FlashSize)) != E_PRG_OK)
    {
        DBG_vPrintf(TRACE_PROTOCOL, "No flash devices discovered\n");
        if( (CHIP_ID_PART(psChipDetails->u32ChipId) == CHIP_ID_PART(CHIP_ID_JN5168)) ||
            (CHIP_ID_PART(psChipDetails->u32ChipId) == CHIP_ID_PART(CHIP_ID_JN5169)) ||
            (CHIP_ID_PART(psChipDetails->u32ChipId) == CHIP_ID_PART(CHIP_ID_JN5172)) ||
            (CHIP_ID_PART(psChipDetails->u32ChipId) == CHIP_ID_PART(CHIP_ID_JN518x))
        )
        {
            DBG_vPrintf(TRACE_PROTOCOL, "This is OK ....\n");
            /* It is expected behaviour to not find a SPI flash connected to the '68, '69 and '72 */
            return E_PRG_OK;
        }
        return ePRG_SetStatus(psContext, eStatus, "discovering flash device");
    }

    return ePRG_AddMemory(psContext, "SPI_FLASH", u32FlashSize, u8ManufacturerID, u8DeviceID, 0);
}

teStatus ePL_Reset(tsPRG_Context *psContext)
{
	teStatus eStatus;

    if ((eStatus = eBL_Reset(psContext)) != E_PRG_OK)
    {
        ePRG_SetStatus(psContext, eStatus, "reseting device");

        uint32_t u32Mask = RSTCTRL_CPU_REBOOT_MASK;

        eStatus = eBL_MemoryWrite(psContext, RSTCTRL_REGISTER_ADDRESS, sizeof(u32Mask), sizeof(uint32_t), (uint8_t *)&u32Mask);

        /* Expect no response as device has reset */
        if (eStatus == E_PRG_COMMS_FAILED) {
            return E_PRG_OK;
        }
    }

    return eStatus;
}

static teStatus ePL_MemOpen(tsPRG_Context *psContext, uint8_t u8Index, uint8_t u8AccessMode, uint32_t *pu32Handle)
{
	teStatus eStatus;
	tsMemInfo *psMemInfo;

    psMemInfo = psGetMemoryInfo(psContext, u8Index);

    if (psMemInfo == NULL)
    {
        DBG_vPrintf(TRACE_PROTOCOL, "Flash %d not present\n", psMemInfo);
        return ePRG_SetStatus(psContext, E_PRG_FLASH_DEVICE_UNAVAILABLE, "selecting flash type");
    }

    DBG_vPrintf(TRACE_PROTOCOL, "psMemInfo->u8ManufacturerID = 0x%x\n", psMemInfo->u8ManufacturerID);
    DBG_vPrintf(TRACE_PROTOCOL, "psMemInfo->u32FlashSize = 0x%x\n", psMemInfo->u32Size);
    DBG_vPrintf(TRACE_PROTOCOL, "psMemInfo->u8ChipSelect = %d\n", psMemInfo->u8ChipSelect);
    DBG_vPrintf(TRACE_PROTOCOL, "psMemInfo->u8DeviceID = 0x%x\n", psMemInfo->u8DeviceID);

    if((eStatus = eBL_FlashSelectDevice(psContext, psMemInfo->u8ManufacturerID, psMemInfo->u8DeviceID, psMemInfo->u8ChipSelect)) != E_PRG_OK)
    {
        DBG_vPrintf(TRACE_PROTOCOL, "Error selecting flash device\n");
        return ePRG_SetStatus(psContext, eStatus, "selecting flash type");
    }

    /* If its not internal flash, we need to enable write access */
    if(!((psMemInfo->u8ManufacturerID == FLASH_MANUFACTURER_JN516X) && (psMemInfo->u8DeviceID == FLASH_DEVICE_JN516X)))
    {
        if((eStatus = eBL_FlashStatusRegisterWrite(psContext, 0x00)) != E_PRG_OK)
        {
            DBG_vPrintf(TRACE_PROTOCOL, "Error enabling write access\n");
            return ePRG_SetStatus(psContext, eStatus, "writing to flash status register");
        }
    }

    *pu32Handle = u8Index;

    return eStatus;
}

static teStatus ePL_MemErase(tsPRG_Context *psContext, uint8_t u8Index, uint32_t u32Address, uint32_t u32Length)
{
	tsMemInfo *psMemInfo;

    psMemInfo = psGetMemoryInfo(psContext, u8Index);

    if (u32Length != psMemInfo->u32Size)
    {
    	DBG_vPrintf(TRACE_PROTOCOL, "Partial flash erase not supported %d %d %d\n", u8Index, u32Length, psMemInfo->u32Size);
    	return E_PRG_UNSUPPORTED_OPERATION;
    }

    return eBL_FlashErase(psContext);
}

static teStatus ePL_MemBlank(tsPRG_Context *psContext, uint8_t u8Index, uint32_t u32Address, uint32_t u32Length)
{
	teStatus eStatus;

	uint8_t au8Buffer1[512 + 1];
	uint8_t au8Buffer2[512 + 1];

    /* Ensure that flash is erased */
    DBG_vPrintf(TRACE_PROTOCOL, "Checking flash is blank...\n");
    memset(au8Buffer2, 0xFF, 64);

    if ((eStatus = eBL_FlashRead(psContext, 0, 64, au8Buffer1)) != E_PRG_OK)
    {
        return ePRG_SetStatus(psContext, eStatus, "reading Flash at address 0x%08X", 0);
    }
    else
    {
        if (memcmp(au8Buffer1, au8Buffer2, 64))
        {
            return ePRG_SetStatus(psContext, E_PRG_ERROR, "flash erase unsuccessful");
        }
    }

    return E_PRG_OK;
}

static teStatus ePL_MemWrite(tsPRG_Context *psContext, uint8_t u8Index, uint32_t u32Address, uint32_t u32Length, uint8_t *pu8Buffer)
{
	return eBL_FlashWrite(psContext, u32Address, u32Length, pu8Buffer);
}

teStatus eBL_SetBaudrate(tsPRG_Context *psContext, uint32_t u32Baudrate)
{
    teBL_Response eResponse = 0;
    teBL_MessageType eRxType = 0;
    uint8_t au8Buffer[6];
    uint32_t u32Divisor;

    DBG_vPrintf(TRACE_BOOTLOADER, "Set BL Baud rate to %d\n", u32Baudrate);

    // Divide 1MHz clock by baudrate to get the divisor
    u32Divisor = (uint32_t)roundf(1000000.0 / (float)u32Baudrate);

    DBG_vPrintf(TRACE_BOOTLOADER, "Set divisor %d\n", u32Divisor);

    au8Buffer[0] = (uint8_t)u32Divisor;
    au8Buffer[1] = 0;
    au8Buffer[2] = 0;
    au8Buffer[3] = 0;
    au8Buffer[4] = 0;

    eResponse = eBL_Request(psContext, BL_TIMEOUT_1S, E_BL_MSG_TYPE_SET_BAUD_REQUEST, 1, au8Buffer, 0, NULL, &eRxType, NULL, NULL);
    return eBL_CheckResponse(__FUNCTION__, eResponse, eRxType, E_BL_MSG_TYPE_SET_BAUD_RESPONSE);
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
teStatus eBL_ChipIdRead(tsPRG_Context *psContext, uint32_t *pu32ChipId, uint32_t *pu32BootloaderVersion)
{

    teBL_Response eResponse = 0;
    teBL_MessageType eRxType = 0;
    uint8_t u8RxDataLen = 0;
    uint8_t au8Buffer[BOOTLOADER_MAX_MESSAGE_LENGTH];

    if(pu32ChipId == NULL)
    {
        return E_PRG_NULL_PARAMETER;
    }

    if(pu32BootloaderVersion == NULL)
    {
        return E_PRG_NULL_PARAMETER;
    }
    
    eResponse = eBL_Request(psContext, BL_TIMEOUT_1S, E_BL_MSG_TYPE_GET_CHIPID_REQUEST, 0, NULL, 0, NULL, &eRxType, &u8RxDataLen, au8Buffer);

    if ((u8RxDataLen != 4) && (u8RxDataLen != 8))
    {
        return E_PRG_COMMS_FAILED;
    }

    *pu32ChipId  = au8Buffer[0] << 24;
    *pu32ChipId |= au8Buffer[1] << 16;
    *pu32ChipId |= au8Buffer[2] << 8;
    *pu32ChipId |= au8Buffer[3] << 0;

    if(u8RxDataLen == 8)
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

    return eBL_CheckResponse(__FUNCTION__, eResponse, eRxType, E_BL_MSG_TYPE_GET_CHIPID_RESPONSE);
}


/****************************************************************************
 *
 * NAME: eBL_DiscoverFlash
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
teStatus eBL_DiscoverFlash(tsPRG_Context *psContext, uint8_t u8ChipSelect, uint8_t *pu8ManufacturerID, uint8_t *pu8DeviceID, const char **ppcName, uint32_t *pu32FlashSize)
{
    teStatus eStatus;
    int n;

    /* Search for flash type given flash device id code */
    for(n = 0; n < sizeof(asFlashDevices) / sizeof(tsBL_FlashDevice); n++)
    {
        if ((asFlashDevices[n].u8ManufacturerID == FLASH_MANUFACTURER_JN516X) && (asFlashDevices[n].u8DeviceID == FLASH_DEVICE_JN516X))
        {
            /* Don't attempt to detect the internal flash - that has already been detected if present */
            continue;
        }
        /* Attempt to automatically discover SPI connected flash */
        DBG_vPrintf(TRACE_BOOTLOADER, "Trying flash type %d (0x%02X:0x%02X)\n", asFlashDevices[n].u8FlashType, asFlashDevices[n].u8ManufacturerID, asFlashDevices[n].u8DeviceID);
        eStatus = eBL_FlashSelectDevice(psContext, asFlashDevices[n].u8ManufacturerID, asFlashDevices[n].u8DeviceID, u8ChipSelect);
        if (eStatus != E_PRG_OK)
        {
            DBG_vPrintf(TRACE_BOOTLOADER, "Flash select device failed(%d)\n", eStatus);
        }
        else
        {
            /* Get the flash Id */
            eStatus = eBL_FlashIdRead(psContext, pu8ManufacturerID, pu8DeviceID, ppcName, pu32FlashSize);
            if (eStatus == E_PRG_OK)
            {
                DBG_vPrintf(TRACE_BOOTLOADER, "Found flash %s\n", asFlashDevices[n].pcFlashName);
                return E_PRG_OK;
            }
        }
    }

    DBG_vPrintf(TRACE_BOOTLOADER, "No SPI flash device\n");
    return E_PRG_FLASH_DEVICE_UNAVAILABLE;
}


/****************************************************************************
 *
 * NAME: iBL_ReadFlashId
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
teStatus eBL_FlashIdRead(tsPRG_Context *psContext, uint8_t *pu8ManufacturerID, uint8_t *pu8DeviceID, const char **ppcName, uint32_t *pu32FlashSize)
{
    teBL_Response eResponse = 0;
    teBL_MessageType eRxType = 0;
    uint8_t u8RxDataLen = 0;
    uint8_t au8Buffer[BOOTLOADER_MAX_MESSAGE_LENGTH];
    int n;

    if(!psContext || !pu8ManufacturerID || !pu8DeviceID || !pu32FlashSize)
    {
        return E_PRG_NULL_PARAMETER;
    }

    eResponse = eBL_Request(psContext, BL_TIMEOUT_1S, E_BL_MSG_TYPE_FLASH_READ_ID_REQUEST, 0, NULL, 0, NULL, &eRxType, &u8RxDataLen, au8Buffer);

    if (u8RxDataLen != 2)
    {
        return E_PRG_COMMS_FAILED;
    }

    /* Search for flash type given flash device id code */
    for(n = 0; n < sizeof(asFlashDevices) / sizeof(tsBL_FlashDevice); n++)
    {
        /* If we found a match, send command to select this flash device type */
        if((asFlashDevices[n].u8ManufacturerID == au8Buffer[0]) && (asFlashDevices[n].u8DeviceID == au8Buffer[1]))
        {
            DBG_vPrintf(TRACE_BOOTLOADER, "Found Flash \"%s\"\n", asFlashDevices[n].pcFlashName);
            *pu8ManufacturerID  = au8Buffer[0];
            *pu8DeviceID        = au8Buffer[1];
            *ppcName            = asFlashDevices[n].pcFlashName;
            *pu32FlashSize      = asFlashDevices[n].u32FlashSize;
            break;
        }
    }

    if (n == sizeof(asFlashDevices) / sizeof(tsBL_FlashDevice))
    {
        /* Got to end of list without finding the device */
        return E_PRG_FLASH_DEVICE_UNAVAILABLE;
    }

    DBG_vPrintf(TRACE_BOOTLOADER, "Flash device manufacturer 0x%02X, device 0x%02X\n", au8Buffer[0], au8Buffer[1]);

    return eBL_CheckResponse(__FUNCTION__, eResponse, eRxType, E_BL_MSG_TYPE_FLASH_READ_ID_RESPONSE);
}


/****************************************************************************
 *
 * NAME: iBL_ReadFlashId
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
teStatus eBL_FlashSelectDevice(tsPRG_Context *psContext, uint8_t u8ManufacturerID, uint8_t u8DeviceID, uint8_t u8ChipSelect)
{
    int n;
    teBL_Response eResponse = 0;
    teBL_MessageType eRxType = 0;
    uint8_t au8Buffer[6];
    uint8_t u8FlashType = 0;
    teStatus eStatus;

    if ((u8ManufacturerID != 0) && (u8DeviceID != 0))
    {
        /* Search for flash type given flash device id code */
        for(n = 0; n < sizeof(asFlashDevices) / sizeof(tsBL_FlashDevice); n++)
        {
            /* If we found a match, send command to select this flash device type */
            if((asFlashDevices[n].u8ManufacturerID == u8ManufacturerID) && (asFlashDevices[n].u8DeviceID == u8DeviceID))
            {
                DBG_vPrintf(TRACE_BOOTLOADER, "Selecting \"%s\"\n", asFlashDevices[n].pcFlashName);
                u8FlashType = asFlashDevices[n].u8FlashType;
                break;
            }
        }

        if (n == sizeof(asFlashDevices) / sizeof(tsBL_FlashDevice))
        {
            /* Got to end of list without finding the device */
            return E_PRG_FLASH_DEVICE_UNAVAILABLE;
        }

        /* Assert the selected chip select */

        if (u8ChipSelect != 0)
        {
            au8Buffer[0] = u8ChipSelect;

            DBG_vPrintf(TRACE_BOOTLOADER, "Selecting chip select %d\n", u8ChipSelect);
            eResponse = eBL_Request(psContext, BL_TIMEOUT_1S, E_BL_MSG_TYPE_SET_CS_REQUEST, 1, au8Buffer, 0, NULL, &eRxType, NULL, NULL);
            if ((eStatus = eBL_CheckResponse(__FUNCTION__, eResponse, eRxType, E_BL_MSG_TYPE_SET_CS_RESPONSE)) != E_PRG_OK)
            {
                return eStatus;
            }
        }
    }

    DBG_vPrintf(TRACE_BOOTLOADER, "Select flash type %d\n", u8FlashType);
    au8Buffer[0] = u8FlashType;
    au8Buffer[1] = 0;
    au8Buffer[2] = 0;
    au8Buffer[3] = 0;
    au8Buffer[4] = 0;

    eResponse = eBL_Request(psContext, BL_TIMEOUT_1S, E_BL_MSG_TYPE_FLASH_SELECT_TYPE_REQUEST, 5, au8Buffer, 0, NULL, &eRxType, NULL, NULL);
    return eBL_CheckResponse(__FUNCTION__, eResponse, eRxType, E_BL_MSG_TYPE_FLASH_SELECT_TYPE_RESPONSE);
}


/****************************************************************************
 *
 * NAME: iBL_WriteStatusRegister
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
teStatus eBL_FlashStatusRegisterWrite(tsPRG_Context *psContext, uint8_t u8StatusReg)
{
    teBL_Response eResponse = 0;
    teBL_MessageType eRxType = 0;

    DBG_vPrintf(TRACE_BOOTLOADER, "Writing %02x to flash status register\n", u8StatusReg);

    eResponse = eBL_Request(psContext, BL_TIMEOUT_1S, E_BL_MSG_TYPE_FLASH_WRITE_PRG_REGISTER_REQUEST, 1, &u8StatusReg, 0, NULL, &eRxType, NULL, NULL);
    return eBL_CheckResponse(__FUNCTION__, eResponse, eRxType, E_BL_MSG_TYPE_FLASH_WRITE_PRG_REGISTER_RESPONSE);
}


/****************************************************************************
 *
 * NAME: iBL_RunRAM
 *
 * DESCRIPTION:
 *  Starts the module executing code from a given address
 *
 * PARAMETERS: Name        RW  Usage
 *
 * RETURNS:
 * int          0 if success
 *              -1 if an error occurred
 *
 ****************************************************************************/
teStatus eBL_MemoryExecute(tsPRG_Context *psContext, uint32_t u32Address)
{
    uint8_t au8CmdBuffer[4];
    teBL_Response eResponse = 0;
    teBL_MessageType eRxType = 0;

    DBG_vPrintf(TRACE_BOOTLOADER, "Execute code at 0x%08X\n", u32Address);

    au8CmdBuffer[0] = (uint8_t)(u32Address >> 0)  & 0xff;
    au8CmdBuffer[1] = (uint8_t)(u32Address >> 8)  & 0xff;
    au8CmdBuffer[2] = (uint8_t)(u32Address >> 16) & 0xff;
    au8CmdBuffer[3] = (uint8_t)(u32Address >> 24) & 0xff;

    eResponse = eBL_Request(psContext, BL_TIMEOUT_1S, E_BL_MSG_TYPE_RAM_RUN_REQUEST, 4, au8CmdBuffer, 0, NULL, &eRxType, NULL, NULL);
    return eBL_CheckResponse(__FUNCTION__, eResponse, eRxType, E_BL_MSG_TYPE_RAM_RUN_RESPONSE);
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
teStatus eBL_MemoryRead(tsPRG_Context *psContext, uint32_t u32Address, uint8_t u8Length, uint8_t u8BlockSize, uint8_t *pu8Buffer)
{
    uint8_t u8RxDataLen = 0;
    uint8_t au8CmdBuffer[6];
    teBL_Response eResponse = 0;
    teBL_MessageType eRxType = 0;
    int i;

    if(u8Length > 0xfc || pu8Buffer == NULL)
    {
        return E_PRG_BAD_PARAMETER;
    }

    au8CmdBuffer[0] = (uint8_t)(u32Address >> 0)  & 0xff;
    au8CmdBuffer[1] = (uint8_t)(u32Address >> 8)  & 0xff;
    au8CmdBuffer[2] = (uint8_t)(u32Address >> 16) & 0xff;
    au8CmdBuffer[3] = (uint8_t)(u32Address >> 24) & 0xff;
    au8CmdBuffer[4] = u8Length;
    au8CmdBuffer[5] = 0;

    eResponse = eBL_Request(psContext, BL_TIMEOUT_1S, E_BL_MSG_TYPE_RAM_READ_REQUEST, 6, au8CmdBuffer, 0, NULL, &eRxType, &u8RxDataLen, pu8Buffer);

    if (u8RxDataLen != u8Length)
    {
        DBG_vPrintf(TRACE_BOOTLOADER, "Requested %d bytes, got %d\n", u8Length, u8RxDataLen);
        return E_PRG_ERROR_READING;
    }

    /* Convert the read byte buffer into native format */
    if (u8BlockSize == sizeof(uint16_t))
    {
        for (i = 0; i < u8Length; i += sizeof(uint16_t))
        {
            uint16_t u16Short;
            memcpy(&u16Short, &pu8Buffer[i], sizeof(uint16_t));

            if (psContext->sChipDetails.eEndianness == E_CHIP_LITTLE_ENDIAN)
            {
                u16Short = le16toh(u16Short);
            }
            else if (psContext->sChipDetails.eEndianness == E_CHIP_BIG_ENDIAN)
            {
                u16Short = be16toh(u16Short);
            }
            else
            {
                return E_PRG_BAD_PARAMETER;
            }
            memcpy(&pu8Buffer[i], &u16Short, sizeof(uint16_t));
        }
    }
    else if (u8BlockSize == sizeof(uint32_t))
    {
        for (i = 0; i < u8Length; i += sizeof(uint32_t))
        {
            uint32_t u32Word;
            memcpy(&u32Word, &pu8Buffer[i], sizeof(uint32_t));

            if (psContext->sChipDetails.eEndianness == E_CHIP_LITTLE_ENDIAN)
            {
                u32Word = le32toh(u32Word);
            }
            else if (psContext->sChipDetails.eEndianness == E_CHIP_BIG_ENDIAN)
            {
                u32Word = be32toh(u32Word);
            }
            else
            {
                return E_PRG_BAD_PARAMETER;
            }
            memcpy(&pu8Buffer[i], &u32Word, sizeof(uint32_t));
        }
    }

    return eBL_CheckResponse(__FUNCTION__, eResponse, eRxType, E_BL_MSG_TYPE_RAM_READ_RESPONSE);
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
teStatus eBL_MemoryWrite(tsPRG_Context *psContext, uint32_t u32Address, uint8_t u8Length, uint8_t u8BlockSize, uint8_t *pu8Buffer)
{
    uint8_t au8CmdBuffer[6];
    teBL_Response eResponse = 0;
    teBL_MessageType eRxType = 0;
    int i;

    if(u8Length > 0xfc || pu8Buffer == NULL)
    {
        return E_PRG_BAD_PARAMETER;
    }

    au8CmdBuffer[0] = (uint8_t)(u32Address >> 0)  & 0xff;
    au8CmdBuffer[1] = (uint8_t)(u32Address >> 8)  & 0xff;
    au8CmdBuffer[2] = (uint8_t)(u32Address >> 16) & 0xff;
    au8CmdBuffer[3] = (uint8_t)(u32Address >> 24) & 0xff;
    au8CmdBuffer[4] = u8Length;
    au8CmdBuffer[5] = 0;

    /* Convert the native byte buffer into chip format */
    if (u8BlockSize == sizeof(uint16_t))
    {
        for (i = 0; i < u8Length; i += sizeof(uint16_t))
        {
            uint16_t u16Short;
            memcpy(&u16Short, &pu8Buffer[i], sizeof(uint16_t));

            if (psContext->sChipDetails.eEndianness == E_CHIP_LITTLE_ENDIAN)
            {
                u16Short = htole16(u16Short);
            }
            else if (psContext->sChipDetails.eEndianness == E_CHIP_BIG_ENDIAN)
            {
                u16Short = htobe16(u16Short);
            }
            else
            {
                return E_PRG_BAD_PARAMETER;
            }
            memcpy(&pu8Buffer[i], &u16Short, sizeof(uint16_t));
        }
    }
    else if (u8BlockSize == sizeof(uint32_t))
    {
        for (i = 0; i < u8Length; i += sizeof(uint32_t))
        {
            uint32_t u32Word;
            memcpy(&u32Word, &pu8Buffer[i], sizeof(uint32_t));

            if (psContext->sChipDetails.eEndianness == E_CHIP_LITTLE_ENDIAN)
            {
                u32Word = htole32(u32Word);
            }
            else if (psContext->sChipDetails.eEndianness == E_CHIP_BIG_ENDIAN)
            {
                u32Word = htobe32(u32Word);
            }
            else
            {
                return E_PRG_BAD_PARAMETER;
            }
            memcpy(&pu8Buffer[i], &u32Word, sizeof(uint32_t));
        }
    }

    eResponse = eBL_Request(psContext, BL_TIMEOUT_250MS, E_BL_MSG_TYPE_RAM_WRITE_REQUEST, 4, au8CmdBuffer, u8Length, pu8Buffer, &eRxType, NULL, NULL);
    return eBL_CheckResponse(__FUNCTION__, eResponse, eRxType, E_BL_MSG_TYPE_RAM_WRITE_RESPONSE);
}


/****************************************************************************
 *
 * NAME: iBL_EraseFlash
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
teStatus eBL_FlashErase(tsPRG_Context *psContext)
{
    teBL_Response eResponse = 0;
    teBL_MessageType eRxType = 0;

    eResponse = eBL_Request(psContext, BL_TIMEOUT_10S, E_BL_MSG_TYPE_FLASH_ERASE_REQUEST, 0, NULL, 0, NULL, &eRxType, NULL, NULL);
    return eBL_CheckResponse(__FUNCTION__, eResponse, eRxType, E_BL_MSG_TYPE_FLASH_ERASE_RESPONSE);
}


/****************************************************************************
 *
 * NAME: iBL_ReadFlash
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
teStatus eBL_FlashRead(tsPRG_Context *psContext, uint32_t u32Address, uint8_t u8Length, uint8_t *pu8Buffer)
{
    uint8_t u8RxDataLen = 0;
    uint8_t au8CmdBuffer[6];
    teBL_Response eResponse = 0;
    teBL_MessageType eRxType = 0;

    if(u8Length > 0xfc || pu8Buffer == NULL)
    {
        DBG_vPrintf(TRACE_BOOTLOADER, "Parameter error\n");
        return E_PRG_BAD_PARAMETER;
    }

    au8CmdBuffer[0] = (uint8_t)(u32Address >> 0)  & 0xff;
    au8CmdBuffer[1] = (uint8_t)(u32Address >> 8)  & 0xff;
    au8CmdBuffer[2] = (uint8_t)(u32Address >> 16) & 0xff;
    au8CmdBuffer[3] = (uint8_t)(u32Address >> 24) & 0xff;
    au8CmdBuffer[4] = u8Length;
    au8CmdBuffer[5] = 0;

    eResponse = eBL_Request(psContext, BL_TIMEOUT_1S, E_BL_MSG_TYPE_FLASH_READ_REQUEST, 6, au8CmdBuffer, 0, NULL, &eRxType, &u8RxDataLen, pu8Buffer);

    if (u8RxDataLen != u8Length)
    {
        DBG_vPrintf(TRACE_BOOTLOADER, "Requested %d bytes, got %d\n", u8Length, u8RxDataLen);
        return E_PRG_ERROR_READING;
    }

    return eBL_CheckResponse(__FUNCTION__, eResponse, eRxType, E_BL_MSG_TYPE_FLASH_READ_RESPONSE);
}


/****************************************************************************
 *
 * NAME: iBL_WriteFlash
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
teStatus eBL_FlashWrite(tsPRG_Context *psContext, uint32_t u32Address, uint8_t u8Length, uint8_t *pu8Buffer)
{
    uint8_t au8CmdBuffer[4];
    teBL_Response eResponse = 0;
    teBL_MessageType eRxType = 0;

    if(u8Length > 0xfc || pu8Buffer == NULL)
    {
        return E_PRG_BAD_PARAMETER;
    }

    au8CmdBuffer[0] = (uint8_t)(u32Address >> 0)  & 0xff;
    au8CmdBuffer[1] = (uint8_t)(u32Address >> 8)  & 0xff;
    au8CmdBuffer[2] = (uint8_t)(u32Address >> 16) & 0xff;
    au8CmdBuffer[3] = (uint8_t)(u32Address >> 24) & 0xff;

    eResponse = eBL_Request(psContext, BL_TIMEOUT_1S, E_BL_MSG_TYPE_FLASH_PROGRAM_REQUEST, 4, au8CmdBuffer, u8Length, pu8Buffer, &eRxType, NULL, NULL);
    return eBL_CheckResponse(__FUNCTION__, eResponse, eRxType, E_BL_MSG_TYPE_FLASH_PROGRAM_RESPONSE);
}


teStatus eBL_EEPROMErase(tsPRG_Context *psContext, int iEraseAll)
{
    uint8_t au8CmdBuffer[1];
    teBL_Response eResponse = 0;
    teBL_MessageType eRxType = 0;

    au8CmdBuffer[0] = (uint8_t)(iEraseAll) & 0xff;

    eResponse = eBL_Request(psContext, BL_TIMEOUT_1S, E_BL_MSG_TYPE_PDM_ERASE_REQUEST, 1, au8CmdBuffer, 0, NULL, &eRxType, NULL, NULL);
    return eBL_CheckResponse(__FUNCTION__, eResponse, eRxType, E_BL_MSG_TYPE_PDM_ERASE_RESPONSE);
}


teStatus eBL_EEPROMRead(tsPRG_Context *psContext, uint32_t u32Address, uint8_t u8Length, uint8_t *pu8Buffer)
{
    uint8_t u8RxDataLen = 0;
    uint8_t au8CmdBuffer[6];
    teBL_Response eResponse = 0;
    teBL_MessageType eRxType = 0;

    if(u8Length > 0xfc || pu8Buffer == NULL)
    {
        DBG_vPrintf(TRACE_BOOTLOADER, "Parameter error\n");
        return E_PRG_BAD_PARAMETER;
    }

    au8CmdBuffer[0] = (uint8_t)(u32Address >> 0)  & 0xff;
    au8CmdBuffer[1] = (uint8_t)(u32Address >> 8)  & 0xff;
    au8CmdBuffer[2] = (uint8_t)(u32Address >> 16) & 0xff;
    au8CmdBuffer[3] = (uint8_t)(u32Address >> 24) & 0xff;
    au8CmdBuffer[4] = u8Length;
    au8CmdBuffer[5] = 0;

    eResponse = eBL_Request(psContext, BL_TIMEOUT_1S, E_BL_MSG_TYPE_EEPROM_READ_REQUEST, 6, au8CmdBuffer, 0, NULL, &eRxType, &u8RxDataLen, pu8Buffer);

    if (u8RxDataLen != u8Length)
    {
        DBG_vPrintf(TRACE_BOOTLOADER, "Requested %d bytes, got %d\n", u8Length, u8RxDataLen);
        return E_PRG_ERROR_READING;
    }

    return eBL_CheckResponse(__FUNCTION__, eResponse, eRxType, E_BL_MSG_TYPE_EEPROM_READ_RESPONSE);
}


teStatus eBL_EEPROMWrite(tsPRG_Context *psContext, uint32_t u32Address, uint8_t u8Length, uint8_t *pu8Buffer)
{
    uint8_t au8CmdBuffer[4];
    teBL_Response eResponse = 0;
    teBL_MessageType eRxType = 0;

    if(u8Length > 0xfc || pu8Buffer == NULL)
    {
        DBG_vPrintf(TRACE_BOOTLOADER, "Parameter error\n");
        return E_PRG_BAD_PARAMETER;
    }

    au8CmdBuffer[0] = (uint8_t)(u32Address >> 0)  & 0xff;
    au8CmdBuffer[1] = (uint8_t)(u32Address >> 8)  & 0xff;
    au8CmdBuffer[2] = (uint8_t)(u32Address >> 16) & 0xff;
    au8CmdBuffer[3] = (uint8_t)(u32Address >> 24) & 0xff;

    eResponse = eBL_Request(psContext, BL_TIMEOUT_1S, E_BL_MSG_TYPE_EEPROM_WRITE_REQUEST, 4, au8CmdBuffer, u8Length, pu8Buffer, &eRxType, NULL, NULL);
    return eBL_CheckResponse(__FUNCTION__, eResponse, eRxType, E_BL_MSG_TYPE_EEPROM_WRITE_RESPONSE);
}


teStatus eBL_IndexSectorWrite(tsPRG_Context *psContext, uint8_t u8Page, uint8_t u8WordNumber, uint32_t au32Data[4])
{
    uint8_t au8CmdBuffer[18];
    teBL_Response eResponse = 0;
    teBL_MessageType eRxType = 0;
    int i;

    DBG_vPrintf(TRACE_BOOTLOADER, "Program index sector page %d, word %d, data: 0x%08X%08X%08X%08X\n",
                u8Page, u8WordNumber, au32Data[0], au32Data[1], au32Data[2], au32Data[3]);

    au8CmdBuffer[0] = u8Page;
    au8CmdBuffer[1] = u8WordNumber;

    DBG_vPrintf(TRACE_BOOTLOADER, "Device buffer:");
    for (i = 0; i < 4; i++)
    {
        uint32_t u32Word = au32Data[i];

        if (psContext->sChipDetails.eEndianness == E_CHIP_LITTLE_ENDIAN)
        {
            u32Word = htole32(u32Word);
        }
        else if (psContext->sChipDetails.eEndianness == E_CHIP_BIG_ENDIAN)
        {
            u32Word = htobe32(u32Word);
        }
        else
        {
            return E_PRG_BAD_PARAMETER;
        }
        memcpy(&au8CmdBuffer[2 + (i * sizeof(uint32_t))], &u32Word, sizeof(uint32_t));
        DBG_vPrintf(TRACE_BOOTLOADER, "%02X%02X%02X%02X",
                    au8CmdBuffer[2 + (i * sizeof(uint32_t))], au8CmdBuffer[3 + (i * sizeof(uint32_t))],
                    au8CmdBuffer[4 + (i * sizeof(uint32_t))], au8CmdBuffer[5 + (i * sizeof(uint32_t))]);
    }
    DBG_vPrintf(TRACE_BOOTLOADER, "\n");

    //return E_BL_RESPONSE_OK;
    eResponse = eBL_Request(psContext, BL_TIMEOUT_1S, E_BL_MSG_TYPE_PROGRAM_INDEX_SECTOR_REQUEST, 18, au8CmdBuffer, 0, NULL, &eRxType, NULL, NULL);
    return eBL_CheckResponse(__FUNCTION__, eResponse, eRxType, E_BL_MSG_TYPE_PROGRAM_INDEX_SECTOR_RESPONSE);
}

teStatus eBL_Reset(tsPRG_Context *psContext)
{
    teBL_Response eResponse = 0;
    teBL_MessageType eRxType = 0;

    eResponse = eBL_Request(psContext, BL_TIMEOUT_1S, E_BL_MSG_TYPE_RESET_REQUEST, 0, NULL, 0, NULL, &eRxType, NULL, NULL);
    return eBL_CheckResponse(__FUNCTION__, eResponse, eRxType, E_BL_MSG_TYPE_RESET_RESPONSE);
}

/****************************************************************************/
/***        Local Functions                                               ***/
/****************************************************************************/

/****************************************************************************
 *
 * NAME: eBL_Request
 *
 * DESCRIPTION:
 *
 * PARAMETERS: Name        RW  Usage
 *
 * RETURNS:
 * void
 ****************************************************************************/
static teBL_Response eBL_Request(tsPRG_Context *psContext, int iTimeoutMicroseconds, teBL_MessageType eTxType, uint8_t u8HeaderLen, uint8_t *pu8Header, uint8_t u8TxLength, uint8_t *pu8TxData,
                          teBL_MessageType *peRxType, uint8_t *pu8RxLength, uint8_t *pu8RxData)
{
    /* Check data is not too long */
    if(u8TxLength > 0xfd)
    {
        DBG_vPrintf(TRACE_BOOTLOADER, "Data too long\n");
        return E_BL_RESPONSE_ERROR;
    }

    /* Send message */
    if(eBL_WriteMessage(psContext, eTxType, u8HeaderLen, pu8Header, u8TxLength, pu8TxData) != E_PRG_OK)
    {
        return E_BL_RESPONSE_ERROR;
    }

    /* Get the response to the request */
    return eBL_ReadMessage(psContext, iTimeoutMicroseconds, peRxType, pu8RxLength, pu8RxData);
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
static teStatus eBL_WriteMessage(tsPRG_Context *psContext, teBL_MessageType eType, uint8_t u8HeaderLength, uint8_t *pu8Header, uint8_t u8Length, uint8_t *pu8Data)
{
    uint8_t u8CheckSum = 0;
    int n;

    uint8_t au8Msg[256];

    /* total message length cannot be > 255 bytes */
    if(u8HeaderLength + u8Length >= 0xfe)
    {
        DBG_vPrintf(TRACE_BOOTLOADER, "Length too big\n");
        return E_PRG_BAD_PARAMETER;
    }

    /* Message length */
    au8Msg[0] = u8HeaderLength + u8Length + 2;

    /* Message type */
    au8Msg[1] = (uint8_t)eType;

    /* Message header */
    memcpy(&au8Msg[2], pu8Header, u8HeaderLength);

    /* Message payload */
    memcpy(&au8Msg[2 + u8HeaderLength], pu8Data, u8Length);

    DBG_vPrintf(TRACE_BOOTLOADER, "Tx: ");
    for(n = 0; n < u8HeaderLength + u8Length + 2; n++)
    {
        DBG_vPrintf(TRACE_BOOTLOADER, "%02x ", au8Msg[n]);
        u8CheckSum ^= au8Msg[n];
    }
    DBG_vPrintf(TRACE_BOOTLOADER, "%02x\n", u8CheckSum);

    /* Message checksum */
    au8Msg[u8HeaderLength + u8Length + 2] = u8CheckSum;

    /* Write whole message to UART */
    return eUART_Write(psContext, au8Msg, u8HeaderLength + u8Length + 3);
}

/****************************************************************************
 *
 * NAME: eBL_Flush
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
teStatus eBL_Flush(tsPRG_Context *psContext)
{
	int i;
	uint8_t u8Byte = 0;
    int iBytesRead = 0;

    uint8_t au8Msg[2] = {0x00, 0x02};

    /* Keep pushing a sequence of 0x00 0x02 until the bootloader responds.
     * This sequence is chosen as wherever the bootloader starts it will see
     * one of two byte sequences:
     * 0x00           - will be ignored by the bootloader
     * 0x02 0x00 0x02 - will be responded to as a valid packet
     */
    for (i = 0; (i < 128) && (iBytesRead == 0); i++)
	{
        eUART_Write(psContext, au8Msg, sizeof(au8Msg));
		eUART_Read(psContext, 25000, 1, &u8Byte, &iBytesRead);
	}

    DBG_vPrintf(TRACE_BOOTLOADER, "Received %d bytes after sending %d\n", iBytesRead, i * sizeof(au8Msg));

    /* Keep reading bytes until there are no more */
    for (i = 0; (i < 128) && (iBytesRead != 0); i++)
    {
        eUART_Read(psContext, 25000, 1, &u8Byte, &iBytesRead);
    }

    return E_PRG_OK;
}


/****************************************************************************
 *
 * NAME: eBL_ReadMessage
 *
 * DESCRIPTION:
 *
 * PARAMETERS: Name        RW  Usage
 *
 * RETURNS:
 * void
 ****************************************************************************/
static teBL_Response eBL_ReadMessage(tsPRG_Context *psContext, int iTimeoutMicroseconds, teBL_MessageType *peType, uint8_t *pu8Length, uint8_t *pu8Data)
{

    int n;
    teStatus eStatus;
    uint8_t au8Msg[BOOTLOADER_MAX_MESSAGE_LENGTH];
    uint8_t u8CalculatedCheckSum = 0;
    uint8_t u8Length = 0;
    teBL_Response eResponse = E_BL_RESPONSE_OK;
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

    /* Get the length byte */
    eStatus = eBL_ReadData(psContext, &sTimeout, au8Msg, 1, &iTotalBytesRead);

    if((eStatus != E_PRG_OK) || (iTotalBytesRead != 1) )
    {
        DBG_vPrintf(TRACE_BOOTLOADER, "Error getting length\n");
    }
    else
    {
        /* Get the length */
        u8Length = au8Msg[0] + 1;

        DBG_vPrintf(TRACE_BOOTLOADER, "Incoming message length %d\n", u8Length);

        /* Try and receive the rest of the message */
        if (u8Length < BOOTLOADER_MAX_MESSAGE_LENGTH)
        {
            eStatus = eBL_ReadData(psContext, &sTimeout, au8Msg, u8Length, &iTotalBytesRead);
        }
    }

    /* Print out and checksum everything received */
    DBG_vPrintf(TRACE_BOOTLOADER, "Rx: ");
    for(n = 0; n < iTotalBytesRead; n++)
    {
        DBG_vPrintf(TRACE_BOOTLOADER, "%02x ", au8Msg[n]);
        u8CalculatedCheckSum ^= au8Msg[n];
    }
    DBG_vPrintf(TRACE_BOOTLOADER, "\n");

    if((eStatus != E_PRG_OK) || (iTotalBytesRead != u8Length) || (u8Length < 4))
    {
        DBG_vPrintf(TRACE_BOOTLOADER, "Got %d / %d bytes (Status %d)\n", iTotalBytesRead, u8Length, eStatus);
        return E_BL_RESPONSE_NO_RESPONSE;
    }

    if(u8CalculatedCheckSum != 0x00)
    {
        DBG_vPrintf(TRACE_BOOTLOADER, "Checksum bad, got %02x expected %02x\n", u8CalculatedCheckSum, 0);
        return E_BL_RESPONSE_CRC_ERROR;
    }
    
    *peType = au8Msg[1];
    eResponse = au8Msg[2];
    if (pu8Length)
    {
        *pu8Length = u8Length - 4;

        if (pu8Data)
        {
            memcpy(pu8Data, &au8Msg[3], *pu8Length);
        }
    }

    DBG_vPrintf(TRACE_BOOTLOADER, "Got response 0x%02x\n", eResponse);

    return eResponse;
}


static teStatus eBL_CheckResponse(const char *pcFunction, teBL_Response eResponse, teBL_MessageType eRxType, teBL_MessageType eExpectedRxType)
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

