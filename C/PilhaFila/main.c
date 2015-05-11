#include <stdio.h>
#include <stdlib.h>
#include "List.h"

void ImprimirPilha(Pilha **paramPilha)
{
  Pilha *Aux;
  Aux = *paramPilha;

  printf("\n Imprimindo a pilha \n");

  while(Aux != NULL)
  {

     printf(" %d ", Aux->nValor);//imprimindo o valor
     Aux = Aux->oProximo; //apontando a pilha

  }
   printf("\n");
}

/**************************************
/                                     /
/   Inicia programa principal         /
/                                     /
**************************************/

int main(int argc, char *argv[])
{
  Pilha *p1 ;
  p1=NULL;

  printf("\n\n--------------\n");
  printf("TESTES PILHA");
  printf("\n---------------\n");

  PUSH(&p1,1);

  PUSH(&p1,2);
  PUSH(&p1,3);
  PUSH(&p1,4);
  PUSH(&p1,5);
  ImprimirPilha(&p1);


  printf("\n Sai um da PILHA");
  POP(&p1);
  ImprimirPilha(&p1);

  POP(&p1);
  POP(&p1);
  printf("\n Sai mais dois da PILHA");
  ImprimirPilha(&p1);

  printf("\n entra mais um da PILHA");
  PUSH(&p1,9);
  ImprimirPilha(&p1);

  printf("\n Sai do comeco e vai para o final altomaticamente");
  POPEND(&p1);
  ImprimirPilha(&p1);

  printf("\n Possui %i itens", SizePilha(&p1));
  printf("\n O topo da PILHA e %i \n", TOP(&p1));

  system("PAUSE");
  return 0;
}
