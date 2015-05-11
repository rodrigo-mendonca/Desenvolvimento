CC= mingw32-gcc
CFLAGS= -g -DDEBUGGING
LDFLAGS= -pthread -lws2_32
SRCDIR= src
TESTDIR= test
LIBDIR= lib
INCLUDEDIR= include

all: src test

.PHONY: src test

src:
	$(MAKE) -C $(SRCDIR)
	copy $(SRCDIR)\libheredianet.a $(LIBDIR)
	copy $(SRCDIR)\*.h $(INCLUDEDIR)

test:
	$(MAKE) -C $(TESTDIR)
