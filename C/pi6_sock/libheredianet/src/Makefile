CC= mingw32-gcc
CFLAGS= # -g -DDEBUGGING
LDFLAGS= -pthread -lws2_32

all: libheredianet.a

libheredianet.a: misc.o stream_reader.o stream_writer.o client.o stack.o \
	queue.o list.o packet.o io_thread.o server.o
	ar rcs $@ $^
