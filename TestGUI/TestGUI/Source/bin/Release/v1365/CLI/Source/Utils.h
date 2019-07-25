/****************************************************************************
 *
 * MODULE:             JN51xx Programmer
 *
 * COMPONENT:          Threads / Mutexes Library
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

#ifndef __UTILS_H__
#define __UTILS_H__

#include <stdio.h>

#include <windows.h>

#ifndef DBG_LOCKS
#define DBG_LOCKS 0
#endif /* DBG_LOCKS */

#define DBG_vPrintf(a,b,ARGS...) do {  if (a) printf("%s: " b, __FUNCTION__, ## ARGS); } while(0)
#define DBG_vAssert(a,b) do {  if (a && !(b)) printf(__FILE__ " %d : Asset Failed\n", __LINE__ ); } while(0)

/** Enumerated type of thread status's */
typedef enum
{
    E_UTILS_OK,
    E_UTILS_ERROR_FAILED,
    E_UTILS_ERROR_TIMEOUT,
    E_UTILS_ERROR_NO_MEM,
    E_UTILS_ERROR_BUSY,
    E_UTILS_ERROR_BLOCK,    /**< The operation would have blocked */
} teUtilsStatus;


/** Enumerated type for thread detached / joinable states */
typedef enum
{
    E_THREAD_JOINABLE,      /**< Thread is created so that it can be waited on and joined */
    E_THREAD_DETACHED,      /**< Thread is created detached so all resources are automatically free'd at exit. */
} teThreadDetachState;


/** Structure to represent a thread */
typedef struct
{
    volatile enum
    {
        E_THREAD_STOPPED,   /**< Thread stopped */
        E_THREAD_RUNNING,   /**< Thread running */
        E_THREAD_STOPPING,  /**< Thread signaled to stop */
    } eState;               /**< Enumerated type of thread states */
    teThreadDetachState
        eThreadDetachState; /**< Detach state of the thread */
    void *pvPriv;           /**< Implementation specfific private structure */
    void *pvThreadData;     /**< Pointer to threads data parameter */
} tsUtilsThread;

typedef void *(*tprThreadFunction)(tsUtilsThread *psThreadInfo);

/** Function to start a thread */
teUtilsStatus eUtils_ThreadStart(tprThreadFunction prThreadFunction, tsUtilsThread *psThreadInfo, teThreadDetachState eDetachState);

/** Function to wait for a thread to exit.
 *  This function blocks the calling thread until the specified thread terminates.
 */
teUtilsStatus eUtils_ThreadWait(tsUtilsThread *psThreadInfo);

#endif /* __UTILS_H__ */
