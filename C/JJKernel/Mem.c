#include <Mem.h>

//Funçao que aloca memoria
void Alocar(int nQuant){  
     
     Memoria *oMem;//criando ponteiro nova Memoria.
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

int BuscarLista(Memoria **PMem, int nTam)
{
    Memoria *Aux;
    int nEnd = 0,nEsp;
    Aux = *PMem; //coloca a lista atual na lista auxiliar  
       
       while(Aux != NULL) 
        {
          if (Aux->oProx!=NULL)
            nEsp = Aux->oProx->nIniMem - (Aux->nIniMem + Aux->nTam);
          else
            nEsp = nTamMem - (Aux->nIniMem + Aux->nTam);
 
          if (nEsp>=nTam)
          {
             nEnd = (Aux->nIniMem + Aux->nTam) + 1; // pega o proximo endereço livre
             Memoria *New; //cria um novo no para a memoria
             New->oProx   = Aux->oProx; // coloca o proximo como o proximo do novo
             New->nIniMem = nEnd;
             New->nTam    = nTam;
             Aux->oProx   = New;

             break;
          }                
          nEnd = 0;
          Aux = Aux->oProx;
            
        }
        
        return nEnd;
}
