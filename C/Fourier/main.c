#include <stdio.h>
#include <stdlib.h>
#include "PGM.h"
#include <limits.h>
#include "INumber.h"
void CalcFourier(PGM *);
void CalcParallelFourier(PGM *);
void CalcDistributedFourier(PGM *file);

int main(int argc, char *argv[])
{
    char *entrada = "imagem.pgm";
    char *saida   = "imagem.pgm";
    char *tipo = "I";

    if(argc > 1){
        entrada = argv[1];
        tipo    = argv[2];
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

    INumber n1;
    n1.r=10;
    n1.i=5;

    INumber n2;
    n2.r=5;
    n2.i=2;
    INumber n3 = Multipl(n1,n2);
    //teste calculo
    printf("n3=%f+%fi \n",n3.r,n3.i);
}

void CalcParallelFourier(PGM *file)
{
    //printf("Paralelo\n");
}

void CalcDistributedFourier(PGM *file)
{
    //printf("Distribuido\n");
}

