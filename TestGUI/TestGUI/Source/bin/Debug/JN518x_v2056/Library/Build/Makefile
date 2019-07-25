############################################################################
#
# This software is owned by NXP B.V. and/or its supplier and is protected
# under applicable copyright laws. All rights are reserved. We grant You,
# and any third parties, a license to use this software solely and
# exclusively on NXP products [NXP Microcontrollers such as JN5148, JN5142, JN5139]. 
# You, and any third parties must reproduce the copyright and warranty notice
# and any other legend of ownership on each copy or partial copy of the 
# software.
#
# THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS"
# AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE
# IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE
# ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE
# LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR
# CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF
# SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS
# INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN
# CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE)
# ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE
# POSSIBILITY OF SUCH DAMAGE.
#
# Copyright NXP B.V. 2012. All rights reserved
#
############################################################################

CC=gcc
CFLAGS = -Wall -O2

VERSION_MAJOR := 0
VERSION_MINOR := 14

USE_TOMCRYPT := 0

# Define to indicate JN5172 is FPGA version
# CFLAGS += -DJN5172_FPGA
# Define to trace programmer
# CFLAGS += -DDEBUG_PROGRAMMER

# Define to trace protocol layer
# CFLAGS += -DDEBUG_PROTOCOL

# Define to trace bootloader comms
# CFLAGS += -DDEBUG_BOOTLOADER 

# Define to trace firmware image info
# CFLAGS += -DDEBUG_FIRMWARE

# Define to trace serial comms
# CFLAGS += -DDEBUG_UART
# CFLAGS += -DDEBUG_PIPE

CFLAGS += -DREMOVE_FROM_SOURCE_RELEASE

ifeq ($(USE_TOMCRYPT),1)
	CFLAGS += -DUSE_TOMCRYPT
endif

CFLAGS += -g

SOURCE := protocol_common.c protocol_jn51xx_v2.c protocol_jn51xx_v1.c Firmware.c programmer.c

BINARY := FlashProgrammerExtension_JN5168.bin FlashProgrammerExtension_JN5169.bin FlashProgrammerExtension_JN5179.bin

BINARY_DIR := FlashProgrammerExtension/

PROJ_CFLAGS += -DVERSION_MAJOR=\"$(VERSION_MAJOR)\"
PROJ_CFLAGS += -DVERSION_MINOR=\"$(VERSION_MINOR)\"

ifeq ($(ARCH),i386)
    PROJ_LDFLAGS  += -m32
    PROJ_CFLAGS   += -m32
    PROJ_BINFLAGS += -melf_i386
endif

ifeq ($(OS),Windows_NT)
    LIBRARY_NAME=programmer

    CFLAGS += -DWIN32 -DLIBPROGRAMMER_BUILD -march=i686

    SOURCE += uart_win.c
    SOURCE += pipe_win.c
    SOURCE += crc.c

    PROJ_LDFLAGS += -Wl,--out-implib,lib$(LIBRARY_NAME)_dll.a
    PROJ_LDFLAGS += -static-libgcc
    PROJ_LIBS += -lws2_32
    #PROJ_LIBS +=  ftd2xx.lib

    EXT := dll
else
    LIBRARY_NAME=libprogrammer

    CFLAGS += -DPOSIX -fPIC

    ifeq ($(findstring FEATURE_FTDI,$(FEATURES)),FEATURE_FTDI)
        # Enable FTDI devices
        CFLAGS += -DENABLE_FTDI
        CFLAGS += -I/usr/include/libftdi1
        CFLAGS += -I/usr/include/libusb-1.0/
        PROJ_LIBS += -lusb-1.0 -ludev -ldl
    endif

    SOURCE += uart_posix.c

    PROJ_LDFLAGS += -Wl,-soname,$(LIBRARY_NAME).so.$(VERSION_MAJOR) 
    PROJ_LIBS += -lm

    EXT := so.$(VERSION_MAJOR).$(VERSION_MINOR)
endif


OBJ := $(SOURCE:.c=.o)
OBJ += $(BINARY:.bin=.o)

ifeq ($(USE_TOMCRYPT),1)
	LIBS += ../../../libtomcrypt/libtomcrypt.a ../../../tomsfastmath/libtfm.a

	TOMCRYPT_CFLAGS += -DTFM_DESC -DLTC_NO_RSA_BLINDING

	PROJ_CFLAGS += -I../../../libtomcrypt/src/headers -I../../../tomsfastmath/src/headers
	PROJ_CFLAGS += $(TOMCRYPT_CFLAGS) 
endif
	
PROJ_CFLAGS += -I../Include/ -DDBG_ENABLE
PROJ_CFLAGS += -DVERSION_SVN="\"$(shell if [ -f version.txt ]; then cat version.txt; else svnversion ../Source; fi)\""

ARFLAGS = rcs

vpath %.c ../Source

SHARED := $(LIBRARY_NAME).$(EXT)
STATIC := $(LIBRARY_NAME).a

all: $(SHARED) $(STATIC)

../../../tomsfastmath/libtfm.a:
	CFLAGS="-DTFM_DESC -DLTC_NO_RSA_BLINDING" $(MAKE) -C ../../../tomsfastmath install
	 
../../../libtomcrypt/libtomcrypt.a:
	CFLAGS="-DTFM_DESC -DLTC_NO_RSA_BLINDING -I../tomsfastmath/src/headers" $(MAKE) -C ../../../libtomcrypt install

$(SHARED): $(OBJ) $(LIBS) $(WIN_DEPS)
	$(info Building shared library $@ ...)
	$(CC) -shared $(LDFLAGS) $(PROJ_LDFLAGS) -o $@ $^ $(PROJ_LIBS)
	cp $(SHARED) ../../CLI/Build/$(SHARED)
	
$(STATIC): $(OBJ) $(LIBS)
	$(info Building static library $@ ...)
	$(AR) $(ARFLAGS) -o $@ $^
	cp $(STATIC) ../../CLI/Build/$(STATIC)
	
%.o: %.c
	$(CC) $(CFLAGS) $(PROJ_CFLAGS) -c $<

%.o: $(BINARY_DIR)%.bin
	cd $(BINARY_DIR) && $(LD) -r -b binary $(PROJ_BINFLAGS) -o ../$@ $(notdir $<)
	
clean:
	rm -f *.o $(SHARED) $(STATIC) $(LIBRARY_NAME)_dll.a ../../CLI/Build/$(SHARED) ../../CLI/Build/$(STATIC)
