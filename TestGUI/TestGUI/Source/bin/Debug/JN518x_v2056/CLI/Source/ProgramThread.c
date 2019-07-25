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

/****************************************************************************/
/***        Include files                                                 ***/
/****************************************************************************/

#include <stdint.h>
#include <string.h>
#include <stdlib.h>

#include "ProgramThread.h"
#include "UI.h"
#include "MemoryRegion.h"

#include "programmer.h"

/****************************************************************************/
/***        Macro Definitions                                             ***/
/****************************************************************************/
#define FLASH_PAGE_SIZE             512

#define ISP_MEM_ACCESS_READ         (1 << 0)
#define ISP_MEM_ACCESS_WRITE        (1 << 1)
#define ISP_MEM_ACCESS_ERASE        (1 << 2)
#define ISP_MEM_ACCESS_ERASE_ALL    (1 << 3)
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
teStatus do_erase_if_needed(tsPRG_Context*   sContext, tsMemInfo *sMemInfo, tsProgramThreadArgs *psArgs, uint32_t length, uint32_t offset)
{
    teStatus eStatus = E_PRG_OK;

    //Do an erase before programming a binary or writing byte stream if memory allows it
    if (sMemInfo->u8Access & (ISP_MEM_ACCESS_ERASE|ISP_MEM_ACCESS_ERASE_ALL))
    {
        if ((strcmp(sMemInfo->pcMemName, "FLASH") == 0) || (strcmp(sMemInfo->pcMemName, "Config") == 0))
        {
            //prefer partial to full erase
            if (sMemInfo->u8Access & (ISP_MEM_ACCESS_ERASE))
            {
                vUI_UpdateStatus(psArgs, "Partial erase requested on memory %s, start addr=0x%08x, length=%d\n",sMemInfo->pcMemName, offset, length);
            }
            else if (sMemInfo->u8Access & (ISP_MEM_ACCESS_ERASE_ALL))
            {
                length = sMemInfo->u32Size;
                offset = sMemInfo->u32BaseAddress;
                vUI_UpdateStatus(psArgs, "Full erase requested on memory %s, start addr=0x%08x, length=%d\n",sMemInfo->pcMemName, offset, length);
            }
#if 0 //changed by patrick
            if ((psArgs->eStatus = ePRG_Erase(sContext, length, offset, cbUI_Progress, cbUI_Confirm, psArgs)) != E_PRG_OK)
#else
			if ((psArgs->eStatus = ePRG_Erase(sContext, length, offset, NULL, NULL, psArgs)) != E_PRG_OK)
#endif
            {
                vUI_UpdateStatus(psArgs, "Error: %s", pcPRG_GetLastStatusMessage(sContext));
                eStatus = E_PRG_ERROR;
            }
        }
        else
        {
            vUI_UpdateStatus(psArgs, "No erase requested for memory %s",sMemInfo->pcMemName);
        }
        vUI_UpdateStatus(psArgs, "Completed");
    }
    else
    {
        vUI_UpdateStatus(psArgs, "Error: selected memory %s is not erasable!", sMemInfo->pcMemName);
        eStatus = E_PRG_ERROR;
    }
    return eStatus;
}


teStatus do_operation_if_allowed(tsMemInfo *sMemInfo, tsProgramThreadArgs *psArgs, uint8_t requested_operations)
{
    teStatus eStatus = E_PRG_OK;

    if (requested_operations & (ISP_MEM_ACCESS_READ))
    {
        if (!(sMemInfo->u8Access & ISP_MEM_ACCESS_READ))
        {
            if ((strcmp(sMemInfo->pcMemName, "FLASH") == 0) || (strcmp(sMemInfo->pcMemName, "Config") == 0))
            {
                vUI_UpdateStatus(psArgs, "Error: READ access not allowed on %s memory\n", sMemInfo->pcMemName);
                eStatus = E_PRG_ERROR;
            }
            else // PSECT, pFlash do not require read access when read/modifying it through write operation as it uses the update page mechanism
            {
                if (requested_operations & (ISP_MEM_ACCESS_WRITE))
                {
                    vUI_UpdateStatus(psArgs, "No READ access needed on %s memory\n", sMemInfo->pcMemName);
                }
                else
                {
                    vUI_UpdateStatus(psArgs, "Error: READ access not allowed on %s memory\n", sMemInfo->pcMemName);
                    eStatus = E_PRG_ERROR;
                }
            }
        }
    }

    if (requested_operations & (ISP_MEM_ACCESS_WRITE))
    {
        if (!(sMemInfo->u8Access & ISP_MEM_ACCESS_WRITE))
        {
            vUI_UpdateStatus(psArgs, "Error: WRITE access not allowed on %s memory\n", sMemInfo->pcMemName);
            eStatus = E_PRG_ERROR;
        }
    }

    return eStatus;
}

void *pvProgramThread(tsUtilsThread *psThreadInfo)
{
    tsPRG_Context   sContext;
    tsMemInfo sMemInfo;
    tsProgramThreadArgs *psArgs = (tsProgramThreadArgs*)psThreadInfo->pvThreadData;

    uint32_t u32InitialSpeed;

    if ((psArgs->eStatus = ePRG_Init(&sContext)) != E_PRG_OK)
    {
        fprintf(stderr, "Error initialising context\n");
        return (void*)-1;
    }
    else
    {
        //Disable automatic reset of the device by default
        //if -R (--resetdevice) option is provided in the command line, the device will be reset once
        //all operations in the command line are completed
        sContext.sFlags.bAutoProgramReset = psArgs->iResetDevice;
    }

    switch (psArgs->sConnection.eType)
    {
        case (E_CONNECT_SERIAL):
            psArgs->sConnection.uDetails.sSerial.u32BaudRate = psArgs->iInitialSpeed;
            break;

        default:
            break;
    }


    if ((psArgs->eStatus = ePRG_ConnectionOpen(&sContext, &psArgs->sConnection)) != E_PRG_OK)
    {
        vUI_UpdateStatus(psArgs, "Error opening connection: %s", pcPRG_GetLastStatusMessage(&sContext));
        return (void*)-1;
    }

    if ((psArgs->eStatus = ePRG_ConnectionGet(&sContext, &psArgs->sConnection)) != E_PRG_OK)
    {
        vUI_UpdateStatus(psArgs, "Error: %s", pcPRG_GetLastStatusMessage(&sContext));
        return (void*)-1;
    }

    u32InitialSpeed = psArgs->sConnection.uDetails.sSerial.u32BaudRate;

    vUI_UpdateStatus(psArgs, "Connected at %d", u32InitialSpeed);

    {
        if ((psArgs->eStatus = ePRG_Unlock(&sContext, psArgs->u8Mode, psArgs->pu8UnlockKey, 16)) != E_PRG_OK)
        {
            vUI_UpdateStatus(psArgs, "Error: %s - check cabling and power", pcPRG_GetLastStatusMessage(&sContext));
            return (void*)-1;
        }
    }

    /* Read module details at initial baud rate */
    if ((psArgs->eStatus = ePRG_ChipGetDetails(&sContext)) != E_PRG_OK)
    {
        vUI_UpdateStatus(psArgs, "Error: %s - check cabling and power", pcPRG_GetLastStatusMessage(&sContext));
        goto done_error;
    }

  //  vUI_UpdateDeviceInfo(psArgs, &sContext.sChipDetails); //comment by patrick

    if (psArgs->iListMemory)
    {
        tsMemInfo* psMemInfo = sContext.sChipDetails.psMemInfoList;

        char acMemInfo[50] = "Available memories:";

        while (psMemInfo != NULL)
        {
            strcat(acMemInfo, " ");
            strcat(acMemInfo, psMemInfo->pcMemName);

            psMemInfo = psMemInfo->psNext;
        }

        vUI_UpdateStatus(psArgs, "%s", acMemInfo);
    }

    //Save chip details and leave
    //Chip details will be used to output the relevant device specific help
    if (psArgs->iListAlias)
    {
        eMemorySaveChipId(&sContext.sChipDetails);
        goto done;
    }

    if (u32InitialSpeed != psArgs->iProgramSpeed)
    {
        if (psArgs->iVerbosity > 2)
        {
            vUI_UpdateStatus(psArgs, "Setting baudrate: %d", psArgs->iProgramSpeed);
        }

        psArgs->sConnection.uDetails.sSerial.u32BaudRate = psArgs->iProgramSpeed;

        if ((psArgs->eStatus = ePRG_ConnectionUpdate(&sContext, &psArgs->sConnection)) != E_PRG_OK)
        {
            vUI_UpdateStatus(psArgs, "Error: %s", pcPRG_GetLastStatusMessage(&sContext));
            return (void*)-1;
        }
    }

    tsOperation *psOperation = psArgs->psOperations;

    int iMemIndex = -1;
    int isMemoryProgrammed = 0;

    while (psOperation != NULL)
    {
        tsPRG_MemoryRegion *psRegion = psOperation->psMemoryRegion;

        if (eMemoryAlias(psRegion, sContext.sChipDetails.u32ChipId) != E_PRG_OK)
        {
            vUI_UpdateStatus(psArgs, "Error: Invalid memory region %s", psRegion->pcMemoryName);
            goto done;
        }

        if ((psArgs->eStatus = ePRG_GetMemoryByName(&sContext, &sMemInfo, psRegion->pcMemoryName)) != E_PRG_OK)
        {
            vUI_UpdateStatus(psArgs, "Error: %s", pcPRG_GetLastStatusMessage(&sContext));
            goto done;
        }

        uint32_t u32Length = psRegion->u32RegionLength;

        if (u32Length == 0)
        {
            //vUI_UpdateStatus(psArgs, "u32Length equal 0");
            u32Length = sMemInfo.u32Size - psRegion->u32RegionAddress;
        }

        if (sMemInfo.u8Index != iMemIndex){

            if (iMemIndex != -1)
            {
                //vUI_UpdateStatus(psArgs, "Close memory1: %s", sMemInfo.pcMemName);
                /* Close memory */
                if ((psArgs->eStatus = ePRG_MemoryClose(&sContext)) != E_PRG_OK)
                {
                    vUI_UpdateStatus(psArgs, "Error: %s", pcPRG_GetLastStatusMessage(&sContext));
                    goto done;
                }
            }

            //vUI_UpdateStatus(psArgs, "Open memory1: %s", sMemInfo.pcMemName);
            /* Select memory to access */
            if ((psArgs->eStatus = ePRG_MemoryOpen(&sContext, sMemInfo.u8Index, sMemInfo.u8Access)) != E_PRG_OK)
            {
                vUI_UpdateStatus(psArgs, "Error: %s", pcPRG_GetLastStatusMessage(&sContext));
                goto done;
            }

            iMemIndex = sMemInfo.u8Index;
        }

        vUI_UpdateStatus(psArgs, "Selected memory: %s", sMemInfo.pcMemName);

        /* Set the offsets for flash/eeprom operations */
        sContext.u32FlashOffset     = psRegion->u32RegionAddress;

        switch (psOperation->eOperation)
        {
        case OPERATION_PROGRAM:
        {
            /* Have file to program */
            if ((psArgs->eStatus = ePRG_FwOpen(&sContext, (char *)psOperation->pcMemoryFile)) != E_PRG_OK)
            {
                vUI_UpdateStatus(psArgs, "Error: %s", pcPRG_GetLastStatusMessage(&sContext));
                goto done;
            }


            vUI_UpdateStatus(psArgs, "Programming %s at 0x%x", sMemInfo.pcMemName, sContext.u32FlashOffset);
            //if write operation not allowed
            if (do_operation_if_allowed(&sMemInfo, psArgs, ISP_MEM_ACCESS_WRITE) != E_PRG_OK)
            {
                goto done;
            }
            //Do an erase before programming a binary
            tsFW_Info *psFWImage = &sContext.sFirmwareInfo;
            if (do_erase_if_needed(&sContext, &sMemInfo, psArgs, psFWImage->u32ImageLength, sContext.u32FlashOffset) != E_PRG_OK)
            {
                goto done;
            }
#if 0 //changed by patrick
            if ((psArgs->eStatus = ePRG_Program(&sContext, cbUI_Progress, cbUI_Confirm, psArgs)) != E_PRG_OK)
#else
			if ((psArgs->eStatus = ePRG_Program(&sContext, NULL, NULL, psArgs)) != E_PRG_OK)
#endif
            {
                vUI_UpdateStatus(psArgs, "Error: %s", pcPRG_GetLastStatusMessage(&sContext));
                goto done;
            }
            vUI_UpdateStatus(psArgs, "Memory programmed successfully");

            if (psArgs->iVerify)
            {
                vUI_UpdateStatus(psArgs, "Verifying %s", sMemInfo.pcMemName);
#if 0 //changed by patrick
                if ((psArgs->eStatus = ePRG_Verify(&sContext, cbUI_Progress, psArgs)) != E_PRG_OK)
#else
                if ((psArgs->eStatus = ePRG_Verify(&sContext, NULL, psArgs)) != E_PRG_OK)
#endif
                 {
                    vUI_UpdateStatus(psArgs, "Error: %s", pcPRG_GetLastStatusMessage(&sContext));
                    goto done;
                }
                vUI_UpdateStatus(psArgs, "Memory verified successfully");
            }

            isMemoryProgrammed = 1;
            break;
        }
        case OPERATION_DUMP:
        {
            char *pcExt = NULL;
            char acDumpFlashFile[1024] = "";

            //if read operation not allowed
            if (do_operation_if_allowed(&sMemInfo, psArgs, ISP_MEM_ACCESS_READ) != E_PRG_OK)
            {
                goto done;
            }

            pcExt = strrchr(psOperation->pcMemoryFile, '.');
            if (!pcExt)
            {
                vUI_UpdateStatus(psArgs, "Dump file has no extension (%s)", psOperation->pcMemoryFile);
                goto done;
            }
            else
            {
                int iFileNameLength = pcExt - psOperation->pcMemoryFile;
                memcpy(acDumpFlashFile, psOperation->pcMemoryFile, iFileNameLength);
                sprintf(&acDumpFlashFile[iFileNameLength], "_%02X-%02X-%02X-%02X-%02X-%02X-%02X-%02X%s",
                    sContext.sChipDetails.au8MacAddress[0] & 0xFF, sContext.sChipDetails.au8MacAddress[1] & 0xFF,
                    sContext.sChipDetails.au8MacAddress[2] & 0xFF, sContext.sChipDetails.au8MacAddress[3] & 0xFF,
                    sContext.sChipDetails.au8MacAddress[4] & 0xFF, sContext.sChipDetails.au8MacAddress[5] & 0xFF,
                    sContext.sChipDetails.au8MacAddress[6] & 0xFF, sContext.sChipDetails.au8MacAddress[7] & 0xFF,
                    pcExt);

                vUI_UpdateStatus(psArgs, "Dumping %s into file %s", sMemInfo.pcMemName, acDumpFlashFile);

                if ((psArgs->eStatus = ePRG_Dump(&sContext, acDumpFlashFile, cbUI_Progress, psArgs)) != E_PRG_OK)
                {
                    vUI_UpdateStatus(psArgs, "Error: %s", pcPRG_GetLastStatusMessage(&sContext));
                    goto done;
                }
                vUI_UpdateStatus(psArgs, "Memory dumped successfully");
            }

            break;
        }
        case OPERATION_READ:
        {
            int i;
            char *pcDataString = malloc(u32Length * 2 + 1);

            //if read operation not allowed
            if (do_operation_if_allowed(&sMemInfo, psArgs, ISP_MEM_ACCESS_READ) != E_PRG_OK)
            {
                goto done;
            }

            psOperation->pu8MemoryData = malloc(u32Length);

            if (!psOperation->pu8MemoryData)
            {
                vUI_UpdateStatus(psArgs, "Error: Failed to allocate memory");
                goto done;
            }


            uint32_t region_address     = psRegion->u32RegionAddress;
            uint32_t length_to_read     = u32Length;
            uint32_t offset_read        = 0;
            uint32_t offset_page        = psRegion->u32RegionAddress%FLASH_PAGE_SIZE;
            uint32_t address_page       = psRegion->u32RegionAddress - offset_page;

            vUI_UpdateStatus(psArgs, "Reading %d bytes from %s at %08x", length_to_read, sMemInfo.pcMemName, region_address);

            //Workaround to fix issue in ISP for FLASH and Config memory section access when address to read is offset
            //more than 256 bytes from a page boundary address
            if (offset_page >= 256)
            {
                //Read a full page
                offset_read    = offset_page;
                region_address = address_page;
                // Workaround for pFLASH and PSECT, where 'page' is smaller than FLASH_PAGE_SIZE
                if (sMemInfo.u32Size < FLASH_PAGE_SIZE)
                {
                    length_to_read = sMemInfo.u32Size;
                }
                else
                {
                    length_to_read = FLASH_PAGE_SIZE;
                }
                //vUI_UpdateStatus(psArgs, "Reading %d bytes from %s at %08x", length_to_read, sMemInfo.pcMemName, region_address);

                free(psOperation->pu8MemoryData);
                psOperation->pu8MemoryData = malloc(length_to_read);
            }
#if 0 //changed by patrick
            if ((psArgs->eStatus = ePRG_Read(&sContext, psOperation->pu8MemoryData, length_to_read, region_address, cbUI_Progress, cbUI_Confirm, psArgs)) != E_PRG_OK)
#else
            if ((psArgs->eStatus = ePRG_Read(&sContext, psOperation->pu8MemoryData, length_to_read, region_address, NULL, NULL, psArgs)) != E_PRG_OK)
#endif
            {
                vUI_UpdateStatus(psArgs, "Error: %s", pcPRG_GetLastStatusMessage(&sContext));
                free(psOperation->pu8MemoryData);
                goto done;
            }

            for (i = 0; i < u32Length; i++)
            {
                snprintf(&pcDataString[i * 2], 3, "%02x", psOperation->pu8MemoryData[i+offset_read]);
            }
            free(psOperation->pu8MemoryData);
            vUI_UpdateStatus(psArgs, "%s = %s", psOperation->psMemoryRegion->pcMemoryName, pcDataString);
            break;
        }
        case OPERATION_WRITE:
        {
            //if read/write operation not allowed
            if (do_operation_if_allowed(&sMemInfo, psArgs, ISP_MEM_ACCESS_READ|ISP_MEM_ACCESS_WRITE) != E_PRG_OK)
            {
                goto done;
            }


            uint8_t*  pu8DataToWrite = psOperation->pu8MemoryData;
            vUI_UpdateStatus(psArgs, "Programming %s", sMemInfo.pcMemName);

            //Workaround to fix issue in ISP for FLASH and Config memory section access when address to write is offset
            //more than 256 bytes from a page boundary address
            //Also used when offset into page is non-zero
            uint32_t region_address     = psRegion->u32RegionAddress;
            uint32_t length_to_write    = u32Length;
            uint32_t offset_page        = psRegion->u32RegionAddress%FLASH_PAGE_SIZE;
            uint32_t address_page       = psRegion->u32RegionAddress - offset_page;

            if (   (offset_page >= 256)
                || (   ((strcmp(sMemInfo.pcMemName, "FLASH") == 0) || (strcmp(sMemInfo.pcMemName, "Config") == 0))
                    && (offset_page > 0)
                    && ((offset_page + length_to_write) <= FLASH_PAGE_SIZE)
                   )
               )
            {
                uint8_t* read_buffer;
                uint32_t length_to_access;

                //Read and write a full page
                region_address = address_page;
                // Workaround for pFLASH and PSECT, where 'page' is smaller than FLASH_PAGE_SIZE
                if (sMemInfo.u32Size < FLASH_PAGE_SIZE)
                {
                    length_to_access = sMemInfo.u32Size;
                }
                else
                {
                    length_to_access = FLASH_PAGE_SIZE;
                }

                read_buffer = malloc(length_to_access);

                //Read full page
#if 0 //changed by patrick
                if ((psArgs->eStatus = ePRG_Read(&sContext, read_buffer, length_to_access, address_page, cbUI_Progress, cbUI_Confirm, psArgs)) != E_PRG_OK)
#else
				if ((psArgs->eStatus = ePRG_Read(&sContext, read_buffer, length_to_access, address_page, NULL, NULL, psArgs)) != E_PRG_OK)
#endif
                {
                    vUI_UpdateStatus(psArgs, "Error: %s", pcPRG_GetLastStatusMessage(&sContext));
                    goto done;
                }
                vUI_UpdateStatus(psArgs, "Programming full page of %s, reg_addr=0x%08x, length=%d", sMemInfo.pcMemName, psRegion->u32RegionAddress, length_to_access);
                //Memset to 0 the length of byte (u32Length) to write at address (psRegion->u32RegionAddress)
                memset(&read_buffer[offset_page], 0, length_to_write);
                memcpy(&read_buffer[offset_page], psOperation->pu8MemoryData, psOperation->u32DataLength);
                pu8DataToWrite = read_buffer;
                length_to_write = length_to_access;
            }
            else
            {
                //If operation length (length of bytes provided by user) is smaller than alias size then pad with 0s
                if (psOperation->u32DataLength < length_to_write)
                {
                    vUI_UpdateStatus(psArgs, "Programming alias_length=%d (operation length was %d)", length_to_write, psOperation->u32DataLength);
                    pu8DataToWrite = malloc(length_to_write);
                    memset(pu8DataToWrite, 0, length_to_write);
                    memcpy(pu8DataToWrite, psOperation->pu8MemoryData, psOperation->u32DataLength);
                }
            }

            //Do an erase before programming a page in Config or FLASH memory sections
            if (do_erase_if_needed(&sContext, &sMemInfo, psArgs, length_to_write, region_address) != E_PRG_OK)
            {
                free(pu8DataToWrite);
                goto done;
            }
#if 0 //changed by patrick
            if ((psArgs->eStatus = ePRG_Write(&sContext, pu8DataToWrite, length_to_write, region_address, cbUI_Progress, cbUI_Confirm, psArgs)) != E_PRG_OK)
#else
			if ((psArgs->eStatus = ePRG_Write(&sContext, pu8DataToWrite, length_to_write, region_address, NULL, NULL, psArgs)) != E_PRG_OK)
#endif
            {
                vUI_UpdateStatus(psArgs, "Error: %s", pcPRG_GetLastStatusMessage(&sContext));
                free(pu8DataToWrite);
                goto done;
            }
            free(pu8DataToWrite);
            vUI_UpdateStatus(psArgs, "Memory programmed successfully");

            if (psArgs->iVerify)
            {
                vUI_UpdateStatus(psArgs, "Verifying %s", sMemInfo.pcMemName);
#if 0 //changed by patrick
                if ((psArgs->eStatus = ePRG_Verify(&sContext, cbUI_Progress, psArgs)) != E_PRG_OK)
#else
				if ((psArgs->eStatus = ePRG_Verify(&sContext, NULL, psArgs)) != E_PRG_OK)
#endif
                {
                    vUI_UpdateStatus(psArgs, "Error: %s", pcPRG_GetLastStatusMessage(&sContext));
                    goto done;
                }
                vUI_UpdateStatus(psArgs, "Memory verified successfully");
            }
            isMemoryProgrammed = 1;
            break;
        }
        case OPERATION_ERASE:
        {
            if (isMemoryProgrammed)
            {
                /*Do an exlicit close to fix issue*/
                if (iMemIndex != -1)
                {
                    //vUI_UpdateStatus(psArgs, "Close memory2: %s", sMemInfo.pcMemName);
                    /* Close memory */
                    if ((psArgs->eStatus = ePRG_MemoryClose(&sContext)) != E_PRG_OK)
                    {
                        vUI_UpdateStatus(psArgs, "Error: %s", pcPRG_GetLastStatusMessage(&sContext));
                        goto done;
                    }
                }
                //vUI_UpdateStatus(psArgs, "Open memory2: %s", sMemInfo.pcMemName);
                /* Select memory to access */
                if ((psArgs->eStatus = ePRG_MemoryOpen(&sContext, sMemInfo.u8Index, sMemInfo.u8Access)) != E_PRG_OK)
                {
                    vUI_UpdateStatus(psArgs, "Error: %s", pcPRG_GetLastStatusMessage(&sContext));
                    goto done;
                }
            }
            vUI_UpdateStatus(psArgs, "Erasing %s", sMemInfo.pcMemName);
            if (do_erase_if_needed(&sContext, &sMemInfo, psArgs, u32Length, psRegion->u32RegionAddress) != E_PRG_OK)
            {
                goto done;
            }
            break;
        }
        }

        psOperation = psOperation->psNext;
    }

    if (iMemIndex != -1)
    {
        //Keep commented else read value not correctly reported
        //vUI_UpdateStatus(psArgs, "Close memory3: %s", sMemInfo.pcMemName);
        /* Close memory */
        if ((psArgs->eStatus = ePRG_MemoryClose(&sContext)) != E_PRG_OK)
        {
            vUI_UpdateStatus(psArgs, "Error: %s", pcPRG_GetLastStatusMessage(&sContext));
            goto done;
        }
    }

done:
	if (u32InitialSpeed != psArgs->iProgramSpeed)
    {
        if (psArgs->iVerbosity > 2)
        {
            vUI_UpdateStatus(psArgs, "Setting baudrate: %d", u32InitialSpeed);
        }

        psArgs->sConnection.uDetails.sSerial.u32BaudRate = u32InitialSpeed;

        if (ePRG_ConnectionUpdate(&sContext, &psArgs->sConnection) != E_PRG_OK)
        {
            vUI_UpdateStatus(psArgs, "Error: %s", pcPRG_GetLastStatusMessage(&sContext));
            vUI_UpdateStatus(psArgs, "Error setting baudrate - check cabling and power");
            return (void*)-1;
        }
    }

done_error:
    if (ePRG_ConnectionClose(&sContext) != E_PRG_OK)
    {
        return (void*)-1;
    }

    if (ePRG_Destroy(&sContext) != E_PRG_OK)
    {
        return (void*)-1;
    }

    return (void*)0;
}


/****************************************************************************/
/***        Local Functions                                               ***/
/****************************************************************************/

/****************************************************************************/
/***        END OF FILE                                                   ***/
/****************************************************************************/



