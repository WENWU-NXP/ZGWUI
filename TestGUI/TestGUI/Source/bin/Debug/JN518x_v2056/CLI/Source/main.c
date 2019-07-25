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


#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <getopt.h>
#include <errno.h>
#include <stdarg.h>

#include <sys/time.h>
#include <unistd.h>

#include "Utils.h"
#include "UI.h"
#include "ProgramThread.h"
#include "LicenseFile.h"
#include "MemoryRegion.h"

#define vDelay(a) usleep(a * 1000)

#ifndef VERSION
#error Version is not defined!
#else
static char acProgramName[256] = "NXP JN51xx Device Programmer (Build " VERSION ")";
#endif

#define VERBOSITY_DEFAULT 1

/** Total number of connections */
uint32_t        u32NumConnections   = 0;

typedef enum {
    OPT_ERROR         = 0,
    OPT_CONN_PIPE,

    OPT_LIST_DEVICES    = 'l',
    OPT_HELP            = 'h',
    OPT_VERBOSITY       = 'V',
    OPT_FORCE           = 'Y',
    OPT_INITIALBAUD     = 'I',
    OPT_PROGRAMBAUD     = 'P',
    OPT_CONN_SERIAL     = 's',
    OPT_NO_DEVICE_RESET = 'N',
    OPT_MEMORY          = 'm',
    OPT_PROGRAM         = 'p',
    OPT_FLASH           = 'f',
    OPT_DUMP            = 'd',
    OPT_READ            = 'r',
    OPT_WRITE           = 'w',
    OPT_ERASE           = 'e',
    OPT_VERIFY          = 'v',
} teOptions;

static tsProgramThreadArgs sProgramThreadArgs = 
{
    .iInitialSpeed      = 0,
    .iProgramSpeed      = 115200, //Use 115200 instead of 1000000 as we face COM port instabilities with latter value
    .iVerbosity         = VERBOSITY_DEFAULT,
    .iResetDevice       = 1,      //By default, the device will be reset when all operations on the command line are done.
    .u8Mode             = 1,
};

static void print_usage_exit(char *argv[], int is_exit_requested);

static tsOperation *add_operation(tsProgramThreadArgs *psArgs, tsPRG_MemoryRegion *pcDefaultRegion);

int main(int argc, char *argv[])
{
    tsConnection    *asConnections      = NULL;    
    tsProgramThreadArgs *asThreads      = NULL;
    
    int             iListDevices        = 0;
    int             i;
    int             iExitStatus         = 0;

    tsOperation* psOperation = NULL;

    tsPRG_MemoryRegion sDefaultRegion = { "FLASH", 0, 0 };

    {
        static struct option long_options[] =
        {
            {"help",                    no_argument,        NULL,       OPT_HELP},
            {"verbosity",               required_argument,  NULL,       OPT_VERBOSITY},
            {"force",                   no_argument,        NULL,       OPT_FORCE},
            {"list",                    no_argument,        NULL,       OPT_LIST_DEVICES},
            {"initialbaud",             required_argument,  NULL,       OPT_INITIALBAUD},
            {"programbaud",             required_argument,  NULL,       OPT_PROGRAMBAUD},
            {"serial",                  required_argument,  NULL,       OPT_CONN_SERIAL},
            {"pipe",                    required_argument,  NULL,       OPT_CONN_PIPE},
            {"nodevicereset",           no_argument,        NULL,       OPT_NO_DEVICE_RESET},
            {"memory",                  no_argument,        NULL,       OPT_MEMORY},
            {"program",                 required_argument,  NULL,       OPT_PROGRAM},
            {"flash",                   required_argument,  NULL,       OPT_FLASH},
            {"dump",                    required_argument,  NULL,       OPT_DUMP},
            {"write",                   required_argument,  NULL,       OPT_WRITE},
            {"read",                    required_argument,  NULL,       OPT_READ},
            {"erase",                   optional_argument,  NULL,       OPT_ERASE},
            {"verify",                  no_argument,        NULL,       OPT_VERIFY},
            { NULL, 0, NULL, 0}
        };

        signed char opt;
        int option_index;
        teOptions option;
        
        /*
         * This builds the short option string for getopt from the long_options
         * structure to make sure the two sets of options stay in sync.
         */
        static char optstring[sizeof(long_options) / sizeof(struct option) * 2 + 1] = "";

        i = 0;
        for (option_index = 0; long_options[option_index].name; option_index++)
        {
            if (long_options[option_index].val > 0x1F)
            {
                optstring[i++] = long_options[option_index].val;

                if (long_options[option_index].has_arg != no_argument)
                {
                    optstring[i++] = ':';

                    if (long_options[option_index].has_arg == optional_argument)
                    {
                        optstring[i++] = ':';
                    }
                }
            }
        }

        option_index = -1;

        while ((opt = getopt_long(argc, argv, optstring, long_options, &option_index)) != -1)
        {
            if (option_index == -1)
            {
                for (i = 0; long_options[i].name; i++)
                {
                    if (long_options[i].val == opt)
                    {
                        option_index = i;
                        break;
                    }
                }
            }

            if (option_index != -1)
            {
                /* Grab next argument as option argument if it doesn't look like another option */
                if (long_options[option_index].has_arg == optional_argument)
                {
                    if ((optarg == NULL) && (argv[optind] != NULL) && (argv[optind][0] != '-'))
                    {
                        optarg = argv[optind];
                        optind++;
                    }
                }
            }

            option = opt;

            switch (option)
            {
                case OPT_ERROR:
                    
                case OPT_HELP:
                    //Do not exit after displaying the help as device specific help
                    //(needing establishment of a connection to the device) may be output
                    print_usage_exit(argv, 0);
                    sProgramThreadArgs.iListAlias = 1;
                    break;
                case OPT_VERBOSITY:
                    sProgramThreadArgs.iVerbosity = atoi(optarg);
                    break;
                case OPT_FORCE:
                    sProgramThreadArgs.iForce = 1;
                    break;
                case OPT_VERIFY:
                    sProgramThreadArgs.iVerify = 1;
                    break;
                case OPT_LIST_DEVICES:
                    iListDevices = 1;
                    break;
                case OPT_INITIALBAUD:
                {
                    char *pcEnd;
                    errno = 0;
                    sProgramThreadArgs.iInitialSpeed = strtoul(optarg, &pcEnd, 0);
                    if (errno)
                    {
                        printf("Initial baud rate '%s' cannot be converted to 32 bit integer (%s)\n", optarg, strerror(errno));
                        print_usage_exit(argv, 1);
                    }
                    if (*pcEnd != '\0')
                    {
                        printf("Initial baud rate '%s' contains invalid characters\n", optarg);
                        print_usage_exit(argv, 1);
                    }
                    break;
                }
                case OPT_PROGRAMBAUD:
                {
                    char *pcEnd;
                    errno = 0;
                    sProgramThreadArgs.iProgramSpeed = strtoul(optarg, &pcEnd, 0);
                    if (errno)
                    {
                        printf("Program baud rate '%s' cannot be converted to 32 bit integer (%s)\n", optarg, strerror(errno));
                        print_usage_exit(argv, 1);
                    }
                    if (*pcEnd != '\0')
                    {
                        printf("Program baud rate '%s' contains invalid characters\n", optarg);
                        print_usage_exit(argv, 1);
                    }
                    break;
                }
                case OPT_CONN_SERIAL:
                {
                    tsConnection *asNewConnection = realloc(asConnections, sizeof(tsConnection) * (u32NumConnections + 1));
                    if (!asNewConnection)
                    {
                        printf("Memory allocation failure\n");
                        return -1;
                    }
                    asConnections = asNewConnection;
                    
                    memset(&asConnections[u32NumConnections], 0, sizeof(tsConnection));
                    asConnections[u32NumConnections].eType   = E_CONNECT_SERIAL;
                    asConnections[u32NumConnections].pcName  = optarg;
                    u32NumConnections++;
                    break;
                }
                case OPT_CONN_PIPE:
                {
                    tsConnection *asNewConnection = realloc(asConnections, sizeof(tsConnection) * (u32NumConnections + 1));
                    if (!asNewConnection)
                    {
                        printf("Memory allocation failure\n");
                        return -1;
                    }
                    asConnections = asNewConnection;
                    
                    memset(&asConnections[u32NumConnections], 0, sizeof(tsConnection));
                    asConnections[u32NumConnections].eType   = E_CONNECT_PIPE;
                    asConnections[u32NumConnections].pcName  = optarg;
                    u32NumConnections++;
                    break;
                }
                case OPT_NO_DEVICE_RESET:
                    sProgramThreadArgs.iResetDevice = 0;
                    break;
                case OPT_MEMORY:
                    sProgramThreadArgs.iListMemory = 1;

                    break;
                case OPT_FLASH:
                case OPT_PROGRAM:
                {
                    psOperation = add_operation(&sProgramThreadArgs, &sDefaultRegion);
                    psOperation->eOperation = OPERATION_PROGRAM;

                    char *pcData = strchr(optarg, '=');

                    if (pcData != NULL)
                    {
                        *pcData = '\0';
                        pcData++;

                        eMemoryRegionFromString(optarg, psOperation->psMemoryRegion);
                        psOperation->pcMemoryFile = pcData;
                    } else {
                        psOperation->pcMemoryFile = optarg;
                    }

                    break;
                }
                case OPT_DUMP:
                {
                    psOperation = add_operation(&sProgramThreadArgs, &sDefaultRegion);
                    psOperation->eOperation = OPERATION_DUMP;

                    char *pcData = strchr(optarg, '=');

                    if (pcData != NULL)
                    {
                        *pcData = '\0';
                        pcData++;

                        eMemoryRegionFromString(optarg, psOperation->psMemoryRegion);
                        psOperation->pcMemoryFile = pcData;
                    } else {
                        psOperation->pcMemoryFile = optarg;
                    }
                    break;
                }
                case OPT_READ:
                {
                    psOperation     = add_operation(&sProgramThreadArgs, &sDefaultRegion);
                    psOperation->eOperation      = OPERATION_READ;
                    psOperation->pu8MemoryData   = NULL;
                    eMemoryRegionFromString(optarg, psOperation->psMemoryRegion);
                    break;
                }
                case OPT_WRITE:
                {
                    psOperation     = add_operation(&sProgramThreadArgs, &sDefaultRegion);
                    psOperation->eOperation      = OPERATION_WRITE;

                    char *pcData = strchr(optarg, '=');

                    if (pcData == NULL)
                    {
                        fprintf(stderr, "Invalid write argument\n");
                        print_usage_exit(argv, 1);
                    }

                    *pcData = '\0';
                    pcData++;

                    psOperation->u32DataLength = strlen(pcData) / 2;

                    //number of provided hex digits cannot be odd when byte stream has to be provided
                    if (strlen(pcData) & 0x1)
                    {
                        fprintf(stderr, "Data '%s' could not be converted into byte stream (odd number of hex digits)\n", optarg);
                        print_usage_exit(argv, 1);
                    }

                    if (psOperation->u32DataLength > 512)
                    {
                        fprintf(stderr, "Write operation is not supported for more than 512 bytes (requested byte nbr = %d)\n", psOperation->u32DataLength);
                        print_usage_exit(argv, 1);
                    }

                    eMemoryRegionFromString(optarg, psOperation->psMemoryRegion);

                    psOperation->pu8MemoryData = malloc(psOperation->u32DataLength);

                    if (!psOperation->pu8MemoryData)
                    {
                        printf("Memory allocation failure\n");
                        free(psOperation->pu8MemoryData);
                        return -1;
                    }

                    if (iStrToBuffer(pcData, psOperation->u32DataLength, psOperation->pu8MemoryData) <= 0)
                    {
                        fprintf(stderr, "Data '%s' could not be converted into %d byte value\n", optarg, psOperation->u32DataLength);
                        free(psOperation->pu8MemoryData);
                        print_usage_exit(argv, 1);
                    }
                    break;
                }
                case OPT_ERASE:
                {
                    psOperation     = add_operation(&sProgramThreadArgs, &sDefaultRegion);
                    psOperation->eOperation      = OPERATION_ERASE;
                    psOperation->pu8MemoryData   = NULL;

                    eMemoryRegionFromString(optarg, psOperation->psMemoryRegion);
                    break;
                }

                default: /* '?' */
                    print_usage_exit(argv, 1);
            }

            option_index = -1;
        }
    }

	if (iListDevices)
    {
	
        tsPRG_Context       sContext;
        
        if (ePRG_Init(&sContext) != E_PRG_OK)
        {
            fprintf(stderr, "Error initialising context\n");
            return -1;
        }
        
        if (ePRG_ConnectionListInit(&sContext, &u32NumConnections, &asConnections) != E_PRG_OK)
        {
            printf("Error getting connection list: %s\n", pcPRG_GetLastStatusMessage(&sContext));
            return -1;
        }
        printf("Total COMs = %d\n",u32NumConnections);
        for (i = 0; i < u32NumConnections; i++)
        {
			asConnections[i].eType	 = E_CONNECT_SERIAL; //added by patrick
            printf("%s\n", asConnections[i].pcName);
        }
	}
    
    //if help has not been already output and connection failed, output help
    if ((u32NumConnections == 0) && (!sProgramThreadArgs.iListAlias))
    {
        /* If nothing to do, just print the help & exit */
        print_usage_exit(argv, 1);
    }

    asThreads = malloc(sizeof(tsProgramThreadArgs) * u32NumConnections);
    if (!asThreads)
    {
        printf("Memory allocation failure\n");
        return -1;
    }

#if 0 //comment by patrick
    if (eUI_Init(acProgramName, sProgramThreadArgs.iVerbosity, sProgramThreadArgs.iListAlias, u32NumConnections, asConnections) != E_PRG_OK)
    {
        return -1;
    }
#endif

    for (i = 0; i < u32NumConnections; i++)
    {
        memcpy(&asThreads[i], &sProgramThreadArgs, sizeof(tsProgramThreadArgs));
        
        asThreads[i].u32ConnectionNum       = i;
        asThreads[i].sConnection            = asConnections[i];
        asThreads[i].sThread.pvThreadData   = &asThreads[i];
        
        if (eUtils_ThreadStart(pvProgramThread, &asThreads[i].sThread, E_THREAD_JOINABLE) != E_UTILS_OK)
        {
            printf("Error starting thread for device %s\n", asThreads[i].sConnection.pcName);
        }
    }

    for (i = 0; i < u32NumConnections; i++)
    {
        if (eUtils_ThreadWait(&asThreads[i].sThread) != E_UTILS_OK)
        {
            printf("Error joining thread for device %s\n", asConnections[i].pcName);
        }
        
        if (asThreads[i].eStatus != E_PRG_OK)
        {
            /* Increse count of failed devices */
            iExitStatus++;
        }
    }
	
#if 0 //comment by patrick
    if (eUI_Destroy() != E_PRG_OK)
    {
        return -1;
    }
#endif

#if 1 //added by patrick
	uint8_t	FinishedTheadNo=0;

	while (1)
	{
	    for (i = 0; i < u32NumConnections; i++)
	    {
		  if (asThreads[i].sThread.eState==E_THREAD_STOPPED)
		  {
			    asThreads[i].sThread.eState=E_THREAD_STOPPING;
				FinishedTheadNo++;
		  }	
		  Sleep(100);
	    }	
		if (FinishedTheadNo==u32NumConnections)
			break;
	}
#endif	

    //Output device specific help if connection to device has been successful
    if ((sProgramThreadArgs.iListAlias) && (u32NumConnections))
    {
        eMemoryAliasPrintHelp();
    }

    
    free(asConnections);
    free(asThreads);
    return iExitStatus;
}

static tsOperation *add_operation(tsProgramThreadArgs *psArgs, tsPRG_MemoryRegion *pcDefaultRegion)
{
    tsOperation *psOperation = calloc(1, sizeof(tsOperation));

    psOperation->psMemoryRegion = calloc(1, sizeof(tsPRG_MemoryRegion));

    memcpy(psOperation->psMemoryRegion, pcDefaultRegion, sizeof(tsPRG_MemoryRegion));

    tsOperation **ppsHead = &psArgs->psOperations;

    while (*ppsHead != NULL)
    {
        ppsHead = &((*ppsHead)->psNext);
    }

    *ppsHead = psOperation;

    return psOperation;
}

static void print_usage_exit(char *argv[], int is_exit_requested)
{
    fprintf(stderr, "\n%s\n", acProgramName);
    fprintf(stderr, "   Flash programmer library version: %s\n\n", pcPRG_Version);
    fprintf(stderr, "  -h --help\n");
    fprintf(stderr, "    Provides help on the available options. To get device specific options for\n");
    fprintf(stderr, "    device attached to COM port x, combine the -h option with the -s option:\n");
    fprintf(stderr, "      JN518xProgrammer.exe -s COMx -h\n");
    fprintf(stderr, "\n Program Options:\n");
    fprintf(stderr, "  -l --list\n");
    fprintf(stderr, "    List available COM port connections\n\n");
    fprintf(stderr, "  -V --verbosity     <verbosity>\n");
    fprintf(stderr, "    Verbosity level. Increase/decrease amount of debug information. Default %d.\n\n", VERBOSITY_DEFAULT);
    fprintf(stderr, "  -Y --force\n");
    fprintf(stderr, "    Force operation. This option avoids the confirmation dialogue when\n");
    fprintf(stderr, "    programming One Time Programmable memory, or loading incompatible files.\n");
    fprintf(stderr, "\n Connection options:\n");
    fprintf(stderr, "  -s --serial        <serial device>\n");
    fprintf(stderr, "      Serial device for 15.4 device, e.g. COM1, /dev/ttyS1.\n");
    fprintf(stderr, "      May be specified multiple times for multiple devices.\n\n");
    fprintf(stderr, "  -I --initialbaud   <rate>\n");
    fprintf(stderr, "      Set initial baud rate of serial connection\n\n");
    fprintf(stderr, "  -P --programbaud   <rate>\n");
    fprintf(stderr, "      Set programming baud rate of serial connection\n\n");
    fprintf(stderr, "  -N --nodevicereset\n");
    fprintf(stderr, "      Do not reset the device when all operations of the command line are done.\n");
    fprintf(stderr, "      By default, the device is reset: if non-FTDI UART is used the device must\n");
    fprintf(stderr, "      be put back into ISP mode again manually for subsequent commands. If FTDI\n");
    fprintf(stderr, "      UART is used, entering ISP mode after reset is performed automatically.\n");
    fprintf(stderr, "\n Programming options:\n");
    fprintf(stderr, "  -m --memory\n");
    fprintf(stderr, "      List names of the available device memories to use with the following\n");
    fprintf(stderr, "      commands\n\n");
    fprintf(stderr, "  -p --program       [memory][:length][@offset]=<filename>\n");
    fprintf(stderr, "      Program device memory with the given file.\n\n");
    fprintf(stderr, "  -d --dump          [memory][:length][@offset]=<filename>\n");
    fprintf(stderr, "      Dump device memory contents into a file.\n\n");
    fprintf(stderr, "  -w --write         [memory][:length][@offset]=<data>\n");
    fprintf(stderr, "                     <alias>=<data>\n");
    fprintf(stderr, "      Write device memory or alias with the specified data. The data is a byte\n");
    fprintf(stderr, "      stream of maximum 512 bytes, so please provide an even number of\n");
    fprintf(stderr, "      hexadecimal digits. For example, to write 0xcafe, replace <data> field\n");
    fprintf(stderr, "      with cafe. The list of available aliases is shown below in the device\n");
    fprintf(stderr, "      specific help section.\n\n");
    fprintf(stderr, "  -r --read          [memory][:length][@offset]\n");
    fprintf(stderr, "                     <alias>\n");
    fprintf(stderr, "      Read device memory or alias contents. The memory reported content cannot\n");
    fprintf(stderr, "      exceed 512 bytes. The list of available aliases is shown below in the\n");
    fprintf(stderr, "      device specific help section.\n\n");
    fprintf(stderr, "  -e --erase         [memory][:length][@offset]\n");
    fprintf(stderr, "      Erase device memory.\n\n");
    fprintf(stderr, "  -v --verify\n");
    fprintf(stderr, "      If specified, verify the flash image programmed was loaded correctly.\n\n");
    //Do not necessarily exit after displaying the help as device specific help
    //(needing establishment of a connection to the device) may be output
    if (is_exit_requested)
    {
        exit(EXIT_FAILURE);
    }
}

