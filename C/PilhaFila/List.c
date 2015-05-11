#include <stdio.h>
#include <stdlib.h>
#include "List.h"

//Funçao que insere na pilha
void PUSH(Pilha **paramPilha, int nValue){
	Pilha *Aux;
     Pilha *nPilha;
     nPilha=(Pilha*)malloc(sizeof(Pilha));

     nPilha->nValor = nValue;
     nPilha->oProximo = NULL;

     if(*paramPilha == NULL)
     {
        *paramPilha = nPilha;
     }
     else
     {
       Aux = *paramPilha;

        while(Aux->oProximo != NULL)
        {
            Aux = Aux->oProximo;
        }
        Aux->oProximo = nPilha;
    }
}

void POP(Pilha **paramPilha){
     Pilha *Aux,*Ant;

     if(*paramPilha == NULL)
     {
        printf("\n Pilha Vazia \n");
     }
     else
     {
       Aux = *paramPilha;
       Ant = Aux->oProximo;

        while(Ant->oProximo != NULL)
        {
            if(Aux->oProximo == NULL )
            {
               free(Aux);
               Ant->oProximo = NULL;
            }
			else
			{
               Ant = Aux;
               Aux = Aux->oProximo;
			}
        }
    }
}

void POPEND(Pilha **paramPilha)
{
	int Topo = TOP(paramPilha);
	Pilha *Aux = (Pilha*)malloc(sizeof(Pilha));

	POP(paramPilha);

	Aux->oProximo = *paramPilha;
	Aux->nValor = Topo;

	*paramPilha = Aux;
}

int TOP(Pilha **paramPilha)
{
     int nRetorno = 0;

     if(*paramPilha != NULL)
     {
       Pilha *Aux;
       Aux = *paramPilha;

        while(Aux != NULL)
        {
            if(Aux->oProximo == NULL)
            {

               nRetorno = Aux->nValor;

            }

            Aux = Aux->oProximo;
        }


    }
    return nRetorno;
}

//tras tamanho da fila
int SizePilha(Pilha **paramPilha)
{
  int nRetorno = 0;
  Pilha *Aux;
  Aux = *paramPilha;

  while(Aux != NULL)
  {
     nRetorno++;
     Aux = Aux->oProximo; //apontando a Pilha

  }
  return nRetorno;
}
