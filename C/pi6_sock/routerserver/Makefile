CC= mingw32-gcc
CFLAGS= -I..\libheredianet\include # -g -DDEBUGGING
LDFLAGS= -L..\libheredianet\lib -LD:\MinGW\lib -lheredianet -lpthread -lws2_32

.PHONY: libheredianet

all: libheredianet router.exe

libheredianet:

router.exe: router.o
	$(CC) $^ -o $@ $(CFLAGS) $(LDFLAGS)
