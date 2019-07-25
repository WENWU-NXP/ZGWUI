/****************************************************************************
 *
 * MODULE:             JN51xx_BootLoader
 *
 * COMPONENT:          $RCSfile: JN51xx_BootLoader.h,v $
 *
 * VERSION:            $Name:  $
 *
 * REVISION:           $Revision: 1.1 $
 *
 * DATED:              $Date: 2008/10/17 10:22:11 $
 *
 * STATUS:             $State: Exp $
 *
 * AUTHOR:             Lee Mitchell
 *
 * DESCRIPTION:
 *
 * LAST MODIFIED BY:   $Author: lmitch $
 *                     $Modtime: $
 *
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

#ifndef  PROTOCOL_JN51XX_V2_H_INCLUDED
#define  PROTOCOL_JN51XX_V2_H_INCLUDED

#if defined __cplusplus
extern "C" {
#endif

/****************************************************************************/
/***        Include files                                                 ***/
/****************************************************************************/

#include <stdint.h>

#include <programmer.h>
#include "protocol.h"

/****************************************************************************/
/***        Macro Definitions                                             ***/
/****************************************************************************/

#define FLASH_MANUFACTURER_JN516X   0xCC
#define FLASH_DEVICE_JN516X         0xEE


/****************************************************************************/
/***        Type Definitions                                              ***/
/****************************************************************************/

/****************************************************************************/
/***        Local Function Prototypes                                     ***/
/****************************************************************************/

/****************************************************************************/
/***        Exported Variables                                            ***/
/****************************************************************************/

extern tsProtocol sProtocol_v2;

/****************************************************************************/
/***        Local Variables                                               ***/
/****************************************************************************/

/****************************************************************************/
/***        Exported Functions                                            ***/
/****************************************************************************/

teStatus eBL2_SetBaudrate(tsPRG_Context *psContext, uint32_t u32Baudrate);

teStatus eBL2_ChipIdRead(tsPRG_Context *psContext, uint32_t *pu32ChipId, uint32_t *pu32BootloaderVersion);


teStatus eBL2_Unlock(tsPRG_Context *psContext, uint8_t u8Mode, const uint8_t *pu8Key, uint16_t u16KeyLen);


teStatus eBL2_MemInfo(tsPRG_Context *psContext, uint8_t u8Index, tsMemInfo *psMemInfo);
teStatus eBL2_MemOpen(tsPRG_Context *psContext, uint8_t u8Index, uint8_t u8AccessMode, uint32_t *pu32Handle);
teStatus eBL2_MemErase(tsPRG_Context *psContext, uint8_t u8Index, uint32_t u32Address, uint32_t u32Length);
teStatus eBL2_MemBlank(tsPRG_Context *psContext, uint8_t u8Index, uint32_t u32Address, uint32_t u32Length);
teStatus eBL2_MemRead(tsPRG_Context *psContext, uint8_t u8Index, uint32_t u32Address, uint32_t *pu32Length, uint8_t *pu8Buffer);
teStatus eBL2_MemWrite(tsPRG_Context *psContext, uint8_t u8Index, uint32_t u32Address, uint32_t u32Length, uint8_t *pu8Buffer);
teStatus eBL2_MemClose(tsPRG_Context *psContext, uint8_t u8Index);


teStatus eBL2_Reset(tsPRG_Context *psContext);

/****************************************************************************/
/***        Local Functions                                               ***/
/****************************************************************************/

#if defined __cplusplus
}
#endif

#endif  /* PROTOCOL_JN51XX_V1_H_INCLUDED */

/****************************************************************************/
/***        END OF FILE                                                   ***/
/****************************************************************************/

