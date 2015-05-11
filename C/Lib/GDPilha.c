#include <stdlib.h>
#include "Include/GDPilha.h"

//Funçao que insere na pilha
void gdp_push(GDPilha **paramPilha, Sprite *oObj){
	GDPilha *Aux;
    GDPilha *oPilha;
    oPilha=(GDPilha*)malloc(sizeof(GDPilha));

     oPilha->oObject  = oObj;
     oPilha->oProximo = NULL;

     if(*paramPilha == NULL)
     {
        *paramPilha = oPilha;
     }
     else
     {
       Aux = *paramPilha;

        while(Aux->oProximo != NULL)
        {
            Aux = Aux->oProximo;
        }
        Aux->oProximo = oPilha;
    }
}

void gdp_pop(GDPilha **paramPilha){

     GDPilha *Aux,*Ant = NULL;

     if(*paramPilha != NULL)
     {

       Aux = *paramPilha;

        while(Aux->oProximo != NULL)
        {
            Ant = Aux;
            Aux = Aux->oProximo;
        }

        if (Aux == *paramPilha)
            *paramPilha = NULL;

        free(Aux);
        if (Ant != NULL)
            Ant->oProximo = NULL;
    }
}

void gdp_popend(GDPilha **paramPilha)
{
	Sprite *Topo = malloc(sizeof(Sprite));
	*Topo = *(gdp_top(paramPilha));

	GDPilha *Aux = (GDPilha*)malloc(sizeof(GDPilha));

	gdp_pop(paramPilha);

	Aux->oProximo = *paramPilha;
	Aux->oObject = Topo;

	*paramPilha = Aux;
}

Sprite* gdp_top(GDPilha **paramPilha)
{
     Sprite* nRetorno = NULL;

     if(*paramPilha != NULL)
     {
       GDPilha *Aux;
       Aux = *paramPilha;

        while(Aux != NULL)
        {
            if(Aux->oProximo == NULL)
            {

               nRetorno = Aux->oObject;
            }

            Aux = Aux->oProximo;
        }
    }
    return nRetorno;
}

//tras tamanho da fila
int gdp_stacksize(GDPilha **paramPilha)
{
  int nRetorno = 0;
  GDPilha *Aux;
  Aux = *paramPilha;

  while(Aux != NULL)
  {
     nRetorno++;
     Aux = Aux->oProximo; //apontando a Pilha

  }
  return nRetorno;
}
