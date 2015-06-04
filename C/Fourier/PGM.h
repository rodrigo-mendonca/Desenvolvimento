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
    int **matrixinv;
    INumber **imatrixinv;
    INumber **imatrix;
    long double **espectro;
    int **norespectro;
    long double maxespectro;
} PGM;

int **alloc_matrix(int , int );
void free_matrix(int **, int );

long double **alloc_dmatrix(int , int );
void free_dmatrix(long double **, int );

INumber **alloc_Imatrix(int , int );
void free_Imatrix(INumber **, int );

void commentskip(FILE *);
PGM* readfile(const char *, PGM *);
void savefile(const char *, const PGM *);
FILE* openpgm(const char *);
