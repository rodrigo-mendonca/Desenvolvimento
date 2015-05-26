#include <stdio.h>
#include <stdlib.h>
#include "PGM.h"
#include <limits.h>
#include "INumber.h"
void CalcFourier(PGM *);
void CalcParallelFourier(PGM *);
void CalcDistributedFourier(PGM *file);
int Fourier(int **,int,int);

int main(int argc, char *argv[])
{
    char *entrada = "imagem.pgm";
    char *saida   = "imagem.pgm";
    char *tipo = "I";

    if(argc > 1){
        entrada = argv[1];
        saida    = argv[2];
        tipo    = argv[3];
    }

    PGM *file = (PGM*)malloc(sizeof(PGM));

    readfile(entrada, file);

    switch (*tipo){
        case 'I':
            CalcFourier(file);
            break;
        case 'P':
            CalcParallelFourier(file);
            break;
        case 'D':
            CalcDistributedFourier(file);
            break;
    }
    savefile(saida, file);
    free(file);

    printf(saida);
    return 0;
}

void CalcFourier(PGM *file)
{
    //printf("Normal\n");

    printf("calculating...\n");

    int w = file->width;
    int h = file->height;

    int **matriz = file->matrix; // alloc_matrix(h,w);

    int i,j;
    for(i=0;i<h;i++)
        for(j=0;j<w;j++)
            file->matrix[i][j] = Fourier(file->matrix,i,j);

    //file->matrix = matriz;
}

void CalcParallelFourier(PGM *file)
{
    //printf("Paralelo\n");
}

void CalcDistributedFourier(PGM *file)
{
    //printf("Distribuido\n");
}

int Fourier(int **matriz,int y,int x)
{
    int min = min(matriz[y][x] + 10,255);
    return min;
}
