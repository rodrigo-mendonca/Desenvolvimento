#include <stdio.h>
#include <stdlib.h>
int* Ordenar(int*,int);
int Sum(int*,int);

typedef struct HeapSort HeapSort;
struct HeapSort {
   HeapSort *Rigth;
   HeapSort *Left;
   int Value;
};

int main()
{
    // inicializações
    int PenDrive = 1000;
    int Arquivos[15] = {241,241,151,151,451,151,151,651,151,151,351,151,251,95,151};

    int *Arq = Ordenar(Arquivos,sizeof(Arquivos)/sizeof(int));;
    int soma = 0,i=0;
    int Tam = sizeof(Arquivos)/sizeof(int);

    soma= Sum(Arq,Tam);
    printf("Total %i MBs de Arquivos!\n\n",soma);

    for(i=0;i<Tam ;i++)
    {
        if(PenDrive >= Arq[i])
        {
            printf("Adicionou ao PenDrive %i MB(s)\n",Arq[i]);
            PenDrive -= Arq[i];
            Arq[i] = 0;
        }
    }
    printf("Sobrou %i MBs no Pendrive!\n",PenDrive);

    for(i=0;i<Tam ;i++)
    {
        if(Arq[i] !=0)
            printf("Fora do PenDrive %i MB(s)\n",Arq[i]);
    }
    soma= Sum(Arq,Tam);
    printf("Sobrou %i MBs em Arquivos!\n",soma);
    return 0;
}

int* Ordenar(int *tList,int Tam)
{
    int aux;
    int i,j;
    for(i=0;i<Tam-1;i++)
        for(j=i+1;j<Tam;j++)
            if(tList[i] > tList[j])
            {
                aux = tList[j];
                tList[j] = tList[i];
                tList[i] = aux;
            }
    return tList;
}

int Sum(int *tList,int Tam)
{
    int Retorno = 0;
    int i;
    for(i=0;i<Tam;i++)
        Retorno+=tList[i];

    return Retorno;
}
