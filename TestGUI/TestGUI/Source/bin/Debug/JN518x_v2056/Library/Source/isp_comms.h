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
    
#ifndef ISP_COMMS_H_
#define ISP_COMMS_H_

#if defined __cplusplus
extern "C" {
#endif

/****************************************************************************/
/***        Include Files                                                 ***/
/****************************************************************************/

#include <stdint.h>

#include "portable_endian.h"

/****************************************************************************/
/***        Macro Definitions                                             ***/
/****************************************************************************/

/****************************************************************************/
/***        Type Definitions                                              ***/
/****************************************************************************/

typedef struct
{
	uint8_t *buffer;
	uint32_t offset;
	uint32_t length;
} ISP_BUFFER_T;

/****************************************************************************/
/***        Exported Functions                                            ***/
/****************************************************************************/

static inline void putBuffer(const void *data, uint32_t length, uint8_t **pu8Store)
{
	memcpy(*pu8Store, data, length);
	*pu8Store += length;
}

static inline void getBuffer(void *data, uint32_t length, uint8_t **pu8Store)
{
	memcpy(data, *pu8Store, length);
	*pu8Store += length;
}

static inline void put32b(uint32_t u32Host, uint8_t **pu8Store)
{
    uint32_t u32Be = htobe32(u32Host);
    memcpy(*pu8Store, &u32Be, sizeof(uint32_t));
    *pu8Store += sizeof(uint32_t);
}

static inline void put32l(uint32_t u32Host, uint8_t **pu8Store)
{
    uint32_t u32Le = htole32(u32Host);
    memcpy(*pu8Store, &u32Le, sizeof(uint32_t));
    *pu8Store += sizeof(uint32_t);
}

static inline uint32_t get32b(uint8_t **pu8Store)
{
   uint32_t u32Be;
   memcpy(&u32Be, *pu8Store, sizeof(uint32_t));
   *pu8Store += sizeof(uint32_t);
   return be32toh(u32Be);
}

static inline uint32_t be32btoh(uint8_t *pu8Store)
{
   uint32_t u32Be;
   memcpy(&u32Be, pu8Store, sizeof(uint32_t));
   return be32toh(u32Be);
}

static inline uint32_t get32l(uint8_t **pu8Store)
{
   uint32_t u32Le;
   memcpy(&u32Le, *pu8Store, sizeof(uint32_t));
   *pu8Store += sizeof(uint32_t);
   return le32toh(u32Le);
}

static inline void put16b(uint16_t u16Host, uint8_t **pu8Store)
{
    uint16_t u16Be = htobe16(u16Host);
    memcpy(*pu8Store, &u16Be, sizeof(uint16_t));
    *pu8Store += sizeof(uint16_t);
}

static inline void put16l(uint16_t u16Host, uint8_t **pu8Store)
{
    uint16_t u16Le = htole16(u16Host);
    memcpy(*pu8Store, &u16Le, sizeof(uint16_t));
    *pu8Store += sizeof(uint16_t);
}

static inline uint16_t get16b(uint8_t **pu8Store)
{
   uint16_t u16Be;
   memcpy(&u16Be, *pu8Store, sizeof(uint16_t));
   *pu8Store += sizeof(uint16_t);
   return be16toh(u16Be);
}

static inline uint16_t be16btoh(uint8_t *pu8Store)
{
   uint16_t u16Be;
   memcpy(&u16Be, pu8Store, sizeof(uint16_t));
   return be16toh(u16Be);
}

static inline uint16_t get16l(uint8_t **pu8Store)
{
   uint16_t u16Le;
   memcpy(&u16Le, *pu8Store, sizeof(uint16_t));
   *pu8Store += sizeof(uint16_t);
   return le16toh(u16Le);
}

static inline void put8(uint8_t u8Host, uint8_t **pu8Store)
{
    **pu8Store = u8Host;
    *pu8Store += sizeof(uint8_t);
}

static inline uint8_t get8(uint8_t **pu8Store)
{
   uint8_t u8Be = **pu8Store;
   *pu8Store += sizeof(uint8_t);
   return u8Be;
}

/****************************************************************************/
/***        Exported Variables                                            ***/
/****************************************************************************/

#if defined __cplusplus
}
#endif

#endif /* ISP_COMMS_H_ */

/****************************************************************************/
/***        END OF FILE                                                   ***/
/****************************************************************************/
