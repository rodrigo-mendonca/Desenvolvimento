#include <stdio.h>
#include <stdlib.h>
#include "GeMem.h"

int main()
{
    char *Teste1 = Aloc(512);
    char *Teste2 = Aloc(512);

    Libera(Teste1);
    char *Teste3 = Aloc(256);
    char *Teste4 = Aloc(256);
    Libera(Teste2);
    char *Teste5 = Aloc(256);
    char *Teste6 = Aloc(256);
    Libera(Teste4);
    Libera(Teste6);
    char *Teste7 = Aloc(512);
    Libera(Teste7);
    char *Teste8 = Aloc(128);
    char *Teste9 = Aloc(128);
    char *Teste10 = Aloc(1024);

    Memoria *Aux = PMemoria;
    while(Aux!=NULL){
        printf("Aloc= %c -> %d -> %d Fim->%c\n",Aux->lAloc,Aux->nMem,Aux->nTam,Aux->lFim);
        Aux = Aux->oProx;
    }
    printf("Fim!");

    return 0;
}
