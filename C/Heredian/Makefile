#CC=g++
#CFLAGS= -g -ansi
CFLAGS= -g
LDFLAGS= `pkg-config --libs allegro_image-5 allegro_acodec-5 allegro_ttf-5 allegro_primitives-5` -pthread 

MYLIBPREFIX = ../Lib

all: heredian

heredian: $(MYLIBPREFIX)/client.o $(MYLIBPREFIX)/GameLoader.o $(MYLIBPREFIX)/packet.o $(MYLIBPREFIX)/stream_reader.o \
		$(MYLIBPREFIX)/Events.o $(MYLIBPREFIX)/GameMenu.o $(MYLIBPREFIX)/io_thread.o $(MYLIBPREFIX)/queue.o \
		$(MYLIBPREFIX)/stream_writer.o $(MYLIBPREFIX)/FileManager.o $(MYLIBPREFIX)/GameServer.o $(MYLIBPREFIX)/list.o \
		$(MYLIBPREFIX)/server.o $(MYLIBPREFIX)/GameLib.o $(MYLIBPREFIX)/GDPilha.o $(MYLIBPREFIX)/misc.o $(MYLIBPREFIX)/stack.o \
		main.o $(MYLIBPREFIX)/GameDraw.o
	$(CC) -o heredian $(MYLIBPREFIX)/*.o *.o $(LDFLAGS)
