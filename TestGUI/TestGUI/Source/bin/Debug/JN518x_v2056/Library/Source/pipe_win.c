
/****************************************************************************
 *
 * MODULE:             JN51xx Programmer
 *
 * COMPONENT:          Serial port handling
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
#include <unistd.h>
#include <stdio.h>
#include <stdlib.h>
#include <fcntl.h>
#include <string.h>
#include <ctype.h>
#include <assert.h>

#include <windows.h>

#include "ftd2xx.h"

#include <programmer.h>

#include "programmer_private.h"
#include "pipe.h"
#include "dbg.h"

/****************************************************************************/
/***        Macro Definitions                                             ***/
/****************************************************************************/

#ifdef DEBUG_PIPE
#define TRACE_PIPE TRUE
#else
#define TRACE_PIPE FALSE
#endif

/****************************************************************************/
/***        Type Definitions                                              ***/
/****************************************************************************/

typedef struct
{
    tsPRG_PrivateContext sPriv;
    HANDLE  hPipeHandle;
    OVERLAPPED sOverlapped;
} tsCommsPrivate;

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

teStatus ePRG_ConnectionPipeOpen(tsPRG_Context *psContext, tsConnection *psConnection)
{
    char acDevice[32];
    tsCommsPrivate *psCommsPriv;
    
    DBG_vPrintf(TRACE_PIPE, "Using PIPE device %s\n", psConnection->pcName);
    
    psCommsPriv = realloc(psContext->pvPrivate, sizeof(tsCommsPrivate));
    if (!psCommsPriv)
    {
        return ePRG_SetStatus(psContext, E_PRG_OUT_OF_MEMORY, "");
    }
    psContext->pvPrivate = psCommsPriv;
    
    memset(psCommsPriv, 0, sizeof(tsCommsPrivate));
    memcpy(&psCommsPriv->sPriv.sConnection, psConnection, sizeof(tsConnection));

    psCommsPriv->sPriv.sConnection.pcName = strdup(psConnection->pcName);
    if (!psCommsPriv->sPriv.sConnection.pcName)
    {
        return ePRG_SetStatus(psContext, E_PRG_OUT_OF_MEMORY, "");
    }
    
    psCommsPriv->sOverlapped.hEvent = CreateEvent( 
         NULL,
         TRUE,
         TRUE,
         NULL);
    
    snprintf(acDevice, sizeof(acDevice), "\\\\.\\pipe\\%s", psConnection->pcName);
    
    DBG_vPrintf(TRACE_PIPE, "Opening PIPE device %s\n", acDevice);
    
    psCommsPriv->hPipeHandle = CreateFile(acDevice,
        GENERIC_READ | GENERIC_WRITE,
        0,
        NULL,
        OPEN_EXISTING,
        FILE_FLAG_OVERLAPPED,
        NULL);
        
    if (psCommsPriv->hPipeHandle == INVALID_HANDLE_VALUE)
    {
        return ePRG_SetStatus(psContext, E_PRG_ERROR, "%s", pcPRG_GetLastErrorMessage(psContext));
    }

    DBG_vPrintf(TRACE_PIPE, "Pipe opened\n");
    
    psCommsPriv->sPriv.sConnection.eType = E_CONNECT_PIPE;
    
    return ePRG_SetStatus(psContext, E_PRG_OK, "");
}
 
 
/****************************************************************************
 *
 * NAME: ePRG_UartClose
 *
 * DESCRIPTION:
 * Close the specified PIPE
 *
 * RETURNS:
 * teStatus
 *
 ****************************************************************************/
teStatus ePRG_ConnectionPipeClose(tsPRG_Context *psContext)
{
    tsCommsPrivate *psCommsPriv;

    if(psContext == NULL)
    {
        return E_PRG_NULL_PARAMETER;
    }
    psCommsPriv = (tsCommsPrivate *)psContext->pvPrivate;
    
    if (psCommsPriv->sPriv.sConnection.eType != E_CONNECT_PIPE)
    {
        return ePRG_SetStatus(psContext, E_PRG_INVALID_TRANSPORT, "");
    }
    
    CloseHandle(psCommsPriv->hPipeHandle);
    psCommsPriv->hPipeHandle = INVALID_HANDLE_VALUE;
    
    free(psCommsPriv->sPriv.sConnection.pcName);
    
    return ePRG_SetStatus(psContext, E_PRG_OK, "");
}

/****************************************************************************
 *
 * NAME: ePIPE_Read
 *
 * DESCRIPTION:
 * Reads from the specified PIPE
 *
 * RETURNS:
 * teStatus
 *
 ****************************************************************************/
teStatus ePIPE_Read(tsPRG_Context *psContext, int iTimeoutMicroseconds, int iBufferLen, uint8_t *pu8Buffer, int *piBytesRead)
{   
    tsCommsPrivate *psCommsPriv;
    DWORD dwBytesRead = 0;
    DWORD status;
    
    if((pu8Buffer == NULL) || (psContext == NULL) || (piBytesRead == NULL))
    {
        return E_PRG_NULL_PARAMETER;
    }
    psCommsPriv = (tsCommsPrivate *)psContext->pvPrivate;
    
    if (psCommsPriv->sPriv.sConnection.eType != E_CONNECT_PIPE)
    {
        return ePRG_SetStatus(psContext, E_PRG_INVALID_TRANSPORT, "");
    }
    
    DBG_vPrintf(TRACE_PIPE, "Reading %d bytes from Pipe (timeout %dus)\n", iBufferLen, iTimeoutMicroseconds);
        
    if(!ReadFile(psCommsPriv->hPipeHandle, pu8Buffer, iBufferLen, &dwBytesRead, &psCommsPriv->sOverlapped))
    {
        if (GetLastError() == ERROR_IO_PENDING)
        {
            status = WaitForSingleObject(psCommsPriv->sOverlapped.hEvent, iTimeoutMicroseconds / 1000);
            
            if (status != WAIT_OBJECT_0)
            {
                DBG_vPrintf(TRACE_PIPE, "Error reading (%d)\n", GetLastError());
            }
            
            GetOverlappedResult(psCommsPriv->hPipeHandle, &psCommsPriv->sOverlapped, &dwBytesRead, FALSE);
        } else {
            DBG_vPrintf(TRACE_PIPE, "Error reading (%d)\n", GetLastError());
            return ePRG_SetStatus(psContext, E_PRG_ERROR_READING, "(%s)", pcPRG_GetLastErrorMessage(psContext));
        }
    }
    
    DBG_vPrintf(TRACE_PIPE, "%d bytes read from port\n", dwBytesRead);
    
    *piBytesRead = dwBytesRead;

    return ePRG_SetStatus(psContext, E_PRG_OK, "");
}
 

/****************************************************************************
 *
 * NAME: ePIPE_Write
 *
 * DESCRIPTION:
 * Write to the specified PIPE
 *
 * RETURNS:
 * teStatus
 *
 ****************************************************************************/
teStatus ePIPE_Write(tsPRG_Context *psContext, uint8_t *pu8Data, int iLength)
{
    tsCommsPrivate *psCommsPriv;
    DWORD iBytesWritten;
    DWORD iTotalBytesWritten = 0;
    DWORD iAttempts = 0;
    
    if((pu8Data == NULL) || (psContext == NULL))
    {
        return E_PRG_NULL_PARAMETER;
    }
    psCommsPriv = (tsCommsPrivate *)psContext->pvPrivate;
    
    if (psCommsPriv->sPriv.sConnection.eType != E_CONNECT_PIPE)
    {
        return ePRG_SetStatus(psContext, E_PRG_INVALID_TRANSPORT, "");
    }
    
    do
    {
        DBG_vPrintf(TRACE_PIPE, "Writing %d bytes to port\n", iLength);
        
        if(!WriteFile(psCommsPriv->hPipeHandle, &pu8Data[iTotalBytesWritten], iLength - iTotalBytesWritten, &iBytesWritten, &psCommsPriv->sOverlapped))
        {
            if (GetLastError() == ERROR_IO_PENDING)
            {
                WaitForSingleObject(psCommsPriv->sOverlapped.hEvent, INFINITE);
            } else {
                return ePRG_SetStatus(psContext, E_PRG_ERROR_WRITING, "(%s)", pcPRG_GetLastErrorMessage(psContext));
            }
        }
        else
        {
            iTotalBytesWritten += iBytesWritten;
        }
        
    } while((iTotalBytesWritten < iLength) && (++iAttempts < 2));

    if (iTotalBytesWritten < iLength)
    {
        return ePRG_SetStatus(psContext, E_PRG_ERROR_WRITING, "pipe error");
    }
    return ePRG_SetStatus(psContext, E_PRG_OK, "");
}


/****************************************************************************/
/***        Local Functions                                               ***/
/****************************************************************************/    

/****************************************************************************/
/***        END OF FILE                                                   ***/
/****************************************************************************/
