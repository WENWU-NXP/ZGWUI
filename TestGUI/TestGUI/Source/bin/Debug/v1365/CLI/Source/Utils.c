/****************************************************************************
 *
 * MODULE:             JN51xx Programmer
 *
 * COMPONENT:          Utils.c
 *
 * REVISION:           $Revision: 63883 $
 *
 * DATED:              $Date: 2014-09-09 15:01:25 +0100 (Tue, 09 Sep 2014) $
 *
 * AUTHOR:             Matt Redfearn
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

#include <stdint.h>
#include <stdio.h>
#include <string.h>
#include <stdlib.h>
#include <signal.h>
#include <time.h>
#include <errno.h>

#include <Utils.h>

#ifndef DBG_THREADS
#define DBG_THREADS 0
#endif /* DBG_THREADS */

#ifndef DBG_QUEUE
#define DBG_QUEUE   0
#endif /* DBG_QUEUE */

/************************** Threads Functionality ****************************/

/** Structure representing an OS independant thread */
typedef struct
{
    DWORD               thread_pid;
    HANDLE              thread_handle;
	tprThreadFunction   prThreadFunction;
} tsThreadPrivate;


static DWORD WINAPI dwThreadFunction(void *psThreadInfoVoid)
{
    tsUtilsThread *psThreadInfo = (tsUtilsThread *)psThreadInfoVoid;
    tsThreadPrivate *psThreadPrivate = (tsThreadPrivate *)psThreadInfo->pvPriv;
    
    DBG_vPrintf(DBG_THREADS, "Thread %p running for function %p\n", psThreadInfo, psThreadPrivate->prThreadFunction);

    if (psThreadInfo->eThreadDetachState == E_THREAD_DETACHED)
    {
        DBG_vPrintf(DBG_THREADS, "Detach Thread %p\n", psThreadInfo);
    }
    
    psThreadPrivate->prThreadFunction(psThreadInfo);
    return 0;
}


teUtilsStatus eUtils_ThreadStart(tprThreadFunction prThreadFunction, tsUtilsThread *psThreadInfo, teThreadDetachState eDetachState)
{
    tsThreadPrivate *psThreadPrivate;
    
    psThreadInfo->eState = E_THREAD_STOPPED;
    
    DBG_vPrintf(DBG_THREADS, "Start Thread %p to run function %p\n", psThreadInfo, prThreadFunction);
    
    psThreadPrivate = malloc(sizeof(tsThreadPrivate));
    if (!psThreadPrivate)
    {
        return E_UTILS_ERROR_NO_MEM;
    }
    
    psThreadInfo->pvPriv = psThreadPrivate;
    
    psThreadInfo->eThreadDetachState = eDetachState;
    
    psThreadPrivate->prThreadFunction = prThreadFunction;
    
    psThreadPrivate->thread_handle = CreateThread(
        NULL,                                       // default security attributes
        0,                                          // use default stack size  
        (LPTHREAD_START_ROUTINE)dwThreadFunction,   // thread function name
        psThreadInfo,                               // argument to thread function 
        0,                                          // use default creation flags 
        &psThreadPrivate->thread_pid);              // returns the thread identifier
    if (!psThreadPrivate->thread_handle)
    {
        perror("Could not start thread");
        return E_UTILS_ERROR_FAILED;
    }
    return  E_UTILS_OK;
}

teUtilsStatus eUtils_ThreadWait(tsUtilsThread *psThreadInfo)
{
    tsThreadPrivate *psThreadPrivate = (tsThreadPrivate *)psThreadInfo->pvPriv;

    DBG_vPrintf(DBG_THREADS, "Wait for Thread %p\n", psThreadInfo);
    
    if (psThreadPrivate)
    {
        if (psThreadInfo->eThreadDetachState == E_THREAD_JOINABLE)
        {
            WaitForSingleObject(psThreadPrivate->thread_handle, INFINITE);
            /* We can now free the thread private info */
            free(psThreadPrivate);
        }
        else
        {
            DBG_vPrintf(DBG_THREADS, "Cannot join detached thread %p\n", psThreadInfo);
            return E_UTILS_ERROR_FAILED;
        }
    }
    
    psThreadInfo->eState = E_THREAD_STOPPED;

    return E_UTILS_OK;
}
