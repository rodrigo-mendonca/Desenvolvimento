CC=gcc
LDFLAGS=-fopenmp -lm 
 
all: lib 
	
lib:  INumber.o PGM.o main.c
	$(CC) -o Fourier main.c INumber.o PGM.o $(LDFLAGS) 
                                                 
