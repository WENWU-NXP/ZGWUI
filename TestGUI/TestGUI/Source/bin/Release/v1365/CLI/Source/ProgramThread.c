/****************************************************************************
 *
 * MODULE:             ProgramThread.c
 *
 * COMPONENT:          $RCSfile: Firmware.c,v $
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

/****************************************************************************/
/***        Include files                                                 ***/
/****************************************************************************/

#include <stdint.h>
#include <string.h>
#include <stdlib.h>

#include "ProgramThread.h"
#include "programmer.h"

/****************************************************************************/
/***        Macro Definitions                                             ***/
/****************************************************************************/

/****************************************************************************/
/***        Type Definitions                                              ***/
/****************************************************************************/


/****************************************************************************/
/***        Local Function Prototypes                                     ***/
/****************************************************************************/

/****************************************************************************/
/***        Exported Variables                                            ***/
/****************************************************************************/

/****************************************************************************/
/***        Local Variables                                               ***/
/****************************************************************************/

/****************************************************************************/
/***        Exported Functions                                            ***/
/****************************************************************************/


void *pvProgramThread(tsUtilsThread *psThreadInfo)
{
    tsPRG_Context   sContext;
    tsProgramThreadArgs *psArgs = (tsProgramThreadArgs*)psThreadInfo->pvThreadData;
    
    if ((psArgs->eStatus = ePRG_Init(&sContext)) != E_PRG_OK)
    {
        fprintf(stderr, "Error initialising context\n");
        return (void*)-1;
    }
    
    switch (psArgs->sConnection.eType)
    {
        case (E_CONNECT_SERIAL):
            psArgs->sConnection.uDetails.sSerial.u32BaudRate = psArgs->iInitialSpeed;
            break;
            
        default:
            break;
    }
    
    /* Set the offsets for flash/eeprom operations */
    sContext.u32FlashOffset     = psArgs->u32FlashOffset;
    sContext.u32EepromOffset    = psArgs->u32EepromOffset;
    
    if ((psArgs->eStatus = ePRG_ConnectionOpen(&sContext, &psArgs->sConnection)) != E_PRG_OK)
    {
        return (void*)-1;
    }

    /* Read module details at initial baud rate */
    if ((psArgs->eStatus = ePRG_ChipGetDetails(&sContext)) != E_PRG_OK)
    {
        return (void*)-1;
    }
    
    if (psArgs->iInitialSpeed != psArgs->iProgramSpeed)
    {
        if (psArgs->iVerbosity > 2)
        {
        }
        
        psArgs->sConnection.uDetails.sSerial.u32BaudRate = psArgs->iProgramSpeed;

        if ((psArgs->eStatus = ePRG_ConnectionUpdate(&sContext, &psArgs->sConnection)) != E_PRG_OK)
        {
            return (void*)-1;
        }
    }
    
    if (psArgs->u32FlashIndex)
    {
        /* Select non-default flash index */
        if ((psArgs->eStatus = ePRG_FlashSelectDevice(&sContext, psArgs->u32FlashIndex)) != E_PRG_OK)
        {
            goto done;
        }
    }
    
    if (psArgs->pcFirmwareFile)
    {
        /* Have file to program */
        if ((psArgs->eStatus = ePRG_FwOpen(&sContext, (char *)psArgs->pcFirmwareFile)) != E_PRG_OK)
        {
            goto done;
        }
        
		if ((psArgs->eStatus = ePRG_FlashProgram(&sContext, NULL, NULL, psArgs)) != E_PRG_OK)
        {
            goto done;
        }
        
        if (psArgs->iVerify)
        {
			if ((psArgs->eStatus = ePRG_FlashVerify(&sContext, NULL, psArgs)) != E_PRG_OK)
            {
                goto done;
            }
        }
    }

    if (psArgs->eEepromErase != E_ERASE_EEPROM_NONE)
    {
		if ((psArgs->eStatus = ePRG_EepromErase(&sContext, psArgs->eEepromErase, NULL, psArgs)) != E_PRG_OK)
        {
            goto done;
        }
    }

done:
    if (psArgs->iInitialSpeed != psArgs->iProgramSpeed)
    {
        if (psArgs->iVerbosity > 2)
        {
        }

        psArgs->sConnection.uDetails.sSerial.u32BaudRate = psArgs->iInitialSpeed;

        if (ePRG_ConnectionUpdate(&sContext, &psArgs->sConnection) != E_PRG_OK)
        {
            return (void*)-1;
        }
    }
    
done_error:
    if (ePRG_ConnectionClose(&sContext) != E_PRG_OK)
    {
        return (void*)-1;
    }
    
    if (ePRG_Destroy(&sContext) != E_PRG_OK)
    {
        return (void*)-1;
    }
    
    return (void*)0;
}


/****************************************************************************/
/***        Local Functions                                               ***/
/****************************************************************************/

/****************************************************************************/
/***        END OF FILE                                                   ***/
/****************************************************************************/



