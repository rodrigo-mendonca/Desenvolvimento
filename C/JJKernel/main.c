#include <stdio.h>
#include <stdlib.h>

// metodos
int *Alocar();
//variaveis
unsigned long long b=0,k=0,m=0,g=0;
char *nIniMem;


int main(int argc, char *argv[])
{
    int *nTeste = malloc(sizeof(int));
    *nTeste= 0;
    printf("Valor da Memoria=%d\n",*nTeste);
    system("PAUSE");
    
    
  // unidades
  b=1,k=1024,m=k*1024,g=m*1024;
  
  int a;
  nIniMem = malloc(k);
  char *nAux = nIniMem;

  int i=0;
  printf("Memoria Inicial=%llu\n",nIniMem);
  printf("Memoria Aux    =%llu\n",nAux);
  printf("Memoria de A   =%llu\n",a);
  
  for(i;i<k;i++)
  {
      *nAux='a'; 
      if(i>=4)
          nAux= nAux+1;

  }

  // cria index de memoria
  int vMem[k];

  printf("Tamanho do int=%d\n",sizeof(int));
  printf("Tamanho do char=%d\n",sizeof(char));
  printf("Tamanho do long=%d\n",sizeof(long));
  
  /*
  int *pa = k,*pb=m+1;
  a=10;
  pa = &a;
  
  *pa = 100;
  *pa = *pa +1;
  pa = pa +1;
  *pa = 120;
  */
  printf("Memoria Alocada=%c\n",*nIniMem);
  printf("Memoria Alocada=%d\n",a);
 
  system("PAUSE");	
  return 0;
}

int *Alocar(int nTam)
{
     
}
