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
VERSION_MINOR := 13

# Define to indicate JN5172 is FPGA version
# CFLAGS += -DJN5172_FPGA
# Define to trace programmer
# CFLAGS += -DDEBUG_PROGRAMMER

# Define to trace bootloader comms
# CFLAGS += -DDEBUG_BOOTLOADER 

# Define to trace firmware image info
# CFLAGS += -DDEBUG_FIRMWARE

# Define to trace serial comms
# CFLAGS += -DDEBUG_UART

SOURCE := JN51xx_BootLoader.c Firmware.c programmer.c

BINARY := FlashProgrammerExtension_JN5168.bin FlashProgrammerExtension_JN5169.bin FlashProgrammerExtension_JN5179.bin

BINARY_DIR := FlashProgrammerExtension/

PROJ_CFLAGS += -DVERSION_MAJOR=\"$(VERSION_MAJOR)\"
PROJ_CFLAGS += -DVERSION_MINOR=\"$(VERSION_MINOR)\"

ifeq ($(ARCH),i386)
    PROJ_LDFLAGS  += -m32
    PROJ_CFLAGS   += -m32
    PROJ_BINFLAGS += -melf_i386
endif

    LIBRARY_NAME=programmer

    CFLAGS += -DWIN32 -DLIBPROGRAMMER_BUILD -march=i686

    SOURCE += uart_win.c

    PROJ_LDFLAGS += -Wl,--out-implib,lib$(LIBRARY_NAME)_dll.a
    PROJ_LDFLAGS += -static-libgcc
    PROJ_LIBS += -lws2_32
    #PROJ_LIBS +=  ftd2xx.lib

    EXT := dll

OBJ := $(SOURCE:.c=.o)
OBJ += $(BINARY:.bin=.o)


PROJ_CFLAGS += -I../Include/ -DDBG_ENABLE
PROJ_CFLAGS += -DVERSION_SVN="\"$(shell if [ -f version.txt ]; then cat version.txt; else svnversion ../Source; fi)\""

ARFLAGS = rcs

vpath %.c ../Source

SHARED := $(LIBRARY_NAME).$(EXT)
STATIC := $(LIBRARY_NAME).a

all: $(SHARED) $(STATIC)


$(SHARED): $(OBJ) $(WIN_DEPS)
	$(info Building shared library $@ ...)
	$(CC) -shared $(LDFLAGS) $(PROJ_LDFLAGS) -o $@ $^ $(PROJ_LIBS)
	cp $(SHARED) ../../CLI/Build/$(SHARED)
	
$(STATIC): $(OBJ)
	$(info Building static library $@ ...)
	$(AR) $(ARFLAGS) -o $@ $^
	cp $(STATIC) ../../CLI/Build/$(STATIC)
	
%.o: %.c
	$(CC) $(CFLAGS) $(PROJ_CFLAGS) -c $<

%.o: $(BINARY_DIR)%.bin
	cd $(BINARY_DIR) && $(LD) -r -b binary $(PROJ_BINFLAGS) -o ../$@ $(notdir $<)
	
clean:
	rm -f *.o $(SHARED) $(STATIC) $(LIBRARY_NAME)_dll.a ../../CLI/Build/$(SHARED) ../../CLI/Build/$(STATIC)
