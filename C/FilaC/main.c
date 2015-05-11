#include <stdio.h>
#include <stdlib.h>



/*************************************
/                                     /
/   Operações para Lista              /
/                                     /
**************************************/
typedef struct MyList
{
  int nValor;
  struct MyList *oProximo;        
       
} Lista;

//Funçao que insere na lista
void InserirLista(Lista **paramLista, int nValue){  
     
     Lista *nList;//criando ponteiro nova lista.  
     nList=(Lista*)malloc(sizeof(Lista));//alocando o espaço em memória e converte
     
     nList->nValor = nValue; //atribuindo valor para n nova lista.   
     nList->oProximo = NULL;// apontando para fim da lista  
  
     if(*paramLista == NULL) //verifica se é o final da fila  
     {
        *paramLista = nList; //se for insere a nova lista no final  
     }
     else
     {            
       Lista *listaAux; //senao cria uma lista auxiliar  
       listaAux = *paramLista; //coloca a lista atual na lista auxiliar  
       
        while(listaAux->oProximo != NULL) //e vai percorrendo a lista ate encontrar o final ou seja NULL  
        {
            listaAux = listaAux->oProximo; //enquato nao acha o final ela fica colocando "as estruturas listas" na lista temporaria  
        }
        
        listaAux->oProximo = nList;//como ele ja esta no final da fila ele so adiciona a nova lista  
    }         
}            
  

void RemoverLista(Lista **paramLista, int nValor){
       
       Lista *Aux,*Ant=NULL,*Limpa; 
       Aux = *paramLista; //coloca a lista atual na lista auxiliar  
       
        while(Aux != NULL) 
        {
            if (nValor == Aux->nValor)
            {
               //guarda ponteiro que sera removido
               Limpa = Aux;
               
               //faz a troca
               if (Ant == NULL)
                  *paramLista = Aux->oProximo;
               else
                  Ant->oProximo = (Aux->oProximo);
               
               //libera memoria do removido
               free(Limpa);

            }                
            else
            {   
               Ant = Aux;        
               Aux = Aux->oProximo;   
            }
        }
                 
}   




//Esta funçao imprime toda a lista   
void ImprimirLista(Lista **paramList)  
{  
  printf("\nImprimindo a Lista\n");    
  
  Lista *Aux;
  Aux = *paramList; //coloca a lista atual na lista auxiliar  
   
  while(Aux != NULL)//enquanto nao é o final da lista leia e imprima o conteudo  
  {                 
                  
     printf("%d  ", Aux->nValor);//imprimindo o valor    
     Aux = Aux->oProximo; //apontando a lista a proxxima lista     
              
  }     
   printf("\n");                 
}   




int BuscarLista(Lista **paramLista, int nValor)
{
      Lista *Aux;
      int nOccurs = 0;
      Aux = *paramLista; //coloca a lista atual na lista auxiliar  
       
        while(Aux != NULL) 
        {
            if (nValor == Aux->nValor)
            {
               nOccurs++;
            }                
          
            Aux = Aux->oProximo;   
            
        }
        
        return nOccurs;

}





/*************************************
/                                     /
/   Operações para Fila              /
/                                     /
**************************************/

typedef struct MyFila
{
  int nValor;
  struct MyFila *oProximo;        
       
} Fila;

//Funçao que insere na fila
void Enqueue(Fila **paramFila, int nValue){  
     
     Fila *nFila;
     nFila=(Fila*)malloc(sizeof(Fila));
     
     nFila->nValor = nValue;    
     nFila->oProximo = NULL;
  
     if(*paramFila == NULL)  
     {
        *paramFila = nFila; 
     }
     else
     {            
       Fila *FilaAux; 
       FilaAux = *paramFila;  
       
        while(FilaAux->oProximo != NULL)
        {
            FilaAux = FilaAux->oProximo;
        }
        
        FilaAux->oProximo = nFila;
    }         
}            
  

void Dequeue(Fila **paramFila){
       
     if(*paramFila == NULL)//verirfica se a fila esta vazia  
        printf("\n A fila ja esta vazia");  
    else{
        Fila *auxiliar;  
        auxiliar = *paramFila; //coloca a fila atual dentro da auxiliar  
        *paramFila = (*paramFila)->oProximo;//pega a fila e converte para *paramFila  
        free(paramFila);// a função free libera a memoria  
    }  
                 
}   

//Esta funçao imprime toda a lista   
void ImprimirFila(Fila **paramFila)  
{  
  printf("\n Imprimindo a Fila \n");    
  
  Fila *Aux;
  Aux = *paramFila; 
   
  while(Aux != NULL)
  {                 
                  
     printf(" %d ", Aux->nValor);//imprimindo o valor    
     Aux = Aux->oProximo; //apontando a fila 
              
  }     
   printf("\n");                 
}   

int Front(Fila **paramFila)
{
   
   return (*paramFila)->nValor;
      
}

//tras tamanho da fila
int SizeFila(Fila **paramFila)
{
  
  int nRetorno = 0;    
  Fila *Aux;
  Aux = *paramFila; 
   
  while(Aux != NULL)
  {                              
     nRetorno++;                 
     Aux = Aux->oProximo; //apontando a fila 
              
  }     
  return nRetorno; 
       
}



/*************************************
/                                     /
/   Operações para Pilha              /
/                                     /
**************************************/

typedef struct MyPilha
{
  int nValor;
  struct MyPilha *oProximo;        
       
} Pilha;

//Funçao que insere na fila
void PUSH(Pilha **paramPilha, int nValue){  
     
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
       Pilha *Aux; 
       Aux = *paramPilha;  
       
        while(Aux->oProximo != NULL)
        {
            Aux = Aux->oProximo;
        }
        
        Aux->oProximo = nPilha;
    }         
}            
  

void POP(Pilha **paramPilha){
    
  
     if(*paramPilha == NULL)  
     {
        printf("\n Pilha Vazia \n");  
     }
     else
     {            
       Pilha *Aux,*Ant; 
       Aux = *paramPilha;  
       Ant = NULL;
       
        while(Aux != NULL)
        {            
            if(Aux->oProximo == NULL )
            {
               free(Aux);
               Ant->oProximo = NULL;
            
            }   
               Ant = Aux;
               Aux = Aux->oProximo;
        }
        
        
    }                  
}   


void ImprimirPilha(Pilha **paramPilha)  
{  
  printf("\n Imprimindo a pilha \n");    
  
  Pilha *Aux;
  Aux = *paramPilha; 
   
  while(Aux != NULL)
  {                 
                  
     printf(" %d ", Aux->nValor);//imprimindo o valor    
     Aux = Aux->oProximo; //apontando a pilha 
              
  }     
   printf("\n");                 
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


/**************************************
/                                     /
/   Inicia programa principal         /
/                                     /
**************************************/

int main(int argc, char *argv[])
{
  
  printf("\n--------------\n");
  printf("TESTES LISTA");
  printf("\n---------------\n");
    
  Lista *l1;
  l1=NULL;
  InserirLista(&l1,5);
  InserirLista(&l1,8);
  InserirLista(&l1,7);
  InserirLista(&l1,9);
  
  RemoverLista(&l1,9);

  printf("\n Numero 8 foi encontrado: %i vezes \n",BuscarLista(&l1,8) ); 
  printf("\n Numero 15 foi encontrado: %i vezes \n",BuscarLista(&l1,15) ); 
  printf("\n Numero 9 foi encontrado: %i vezes \n", BuscarLista(&l1,9) ); 
  
  ImprimirLista(&l1);
  

  printf("\n\n--------------\n");
  printf("TESTES FILA");
  printf("\n---------------\n");
  
  Fila *f1;
  f1=NULL;
  
  Enqueue(&f1,1);
  Enqueue(&f1,2);
  Enqueue(&f1,3);
  Enqueue(&f1,4);
  Enqueue(&f1,5);
  ImprimirFila(&f1);
  
  printf("\n Sai um da fila");
  Dequeue(&f1);
  ImprimirFila(&f1);
  
  Dequeue(&f1);
  Dequeue(&f1);
  printf("\n Sai mais dois da fila");
  ImprimirFila(&f1);
  
  
  printf("\n entra mais um da fila");
  Enqueue(&f1,9);
  ImprimirFila(&f1);
  
  printf("\n Possui %i itens", SizeFila(&f1));
  printf("\n O primeiro da fila e %i \n", Front(&f1));
  
  
  printf("\n\n--------------\n");
  printf("TESTES PILHA");
  printf("\n---------------\n");

  
  Pilha *p1;
  p1=NULL;
  
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
  
  printf("\n Possui %i itens", SizePilha(&p1));
  printf("\n O topo da PILHA e %i \n", TOP(&p1));
  

  system("PAUSE");	
  return 0; 
}
