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
 * Copyright NXP B.V. 2016. All rights reserved
 ****************************************************************************/
    
#ifndef CLI_ALIAS_CONFIG_COMMON_H_
#define CLI_ALIAS_CONFIG_COMMON_H_

#if defined __cplusplus
extern "C" {
#endif

/****************************************************************************/
/***        Include Files                                                 ***/
/****************************************************************************/
#include "rom_psector.h"
/****************************************************************************/
/***        Macro Definitions                                             ***/
/****************************************************************************/
#define ALIAS_CONFIG_NBR     7
#define OFFSET_ALIAS(type, member)      (offsetof(type, member)-sizeof(psector_header_t))
#define SIZEOF_ALIAS(type, member)      (sizeof(((type *)0)->member))
#define INFO_ALIAS(type, member)        OFFSET_ALIAS(type, member), SIZEOF_ALIAS(type, member)
#define INFO_ARRAY_ALIAS(index, type, member, size_record)        (OFFSET_ALIAS(type, member)+(index*size_record)), size_record
/****************************************************************************/
/***        Type Definitions                                              ***/
/****************************************************************************/
typedef struct {
    const char *alias;
    tsPRG_MemoryRegion sRegion;
    const char *alias_help;
} tsMemoryAlias;

typedef struct {
    const tsMemoryAlias*  alias;
    uint32_t        chip_id;
    uint32_t        alias_nbr;
}
tsMemoryAliasConfig;
/****************************************************************************/
/***        Exported Functions                                            ***/
/****************************************************************************/


/****************************************************************************/
/***        Exported Variables                                            ***/
/****************************************************************************/

#if defined __cplusplus
}
#endif

#endif /* CLI_ALIAS_CONFIG_COMMON_H_ */

/****************************************************************************/
/***        END OF FILE                                                   ***/
/****************************************************************************/
