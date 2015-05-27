#include <stdio.h>
#include <string.h>
#include <ctype.h>
#include <stdlib.h>
#include "INumber.h"

typedef struct _PGM {
    int height;
    int width;
    int maxGray;
    int **matrix;
    INumber **imatrix;
    int **espectro;
} PGM;

int **alloc_matrix(int , int );
void free_matrix(int **, int );
INumber **alloc_Imatrix(int , int );
void free_Imatrix(INumber **, int );

void commentskip(FILE *);
PGM* readfile(const char *, PGM *);
void savefile(const char *, const PGM *);
FILE* openpgm(const char *);
