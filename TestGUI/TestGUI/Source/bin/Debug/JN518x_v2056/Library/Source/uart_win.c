
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
#include "uart.h"
#include "dbg.h"

/****************************************************************************/
/***        Macro Definitions                                             ***/
/****************************************************************************/

#ifdef DEBUG_UART
#define TRACE_UART TRUE
#else
#define TRACE_UART FALSE
#endif


/****************************************************************************/
/***        Type Definitions                                              ***/
/****************************************************************************/

typedef struct
{
    tsPRG_PrivateContext sPriv;
    FT_HANDLE hFTDIHandle;
    HANDLE  hSerialHandle;
    DCB dcbSerialParams;
} tsCommsPrivate;

/****************************************************************************/
/***        Local Function Prototypes                                     ***/
/****************************************************************************/

static FT_STATUS WINAPI (*FTdll_ListDevices)(
	PVOID pArg1,
	PVOID pArg2,
	DWORD Flags
	) = NULL;

static FT_STATUS WINAPI (*FTdll_Open)(
	int deviceNumber,
	FT_HANDLE *pHandle
	) = NULL;
    
static FT_STATUS WINAPI (*FTdll_Close)(
    FT_HANDLE ftHandle
    ) = NULL;

static FT_STATUS WINAPI (*FTdll_ResetPort)(
    FT_HANDLE ftHandle
    ) = NULL;
    
static FT_STATUS WINAPI (*FTdll_GetComPortNumber)(
    FT_HANDLE ftHandle,
	LPLONG	lpdwComPortNumber
	) = NULL;
    
static FT_STATUS WINAPI (*FTdll_Purge)(
    FT_HANDLE ftHandle,
	ULONG Mask
	) = NULL;
    
static FT_STATUS WINAPI (*FTdll_SetTimeouts)(
    FT_HANDLE ftHandle,
	ULONG ReadTimeout,
	ULONG WriteTimeout
	) = NULL;
    
static FT_STATUS WINAPI (*FTdll_SetBitMode)(
    FT_HANDLE ftHandle,
    UCHAR ucMask,
	UCHAR ucEnable
    ) = NULL;

static FT_STATUS WINAPI (*FTdll_SetLatencyTimer)(
    FT_HANDLE ftHandle,
    UCHAR ucLatency
    ) = NULL;

static FT_STATUS WINAPI (*FTdll_SetBaudRate)(
    FT_HANDLE ftHandle,
	ULONG BaudRate
	) = NULL;
    
static FT_STATUS WINAPI (*FTdll_SetDataCharacteristics)(
    FT_HANDLE ftHandle,
	UCHAR WordLength,
	UCHAR StopBits,
	UCHAR Parity
	) = NULL;

static FT_STATUS WINAPI (*FTdll_Read)(
    FT_HANDLE ftHandle,
    LPVOID lpBuffer,
    DWORD dwBytesToRead,
    LPDWORD lpBytesReturned
    ) = NULL;
 
static FT_STATUS WINAPI (*FTdll_Write)(
    FT_HANDLE ftHandle,
    LPVOID lpBuffer,
    DWORD dwBytesToWrite,
    LPDWORD lpBytesWritten
    ) = NULL;
    
static FT_HANDLE hPRG_FTDI_GetHandle(tsCommsPrivate *psCommsPriv);
teStatus ePRG_FTDI_ModeProgramming(tsCommsPrivate *psCommsPriv);
teStatus ePRG_FTDI_ModeRunning(tsCommsPrivate *psCommsPriv);

/****************************************************************************/
/***        Exported Variables                                            ***/
/****************************************************************************/

/****************************************************************************/
/***        Local Variables                                               ***/
/****************************************************************************/

static int iOpenDevices = 0;
static CRITICAL_SECTION csFTDISection;

static HMODULE hFtd2xxLibrary = NULL;

/****************************************************************************/
/***        Exported Functions                                            ***/
/****************************************************************************/

/* For now only UART devices are supported. */
teStatus ePRG_ConnectionListInit(tsPRG_Context *psContext, uint32_t *pu32NumConnections, tsConnection **pasConnections)
{
    HKEY hSERIALCOMM;
    
    if ((psContext == NULL) || (pu32NumConnections == NULL) || (pasConnections == NULL))
    {
        return E_PRG_NULL_PARAMETER;
    }
    
    *pu32NumConnections = 0;

    if (RegOpenKeyEx(HKEY_LOCAL_MACHINE, "HARDWARE\\DEVICEMAP\\SERIALCOMM", 0, KEY_QUERY_VALUE, &hSERIALCOMM) == ERROR_SUCCESS)
    {
        //Get the max value name and max value lengths
        DWORD dwMaxValueNameLen;
        DWORD dwMaxValueLen;
        DWORD dwQueryInfo = RegQueryInfoKey(hSERIALCOMM, NULL, NULL, NULL, NULL, NULL, NULL, NULL, &dwMaxValueNameLen, &dwMaxValueLen, NULL, NULL);
        if (dwQueryInfo == ERROR_SUCCESS)
        {
            DWORD dwMaxValueNameSizeInChars = dwMaxValueNameLen + 1; //Include space for the NULL terminator
            DWORD dwMaxValueNameSizeInBytes = dwMaxValueNameSizeInChars * sizeof(TCHAR);
            DWORD dwMaxValueDataSizeInChars = dwMaxValueLen/sizeof(TCHAR) + 1; //Include space for the NULL terminator
            DWORD dwMaxValueDataSizeInBytes = dwMaxValueDataSizeInChars * sizeof(TCHAR);
            char *pcName;
            LPBYTE pcData;
            DWORD dwIndex = 0;
            
            pcName = malloc(dwMaxValueNameSizeInBytes);
            if (!pcName)
            {
                RegCloseKey(hSERIALCOMM);
                return ePRG_SetStatus(psContext, E_PRG_OUT_OF_MEMORY, "");
            }
            
            pcData = malloc(dwMaxValueDataSizeInBytes);
            if (!pcData)
            {
                free(pcName);
                RegCloseKey(hSERIALCOMM);
                return ePRG_SetStatus(psContext, E_PRG_OUT_OF_MEMORY, "");
            }
            
            for (;;)
            {
                DWORD dwType;
                DWORD dwValueNameSize = dwMaxValueNameSizeInChars;
                tsConnection *asNewConnections;
                DWORD dwDataSize = dwMaxValueDataSizeInBytes;
                DWORD dwResult;

                asNewConnections = realloc(*pasConnections, ((*pu32NumConnections)+1) * sizeof(tsConnection));
                if (!asNewConnections)
                {
                    free(pcName);
                    free(pcData);
                    RegCloseKey(hSERIALCOMM);
                    return ePRG_SetStatus(psContext, E_PRG_OUT_OF_MEMORY, "");
                }
                *pasConnections = asNewConnections;
                
                dwResult = RegEnumValue(hSERIALCOMM, dwIndex, pcName, &dwValueNameSize, NULL, &dwType, pcData, &dwDataSize);
                if (dwResult != ERROR_SUCCESS)
                {
                    if (dwResult == ERROR_NO_MORE_ITEMS)
                    {
                        free(pcName);
                        free(pcData);
                        RegCloseKey(hSERIALCOMM);
                        return ePRG_SetStatus(psContext, E_PRG_OK, "");
                    }
                    RegCloseKey(hSERIALCOMM);
                    return ePRG_SetStatus(psContext, E_PRG_ERROR, "getting device list (%s)", pcPRG_GetLastErrorMessage(psContext));
                }
                
                //If the value is of the correct type, then add it to the array
                if (dwType == REG_SZ)
                {
                    DBG_vPrintf(TRACE_UART, "Found name %s\n", pcName);
                    DBG_vPrintf(TRACE_UART, "Found data %s\n", pcData);

                    memset(&asNewConnections[*pu32NumConnections], 0, sizeof(tsConnection)); //added by patrick
                    asNewConnections[*pu32NumConnections].eType   = E_CONNECT_SERIAL;
                    asNewConnections[*pu32NumConnections].pcName  = strdup((char*)pcData);
                    
                    // Check strdup succeeded
                    if (!asNewConnections[*pu32NumConnections].pcName)
                    {
                        free(pcName);
                        free(pcData);
                        return ePRG_SetStatus(psContext, E_PRG_OUT_OF_MEMORY, "");
                    }
                    (*pu32NumConnections)++;
                }
                else
                {
                    free(pcData);
                }
                
                dwIndex++;
            }
            RegCloseKey(hSERIALCOMM);
        }
        else
        {
            RegCloseKey(hSERIALCOMM);
            return ePRG_SetStatus(psContext, E_PRG_ERROR, "querying available devices (%s)", pcPRG_GetLastErrorMessage(psContext));
        }
    }
    else
    {
        return ePRG_SetStatus(psContext, E_PRG_ERROR, "querying available devices (%s)", pcPRG_GetLastErrorMessage(psContext));
    }

    return E_PRG_OK;
}

teStatus ePRG_ConnectionListDestroy(tsPRG_Context *psContext, uint32_t u32NumConnections, tsConnection **pasConnections)
{
    int i;
    
    if ((psContext == NULL) || (pasConnections == NULL))
    {
        return E_PRG_NULL_PARAMETER;
    }
    
    for (i = 0; i < u32NumConnections; i++)
    {
        free((*pasConnections)[i].pcName);
    }
    
    free(*pasConnections);
    *pasConnections = NULL;
    return E_PRG_OK;
}


teStatus ePRG_ConnectionUartOpen(tsPRG_Context *psContext, tsConnection *psConnection)
{
    char acDevice[16];
    tsCommsPrivate *psCommsPriv;
    int i;
    
    DBG_vPrintf(TRACE_UART, "ePRG_ConnectionUartOpen() win\n");
    for (i = 0; i < strlen(psConnection->pcName); i++)
    {
        // Make name uppercase so that it compares correctly with FTDI device names (lpsw5640)
        psConnection->pcName[i] = toupper(psConnection->pcName[i]);
    }
    
    DBG_vPrintf(TRACE_UART, "Using UART device %s\n", psConnection->pcName);
    
    if (++iOpenDevices == 1)
    {
        InitializeCriticalSection(&csFTDISection);
    }
    
    psCommsPriv = realloc(psContext->pvPrivate, sizeof(tsCommsPrivate));
    if (!psCommsPriv)
    {
        return ePRG_SetStatus(psContext, E_PRG_OUT_OF_MEMORY, "");
    }
    psContext->pvPrivate = psCommsPriv;
    
    EnterCriticalSection(&csFTDISection);
    psCommsPriv->hFTDIHandle = hPRG_FTDI_GetHandle(psCommsPriv);
    LeaveCriticalSection(&csFTDISection);
    
    if (psCommsPriv->hFTDIHandle)
    {
        DBG_vPrintf(TRACE_UART, "Opened FTDI device\n");
       
        // Reset port in case another program has left it in a unexpected state
        if (!FT_SUCCESS(FTdll_ResetPort(psCommsPriv->hFTDIHandle)))
        {
            DBG_vPrintf(TRACE_UART, "Error resetting port");
        }
       
        if (psContext->sFlags.bAutoProgramReset)
        {
            // Enter programming mode
            if (ePRG_FTDI_ModeProgramming(psCommsPriv) != E_PRG_OK)
            {
                DBG_vPrintf(TRACE_UART, "Could not set FTDI into programming mode\n");
            }
            else
            {
                /* If we managed to reset the device correctly, 
                * the bootloader will now be running at 1Mbaud for 100ms 
                * - wait for it to return to normal speed */
                vPRG_WaitMs(200);
            }
        }
        
        // Speed up transfer of our short messages
        if (!FT_SUCCESS(FTdll_SetLatencyTimer(psCommsPriv->hFTDIHandle, 2)))
        {
            DBG_vPrintf(TRACE_UART, "Error setting latency timer\n");
        }
        
        // Set up UART characteristics
        if (!FT_SUCCESS(FTdll_SetDataCharacteristics(psCommsPriv->hFTDIHandle, FT_BITS_8, FT_STOP_BITS_1, FT_PARITY_NONE)))
        {
            DBG_vPrintf(TRACE_UART, "Error setting data characteristics\n");
        }
        
    }
    else
    {
        snprintf(acDevice, sizeof(acDevice), "\\\\.\\%s", psConnection->pcName);
        
        DBG_vPrintf(TRACE_UART, "Opening UART device %s\n", acDevice);
        
        psCommsPriv->hSerialHandle = CreateFile(acDevice,
            GENERIC_READ | GENERIC_WRITE,
            0,
            0,
            OPEN_EXISTING,
            0,
            0);
        
        if (psCommsPriv->hSerialHandle == INVALID_HANDLE_VALUE)
        {
            return ePRG_SetStatus(psContext, E_PRG_ERROR, "%s", pcPRG_GetLastErrorMessage(psContext));
        }

        psCommsPriv->dcbSerialParams.DCBlength = sizeof(DCB);
        if (!GetCommState(psCommsPriv->hSerialHandle, &psCommsPriv->dcbSerialParams))
        {
            return ePRG_SetStatus(psContext, E_PRG_ERROR, "uart error (%s)", pcPRG_GetLastErrorMessage(psContext));
        }
        
        psCommsPriv->dcbSerialParams.ByteSize=8;
        psCommsPriv->dcbSerialParams.StopBits=ONESTOPBIT;
        psCommsPriv->dcbSerialParams.Parity=NOPARITY;

        COMMTIMEOUTS timeouts={0};
        timeouts.ReadIntervalTimeout=50;
        timeouts.ReadTotalTimeoutConstant=50;
        timeouts.ReadTotalTimeoutMultiplier=10;
        timeouts.WriteTotalTimeoutConstant=50;
        timeouts.WriteTotalTimeoutMultiplier=10;
        if(!SetCommTimeouts(psCommsPriv->hSerialHandle, &timeouts))
        {
            return ePRG_SetStatus(psContext, E_PRG_ERROR, "uart error (%s)", pcPRG_GetLastErrorMessage(psContext));
        }
    }

    DBG_vPrintf(TRACE_UART, "Port opened\n");
    
    psCommsPriv->sPriv.sConnection.eType = E_CONNECT_SERIAL;
    
    return E_PRG_OK;
}
 
 
/****************************************************************************
 *
 * NAME: ePRG_UartClose
 *
 * DESCRIPTION:
 * Close the specified UART
 *
 * RETURNS:
 * teStatus
 *
 ****************************************************************************/
teStatus ePRG_ConnectionUartClose(tsPRG_Context *psContext)
{
    tsCommsPrivate *psCommsPriv;

    DBG_vPrintf(TRACE_UART, "ePRG_ConnectionUartClose() win\n");
    if(psContext == NULL)
    {
        return E_PRG_NULL_PARAMETER;
    }
    psCommsPriv = (tsCommsPrivate *)psContext->pvPrivate;
    
    if (psCommsPriv->sPriv.sConnection.eType != E_CONNECT_SERIAL)
    {
        return ePRG_SetStatus(psContext, E_PRG_INVALID_TRANSPORT, "");
    }
    
    if (psCommsPriv->hFTDIHandle)
    {
        if (FT_SUCCESS(FTdll_Close(psCommsPriv->hFTDIHandle)))
        {
            psCommsPriv->hFTDIHandle = NULL;
        }
        else
        {
            DBG_vPrintf(TRACE_UART, "Error could not close FDTI handle!\n");
        }
    }
    else
    {
        CloseHandle(psCommsPriv->hSerialHandle);
        psCommsPriv->hSerialHandle = INVALID_HANDLE_VALUE;
    }
    
    free(psCommsPriv->sPriv.sConnection.pcName);
    
    if (--iOpenDevices == 0)
    {
        DeleteCriticalSection(&csFTDISection);
        
        if (hFtd2xxLibrary)
        {
            DBG_vPrintf(TRACE_UART, "Unloading ftd2xx library\n");
            FreeLibrary(hFtd2xxLibrary);
            hFtd2xxLibrary = NULL;
        }
    }
    
    return ePRG_SetStatus(psContext, E_PRG_OK, "");
}


/****************************************************************************
 *
 * NAME: UART_eSetBaudRate
 *
 * DESCRIPTION:
 * Sets the baud rate of the specified UART
 *
 * RETURNS:
 * teStatus
 *
 ****************************************************************************/
teStatus ePRG_ConnectionUartUpdate(tsPRG_Context *psContext, tsConnection *psConnection)
{   
    tsCommsPrivate *psCommsPriv;
    teStatus eStatus;
    
    if(psContext == NULL)
    {
        return E_PRG_NULL_PARAMETER;
    }
    psCommsPriv = (tsCommsPrivate *)psContext->pvPrivate;
    
    if (psCommsPriv->sPriv.sConnection.eType != E_CONNECT_SERIAL)
    {
        return ePRG_SetStatus(psContext, E_PRG_INVALID_TRANSPORT, "");
    }
    
    DBG_vPrintf(TRACE_UART, "Changing UART baud rate to %d\n", psConnection->uDetails.sSerial.u32BaudRate);

    eStatus = eUART_Flush(psContext);
    
    if (eStatus != E_PRG_OK)
    {
        return eStatus;
    }
    
    if (psCommsPriv->hFTDIHandle)
    {
        if (!FT_SUCCESS(FTdll_SetBaudRate(psCommsPriv->hFTDIHandle, psConnection->uDetails.sSerial.u32BaudRate)))
        {
            return ePRG_SetStatus(psContext, E_PRG_ERROR, "error setting FTDI baudrate");
        }
    }
    else
    {
        psCommsPriv->dcbSerialParams.BaudRate = psConnection->uDetails.sSerial.u32BaudRate;
        
        if(!SetCommState(psCommsPriv->hSerialHandle, &psCommsPriv->dcbSerialParams))
        {
            return ePRG_SetStatus(psContext, E_PRG_ERROR, "uart error (%s)", pcPRG_GetLastErrorMessage(psContext));
        }
    }
    return ePRG_SetStatus(psContext, E_PRG_OK, "");
}


/****************************************************************************
 *
 * NAME: eUART_Flush
 *
 * DESCRIPTION:
 * Flush the specified UART
 *
 * RETURNS:
 * teStatus
 *
 ****************************************************************************/
teStatus eUART_Flush(tsPRG_Context *psContext)
{
    tsCommsPrivate *psCommsPriv;

    if(psContext == NULL)
    {
        return E_PRG_NULL_PARAMETER;
    }
    psCommsPriv = (tsCommsPrivate *)psContext->pvPrivate;
    
    if (psCommsPriv->sPriv.sConnection.eType != E_CONNECT_SERIAL)
    {
        return ePRG_SetStatus(psContext, E_PRG_INVALID_TRANSPORT, "");
    }
    
    DBG_vPrintf(TRACE_UART, "cbFlush %p\n", psCommsPriv->sPriv.sConnection.uDetails.sSerial.cbFlush);
    
    return psCommsPriv->sPriv.sConnection.uDetails.sSerial.cbFlush(psContext);
}

/****************************************************************************
 *
 * NAME: eUART_Flush_Int
 *
 * DESCRIPTION:
 * Flush the specified UART
 *
 * RETURNS:
 * teStatus
 *
 ****************************************************************************/
teStatus eUART_Flush_Int(tsPRG_Context *psContext)
{
    tsCommsPrivate *psCommsPriv;

    if(psContext == NULL)
    {
        return E_PRG_NULL_PARAMETER;
    }
    psCommsPriv = (tsCommsPrivate *)psContext->pvPrivate;
    
    if (psCommsPriv->sPriv.sConnection.eType != E_CONNECT_SERIAL)
    {
        return ePRG_SetStatus(psContext, E_PRG_INVALID_TRANSPORT, "");
    }
    
    DBG_vPrintf(TRACE_UART, "Flushing UART\n");

    if (psCommsPriv->hFTDIHandle)
    {
        if (!FT_SUCCESS(FTdll_Purge(psCommsPriv->hFTDIHandle, FT_PURGE_RX | FT_PURGE_TX)))
        {
            return ePRG_SetStatus(psContext, E_PRG_ERROR, "error purging FTDI buffers");
        }
    }
    else
    {
        if(!PurgeComm(psCommsPriv->hSerialHandle, PURGE_RXCLEAR | PURGE_TXCLEAR))
        {
            return ePRG_SetStatus(psContext, E_PRG_ERROR, "uart error (%s)", pcPRG_GetLastErrorMessage(psContext));
        }
    }

    return ePRG_SetStatus(psContext, E_PRG_OK, "");
}

/****************************************************************************
 *
 * NAME: eUART_Read
 *
 * DESCRIPTION:
 * Reads from the specified UART
 *
 * RETURNS:
 * teStatus
 *
 ****************************************************************************/
teStatus eUART_Read(tsPRG_Context *psContext, int iTimeoutMicroseconds, int iBufferLen, uint8_t *pu8Buffer, int *piBytesRead)
{
    tsCommsPrivate *psCommsPriv;

    if((pu8Buffer == NULL) || (psContext == NULL) || (piBytesRead == NULL))
    {
        return E_PRG_NULL_PARAMETER;
    }
    psCommsPriv = (tsCommsPrivate *)psContext->pvPrivate;
    
    if (psCommsPriv->sPriv.sConnection.eType != E_CONNECT_SERIAL)
    {
        return ePRG_SetStatus(psContext, E_PRG_INVALID_TRANSPORT, "");
    }
    
    return psCommsPriv->sPriv.sConnection.uDetails.sSerial.cbRead(psContext, iTimeoutMicroseconds, iBufferLen, pu8Buffer, piBytesRead);
}

/****************************************************************************
 *
 * NAME: eUART_Read_Int
 *
 * DESCRIPTION:
 * Reads from the specified UART
 *
 * RETURNS:
 * teStatus
 *
 ****************************************************************************/
teStatus eUART_Read_Int(tsPRG_Context *psContext, int iTimeoutMicroseconds, int iBufferLen, uint8_t *pu8Buffer, int *piBytesRead)
{   
    tsCommsPrivate *psCommsPriv;
    DWORD dwBytesRead = 0;
    
    int iTimeoutMilliseconds = (iTimeoutMicroseconds - 1) / 1000 + 1;

    if((pu8Buffer == NULL) || (psContext == NULL) || (piBytesRead == NULL))
    {
        return E_PRG_NULL_PARAMETER;
    }
    psCommsPriv = (tsCommsPrivate *)psContext->pvPrivate;
    
    if (psCommsPriv->sPriv.sConnection.eType != E_CONNECT_SERIAL)
    {
        return ePRG_SetStatus(psContext, E_PRG_INVALID_TRANSPORT, "");
    }
    
    DBG_vPrintf(TRACE_UART, "Read %d bytes from UART (timeout %dus)\n", iBufferLen, iTimeoutMicroseconds);
    
    if (psCommsPriv->hFTDIHandle)
    {
        FTdll_SetTimeouts(psCommsPriv->hFTDIHandle, iTimeoutMilliseconds, 0);

        if (!FT_SUCCESS(FTdll_Read(psCommsPriv->hFTDIHandle, pu8Buffer, iBufferLen, &dwBytesRead)))
        {
            DBG_vPrintf(TRACE_UART, "Error reading\n");
            return ePRG_SetStatus(psContext, E_PRG_ERROR_READING, "(%s)", pcPRG_GetLastErrorMessage(psContext));
        }
    }
    else
    {
        COMMTIMEOUTS timeouts={0};
        timeouts.ReadIntervalTimeout        = 0; // Timeout per byte - not used
        timeouts.ReadTotalTimeoutConstant   = iTimeoutMilliseconds; // Overall timeout
        timeouts.ReadTotalTimeoutMultiplier = 0; // Timeout * num bytes to read - not used

        if(!SetCommTimeouts(psCommsPriv->hSerialHandle, &timeouts))
        {
            return ePRG_SetStatus(psContext, E_PRG_ERROR_READING, "(%s)", pcPRG_GetLastErrorMessage(psContext));
        }
        
        if(!ReadFile(psCommsPriv->hSerialHandle, pu8Buffer, iBufferLen, &dwBytesRead, NULL))
        {
            DBG_vPrintf(TRACE_UART, "Error reading (%d)\n", GetLastError());
            return ePRG_SetStatus(psContext, E_PRG_ERROR_READING, "(%s)", pcPRG_GetLastErrorMessage(psContext));
        }
    }        
    DBG_vPrintf(TRACE_UART, "%d bytes read from port\n", dwBytesRead);
        
    *piBytesRead = dwBytesRead;
    
    return ePRG_SetStatus(psContext, E_PRG_OK, "");
}
 

/****************************************************************************
 *
 * NAME: eUART_Write
 *
 * DESCRIPTION:
 * Write to the specified UART
 *
 * RETURNS:
 * teStatus
 *
 ****************************************************************************/
teStatus eUART_Write(tsPRG_Context *psContext, uint8_t *pu8Data, int iLength)
{
    tsCommsPrivate *psCommsPriv;

    if((pu8Data == NULL) || (psContext == NULL))
    {
        return E_PRG_NULL_PARAMETER;
    }
    psCommsPriv = (tsCommsPrivate *)psContext->pvPrivate;
    
    if (psCommsPriv->sPriv.sConnection.eType != E_CONNECT_SERIAL)
    {
        return ePRG_SetStatus(psContext, E_PRG_INVALID_TRANSPORT, "");
    }
    
    DBG_vPrintf(TRACE_UART, "cbWrite: %p\n", psCommsPriv->sPriv.sConnection.uDetails.sSerial.cbWrite);
    
    return psCommsPriv->sPriv.sConnection.uDetails.sSerial.cbWrite(psContext, pu8Data, iLength);
}
 
/****************************************************************************
 *
 * NAME: eUART_Write_Int
 *
 * DESCRIPTION:
 * Write to the specified UART
 *
 * RETURNS:
 * teStatus
 *
 ****************************************************************************/
teStatus eUART_Write_Int(tsPRG_Context *psContext, uint8_t *pu8Data, int iLength)
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
    
    if (psCommsPriv->sPriv.sConnection.eType != E_CONNECT_SERIAL)
    {
        return ePRG_SetStatus(psContext, E_PRG_INVALID_TRANSPORT, "");
    }

    if (psCommsPriv->hFTDIHandle)
    {
        do
        {
            DBG_vPrintf(TRACE_UART, "Write %d bytes to port\n", iLength);
            
            if (!FT_SUCCESS(FTdll_Write(psCommsPriv->hFTDIHandle, &pu8Data[iTotalBytesWritten], iLength - iTotalBytesWritten, &iBytesWritten)))
            {
                return ePRG_SetStatus(psContext, E_PRG_ERROR_WRITING, "(%s)", pcPRG_GetLastErrorMessage(psContext));
            }
            else
            {
                iTotalBytesWritten += iBytesWritten;
            }
        } while((iTotalBytesWritten < iLength) && (++iAttempts < 2));
    }
    else
    {
    
        COMMTIMEOUTS timeouts={0};
        timeouts.WriteTotalTimeoutConstant   = 100;   // Overall timeout
        timeouts.WriteTotalTimeoutMultiplier = 100;   // Timeout * num bytes to write

        if(!SetCommTimeouts(psCommsPriv->hSerialHandle, &timeouts))
        {
            return ePRG_SetStatus(psContext, E_PRG_ERROR_WRITING, "(%s)", pcPRG_GetLastErrorMessage(psContext));
        }
        
        do
        {
            DBG_vPrintf(TRACE_UART, "Write %d bytes to port\n", iLength);
            
            if(!WriteFile(psCommsPriv->hSerialHandle, &pu8Data[iTotalBytesWritten], iLength - iTotalBytesWritten, &iBytesWritten, NULL))
            {
                return ePRG_SetStatus(psContext, E_PRG_ERROR_WRITING, "(%s)", pcPRG_GetLastErrorMessage(psContext));
            }
            else
            {
                iTotalBytesWritten += iBytesWritten;
            }
        } while((iTotalBytesWritten < iLength) && (++iAttempts < 2));
    }

    if (iTotalBytesWritten < iLength)
    {
        return ePRG_SetStatus(psContext, E_PRG_ERROR_WRITING, "uart error");
    }
    return ePRG_SetStatus(psContext, E_PRG_OK, "");
}


/****************************************************************************/
/***        Local Functions                                               ***/
/****************************************************************************/    
    
FT_HANDLE hPRG_FTDI_GetHandle(tsCommsPrivate *psCommsPriv)
{
    FT_HANDLE hFtdiHandle;
    
    DWORD dwNumDevs;
    DBG_vPrintf(TRACE_UART, "hPRG_FTDI_GetHandle() win\n");
    
    if (!hFtd2xxLibrary)
    {
        hFtd2xxLibrary = LoadLibrary("ftd2xx.dll");
        if (hFtd2xxLibrary == NULL)
        {
            DBG_vPrintf(TRACE_UART, "Could not load ftd2xx library\n");
            return NULL;
        }
        else
        {
            // Loaded the dll ok - lets grab pointers to the functions we need!
            DBG_vPrintf(TRACE_UART, "Loaded ftd2xx library\n");
           
            if (!(*(FARPROC *)&FTdll_ListDevices = GetProcAddress(hFtd2xxLibrary, "FT_ListDevices")))    return NULL;
            DBG_vPrintf(TRACE_UART, "FTdll_ListDevices: %p\n", FTdll_ListDevices);
            
            if (!(*(FARPROC *)&FTdll_Open = GetProcAddress(hFtd2xxLibrary, "FT_Open")))    return NULL;
            DBG_vPrintf(TRACE_UART, "FTdll_Open: %p\n", FTdll_Open);
            
            if (!(*(FARPROC *)&FTdll_Close = GetProcAddress(hFtd2xxLibrary, "FT_Close")))    return NULL;
            DBG_vPrintf(TRACE_UART, "FTdll_Close: %p\n", FTdll_Close);
            
            if (!(*(FARPROC *)&FTdll_ResetPort = GetProcAddress(hFtd2xxLibrary, "FT_ResetPort")))    return NULL;
            DBG_vPrintf(TRACE_UART, "FTdll_ResetPort: %p\n", FTdll_ResetPort);
            
            if (!(*(FARPROC *)&FTdll_GetComPortNumber = GetProcAddress(hFtd2xxLibrary, "FT_GetComPortNumber")))    return NULL;
            DBG_vPrintf(TRACE_UART, "FTdll_GetComPortNumber: %p\n", FTdll_GetComPortNumber);
            
            if (!(*(FARPROC *)&FTdll_Purge = GetProcAddress(hFtd2xxLibrary, "FT_Purge")))    return NULL;
            DBG_vPrintf(TRACE_UART, "FTdll_Purge: %p\n", FTdll_Purge);
            
            if (!(*(FARPROC *)&FTdll_SetTimeouts = GetProcAddress(hFtd2xxLibrary, "FT_SetTimeouts")))    return NULL;
            DBG_vPrintf(TRACE_UART, "FTdll_SetTimeouts: %p\n", FTdll_SetTimeouts);
            
            if (!(*(FARPROC *)&FTdll_SetBitMode = GetProcAddress(hFtd2xxLibrary, "FT_SetBitMode")))    return NULL;
            DBG_vPrintf(TRACE_UART, "FTdll_SetBitMode: %p\n", FTdll_SetBitMode);
            
            if (!(*(FARPROC *)&FTdll_SetLatencyTimer = GetProcAddress(hFtd2xxLibrary, "FT_SetLatencyTimer")))    return NULL;
            DBG_vPrintf(TRACE_UART, "FTdll_SetLatencyTimer: %p\n", FTdll_SetLatencyTimer);
            
            if (!(*(FARPROC *)&FTdll_SetBaudRate = GetProcAddress(hFtd2xxLibrary, "FT_SetBaudRate")))    return NULL;
            DBG_vPrintf(TRACE_UART, "FTdll_SetBaudRate: %p\n", FTdll_SetBaudRate);
            
            if (!(*(FARPROC *)&FTdll_SetDataCharacteristics = GetProcAddress(hFtd2xxLibrary, "FT_SetDataCharacteristics")))    return NULL;
            DBG_vPrintf(TRACE_UART, "FTdll_SetDataCharacteristics: %p\n",FTdll_SetDataCharacteristics);
            
            if (!(*(FARPROC *)&FTdll_Read = GetProcAddress(hFtd2xxLibrary, "FT_Read")))    return NULL;
            DBG_vPrintf(TRACE_UART, "FTdll_Read: %p\n", FTdll_Read);
            
            if (!(*(FARPROC *)&FTdll_Write = GetProcAddress(hFtd2xxLibrary, "FT_Write")))    return NULL;
            DBG_vPrintf(TRACE_UART, "FTdll_Write: %p\n",FTdll_Write);
        }
    }
    
    // search for number of devices first
    if (!FT_SUCCESS(FTdll_ListDevices(&dwNumDevs, NULL, FT_LIST_NUMBER_ONLY)))
    {
        DBG_vPrintf(TRACE_UART, "Error no FTDI device found!\n");
        return NULL;
    }
    else
    {
        int i;
        DBG_vPrintf(TRACE_UART, "FTDI: %d devices\n", dwNumDevs);

        int status = FT_OK;
              
        for (i = 0; i < dwNumDevs; i++)
        {
            status = FTdll_Open(i, &hFtdiHandle);
            if (status == FT_OK)
            {
                /* Opened handle to device */
                LONG lComPortNumber;

                if (FT_SUCCESS(FTdll_GetComPortNumber(hFtdiHandle, &lComPortNumber)))
                {
                    char acComName[8];
                    snprintf(acComName, 8, "COM%ld", lComPortNumber);
                    DBG_vPrintf(TRACE_UART, "COM port %s is an FTDI device\n", acComName);
                    
                    if (strncmp(psCommsPriv->sPriv.sConnection.pcName, acComName, 8) == 0)
                    {
                        DBG_vPrintf(TRACE_UART, "Found FTDI device for our COM port\n");
                        return hFtdiHandle;
                    }
                }
                else
                {
                    DBG_vPrintf(TRACE_UART, "Error: could not open FTDI com port!\n");
                }
                /* Not the one we are looking for */
                FTdll_Close(hFtdiHandle);
            }
            else
            {
                DBG_vPrintf(TRACE_UART, "Error: could not get FTDI handle, try 1!, status=%d\n", status);
            }
        }

        //Could not get the FTDI handle so make a second try a bit later
        DBG_vPrintf(TRACE_UART, "Wait 3000ms and do a second try to get FTDI handle\n");
        vPRG_WaitMs(3000);

        for (i = 0; i < dwNumDevs; i++)
        {
            status = FTdll_Open(i, &hFtdiHandle);
            if (status == FT_OK)
            {
                /* Opened handle to device */
                LONG lComPortNumber;

                if (FT_SUCCESS(FTdll_GetComPortNumber(hFtdiHandle, &lComPortNumber)))
                {
                    char acComName[8];
                    snprintf(acComName, 8, "COM%ld", lComPortNumber);
                    DBG_vPrintf(TRACE_UART, "COM port %s is an FTDI device\n", acComName);

                    if (strncmp(psCommsPriv->sPriv.sConnection.pcName, acComName, 8) == 0)
                    {
                        DBG_vPrintf(TRACE_UART, "Found FTDI device for our COM port\n");
                        return hFtdiHandle;
                    }
                }
                else
                {
                    DBG_vPrintf(TRACE_UART, "Error: could not open FTDI com port!\n");
                }
                /* Not the one we are looking for */
                FTdll_Close(hFtdiHandle);
            }
            else
            {
                DBG_vPrintf(TRACE_UART, "Error: could not get FTDI handle, try 2!, status=%d\n", status);
            }
        }
    }

    return NULL;
}


teStatus ePRG_FTDI_ModeProgramming(tsCommsPrivate *psCommsPriv)
{

    const int aaiBitModes[5][2] = 
    {
        { 0xC0, 0x20 }, /* Drive reset and program low */
        { 0xC4, 0x20 }, /* Drive reset high */
        { 0xCC, 0x20 }, /* Drive program high */
        { 0x0C, 0x20 }, /* Release reset and program */
        { 0x00, 0x00 }, /* Normal bit mode */
    };
    int i;

    if (!psCommsPriv->hFTDIHandle)
    {
        DBG_vPrintf(TRACE_UART, "Not an FTDI COM port\n");
        return E_PRG_ERROR;
    }
    
    if (!FT_SUCCESS(FTdll_Purge(psCommsPriv->hFTDIHandle, FT_PURGE_TX | FT_PURGE_RX)))
    {
        DBG_vPrintf(TRACE_UART, "Error purging TX / RX buffers\n");
        return E_PRG_ERROR;
    }
    
    for (i = 0; i < (sizeof(aaiBitModes) / sizeof(aaiBitModes[0])); i++)
    {
        DBG_vPrintf(TRACE_UART, "Setting bit mode 0x%02X, 0x%02X\n", aaiBitModes[i][0], aaiBitModes[i][1]);
        if (!FT_SUCCESS(FTdll_SetBitMode(psCommsPriv->hFTDIHandle, aaiBitModes[i][0], aaiBitModes[i][1])))
        {
            DBG_vPrintf(TRACE_UART, "Error bitbanging device\n");
            return E_PRG_ERROR;
        }
        vPRG_WaitMs(20);
    }
    
    DBG_vPrintf(TRACE_UART, "FTDI set into programming mode\n");
    
    return E_PRG_OK;
}


teStatus ePRG_FTDI_ModeRunning(tsCommsPrivate *psCommsPriv)
{
    const int aaiBitModes[3][2] = 
    {
        { 0x40, 0x20 },
        { 0x00, 0x20 },
        { 0x00, 0x00 }, /* Normal bit mode */
    };
    int i;
    
    if (!psCommsPriv->hFTDIHandle)
    {
        DBG_vPrintf(TRACE_UART, "Not an FTDI COM port\n");
        return E_PRG_ERROR;
    }
    
    if (!FT_SUCCESS(FTdll_Purge(psCommsPriv->hFTDIHandle, FT_PURGE_TX | FT_PURGE_RX)))
    {
        DBG_vPrintf(TRACE_UART, "Error purging TX / RX buffers\n");
        return E_PRG_ERROR;
    }
    
    for (i = 0; i < (sizeof(aaiBitModes) / sizeof(aaiBitModes[0])); i++)
    {
        DBG_vPrintf(TRACE_UART, "Setting bit mode 0x%02X, 0x%02X\n", aaiBitModes[i][0], aaiBitModes[i][1]);
        if (!FT_SUCCESS(FTdll_SetBitMode(psCommsPriv->hFTDIHandle, aaiBitModes[i][0], aaiBitModes[i][1])))
        {
            DBG_vPrintf(TRACE_UART, "Error bitbanging device\n");
            return E_PRG_ERROR;
        }
        vPRG_WaitMs(10);
    }
    
    DBG_vPrintf(TRACE_UART, "FTDI set into running mode\n");
    
    return E_PRG_OK;
}




/****************************************************************************/
/***        END OF FILE                                                   ***/
/****************************************************************************/
