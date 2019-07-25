/****************************************************************************
 *
 * MODULE:             JN51xx Programmer
 *
 * COMPONENT:          Main file
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


#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <getopt.h>
#include <errno.h>
#include <stdarg.h>

#include <sys/time.h>
#include <unistd.h>

#include "Utils.h"
#include "ProgramThread.h"

#define vDelay(a) usleep(a * 1000)

#ifndef VERSION
#error Version is not defined!
#else
static char acProgramName[256] = "NXP JN51xx Device Programmer (Build " VERSION ")";
#endif

#define VERBOSITY_DEFAULT 1

/** Total number of connections */
uint32_t        u32NumConnections   = 0;

static tsProgramThreadArgs sProgramThreadArgs = 
{
    .pcFirmwareFile     = NULL,
    .eEepromErase       = E_ERASE_EEPROM_NONE,
    
    .iInitialSpeed      = 38400,
    .iProgramSpeed      = 1000000,
    .iVerify            = 0,
    .iVerbosity         = VERBOSITY_DEFAULT,
    .iForce             = 0,
};

static void print_usage_exit(char *argv[]);

struct timeval start,finish;

int main(int argc, char *argv[])
{
    tsConnection    *asConnections      = NULL;    
    tsProgramThreadArgs *asThreads      = NULL;
    
    int             iListDevices        = 0;
    int             i;
    int             iExitStatus         = 0;

    {
        static struct option long_options[] =
        {
            {"help",                    no_argument,        NULL,       'h'},
            {"verbosity",               required_argument,  NULL,       'V'},
            {"force",                   no_argument,        NULL,       'Y'},
            {"list",                    no_argument,        NULL,       'l'},
            {"initialbaud",             required_argument,  NULL,       'I'},
            {"programbaud",             required_argument,  NULL,       'P'},
            {"serial",                  required_argument,  NULL,       's'},
            {"selectflash",             required_argument,  NULL,       'S'},
            {"eraseeeprom",             optional_argument,  NULL,        10},
            {"verify",                  no_argument,        NULL,       'v'},
            {"mac",                     required_argument,  NULL,       'm'},
            { NULL, 0, NULL, 0}
        };
        signed char opt;
        int option_index;
        
        while ((opt = getopt_long(argc, argv, "hV:YlI:P:s:S:f:F:e:E:r:vL:m:k:u:", long_options, &option_index)) != -1) 
        {
            switch (opt) 
            {
                case 0:
                    
                case 'h':
                    print_usage_exit(argv);
                    break;
                case 'V':
                    sProgramThreadArgs.iVerbosity = atoi(optarg);
                    break;
                case 'Y':
                    sProgramThreadArgs.iForce = 1;
                    break;
                case 'v':
                    sProgramThreadArgs.iVerify = 1;
                    break;
                case 'l':
                    iListDevices = 1;
                    break;
                case 'I':
                {
                    char *pcEnd;
                    errno = 0;
                    sProgramThreadArgs.iInitialSpeed = strtoul(optarg, &pcEnd, 0);
                    if (errno)
                    {
                        printf("Initial baud rate '%s' cannot be converted to 32 bit integer (%s)\n", optarg, strerror(errno));
                        print_usage_exit(argv);
                    }
                    if (*pcEnd != '\0')
                    {
                        printf("Initial baud rate '%s' contains invalid characters\n", optarg);
                        print_usage_exit(argv);
                    }
                    break;
                }
                case 'P':
                {
                    char *pcEnd;
                    errno = 0;
                    sProgramThreadArgs.iProgramSpeed = strtoul(optarg, &pcEnd, 0);
                    if (errno)
                    {
                        printf("Program baud rate '%s' cannot be converted to 32 bit integer (%s)\n", optarg, strerror(errno));
                        print_usage_exit(argv);
                    }
                    if (*pcEnd != '\0')
                    {
                        printf("Program baud rate '%s' contains invalid characters\n", optarg);
                        print_usage_exit(argv);
                    }
                    break;
                }
                case 's':
                {
                    tsConnection *asNewConnection = realloc(asConnections, sizeof(tsConnection) * (u32NumConnections + 1));
                    if (!asNewConnection)
                    {
                        printf("Memory allocation failure\n");
                        return -1;
                    }
                    asConnections = asNewConnection;
                    
                    asConnections[u32NumConnections].eType   = E_CONNECT_SERIAL;
                    asConnections[u32NumConnections].pcName  = optarg;
                    u32NumConnections++;
                    break;
                }
                case 'S':
                    if (strcmp(optarg, "internal") == 0)
                    {
                        sProgramThreadArgs.u32FlashIndex = 0;
                    }
                    else if (strcmp(optarg, "external") == 0)
                    {
                        sProgramThreadArgs.u32FlashIndex = 1;
                    }
                    else
                    {
                        printf("Unknown flash selection \"%s\"\n", optarg);
                        print_usage_exit(argv);
                    }
                    break;
                case 'f':
                    sProgramThreadArgs.pcFirmwareFile = optarg;
                    break;

                case 10:
                    if (optarg == NULL)
                    {
                        sProgramThreadArgs.eEepromErase = E_ERASE_EEPROM_ALL;
                    }
                    else
                    {
                        if (strcmp(optarg, "full") == 0)
                        {
                            sProgramThreadArgs.eEepromErase = E_ERASE_EEPROM_ALL;
                        }
                        else if (strcmp(optarg, "pdm") == 0)
                        {
                            sProgramThreadArgs.eEepromErase = E_ERASE_EEPROM_PDM;
                        }
                        else
                        {
                            printf("EEPROM erase command '%s' is invalid\n", optarg);
                            print_usage_exit(argv);
                        }
                    }
                    break;
					
                default: /* '?' */
                    print_usage_exit(argv);
            }
        }
    }
     if (iListDevices)
    {
        tsPRG_Context       sContext;
        int         i;
        gettimeofday(&start,NULL);
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

        printf("Total COM Ports = %d\n",u32NumConnections);
        for (i = 0; i < u32NumConnections; i++)
        {
			asConnections[i].eType	 = E_CONNECT_SERIAL;
            printf("%s\n", asConnections[i].pcName);
        }
    }
    
    if (u32NumConnections == 0)
    {
        /* If nothing to do, just print the help & exit */
        print_usage_exit(argv);
    }
    
    asThreads = malloc(sizeof(tsProgramThreadArgs) * u32NumConnections);
    if (!asThreads)
    {
        printf("Memory allocation failure\n");
        return -1;
    }
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

	gettimeofday(&finish,NULL);
	printf("Time Elapsed : %d (s)",finish.tv_sec-start.tv_sec);
	
    free(asConnections);
    free(asThreads);
    return iExitStatus;
}


static void print_usage_exit(char *argv[])
{
    fprintf(stderr, "%s\n", acProgramName);
    fprintf(stderr, "   Flash programmer library version: %s\n", pcPRG_Version);
    fprintf(stderr, "\n Program Options:\n");
    fprintf(stderr, "  -l --list\n");
    fprintf(stderr, "    List available connections\n\n");
    fprintf(stderr, "  -V --verbosity     <verbosity>\n");
    fprintf(stderr, "    Verbosity level. Increse/decrease amount of debug information. Default %d.\n", VERBOSITY_DEFAULT);
    fprintf(stderr, "  -Y --force\n");
    fprintf(stderr, "    Force operation. This option prevents display of the confirmation message\n");
    fprintf(stderr, "    when programming One Time Programmable memory, or loading incompatible files\n");
    fprintf(stderr, "\n Connection options:\n");
    fprintf(stderr, "  -s --serial        <serial device>\n");
    fprintf(stderr, "      Serial device for 15.4 device, e.g. COM1, /dev/ttyS1.\n");
    fprintf(stderr, "      May be specified multiple times for multiple devices.\n\n");
    fprintf(stderr, "  -I --initialbaud   <rate>\n");
    fprintf(stderr, "      Set initial baud rate of serial connection\n\n");
    fprintf(stderr, "  -P --programbaud   <rate>\n");
    fprintf(stderr, "      Set programming baud rate of serial connection\n");
    fprintf(stderr, "\n Programming options:\n");
    fprintf(stderr, "  -S --selectflash   <internal,external>\n");
    fprintf(stderr, "      Choose the flash to interract with.\n\n");
    fprintf(stderr, "  -f --loadflash     <filename>\n");
    fprintf(stderr, "      Load device flash with the given firmware file.\n\n");
    fprintf(stderr, "  -F --dumpflash     <filename>\n");
    fprintf(stderr, "      Dump device flash contents into a file.\n\n");
    fprintf(stderr, "     --flashoffset        <offset>\n");
    fprintf(stderr, "      Load / dump the flash this many bytes from the start.\n\n");
    fprintf(stderr, "     --eraseeeprom=<full,pdm>\n");
    fprintf(stderr, "      Erase the chip EEPROM, pdm only or full EEPROM erase.\n\n");
    fprintf(stderr, "  -e --loadeeprom    <filename>\n");
    fprintf(stderr, "      Load device EEPROM contents from a file.\n\n");
    fprintf(stderr, "  -E --dumpeeprom    <filename>\n");
    fprintf(stderr, "      Dump device EEPROM contents into a file.\n\n");
    fprintf(stderr, "     --eepromoffset        <offset>\n");
    fprintf(stderr, "      Load / dump the EEPROM this many bytes from the start.\n\n");
    fprintf(stderr, "  -r --loadram       <filename>\n");
    fprintf(stderr, "      Load device RAM with the given firmware file, then execute it.\n\n");
    fprintf(stderr, "  -v --verify\n");
    fprintf(stderr, "      If specified, verify the flash image programmed was loaded correctly.\n\n");
    fprintf(stderr, "  -L --license       <license file>\n");
    fprintf(stderr, "      Load mac address, aes key & userdata from licensefile\n\n");
    fprintf(stderr, "  -m --mac           <MAC Address>\n");
    fprintf(stderr, "      Set MAC address of device.\n\n");
    fprintf(stderr, "  -k --key\n");
    fprintf(stderr, "      Display AES key\n\n");
    fprintf(stderr, "  -k --key=<AES Key>\n");
    fprintf(stderr, "      Set AES key. Specify key as 128 bit hexadecimal string\n\n");
    fprintf(stderr, "  -u --userdata      <word>\n");
    fprintf(stderr, "      Display one word of userdata. Word is 0, 1 or 2.\n\n");
    fprintf(stderr, "  -u --userdata      <word=data>\n");
    fprintf(stderr, "      Set one word of userdata. Word is 0, 1 or 2.\n");
    fprintf(stderr, "      Specify data as 128 bit hexadecimal string\n\n");
    fprintf(stderr, "     --deviceconfig\n");
    fprintf(stderr, "      Display device configuration\n\n");
    fprintf(stderr, "     --deviceconfig=<config>\n");
    fprintf(stderr, "      Set device configuration. Configuration is a comma seperated\n");
    fprintf(stderr, "      list of one or more of the following settings:\n");
    fprintf(stderr, "       Enable / Disable JTAG access:   JTAG_ENABLE / JTAG_DISABLE\n");
    fprintf(stderr, "       Set brown out voltage:          VBO_195 (1.95V) / VBO_200 (2.0V)\n");
    fprintf(stderr, "                                       VBO_210 (2.1V)  / VBO_220 (2.2V)\n");
    fprintf(stderr, "                                       VBO_230 (2.3V)  / VBO_240 (2.4V)\n");
    fprintf(stderr, "                                       VBO_270 (2.7V)  / VBO_300 (3.0V)\n");
    fprintf(stderr, "       Bootloader code protection:     CRP_LEVEL0 (None)\n");
    fprintf(stderr, "                                       CRP_LEVEL1 (flash read disabled)\n");
    fprintf(stderr, "                                       CRP_LEVEL2 (all access disabled)\n");
    fprintf(stderr, "       External flash is encrypted:    EXTERNAL_FLASH_NOT_ENCRYPTED\n");
    fprintf(stderr, "                                       EXTERNAL_FLASH_ENCRYPTED\n");
    fprintf(stderr, "       External flash image loading:   EXTERNAL_FLASH_LOAD_ENABLE\n");
    fprintf(stderr, "                                       EXTERNAL_FLASH_LOAD_DISABLE\n");
    exit(EXIT_FAILURE);
}

