#include <stdio.h>
#include <stdlib.h>
#include "PGM.h"
#include <limits.h>

int main(int argc, char *argv[])
{
    char *entrada = argv[1];
    char *saida = "imagem.pgm";

    PGM *file = (PGM*)malloc(sizeof(PGM));

    readfile(entrada, file);

/*
    printf("calculating...\n");

    INumber n1;
    n1.R=10;
    n1.I=5;

    INumber n2;
    n2.R=5;
    n2.I=2;


    INumber n3 = Multipl(n1,n2);
    //teste calculo
    printf("n3=%f+%fi \n",n3.R,n3.I);
    */

    savefile(saida, file);
    free(file);

    printf("imagem.pgm");
    return 0;
}



