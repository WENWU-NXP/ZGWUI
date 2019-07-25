/****************************************************************************
 *
 * This software is owned by NXP B.V. and/or its supplier and is protected
 * under applicable copyright laws. All rights are reserved. We grant You,
 * and any third parties, a license to use this software solely and
 * exclusively on NXP products [NXP Microcontrollers such as JN5179, JN5189].
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
 /*
 * psector.h
 *
 *  Created on: 27 Apr 2016
 *      Author: nxp29781
 */

#ifndef ROM_PSECTOR_H_
#define ROM_PSECTOR_H_

#if defined __cplusplus
extern "C" {
#endif

/****************************************************************************/
/***        Include Files                                                 ***/
/****************************************************************************/

#include <stddef.h>
#include "rom_common.h"

/****************************************************************************/
/***        Macro Definitions                                             ***/
/****************************************************************************/

#define PSECTOR_PAGE_WORDS 30

#define PSECTOR_PAGE0_MAGIC  0xc51d8ca9
#define PSECTOR_PFLASH_MAGIC 0xa7b4353d  

#ifdef __GNUC__
#define PSECT_READ(page, page_type, var)                                                    \
( {                                                                                         \
    typeof((((page_type *)0)->var)) _a;                                                     \
    psector_ReadData(page, offsetof(page_type, var), sizeof((((page_type *)0)->var)), &_a); \
    _a;                                                                                     \
} )
#endif

#define ROM_SEC_BOOT_AUTH_ON_UPGRADE ((1 << 0) | ROM_SEC_BOOT_AUTH_ON_BOOT)
#define ROM_SEC_BOOT_AUTH_ON_BOOT (1 << 1)
#define ROM_SEC_BOOT_PREVENT_DOWNGRADE (1 << 2)
#define ROM_SEC_BOOT_USE_NXP_KEY (1 << 3)

/****************************************************************************/
/***        Type Definitions                                              ***/
/****************************************************************************/

typedef enum {
    
    PSECTOR_PAGE0_PART,
    PSECTOR_PFLASH_PART,
    MAX_PSECTOR_PARTITIONS,
} psector_partition_id_t;

typedef enum
{
    PAGE_STATE_BLANK,     /*< Page has never been programmed or has been erased */
    PAGE_STATE_ERROR,     /*< Both subpages constituting the psector contain unrecoverable errors that ECC/parity cannot mend */
    PAGE_STATE_DEGRADED,  /*< One subpage contains unrecoverable errors or is blank  */
    PAGE_STATE_OK,        /*< Both subpages are correct */
    
} psector_page_state_t;

typedef enum
{
    UPDATE_MODE_NONE = 0b0000,
    UPDATE_MODE_ANY  = 0b0001,  /*< Flash word (line) fields unrestrained changes */
    UPDATE_MODE_INC  = 0b0101,  /*< Flash word (line) fields can only be incremented  */
    UPDATE_MODE_DEC  = 0b0110,  /*< Flash word (line) fields can only be decremented  */
    UPDATE_MODE_BITS = 0b1010,  /*< Flash word (line) fileds can only undergo bit set operations  */
    UPDATE_MODE_BITC = 0b1100,  /*< Flash word (line) fields can only undergo bit clear operations  */
    UPDATE_MODE_OTP  = 0b1111,  /*< Flash word (line) fields are one-time-programmable, i.e programmable if blank only */
} psector_update_mode_t;

typedef enum
{
    WRITE_OK = 0x0,
    WRITE_ERROR_BAD_MAGIC,
    WRITE_ERROR_INVALID_PAGE_NUMBER,
    WRITE_ERROR_BAD_VERSION,
    WRITE_ERROR_BAD_CHECKSUM,
    WRITE_ERROR_INCORRECT_UPDATE_MODE,
    WRITE_ERROR_UPDATE_INVALID,
    WRITE_ERROR_PAGE_ERROR
} psector_write_status_t;


typedef enum {
    AUTH_NONE = 0,
    AUTH_ON_FW_UPDATE = 1,
    AUTH_ALWAYS = 2,
    AUTH_LEVEL_NB
} AuthMode_t;


typedef union
{
    uint8_t data_8[16];
    uint32_t data_32[4];
    uint64_t data_64[2];
} psector_page_word_t;

typedef struct
{
    uint32_t checksum;
    uint32_t magic;
    uint16_t psector_size;
    uint16_t page_number;
    uint32_t version;
    uint8_t update_modes[16];

} psector_header_t;

typedef struct
{
    psector_header_t hdr;
    psector_page_word_t page_word[PSECTOR_PAGE_WORDS];
} psector_page_t;

typedef struct
{
    psector_header_t hdr;

    union
    {
        psector_page_word_t page_word[PSECTOR_PAGE_WORDS];

        struct
        {
                /* Word 0 - Any */
            uint32_t SelectedImageAddress;
            uint32_t RESERVED0[3];
            /* Word 1 - Increment */
            uint32_t MinVersion;
            uint32_t img_pk_valid;
            uint32_t RESERVED1[2];
            /* Word [2:17] - OTP */
            uint8_t image_pubkey[256];
            /* Word [18:20] - OTP */
            uint8_t zigbee_install_code[48];
            /* Word [21] - OTP */
            uint8_t zigbee_password[16];
            uint8_t RESERVED2[128];
        } page0_v2;

        struct
        {
            /* Word 0 */
            uint32_t rom_patch_region_sz;
            uint32_t rom_patch_region_addr;    /*< ROM patch entry point address. 
                                                * A value outside of the address range used to store
                                                * the ROM patch binary shall be deemed invalid
                                                */
            uint32_t rom_patch_checksum;
            uint32_t rom_patch_checksum_valid; /*< ROM patch checksum valid:
                                                * 0 means invalid
                                                * Any other value means valid
                                                */
            /* Word 1 */
            uint32_t backdoor_disable;         /*< Back door control:
                                                *  0 means enabled
                                                *  Any other value means disabled 
                                                */

            uint32_t ISP_access_level;         /*< ISP access level:
                                                * 0 means full access, unsecure
                                                * 0x01010101 means full access, secure
                                                * 0x02020202 means write only, unsecure
                                                * 0x03030303 means write only, secure
                                                * 0x04040404 means locked
                                                * Any other value means disabled
                                                */

            uint16_t application_flash_sz;     /*< Application flash size, in kilobytes. 
                                                * 0 is interpreted as maximum (640). 
                                                * This is intended to provide an alternative way of 
                                                * restricting the flash size on a device, and to greater
                                                * granularity, than the eFuse bit. 
                                                * The actual level of granularity that can be obtained is 
                                                * dependent upon the MPU region configuration
                                                */
            uint16_t image_authentication_level; /*< Image authentication level:
                                                  * 0 means check only header validity
                                                  * 1 means check signature of whole image if image has changed
                                                  * 2 means check signature of whole image on every cold start
                                                  */
            uint16_t unlock_key_valid;          /*<  0: unlock key is not valid, >= 1: is present  */
            uint16_t ram1_bank_sz;               /*< RAM bank 1 size, in kilobytes. 
                                                  * This is intended to provide an alternative way of restricting 
                                                  * the RAM size on a device, and to greater granularity, 
                                                  * than the eFuse bit. The actual level of granularity that can
                                                  * be obtained is dependent upon the MPU region configuration */
            /* Word 2 */
            uint32_t app_search_granularity;     /*< Application search granularity (increment), in bytes.
                                                  * Value of 0 shall be equated to 4096. 
                                                  * Other values are to be used directly; 
                                                  * configurations that are not using hardware remapping do not 
                                                  * require hard restrictions
                                                  */
            uint32_t qspi_app_search_granularity;

            uint32_t reserved1[2];
            /* Word 3 */
            uint8_t ISP_protocol_key[16];        /*< ISP protocol key: key used to encrypt messages over ISP UART
                                                  * with secure access level
                                                  */
            /* Word 4 */
            uint64_t ieee_mac_id1;               /*< IEEE_MAC_ID_1 (Used to over-ride MAC ID_1 in N-2 page) */
            uint64_t ieee_mac_id2;               /*< IEEE_MAC_ID_2 if second MAC iID is required */
            /* Word 5 */
            uint64_t ble_mac_id1;                /*< BLE_MAC_ID_1 (Used to over-ride MAC ID_1 in N-2 page) */
            uint64_t ble_mac_id2;                /*< BLE_MAC_ID_2 (Used to over-ride MAC ID_2 in N-2 page) */
            /* Words [6..11] */
            uint8_t  reserved2[96];             /*< Reserved for future use  */
            /* Words 12 */
            uint64_t customer_id;                /*< Customer ID, used for secure handshake */
            uint64_t min_device_id;              /*< Certificate compatibility Min Device ID, used for secure handshake*/
            /* Words 13 */
            uint64_t device_id;                  /*< Certificate compatibility Device ID, used for secure handshake */
            uint64_t max_device_id;              /*< Certificate compatibility Max Device ID, used for secure handshake */
            /* Words 14 */
            uint8_t unlock_key[256];             /*< 2048-bit public key for secure handshake 
                                                  * (equivalent to ‘unlock’ key). Stored encrypted,
                                                  * using the AES key in eFuse*/
        } pFlash;

    };
} psector_page_data_t;

STATIC_ASSERT(sizeof(psector_page_t) == 512, "Psector page size not equal to flash page");
STATIC_ASSERT(sizeof(psector_page_data_t) == 512, "Psector data size not equal to flash page");

/****************************************************************************/
/***        Exported Functions                                            ***/
/****************************************************************************/

/* General access functions */
  
ROM_API psector_write_status_t psector_WriteUpdatePage(psector_partition_id_t part_index, psector_page_t *page);

ROM_API void psector_EraseUpdate(void);

/* Access helper functions */

ROM_API uint64_t psector_Read_CustomerId(void);
ROM_API int      psector_Read_RomPatchInfo(uint32_t *patch_region_sz,
                                           uint32_t *patch_region_addr,
                                           uint32_t *patch_checksum,
                                           uint32_t *patch_checksum_valid);

ROM_API uint16_t psector_Read_ImgAuthLevel(void);
ROM_API uint32_t psector_Read_AppSearchGranularity(void);

ROM_API uint32_t psector_Read_QspiAppSearchGranularity(void);

ROM_API int      psector_Read_UnlockKey(int * valid, uint8_t key[256], bool raw);
ROM_API uint64_t psector_Read_DeviceId(void);
ROM_API int      psector_Read_ISP_protocol_key(uint8_t key[16]);
ROM_API uint64_t psector_ReadIeee802_15_4_MacId2(void);
ROM_API uint64_t psector_ReadIeee802_15_4_MacId1(void);
ROM_API uint64_t psector_Read_MinDeviceId(void);
ROM_API uint64_t psector_Read_MaxDeviceId(void);

/* Helper functions for reading and writing image data */
ROM_API uint32_t psector_Read_MinVersion(void);
ROM_API psector_write_status_t psector_SetEscoreImageData(uint32_t image_addr, uint32_t min_version);
ROM_API psector_page_state_t psector_ReadEscoreImageData(uint32_t *image_addr, uint32_t *min_version);

ROM_API int psector_Read_ImagePubKey(int * valid, uint8_t key[256], bool raw);

/****************************************************************************/
/***        Exported Variables                                            ***/
/****************************************************************************/

#if defined __cplusplus
}
#endif

#endif /* ROM_PSECTOR_H_ */

/****************************************************************************/
/***        END OF FILE                                                   ***/
/****************************************************************************/
