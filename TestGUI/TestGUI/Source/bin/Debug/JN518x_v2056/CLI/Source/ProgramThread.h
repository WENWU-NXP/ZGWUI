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

#ifndef  PROGRAMTHREAD_H_INCLUDED 
#define  PROGRAMTHREAD_H_INCLUDED 

#if defined __cplusplus
extern "C" {
#endif

/****************************************************************************/
/***        Include files                                                 ***/
/****************************************************************************/

#include <stdint.h>

#include "Utils.h"
#include "MemoryRegion.h"
#include "programmer.h"

/****************************************************************************/
/***        Macro Definitions                                             ***/
/****************************************************************************/

/****************************************************************************/
/***        Type Definitions                                              ***/
/****************************************************************************/

typedef enum
{
	OPERATION_PROGRAM,
	OPERATION_DUMP,
	OPERATION_READ,
	OPERATION_WRITE,
	OPERATION_ERASE
} teOperation;

/** Structure defining all arguments for one operation */
typedef struct _tsOperation
{
    struct _tsOperation*      psNext;
    teOperation				  eOperation;
    tsPRG_MemoryRegion*       psMemoryRegion;
    const char*               pcMemoryFile;
    uint8_t*                  pu8MemoryData;
    uint32_t                  u32DataLength;
    uint8_t                   u8Mode;
    int                       iVerify;
} tsOperation;

/** Structure defining all operations and common arguments*/
typedef struct
{
    tsConnection    sConnection;
    tsUtilsThread   sThread;
    
    uint32_t*       apu32UserDataGet[3];    /**< 3 lots of 128 bits of user data for loading from index sector */
    uint32_t*       apu32UserDataSet[3];    /**< 3 lots of 128 bits of user data for programming into index sector */


    int             iInitialSpeed;
    int             iProgramSpeed;
    int             iResetDevice;
    int             iVerbosity;
    int             iForce;
    int             iListMemory;
    int             iListAlias;
    int             iVerify;

    uint32_t        u32ConnectionNum;

    uint8_t         u8Mode;
    uint8_t*        pu8UnlockKey;

    tsOperation*    psOperations;

    teStatus        eStatus;
} tsProgramThreadArgs;

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

void *pvProgramThread(tsUtilsThread *psThreadInfo);

/****************************************************************************/
/***        Local Functions                                               ***/
/****************************************************************************/

#if defined __cplusplus
}
#endif

#endif  /* PROGRAMTHREAD_H_INCLUDED */

/****************************************************************************/
/***        END OF FILE                                                   ***/
/****************************************************************************/



