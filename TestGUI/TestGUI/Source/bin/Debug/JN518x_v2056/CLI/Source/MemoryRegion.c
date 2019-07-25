/****************************************************************************
 *
 * This software is owned by NXP B.V. and/or its supplier and is protected
 * under applicable copyright laws. All rights are reserved. We grant You,
 * and any third parties, a license to use this software solely and
 * exclusively on NXP products [NXP Microcontrollers such as JN5168, JN5179].
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
 *
 * Copyright NXP B.V. 2016-2018. All rights reserved
 ****************************************************************************/
    
    
/****************************************************************************/
/***        Include files                                                 ***/
/****************************************************************************/

#include <stdint.h>
#include <string.h>
#include <stdlib.h>
#include <stddef.h>
#include <stdio.h>
#include <limits.h>

#include "MemoryRegion.h"
#include "LicenseFile.h"
#include "ChipID.h"
#include "AliasConfigJN5189ES2.h"
#include "AliasConfigJN5189ES1.h"

/****************************************************************************/
/***        Macro Definitions                                             ***/
/****************************************************************************/

/****************************************************************************/
/***        Type Definitions                                              ***/
/****************************************************************************/

/****************************************************************************/
/***        Local Function Prototypes                                     ***/
/****************************************************************************/
teStatus eMemoryGetAliasConfig(uint32_t* alias_config_idx, uint32_t *alias_nbr, uint32_t chip_id);
uint16_t u16LenToNextChar(const char *pcString, const char cSearch);

/****************************************************************************/
/***        Exported Variables                                            ***/
/****************************************************************************/

/****************************************************************************/
/***        Local Variables                                               ***/
/****************************************************************************/

/* Lookup for alias data: note that JN518x_ES2, QN90x0 and K32W0x1 all have
   the same data */
tsMemoryAliasConfig config[ALIAS_CONFIG_NBR] = {
    {asAliases_JN5189ES1, CHIP_ID_JN5189_ES1, sizeof(asAliases_JN5189ES1) / sizeof(asAliases_JN5189ES1[0])},
    {asAliases_JN5189ES2, CHIP_ID_JN5188_ES2, sizeof(asAliases_JN5189ES2) / sizeof(asAliases_JN5189ES2[0])},
    {asAliases_JN5189ES2, CHIP_ID_JN5189_ES2, sizeof(asAliases_JN5189ES2) / sizeof(asAliases_JN5189ES2[0])},
    {asAliases_JN5189ES2, CHIP_ID_QN9030,     sizeof(asAliases_JN5189ES2) / sizeof(asAliases_JN5189ES2[0])},
    {asAliases_JN5189ES2, CHIP_ID_QN9090,     sizeof(asAliases_JN5189ES2) / sizeof(asAliases_JN5189ES2[0])},
    {asAliases_JN5189ES2, CHIP_ID_K32W041,    sizeof(asAliases_JN5189ES2) / sizeof(asAliases_JN5189ES2[0])},
    {asAliases_JN5189ES2, CHIP_ID_K32W061,    sizeof(asAliases_JN5189ES2) / sizeof(asAliases_JN5189ES2[0])}
    };

static tsChipDetails chip_details = {0};
/****************************************************************************/
/***        Exported Functions                                            ***/
/****************************************************************************/
teStatus eMemorySaveChipId(tsChipDetails *chip_info)
{
    memcpy(&chip_details, chip_info, sizeof(tsChipDetails));
    return E_PRG_OK;
}
teStatus eMemoryAliasPrintHelp()
{
    int status = E_PRG_ERROR;
    int i = 0;
    int j;
    char const *pcString;
    uint16_t u16Offset;
    uint16_t u16LenToNextSpace;
    uint16_t u16LenToNextLine;
    uint16_t u16LenToEnd;
    uint16_t u16MinLen;

    const char* chip_name = chip_details.pcChipName;
    const tsMemoryAlias *asAliases;
    uint32_t alias_nbr = 0;
    uint32_t alias_config_idx;

    if (!chip_details.u32ChipId)
    {
        fprintf(stderr, "\n Undetermined chip id!\n");
        return E_PRG_ERROR;
    }

    status = eMemoryGetAliasConfig(&alias_config_idx, &alias_nbr, chip_details.u32ChipId);
    asAliases = config[alias_config_idx].alias;

    //clear chip details structure
    memset(&chip_details, 0, sizeof(tsChipDetails));

    if (status != E_PRG_OK)
    {
        fprintf(stderr, "\n No alias supported for %s device.\n", chip_name);
        return E_PRG_ERROR;
    }

    fprintf(stderr, "\n Device specific help:\n");
    fprintf(stderr, " Aliases supported on %s device:\n",chip_name);
    fprintf(stderr, "  If the number of bytes to be written to an alias do not match the\n");
    fprintf(stderr, "  length of the alias's data then: \n");
    fprintf(stderr, "   - if bytes to write < expected bytes, unset bytes will be set to 0.\n");
    fprintf(stderr, "   - if bytes to write > expected bytes, remaining bytes are discarded.\n");
    fprintf(stderr, "  Write of an alias to the targeted memory is performed on next device\n" );
    fprintf(stderr, "  reset. It is recommended to avoid the -N option when writing aliases.\n\n");
    fprintf(stderr, "  Alias           Bytes Description\n\n");
    for (i = 0; i < alias_nbr; i++)
    {
        /* Print then pad out name to 17 bytes (maximum found manually) */
        fprintf(stderr, "  %s", asAliases[i].alias);
        j = 17 - strlen(asAliases[i].alias);
        while (j > 0)
        {
            fprintf(stderr, " ");
            j--;
        }

        /* Print bytes (maximum 3 digits) */
        fprintf(stderr, " %3d ", asAliases[i].sRegion.u32RegionLength);

        /* Print text, formatted to fit line */
        pcString = asAliases[i].alias_help;
        u16Offset = 24;

        /* Look for end of next word */
        u16LenToNextSpace = u16LenToNextChar(pcString, ' ');
        u16LenToNextLine = u16LenToNextChar(pcString, '\n');
        u16LenToEnd = strlen(pcString);
    
        do
        {
            if (u16LenToEnd < u16LenToNextLine)
            {
                u16LenToNextLine = u16LenToEnd;
            }
            if (u16LenToNextSpace < u16LenToNextLine)
            {
                u16MinLen = u16LenToNextSpace;
            }
            else
            {
                u16MinLen = u16LenToNextLine;
            }
            
            if ((u16Offset + u16MinLen) > 79U)
            {
                /* Won't fit, so move to next line and pad */
                fprintf(stderr, "\n                        ");
                u16Offset = 24;
            }
            
            /* Print word */
            for (j = 0; j < u16MinLen; j++)
            {
                if (*pcString == '\n')
                {
                    fprintf(stderr, "\n                        ");
                    u16Offset = 24;
                }
                else
                {
                    (void)fputc(*pcString, stderr);
                    u16Offset++;
                }
                pcString++;
            }

            u16LenToNextSpace -= u16MinLen;
            u16LenToNextLine -= u16MinLen;
            u16LenToEnd -= u16MinLen;
            
            if (u16LenToNextSpace == 0)
            {
                u16LenToNextSpace = u16LenToNextChar(pcString, ' ');
            }
            if (u16LenToNextLine == 0)
            {
                u16Offset = 24;
                u16LenToNextLine = u16LenToNextChar(pcString, '\n');
            }                
        } while (u16LenToEnd > 0);
        
        fprintf(stderr, "\n");
    }
    return status;
}
teStatus eMemoryRegionFromString(const char *pcOptionString, tsPRG_MemoryRegion *psRegion)
{
    teStatus eStatus = E_PRG_OK;

    char *pcEnd;
    uint32_t u32Size = ULONG_MAX;
    uint32_t u32Offset = 0;

    if (pcOptionString == NULL)
    {
        return E_PRG_NULL_PARAMETER;
    }

    char *pcSize   = strchr(pcOptionString, ':');
    char *pcOffset = strchr(pcOptionString, '@');

    if (pcOffset != NULL)
    {
        *pcOffset = '\0';
        pcOffset++;

        errno = 0;
        psRegion->u32RegionAddress = strtoul(pcOffset, &pcEnd, 0);
    }

    if (pcSize != NULL)
    {

        *pcSize = '\0';
        pcSize++;

        errno = 0;
        psRegion->u32RegionLength = strtoul(pcSize, &pcEnd, 0);
    }


    if (pcOffset == NULL)
    {
        errno = 0;
        u32Offset = strtoul(pcOptionString, &pcEnd, 0);

        if (*pcEnd == '\0')
        {
            psRegion->u32RegionAddress = u32Offset;
            pcOffset = (char *)pcOptionString;
        }
    }

    if ((pcOffset != pcOptionString) && (pcSize == NULL))
    {
        errno = 0;
        u32Size = strtoul(pcOptionString, &pcEnd, 0);

        if (*pcEnd == '\0')
        {
            psRegion->u32RegionLength = u32Size;
            pcSize = (char *)pcOptionString;
        }
    }

    if ((pcOffset != pcOptionString) && (pcSize != pcOptionString))
    {
        psRegion->pcMemoryName = (char *)pcOptionString;
    }

    return eStatus;
}

teStatus eMemoryAlias(tsPRG_MemoryRegion *psRegion, uint32_t chip_id)
{
    int i;
    int status = E_PRG_ERROR;

    const tsMemoryAlias *asAliases;
    uint32_t alias_nbr = 0;
    uint32_t alias_config_idx;

    status = eMemoryGetAliasConfig(&alias_config_idx, &alias_nbr, chip_id);
    asAliases = config[alias_config_idx].alias;

    if (status != E_PRG_OK)
    {
        return E_PRG_ERROR;
    }

    for (i = 0; i < alias_nbr; i++)
    {
        if (stricmp(psRegion->pcMemoryName, asAliases[i].alias) == 0)
        {
            if (psRegion->u32RegionAddress + psRegion->u32RegionLength > asAliases[i].sRegion.u32RegionAddress + asAliases[i].sRegion.u32RegionLength)
            {
                return E_PRG_ERROR;
            }
            psRegion->pcMemoryName = asAliases[i].sRegion.pcMemoryName;
            if (psRegion->u32RegionLength == 0)
            {
                psRegion->u32RegionLength = asAliases[i].sRegion.u32RegionLength - psRegion->u32RegionAddress;
            }
            psRegion->u32RegionAddress += asAliases[i].sRegion.u32RegionAddress;
            break;
        }
    }

    return E_PRG_OK;
}

/****************************************************************************/
/***        Local Functions                                               ***/
/****************************************************************************/
teStatus eMemoryGetAliasConfig(uint32_t* alias_config_idx, uint32_t *alias_nbr, uint32_t chip_id)
{
    int status = E_PRG_ERROR;

    for (int j = 0; j<ALIAS_CONFIG_NBR; j++)
    {
        if (config[j].chip_id == chip_id)
        {
            *alias_config_idx = j;
            *alias_nbr = config[j].alias_nbr;
            //printf("Alias offset for %s is 0x%08x, length=%d\n",asAliases->alias, asAliases->sRegion.u32RegionAddress, asAliases->sRegion.u32RegionLength);
            status = E_PRG_OK;
            break;
        }
    }
    return status;
}

uint16_t u16LenToNextChar(const char *pcString, const char cSearch)
{
    char *pcFound;
   
    pcFound = strchr(pcString, cSearch);
    
    if (pcFound == NULL)
    {
        return 65535U;
    }
    
    return ((uint16_t)(pcFound - pcString)) + 1U;
}

/****************************************************************************/
/***        END OF FILE                                                   ***/
/****************************************************************************/
