CC=gcc
LDFLAGS=-lm --openmp
 
all: lib 
	
lib:  INumber.o PGM.o main.c
	$(CC) -o fourier main.c INumber.o PGM.o $(LDFLAGS) 
                                                 
