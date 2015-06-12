CC=gcc
LDFLAGS=-fopenmp -lm 
 
all: lib 
	
lib: OpenMp.o timer.o knap.c
	$(CC) -o MochilabinariaOpenMP knap.c OpenMp.o timer.o $(LDFLAGS) 
