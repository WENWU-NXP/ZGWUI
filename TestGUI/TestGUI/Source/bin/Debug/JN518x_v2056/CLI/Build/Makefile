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
FEATURES ?= FEATURE_FTDI

SOURCE := main.c Utils.c UI.c ProgramThread.c LicenseFile.c MemoryRegion.c

CFLAGS = -Wall -O2 -D_GNU_SOURCE -g
CFLAGS += -DREMOVE_FROM_SOURCE_RELEASE

# Define to debug license file
# CFLAGS += -DDEBUG_LICENSE_FILE

OBJ := $(SOURCE:.c=.o)

ifeq ($(ARCH),i386)
    PROJ_LDFLAGS  += -m32
    PROJ_CFLAGS   += -m32
endif

ifeq ($(OS),Windows_NT)
    CFLAGS += -D WIN32 -I ../WIN32 -march=i686
    PROJ_LDFLAGS += -L../../Library/Build/ -lprogrammer_dll -L../WIN32 -lpdcurses -lws2_32
    SUFFIX = .exe
    
    OBJ += jmp.res

    PROGRAMMER_LIBRARY = ../../Library/Build/libprogrammer_dll.a
else
    CFLAGS += -D POSIX

    ifeq ($(RELEASE),1)
        PROJ_LDFLAGS += -static
    endif

    ifeq ($(findstring FEATURE_FTDI,$(FEATURES)),FEATURE_FTDI)
        # Automatic program / reset support via FTDI devices
        PROJ_LDFLAGS += -lusb-1.0 -ludev -ldl
    endif

    PROJ_LDFLAGS += -lpthread -lncurses -lm

    PROGRAMMER_LIBRARY = ../../Library/Build/libprogrammer.a
endif

PROJ_CFLAGS += -I../Source/ -I../../Library/Include/ -I../../Library/libtomcrypt/src/headers/ -DDBG_ENABLE

VERSION = $(shell sed -n /build.number=/p SDKBuildNumber.txt | sed s/build.number=//)
PROJ_CFLAGS += -DVERSION="\"$(VERSION)\""

vpath %.c ../Source

TARGET = JN518xProgrammer$(SUFFIX)

all:  $(TARGET)

.PHONY: $(PROGRAMMER_LIBRARY)

$(PROGRAMMER_LIBRARY):
	$(MAKE) -C ../../Library/Build FEATURES=$(FEATURES) ARCH=$(ARCH)

$(TARGET): $(OBJ) $(PROGRAMMER_LIBRARY)
	$(CC)  $^ $(LDFLAGS) $(PROJ_LDFLAGS) -o $@

%.res: %.rc
	windres $^ -v -D BUILD_VERSION="\\\"$(VERSION)\\\"" -O coff -o $@
	
%.o: %.c
	$(CC)  -I. $(CFLAGS) $(PROJ_CFLAGS) -c $<

clean:
	$(MAKE) -C ../../Library/Build clean
	rm -f *.res *.o $(TARGET)
