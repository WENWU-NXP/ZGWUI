/****************************************************************************
 *
 * MODULE:             LicesnseFile.c
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

#include <sys/types.h>
#include <sys/stat.h>
#include <string.h>
#include <stdint.h>
#include <stdio.h>
#include <stdlib.h>
#include <errno.h>
#include <fcntl.h>
#include <unistd.h>

#ifdef WIN32
#include <winsock2.h>
#endif

#include "LicenseFile.h"
#include "Utils.h"



int iStrToBuffer(const char *pcStr, uint32_t u32NumBytes, uint8_t *pu8DataBuffer)
{
    char acBuffer[256] = "";
    int i;
    char acWord[3] = { 0, 0, 0 };

    if ((strncmp(pcStr, "0x", 2) == 0) || (strncmp(pcStr, "0X", 2) == 0))
    {
        /* Skip leading 0x */
        pcStr += 2;
    }

    for (i = 0; i < u32NumBytes; i++)
    {
        int iWord;
        strncpy(acWord, &pcStr[i * 2], 2);
        if (sscanf(acWord, "%2x", &iWord) != 1)
            break;

        pu8DataBuffer[i] = iWord;
    }
    if (sscanf(&pcStr[i * 2], "%255s", acBuffer) > 0)
    {
        return i+1;
    }
    return i;
}


int iStrToWordBuffer(const char *pcStr, uint32_t u32NumWords, uint32_t *pu32DataBuffer)
{
    char acBuffer[256] = "";
    int i;
    char acWord[9] = { 0, 0, 0, 0, 0, 0, 0, 0, 0 };

    if ((strncmp(pcStr, "0x", 2) == 0) || (strncmp(pcStr, "0X", 2) == 0))
    {
        /* Skip leading 0x */
        pcStr += 2;
    }

    for (i = 0; i < u32NumWords; i++)
    {
        int iWord;
        strncpy(acWord, &pcStr[i * 8], 8);
        if (sscanf(acWord, "%8x", &iWord) != 1)
            break;

        pu32DataBuffer[i] = iWord;
    }
    if (sscanf(&pcStr[i * 8], "%255s", acBuffer) > 0)
    {
        return i+1;
    }
    return i;
}

/****************************************************************************/
/***        END OF FILE                                                   ***/
/****************************************************************************/
