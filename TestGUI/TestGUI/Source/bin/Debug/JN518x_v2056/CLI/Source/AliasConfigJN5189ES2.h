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
    
#ifndef CLI_ALIAS_CONFIG_JN5189ES2_H_
#define CLI_ALIAS_CONFIG_JN5189ES2_H_

#if defined __cplusplus
extern "C" {
#endif

/****************************************************************************/
/***        Include Files                                                 ***/
/****************************************************************************/
#include "AliasConfigCommon.h"
/****************************************************************************/
/***        Macro Definitions                                             ***/
/****************************************************************************/
#define INSTALL_CODE_HELP     "ZigBee install code (maximum of 36 bytes needed, pad the remaining bytes with 0s)"
#define ZIGBEE_PASS_HELP      "ZigBee password"
#define IMG_KEY_VALID_HELP    "Image public key status:\n  0: not valid\n  1: valid, unencrypted\n  2: valid, encrypted\nWhen reading back, value will be 0 or 2"
#define IMG_KEY_VALUE_HELP    "Image public key, for image authentication level 1 or 2 (see auth_level)"
#define BACKDOOR_DIS_HELP     "Backdoor: 0 means enabled, any other value means disabled"
#define ISP_LVL_HELP          "ISP access level:\n  0:           full access, unsecure\n  0x01010101:  full access, secure\n  0x02020202:  write only, unsecure\n  0x03030303:  write only, secure\n  0x04040404:  locked\n  Other value: ISP access disabled"
#define AUTH_LVL_HELP         "Image authentication level, determines checks performed during boot:\n  0: header validity only\n  1: signature of whole image if image has changed\n  2: signature of whole image on every cold start"
#define UNLOCK_KEY_VALID_HELP "Unlock key validity:\n  0:   unlock key field is not valid\n  >=1: unlock key field is valid"
#define APP_SEARCH_SIZE       "Application search granularity (increment), in bytes: value of 0 is equated to 4096, other values are used directly"
#define ISP_KEY_HELP          "ISP protocol key: key used to encrypt messages over ISP UART with secure access level (note: functionality not implemented)"
#define CUST_154_MAC_0_HELP   "Custom 802.15.4 MAC address (overrides factory address 154_fmac0)"
#define CUST_154_MAC_1_HELP   "Custom 802.15.4 MAC address (if second address is required)"
#define CUST_BLE_MAC_0_HELP   "Custom BLE MAC address (overrides factory address ble_fmac0)"
#define CUST_BLE_MAC_1_HELP   "Custom BLE MAC address (if second address is required)"
#define CUST_ID_HELP          "Customer ID, used for secure handshake"
#define MIN_DEVICE_ID_HELP    "Minimum device ID, used for secure handshake"
#define DEVICE_ID_HELP        "Device ID, used for secure handshake"
#define MAX_DEVICE_ID_HELP    "Maximum device ID, used for secure handshake"
#define UNLOCK_KEY_VALUE_HELP "2048-bit public key for secure handshake (equivalent to unlock key)"
#define SWD_JTAG_DIS_HELP     "Sets the SWD_DIS and JTAG_DIS fields in N-2 page, possible values:\n  0x80: SWD_DIS=1\n  0x70: JTAG_DIS=1\n  0xF0: SWD_DIS=1 and JTAG_DIS=1"
#define RESERVED_FUTURE_HELP  "Reserved for future use"
#define CUSTOMER_USE_HELP     "For customer use"

/****************************************************************************/
/***        Type Definitions                                              ***/
/****************************************************************************/
const tsMemoryAlias asAliases_JN5189ES2[] = {
    {"install_code",       {"PSECT",  INFO_ALIAS(psector_page_data_t, page0_v2.zigbee_install_code)}, INSTALL_CODE_HELP},
    {"zigbee_password",    {"PSECT",  INFO_ALIAS(psector_page_data_t, page0_v2.zigbee_password)}, ZIGBEE_PASS_HELP},
    {"image_key_valid",    {"PSECT",  INFO_ALIAS(psector_page_data_t, page0_v2.img_pk_valid)}, IMG_KEY_VALID_HELP},
    {"image_key_value",    {"PSECT",  INFO_ALIAS(psector_page_data_t, page0_v2.image_pubkey)}, IMG_KEY_VALUE_HELP},
    {"reserved_page0_0",   {"PSECT",  INFO_ARRAY_ALIAS(0, psector_page_data_t, page0_v2.RESERVED2, 16)}, RESERVED_FUTURE_HELP},
    {"reserved_page0_1",   {"PSECT",  INFO_ARRAY_ALIAS(1, psector_page_data_t, page0_v2.RESERVED2, 16)}, RESERVED_FUTURE_HELP},
    {"reserved_page0_2",   {"PSECT",  INFO_ARRAY_ALIAS(2, psector_page_data_t, page0_v2.RESERVED2, 16)}, RESERVED_FUTURE_HELP},
    {"reserved_page0_3",   {"PSECT",  INFO_ARRAY_ALIAS(3, psector_page_data_t, page0_v2.RESERVED2, 16)}, RESERVED_FUTURE_HELP},
    {"reserved_page0_4",   {"PSECT",  INFO_ARRAY_ALIAS(4, psector_page_data_t, page0_v2.RESERVED2, 16)}, RESERVED_FUTURE_HELP},
    {"reserved_page0_5",   {"PSECT",  INFO_ARRAY_ALIAS(5, psector_page_data_t, page0_v2.RESERVED2, 16)}, RESERVED_FUTURE_HELP},
    {"reserved_page0_6",   {"PSECT",  INFO_ARRAY_ALIAS(6, psector_page_data_t, page0_v2.RESERVED2, 16)}, RESERVED_FUTURE_HELP},
    {"reserved_page0_7",   {"PSECT",  INFO_ARRAY_ALIAS(7, psector_page_data_t, page0_v2.RESERVED2, 16)}, RESERVED_FUTURE_HELP},

    {"backdoor_dis",       {"pFlash", INFO_ALIAS(psector_page_data_t, pFlash.backdoor_disable)}, BACKDOOR_DIS_HELP},
    {"isp_level",          {"pFlash", INFO_ALIAS(psector_page_data_t, pFlash.ISP_access_level)}, ISP_LVL_HELP},
    {"auth_level",         {"pFlash", INFO_ALIAS(psector_page_data_t, pFlash.image_authentication_level)}, AUTH_LVL_HELP},
    {"unlock_key_valid",   {"pFlash", INFO_ALIAS(psector_page_data_t, pFlash.unlock_key_valid)}, UNLOCK_KEY_VALID_HELP},
    {"app_search_size",    {"pFlash", INFO_ALIAS(psector_page_data_t, pFlash.app_search_granularity)}, APP_SEARCH_SIZE},
    //Not supported
    //{"isp_key",          {"pFlash", INFO_ALIAS(psector_page_data_t, pFlash.ISP_protocol_key)}, ISP_KEY_HELP},
    {"154_fmac0",          {"Config", 0x70, 0x8},  "Factory 802.15.4 MAC address 0"},
    {"ble_fmac0",          {"Config", 0x100, 0x8}, "Factory BLE MAC address 0"},
    {"154_cmac0",          {"pFlash", INFO_ALIAS(psector_page_data_t, pFlash.ieee_mac_id1)}, CUST_154_MAC_0_HELP},
    {"154_cmac1",          {"pFlash", INFO_ALIAS(psector_page_data_t, pFlash.ieee_mac_id2)}, CUST_154_MAC_1_HELP},
    {"ble_cmac0",          {"pFlash", INFO_ALIAS(psector_page_data_t, pFlash.ble_mac_id1)}, CUST_BLE_MAC_0_HELP},
    {"ble_cmac1",          {"pFlash", INFO_ALIAS(psector_page_data_t, pFlash.ble_mac_id2)}, CUST_BLE_MAC_1_HELP},
    {"reserved_pFlash_0",  {"pFlash", INFO_ARRAY_ALIAS(0, psector_page_data_t, pFlash.reserved2, 16)}, RESERVED_FUTURE_HELP},
    {"reserved_pFlash_1",  {"pFlash", INFO_ARRAY_ALIAS(1, psector_page_data_t, pFlash.reserved2, 16)}, RESERVED_FUTURE_HELP},
    {"reserved_pFlash_2",  {"pFlash", INFO_ARRAY_ALIAS(2, psector_page_data_t, pFlash.reserved2, 16)}, RESERVED_FUTURE_HELP},
    {"customer_0",         {"pFlash", INFO_ARRAY_ALIAS(3, psector_page_data_t, pFlash.reserved2, 16)}, CUSTOMER_USE_HELP},
    {"customer_1",         {"pFlash", INFO_ARRAY_ALIAS(4, psector_page_data_t, pFlash.reserved2, 16)}, CUSTOMER_USE_HELP},
    {"customer_2",         {"pFlash", INFO_ARRAY_ALIAS(5, psector_page_data_t, pFlash.reserved2, 16)}, CUSTOMER_USE_HELP},
    {"cust_id",            {"pFlash", INFO_ALIAS(psector_page_data_t, pFlash.customer_id)}, CUST_ID_HELP},
    {"min_device_id",      {"pFlash", INFO_ALIAS(psector_page_data_t, pFlash.min_device_id)}, MIN_DEVICE_ID_HELP},
    {"device_id",          {"pFlash", INFO_ALIAS(psector_page_data_t, pFlash.device_id)}, DEVICE_ID_HELP},
    {"max_device_id",      {"pFlash", INFO_ALIAS(psector_page_data_t, pFlash.max_device_id)}, MAX_DEVICE_ID_HELP},
    {"unlock_key_value",   {"pFlash", INFO_ALIAS(psector_page_data_t, pFlash.unlock_key)}, UNLOCK_KEY_VALUE_HELP},
    {"swd_jtag_disable",   {"Config", 0x3, 0x1}, SWD_JTAG_DIS_HELP}
};
/****************************************************************************/
/***        Exported Functions                                            ***/
/****************************************************************************/


/****************************************************************************/
/***        Exported Variables                                            ***/
/****************************************************************************/

#if defined __cplusplus
}
#endif

#endif /* CLI_ALIAS_CONFIG_JN5189ES2_H_ */

/****************************************************************************/
/***        END OF FILE                                                   ***/
/****************************************************************************/
