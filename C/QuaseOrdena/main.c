#include <unistd.h>
#include <stdio.h>
#include <windows.h>
#include <time.h>

#define TAM 10;

void list_print(int *);
void list_ordenar(int *);
int list_confere_ordem(int *);
void list_trocar(int*,int,int);
void troca(int *,int,int);

int Speed = 100;

int main()
{
    int Tam = TAM;
    int *List = malloc(sizeof(int)*Tam);
    srand (time(NULL));

    int i;
    for(i = 0;i<Tam;i++)
        List[i] = (int) 1 + (rand() % 20);

    list_ordenar(List);

    return 0;
}

void list_print(int *List)
{
    int Tam = TAM;
    int i;
    for(i=0;i<Tam;i++)
    {
        char str[15];
        sprintf(str, "%d", List[i]);

        printf("%3s|",str);
    }
    printf("\n");
}

int list_confere_ordem(int *List)
{
    int Tam = TAM;
    int i;
    int Erro = 0;
    for(i=1;i<Tam;i++)
    {
        if(List[i-1] > List[i])
        {
            Erro = 1;
            break;
        }
    }

    if(Erro)
        printf("\nNao Ordenado!");
    else{
        system("cls");
        list_print(List);
        printf("\nOrdenado!\n\n");
    }
    Sleep(Speed*3);

    return(Erro);
}

void list_ordenar(int *List)
{
    int i,tro = 0,inv = 0;
    int n = TAM;

    while(list_confere_ordem(List)){
        int ini = -1,fim = -1;
        system("cls");
        list_print(List);

        for(i=1;i<n;i++){
            if(ini < 0){
                if(List[i-1] > List[i])
                    ini = i-1;
            }

            if(fim < 0 && ini > -1){
                if(List[i - 1] < List[i] && ini !=i - 1)
                    fim = i - 1;
            }
        }

        if(ini == -1)
            ini = 0;
        if(fim == -1)
            fim = n-1;

        Sleep(Speed);
        list_trocar(List,ini,fim);

        if(ini+1 == fim)
            tro++;
        else
            inv++;
    }
    printf("Total de Operacoes\n");
    printf("Troca:%i\n",tro);
    printf("Inversa:%i\n",inv);
}

void list_trocar(int *List,int Ini,int Fim)
{
    if(Ini == Fim || Ini > Fim)
        return;

    int i,I1,I2;
    int Tam = (int)1 + (Fim - Ini) / 2;
    for(i=0;i<Tam;i++)
    {
        I1 = i + Ini;
        I2 = Fim - i;
        troca(List,I1,I2);
    }
}

void troca(int *List,int ind1,int ind2)
{
    int Aux = List[ind1];
    List[ind1] = List[ind2];
    List[ind2] = Aux;
}
