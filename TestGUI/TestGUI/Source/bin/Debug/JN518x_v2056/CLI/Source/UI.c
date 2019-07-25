/****************************************************************************
 * *
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


/****************************************************************************/
/***        Include files                                                 ***/
/****************************************************************************/

#include <string.h>
#include <stdio.h>
#include <stdlib.h>
#include <unistd.h>
#include <signal.h>

#if defined POSIX
#include <termios.h>
#include <pthread.h>
#include <sys/ioctl.h>
#include <ncurses.h>
#elif defined WIN32
#include <conio.h>
#include <windows.h>
#include <curses.h>
#endif

#include "UI.h"

/****************************************************************************/
/***        Macro Definitions                                             ***/
/****************************************************************************/

#define NCURSES_VERBOSITY           1

#define NCURSES_MIN_WIDTH           62
#define NCURSES_MIN_HEIGHT          22

#define NCURSES_LINES_PER_DEVICE    5

#define NCURSES_LINE_SEPARATOR      0
#define NCURSES_LINE_DEVICEINFO     1
#define NCURSES_LINE_STATUS         2
#define NCURSES_LINE_PROGRESS       3

/****************************************************************************/
/***        Type Definitions                                              ***/
/****************************************************************************/

typedef struct
{
    uint32_t u32ConnectionNum;
    enum
    {
        E_FB_STATUS,
        E_FB_PROGRESS,
        E_FB_DEVICEINFO,
        E_FB_CONFIRMATION,
        E_FB_WINDOWSIZE,
    } eType;
    union
    {
        char        *pcText;
        uint8_t     u8Progress;
        struct
        {
            char    *pcChipName;
            char    *pcMAC_Address;
            uint32_t u32ChipId;
            uint32_t u32Bootloader;
        } sDeviceInfo;
    } uData;
} tsFeedbackEvent;

typedef struct
{
    int          iVerbosity;
    int          iBypassDisplay;        //Used to bypass UI display when displaying for instance device specific help
    uint32_t     u32NumConnections;
    tsConnection* asConnections;
    const char  *pcProgramName;
    tsUtilsQueue sFeedbackQueue;
    tsUtilsQueue sInputQueue;
} tsFeedbackThreadData;

/****************************************************************************/
/***        Local Function Prototypes                                     ***/
/****************************************************************************/

static void *pvUI_UpdateStatusThread(tsUtilsThread *psThreadInfo);

static void mvwaddstrwrap(WINDOW *window, int y, int x, char *text);

static void vUI_ResizeHandler(int sig);

/****************************************************************************/
/***        Exported Variables                                            ***/
/****************************************************************************/

static tsFeedbackThreadData     sFeedbackThreadData;
static tsUtilsThread            sFeedbackThread;

/****************************************************************************/
/***        Local Variables                                               ***/
/****************************************************************************/

/****************************************************************************/
/***        Exported Functions                                            ***/
/****************************************************************************/

teStatus eUI_Init(const char *pcProgramName, int iVerbosity, int iBypassDisplay, uint32_t u32NumConnections, tsConnection* asConnections)
{
#if defined POSIX
    signal(SIGWINCH, vUI_ResizeHandler);
#endif /* POSIX */
    
    if (eUtils_QueueCreate(&sFeedbackThreadData.sFeedbackQueue, 32, 0) != E_UTILS_OK)
    {
        fprintf(stderr, "Error initialising queue\n");
        return E_PRG_ERROR;
    }
    
    if (eUtils_QueueCreate(&sFeedbackThreadData.sInputQueue, 2, 0) != E_UTILS_OK)
    {
        fprintf(stderr, "Error initialising queue\n");
        return E_PRG_ERROR;
    }
    
    sFeedbackThreadData.iVerbosity          = iVerbosity;
    sFeedbackThreadData.iBypassDisplay      = iBypassDisplay;
    sFeedbackThreadData.u32NumConnections   = u32NumConnections;
    sFeedbackThreadData.asConnections       = asConnections;
    sFeedbackThreadData.pcProgramName       = pcProgramName;
    
    sFeedbackThread.pvThreadData = &sFeedbackThreadData;
    if (eUtils_ThreadStart(pvUI_UpdateStatusThread, &sFeedbackThread, E_THREAD_JOINABLE) != E_UTILS_OK)
    {
        fprintf(stderr, "Error starting feedback thread\n");
        return E_PRG_ERROR;
    }
    
    return E_PRG_OK;
}

teStatus eUI_Destroy(void)
{
    /* Queue final event to kill feedback thread */
    eUtils_QueueQueue(&sFeedbackThreadData.sFeedbackQueue, NULL);
    
    if (eUtils_ThreadWait(&sFeedbackThread) != E_UTILS_OK)
    {
        printf("Error joining feedback thread\n");
    }
    
    eUtils_QueueDestroy(&sFeedbackThreadData.sFeedbackQueue);
    eUtils_QueueDestroy(&sFeedbackThreadData.sInputQueue);
    
    return E_PRG_OK;
}


teStatus cbUI_Progress(void *pvUser, const char *pcTitle, const char *pcText, int iNumSteps, int iProgress)
{
    tsProgramThreadArgs *psThreadArgs = (tsProgramThreadArgs *)pvUser;
    
    if (psThreadArgs->iVerbosity > 0)
    {
        tsFeedbackEvent *psEvent = malloc(sizeof(tsFeedbackEvent));
        if (!psEvent)
        {
            return E_PRG_ERROR;
        }

        psEvent->eType = E_FB_PROGRESS;
        psEvent->u32ConnectionNum = psThreadArgs->u32ConnectionNum;
        if (iNumSteps > 0)
        {
            psEvent->uData.u8Progress = ((iProgress * 100) / iNumSteps);
        }
        else
        {
            psEvent->uData.u8Progress = 0;
        }
        
        vUI_UpdateStatus(psThreadArgs, pcTitle);
        
        if (eUtils_QueueQueue(&sFeedbackThreadData.sFeedbackQueue, psEvent) != E_UTILS_OK)
        {
            fprintf(stderr, "Error queueing feedback\n");
            free(psEvent);
        }
    }
    return E_PRG_OK;
}


teStatus cbUI_Confirm(void *pvUser, const char *pcTitle, const char *pcText)
{
    teStatus eStatus = E_PRG_ERROR;
    tsProgramThreadArgs *psThreadArgs = (tsProgramThreadArgs *)pvUser;
    tsFeedbackEvent *psEvent;
    
    if (psThreadArgs->iForce)
    {
        vUI_UpdateStatus(psThreadArgs, pcText);
        vUI_UpdateStatus(psThreadArgs, "Forcing operation due to command line argument");
        return E_PRG_OK;
    }   

    psEvent = malloc(sizeof(tsFeedbackEvent));
    if (!psEvent)
    {
        return E_PRG_ERROR;
    }
    
    psEvent->eType = E_FB_CONFIRMATION;
    psEvent->u32ConnectionNum = psThreadArgs->u32ConnectionNum;
    psEvent->uData.pcText = strdup(pcText);

    if (eUtils_QueueQueue(&sFeedbackThreadData.sFeedbackQueue, psEvent) != E_UTILS_OK)
    {
        fprintf(stderr, "Error queueing feedback\n");
        free(psEvent);
        return E_PRG_ERROR;
    }
    
    if (eUtils_QueueDequeue(&sFeedbackThreadData.sInputQueue, (void**)&psEvent) == E_UTILS_OK)
    {
        if (psEvent)
        {
            if (psEvent->eType == E_FB_CONFIRMATION)
            {
                if ((psEvent->uData.pcText[0] == 'Y') || (psEvent->uData.pcText[0] == 'y'))
                {
                    eStatus = E_PRG_OK;
                }
                else
                {
                    eStatus = E_PRG_ABORTED;
                }
                free(psEvent->uData.pcText);
            }
        }
    }
    return eStatus;
}

#if 0 //comment by patrick
void vUI_UpdateStatus(tsProgramThreadArgs *psThreadArgs, const char *pcFmt, ...)
{    
    tsFeedbackEvent *psEvent = malloc(sizeof(tsFeedbackEvent));
    va_list vl;
    if (!psEvent)
    {
        return;
    }
    
    va_start(vl, pcFmt);
#ifndef WIN32
    if (vasprintf(&psEvent->uData.pcText, pcFmt, vl) > 0)
#else
    psEvent->uData.pcText = malloc(4096); /* Should be enough :-S */
    if (!psEvent->uData.pcText)
    {
        return;
    }
    if (vsprintf(psEvent->uData.pcText, pcFmt, vl) > 0)
#endif /* WIN32 */
    {
        psEvent->eType = E_FB_STATUS;
        psEvent->u32ConnectionNum = psThreadArgs->u32ConnectionNum;
        if (eUtils_QueueQueue(&sFeedbackThreadData.sFeedbackQueue, psEvent) != E_UTILS_OK)
        {
            fprintf(stderr, "Error queueing feedback\n");
        }
    }
    else
    {
        free(psEvent);
    }
}
#else
void vUI_UpdateStatus(tsProgramThreadArgs *psThreadArgs, const char *pcFmt, ...)
{
}
#endif

void vUI_UpdateDeviceInfo(tsProgramThreadArgs *psThreadArgs, tsChipDetails *psChipDetails)
{
    tsFeedbackEvent *psEvent = malloc(sizeof(tsFeedbackEvent));
    
    if (psEvent)
    {
        char acMacAddress[24];
        sprintf(acMacAddress, "%02X:%02X:%02X:%02X:%02X:%02X:%02X:%02X", 
                psChipDetails->au8MacAddress[0] & 0xFF, psChipDetails->au8MacAddress[1] & 0xFF, 
                psChipDetails->au8MacAddress[2] & 0xFF, psChipDetails->au8MacAddress[3] & 0xFF, 
                psChipDetails->au8MacAddress[4] & 0xFF, psChipDetails->au8MacAddress[5] & 0xFF, 
                psChipDetails->au8MacAddress[6] & 0xFF, psChipDetails->au8MacAddress[7] & 0xFF);
        
        psEvent->eType                              = E_FB_DEVICEINFO;
        psEvent->u32ConnectionNum                   = psThreadArgs->u32ConnectionNum;
        psEvent->uData.sDeviceInfo.u32ChipId        = psChipDetails->u32ChipId;
        psEvent->uData.sDeviceInfo.u32Bootloader    = psChipDetails->u32BootloaderVersion;
        psEvent->uData.sDeviceInfo.pcChipName       = strdup(psChipDetails->pcChipName);
        psEvent->uData.sDeviceInfo.pcMAC_Address    = strdup(acMacAddress);

        if (eUtils_QueueQueue(&sFeedbackThreadData.sFeedbackQueue, psEvent) == E_UTILS_OK)
        {
            return;
        }
    }

    fprintf(stderr, "Error queueing feedback\n");
    free(psEvent->uData.sDeviceInfo.pcChipName);
    free(psEvent->uData.sDeviceInfo.pcMAC_Address);
    free(psEvent);
    return;
}



/****************************************************************************/
/***        Local Functions                                               ***/
/****************************************************************************/


static void *pvUI_UpdateStatusThread(tsUtilsThread *psThreadInfo)
{
    tsFeedbackThreadData *psThreadData = (tsFeedbackThreadData *)psThreadInfo->pvThreadData;
    tsUtilsQueue *psFeedbackQueue = &psThreadData->sFeedbackQueue;
    WINDOW *sWindow = NULL;
    WINDOW *asConWindow[psThreadData->u32NumConnections];
    
    if ((psThreadData->iVerbosity == NCURSES_VERBOSITY) && (sFeedbackThreadData.iBypassDisplay == 0))
    {
        int iNameLength, iNamePosition;
        /* Enter curses mode */
        sWindow = initscr();
        
        if ((getmaxx(sWindow) < NCURSES_MIN_WIDTH) || (getmaxy(sWindow) < NCURSES_MIN_HEIGHT))
        {
            /* End curses mode and switch to lower verbosity */
            endwin();
            psThreadData->iVerbosity = 0;
            printf("Your terminal window is too small for ncurses mode. The minimum size is %dx%d\n", NCURSES_MIN_WIDTH, NCURSES_MIN_HEIGHT);
        }
        else
        {
            cbreak();
            noecho();
            nonl();
        
            box(sWindow, ACS_VLINE, ACS_HLINE);
            
            iNameLength = strlen(psThreadData->pcProgramName);
            iNamePosition = (getmaxx(sWindow) / 2) - (iNameLength / 2);
            
            mvwaddstr(sWindow, 0, iNamePosition-1, " ");
            mvwaddstr(sWindow, 0, iNamePosition, psThreadData->pcProgramName);
            mvwaddstr(sWindow, 0, iNamePosition + iNameLength, " ");
            
            wrefresh(sWindow);
            
            {
                int i;
                for (i = 0; i < psThreadData->u32NumConnections; i++)
                {
                    asConWindow[i] = newwin(NCURSES_LINES_PER_DEVICE, getmaxx(sWindow) - 2, i * NCURSES_LINES_PER_DEVICE + 1, 1);
                    mvwaddstr(asConWindow[i], 0, 0, psThreadData->asConnections[i].pcName);
                    wmove(asConWindow[i], NCURSES_LINES_PER_DEVICE-1, 0);
                    whline(asConWindow[i], ACS_HLINE, getmaxx(asConWindow[i]));
                    wrefresh(asConWindow[i]);
                }
            }
        }
    }

    psThreadInfo->eState = E_THREAD_RUNNING;
    while (psThreadInfo->eState == E_THREAD_RUNNING)
    {
        tsFeedbackEvent *psEvent = NULL;
        
        if (eUtils_QueueDequeue(psFeedbackQueue, (void**)&psEvent) == E_UTILS_OK)
        {
            if (psEvent)
            {
                switch (psEvent->eType)
                {
                    case(E_FB_DEVICEINFO):
                        if ((psThreadData->iVerbosity == NCURSES_VERBOSITY) && (sFeedbackThreadData.iBypassDisplay == 0))
                        {
                            mvwprintw(asConWindow[psEvent->u32ConnectionNum], NCURSES_LINE_DEVICEINFO, 0, "Detected %s with MAC address %s", 
                                      psEvent->uData.sDeviceInfo.pcChipName, psEvent->uData.sDeviceInfo.pcMAC_Address);
                            wrefresh(asConWindow[psEvent->u32ConnectionNum]);
                        }
                        else 
                        {
                            printf("%6s: Detected %s with MAC address %s\n", psThreadData->asConnections[psEvent->u32ConnectionNum].pcName, 
                                   psEvent->uData.sDeviceInfo.pcChipName, psEvent->uData.sDeviceInfo.pcMAC_Address);
                            
                            if (psThreadData->iVerbosity > NCURSES_VERBOSITY)
                            {
                                printf("%6s: Chip ID: 0x%08X\n", 
                                       psThreadData->asConnections[psEvent->u32ConnectionNum].pcName, 
                                       psEvent->uData.sDeviceInfo.u32ChipId);
                                
                                printf("%6s: Bootloader Version: 0x%08X\n", 
                                       psThreadData->asConnections[psEvent->u32ConnectionNum].pcName, 
                                       psEvent->uData.sDeviceInfo.u32Bootloader);
                            }
                        }
                        free(psEvent->uData.sDeviceInfo.pcChipName);
                        free(psEvent->uData.sDeviceInfo.pcMAC_Address);
                        break;
                        
                    case(E_FB_STATUS):
                        if ((psThreadData->iVerbosity == NCURSES_VERBOSITY) && (sFeedbackThreadData.iBypassDisplay == 0))
                        {
                            char acStr[1024];
                            sprintf(acStr, "%s", psEvent->uData.pcText);
                            wmove(asConWindow[psEvent->u32ConnectionNum], NCURSES_LINE_STATUS, 0);
                            wclrtoeol(asConWindow[psEvent->u32ConnectionNum]);
                            mvwaddstr(asConWindow[psEvent->u32ConnectionNum], NCURSES_LINE_STATUS, 0, acStr);
                            wrefresh(asConWindow[psEvent->u32ConnectionNum]);
                        }
                        else
                        {
                            printf("%6s: %s\n", psThreadData->asConnections[psEvent->u32ConnectionNum].pcName, psEvent->uData.pcText);
                        }
                        free(psEvent->uData.pcText);
                        break;
                        
                    case(E_FB_PROGRESS):
                        if ((psThreadData->iVerbosity == NCURSES_VERBOSITY) && (sFeedbackThreadData.iBypassDisplay == 0))
                        {
                            char acStr[1024];
                            int iLength;
                            memset(acStr, ' ', 1024);
                            iLength = sprintf(acStr, "[");
                            memset(&acStr[iLength], '=', ((getmaxx(asConWindow[psEvent->u32ConnectionNum])-6-iLength) * (psEvent->uData.u8Progress)) / 100);
                            sprintf(&acStr[getmaxx(asConWindow[psEvent->u32ConnectionNum])-6], "] %3d%%", psEvent->uData.u8Progress);
                            mvwaddstr(asConWindow[psEvent->u32ConnectionNum], NCURSES_LINE_PROGRESS, 0, acStr);
                            wrefresh(asConWindow[psEvent->u32ConnectionNum]);
                        }
                        else
                        {
                            printf("%6s: %d\n", psThreadData->asConnections[psEvent->u32ConnectionNum].pcName, psEvent->uData.u8Progress);
                        }
                        break;
                        
                    case(E_FB_CONFIRMATION):
                    {
                        char c;
                        tsFeedbackEvent *psInputEvent = malloc(sizeof(tsFeedbackEvent));
                        if (!psInputEvent)
                        {
                            fprintf(stderr, "Error allocating input event\n");
                            break;
                        }
                        
                        if ((psThreadData->iVerbosity == NCURSES_VERBOSITY) && (sFeedbackThreadData.iBypassDisplay == 0))
                        {
                            WINDOW *sConfirmationWindowBorder;
                            WINDOW *sConfirmationWindow;
                            char acStr[1024];
                            sprintf(acStr, " Confirm Operation (%s) ", psThreadData->asConnections[psEvent->u32ConnectionNum].pcName);
                            
                            sConfirmationWindowBorder = newwin(14, 62, (getmaxy(sWindow)/2) - 11, (getmaxx(sWindow) / 2) - 31);
                            box(sConfirmationWindowBorder, ACS_VLINE, ACS_HLINE );
                            mvwaddstr(sConfirmationWindowBorder, 0, 31-(strlen(acStr)/2), acStr);
                            
                            sConfirmationWindow = newwin(12, 58, (getmaxy(sWindow)/2) - 10, (getmaxx(sWindow) / 2) - 29);
                            
                            mvwaddstrwrap(sConfirmationWindow, 1, 0, psEvent->uData.pcText);
                            mvwaddstr(sConfirmationWindow, 11, 28-2, "Y / N");
                            
                            wrefresh(sConfirmationWindowBorder);
                            wrefresh(sConfirmationWindow);
                            
                            c = getch();
                            
                            delwin(sConfirmationWindow);
                            wclear(sConfirmationWindowBorder);
                            wrefresh(sConfirmationWindowBorder);
                            delwin(sConfirmationWindowBorder);
                        }
                        else
                        {
                            printf("%6s: %s\n\nY/N\n", psThreadData->asConnections[psEvent->u32ConnectionNum].pcName, psEvent->uData.pcText);
#if defined POSIX
                            static struct termios sOldt, sNewt;

                            tcgetattr( STDIN_FILENO, &sOldt);
                            sNewt = sOldt;

                            sNewt.c_lflag &= ~(ICANON);          
                            tcsetattr(STDIN_FILENO, TCSANOW, &sNewt);
                            c = getc(stdin); 
                            tcsetattr( STDIN_FILENO, TCSANOW, &sOldt);
#elif defined WIN32
                            c = _getch();
#endif /* POSIX */
                        }
                        psInputEvent->eType = E_FB_CONFIRMATION;
                        psInputEvent->uData.pcText = malloc(1);
                        psInputEvent->uData.pcText[0] = c;
                        if (eUtils_QueueQueue(&sFeedbackThreadData.sInputQueue, psInputEvent) != E_UTILS_OK)
                        {
                            fprintf(stderr, "Error queueing input\n");
                        }
                        
                        free(psEvent->uData.pcText);
                        break;
                    }
                    
                    case(E_FB_WINDOWSIZE):
                        if ((psThreadData->iVerbosity == NCURSES_VERBOSITY) && (sFeedbackThreadData.iBypassDisplay == 0))
                        {
                            // Redraw the screen if the window size changes
                            endwin();
                            refresh();
                            clear();
                            box(sWindow, ACS_VLINE, ACS_HLINE);
                            mvwaddstr(sWindow, 0, (getmaxx(sWindow) / 2) - (strlen(psThreadData->pcProgramName) / 2), psThreadData->pcProgramName);
                        }
                        break;
                        
                    default:
                        fprintf(stderr, "Unhandled event %d\n", psEvent->eType);
                        break;
                }

                free(psEvent);
            }
            else
            {
                break;
            }
        }
    }
    
    if ((psThreadData->iVerbosity == NCURSES_VERBOSITY) && (sFeedbackThreadData.iBypassDisplay == 0))
    {
        WINDOW *sCloseWindow = NULL;
        int i;
        
        // Flush input buffer before waiting for the next keypress.
        flushinp();
        
        sCloseWindow = newwin(6, 23, (getmaxy(sWindow)/2) - 3, (getmaxx(sWindow) / 2) - 11);
        box(sCloseWindow, ACS_VLINE, ACS_HLINE );
        mvwaddstr(sCloseWindow, 2, 2, "Operations Complete");
        mvwaddstr(sCloseWindow, 3, 5, "Press any key");
        wrefresh(sCloseWindow);
        /* Wait for user to quit by pressing any key */
        getch();
        
        delwin(sCloseWindow);
        
        for (i = 0; i < psThreadData->u32NumConnections; i++)
        {
            delwin(asConWindow[i]);
        }
        
        delwin(sWindow);
        
        /* End curses mode */
        endwin();
    }
    
    return NULL;
}


#if defined WIN32
/* 
 * public domain strtok_r() by Charlie Gordon
 *
 *   from comp.lang.c  9/14/2007
 *
 *      http://groups.google.com/group/comp.lang.c/msg/2ab1ecbb86646684
 *
 *     (Declaration that it's public domain):
 *      http://groups.google.com/group/comp.lang.c/msg/7c7b39328fefab9c
 */

char* strtok_r(
    char *str, 
    const char *delim, 
    char **nextp)
{
    char *ret;

    if (str == NULL)
    {
        str = *nextp;
    }

    str += strspn(str, delim);

    if (*str == '\0')
    {
        return NULL;
    }

    ret = str;

    str += strcspn(str, delim);

    if (*str)
    {
        *str++ = '\0';
    }

    *nextp = str;

    return ret;
}
#endif /* WIN32 */

static void mvwaddstrwrap(WINDOW *window, int y, int x, char *text)
{
    int currentcol = x;
    int currentline = y;
    char *line = NULL;
    char *line_save = NULL;
    
    for (line = strtok_r(text, "\n", &line_save);
         line;
         line = strtok_r(NULL, "\n", &line_save))
    {
        char *word = NULL;
        char *word_save = NULL;
        
        for (word = strtok_r(line, " ", &word_save);
            word;
            word = strtok_r(NULL, " ", &word_save))
        {
            int wordlen = strlen(word);
            
            if ((currentcol + wordlen) >= getmaxx(window))
            {
                currentline++;
                currentcol = 0;
            }
            mvwprintw(window, currentline, currentcol, word);
            
            currentcol += wordlen + 1;
        }
        currentline++;
        currentcol = 0;
    }
}

static void __attribute__((used)) vUI_ResizeHandler(int sig)
{
    tsFeedbackEvent *psEvent = malloc(sizeof(tsFeedbackEvent));
    if (psEvent)
    {
        psEvent->eType = E_FB_WINDOWSIZE;
        if (eUtils_QueueQueue(&sFeedbackThreadData.sFeedbackQueue, psEvent) != E_UTILS_OK)
        {
            fprintf(stderr, "Error queueing event\n");
            free(psEvent);
        }
    }
    return;
}

/****************************************************************************/
/***        END OF FILE                                                   ***/
/****************************************************************************/



