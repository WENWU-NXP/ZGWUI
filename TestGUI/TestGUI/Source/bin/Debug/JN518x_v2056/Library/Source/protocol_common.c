/****************************************************************************
 *
 * MODULE:             JN51xx_BootLoader
 *
 * COMPONENT:          $RCSfile: JN51xx_BootLoader.c,v $
 *
 * VERSION:            $Name:  $
 *
 * REVISION:           $Revision: 1.2 $
 *
 * DATED:              $Date: 2009/03/02 13:33:44 $
 *
 * STATUS:             $State: Exp $
 *
 * AUTHOR:             Lee Mitchell
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

teStatus eBL_ReadData(tsPRG_Context *psContext, struct timeval* psTimeout, uint8_t *au8Msg, int iBytesToRead, int *iTotalBytesRead)
{
    teStatus eStatus;
    tsPRG_PrivateContext *psPriv;

    struct timeval      sNow;

    int iTotalBytes     = *iTotalBytesRead;
    int iBytesRead      = 0;
    int iTimeout;

    psPriv = (tsPRG_PrivateContext *)psContext->pvPrivate;

    if (gettimeofday(&sNow, NULL) < 0)
    {
        return ePRG_SetStatus(psContext, E_PRG_ERROR, "gettimeofday");
    }

    do
    {
        iTimeout = (psTimeout->tv_sec - sNow.tv_sec) * 1000000;
        iTimeout += (psTimeout->tv_usec - sNow.tv_usec);

        if (iTimeout <= 0)
        {
            DBG_vPrintf(TRACE_BOOTLOADER, "Timeout reading message\n");
            eStatus = E_PRG_COMMS_FAILED;
            break;
        }

        /* Get the rest of the message */
        switch(psPriv->sConnection.eType)
        {
        case E_CONNECT_SERIAL:
            eStatus = eUART_Read(psContext, iTimeout, iBytesToRead - iTotalBytes, &au8Msg[iTotalBytes], &iBytesRead);
            break;

        case E_CONNECT_PIPE:
            eStatus = ePIPE_Read(psContext, iTimeout, iBytesToRead - iTotalBytes, &au8Msg[iTotalBytes], &iBytesRead);
            break;

        default:
            return E_PRG_INVALID_TRANSPORT;
            break;
        }

        if (gettimeofday(&sNow, NULL) < 0)
        {
            eStatus = ePRG_SetStatus(psContext, E_PRG_ERROR, "gettimeofday");
        }

        if(eStatus != E_PRG_OK)
        {
            DBG_vPrintf(TRACE_BOOTLOADER, "Error reading message\n");
            break;
        }

        iTotalBytes += iBytesRead;

    } while (iTotalBytes < iBytesToRead);

    *iTotalBytesRead = iTotalBytes;

    return eStatus;
}

/****************************************************************************/
/***        Local Functions                                               ***/
/****************************************************************************/

/****************************************************************************/
/***        END OF FILE                                                   ***/
/****************************************************************************/

