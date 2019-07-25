/****************************************************************************
 *
 * MODULE:             JN51xx Programmer
 *
 * COMPONENT:          Private library data
 *
 * VERSION:            $Name:  $
 *
 * REVISION:           $Revision: 1.2 $
 *
 * DATED:              $Date: 2009/03/02 13:33:44 $
 *
 * STATUS:             $State: Exp $
 *
 * AUTHOR:             Matt Redfearn
 *
 * DESCRIPTION:
 *
 *
 * LAST MODIFIED BY:   $Author: lmitch $
 *                     $Modtime: $
 *
 ****************************************************************************
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

 * Copyright NXP B.V. 2012. All rights reserved
 *
 ***************************************************************************/

#ifndef  PROGRAMMER_PRIVATE_H_INCLUDED
#define  PROGRAMMER_PRIVATE_H_INCLUDED

#if defined __cplusplus
extern "C" {
#endif

/****************************************************************************/
/***        Include Files                                                 ***/
/****************************************************************************/

#include "protocol.h"

/****************************************************************************/
/***        Macro Definitions                                             ***/
/****************************************************************************/

#define PRG_MAX_STATUS_LENGTH 1024

/** Inline function to convert index sector page / word into memory mapped address */
static inline uint32_t u32PRG_JN516x_index_sector_address(uint8_t u8Page, uint8_t u8Word)
{
    uint32_t u32Address = 0x01001000 + ((uint32_t)u8Page << 8) + ((uint32_t)u8Word << 4);
    return u32Address;
}

/** Index sector word length in bytes */
#define IP2111_INDEX_SECTOR_WORD_LENGTH                 16

/* IP2111 configuration index sector word */
#define JN516X_INDEX_SECTOR_IP2111_CONFIG_PAGE          4
#define JN516X_INDEX_SECTOR_IP2111_CONFIG_WORD          0
#define JN516X_INDEX_SECTOR_IP2111_CONFIG_ADDRESS       u32PRG_JN516x_index_sector_address(JN516X_INDEX_SECTOR_IP2111_CONFIG_PAGE, JN516X_INDEX_SECTOR_IP2111_CONFIG_WORD)


/* ATE device settings flash index sector word */
#define JN516X_INDEX_SECTOR_ATE_SETTINGS_PAGE           5
#define JN516X_INDEX_SECTOR_ATE_SETTINGS_WORD           0
#define JN516X_INDEX_SECTOR_ATE_SETTINGS_ADDRESS        u32PRG_JN516x_index_sector_address(JN516X_INDEX_SECTOR_ATE_SETTINGS_PAGE, JN516X_INDEX_SECTOR_ATE_SETTINGS_WORD)

/* Customer configuration flash index sector word */
#define JN516X_INDEX_SECTOR_CUSTOMER_CONFIG_PAGE        5
#define JN516X_INDEX_SECTOR_CUSTOMER_CONFIG_WORD        1
#define JN516X_INDEX_SECTOR_CUSTOMER_CONFIG_ADDRESS     u32PRG_JN516x_index_sector_address(JN516X_INDEX_SECTOR_CUSTOMER_CONFIG_PAGE, JN516X_INDEX_SECTOR_CUSTOMER_CONFIG_WORD)

/* Customer user data 0 flash index sector word */
#define JN516X_INDEX_SECTOR_USER0_PAGE                  5
#define JN516X_INDEX_SECTOR_USER0_WORD                  4
#define JN516X_INDEX_SECTOR_USER0_ADDRESS               u32PRG_JN516x_index_sector_address(JN516X_INDEX_SECTOR_USER0_PAGE, JN516X_INDEX_SECTOR_USER0_WORD)

/* Customer user data 1 flash index sector word */
#define JN516X_INDEX_SECTOR_USER1_PAGE                  5
#define JN516X_INDEX_SECTOR_USER1_WORD                  5
#define JN516X_INDEX_SECTOR_USER1_ADDRESS               u32PRG_JN516x_index_sector_address(JN516X_INDEX_SECTOR_USER1_PAGE, JN516X_INDEX_SECTOR_USER1_WORD)

/* Customer user data 2 flash index sector word */
#define JN516X_INDEX_SECTOR_USER2_PAGE                  5
#define JN516X_INDEX_SECTOR_USER2_WORD                  6
#define JN516X_INDEX_SECTOR_USER2_ADDRESS               u32PRG_JN516x_index_sector_address(JN516X_INDEX_SECTOR_USER2_PAGE, JN516X_INDEX_SECTOR_USER2_WORD)

/* Cutomer MAC address flash index sector word */
#define JN516X_INDEX_SECTOR_MAC_CUSTOMER_PAGE           5
#define JN516X_INDEX_SECTOR_MAC_CUSTOMER_WORD           7
#define JN516X_INDEX_SECTOR_MAC_CUSTOMER_ADDRESS        u32PRG_JN516x_index_sector_address(JN516X_INDEX_SECTOR_MAC_CUSTOMER_PAGE, JN516X_INDEX_SECTOR_MAC_CUSTOMER_WORD)

/* Factory MAC address flash index sector word */
#define JN516X_INDEX_SECTOR_MAC_FACTORY_PAGE            5
#define JN516X_INDEX_SECTOR_MAC_FACTORY_WORD            8
#define JN516X_INDEX_SECTOR_MAC_FACTORY_ADDRESS         u32PRG_JN516x_index_sector_address(JN516X_INDEX_SECTOR_MAC_FACTORY_PAGE, JN516X_INDEX_SECTOR_MAC_FACTORY_WORD)

/* AES Key flash index sector word */
#define JN516X_INDEX_SECTOR_AES_KEY_PAGE                5
#define JN516X_INDEX_SECTOR_AES_KEY_WORD                12
#define JN516X_INDEX_SECTOR_AES_KEY_ADDRESS             u32PRG_JN516x_index_sector_address(JN516X_INDEX_SECTOR_AES_KEY_PAGE, JN516X_INDEX_SECTOR_AES_KEY_WORD)


/****************************************************************************/
/***        Type Definitions                                              ***/
/****************************************************************************/

typedef struct
{
    tsConnection    sConnection;
    tsProtocol*     psProtocol;
    uint32_t        u32SelectedMemory;
    uint32_t        u32MemoryHandle;
    char            acStatusMessage[PRG_MAX_STATUS_LENGTH];
    char            acErrorMsgBuffer[PRG_MAX_STATUS_LENGTH];
    void           *pvFwPrivate;
    
    uint8_t        *pu8FlashProgrammerExtensionStart;
    uint32_t        u32FlashProgrammerExtensionLength;

    uint32_t        u32BootloaderEntry;
} tsPRG_PrivateContext;

/****************************************************************************/
/***        Exported Functions                                            ***/
/****************************************************************************/

teStatus ePRG_SetStatus(tsPRG_Context *psContext, teStatus eStatus, const char *pcAdditionalInfoFmt, ...);
void vPRG_WaitMs(uint32_t u32TimeoutMs);


char *pcPRG_GetLastErrorMessage(tsPRG_Context *psContext);


tsMemInfo *psGetMemByName(tsPRG_Context *psContext, const char *pcMemoryName);
tsMemInfo *psGetMemoryInfo(tsPRG_Context *psContext, uint32_t u32MemoryIndex);

/****************************************************************************/
/***        Exported Variables                                            ***/
/****************************************************************************/

#if defined __cplusplus
}
#endif

#endif  /* PROGRAMMER_PRIVATE_H_INCLUDED */

/****************************************************************************/
/***        END OF FILE                                                   ***/
/****************************************************************************/
