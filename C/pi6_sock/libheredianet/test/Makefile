SRCDIR= ../src
TESTDIR= .
LIBDIR= ../lib
INCLUDEDIR= ../include
CFLAGS= # -g -DDEBUGGING
LDFLAGS= -L$(LIBDIR) -lheredianet -pthread -lws2_32 

all: test_server.exe test_client.exe

test_server.exe: test_server.o
	cc -o $@ $^ $(CFLAGS) $(LDFLAGS)

test_client.exe: test_client.o
	cc -o $@ $^ $(CFLAGS) $(LDFLAGS)
