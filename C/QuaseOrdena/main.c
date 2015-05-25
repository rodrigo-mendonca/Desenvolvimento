#include <unistd.h>
#include <stdio.h>
#include <windows.h>

#define TAM 10;

void list_print(int *);
void list_ordenar(int *);
int list_confere_ordem(int *);
void list_posicao(int);
void list_trocar(int*,int,int);
void troca(int *,int,int);

int Speed = 100;

int main()
{
    int Tam = TAM;
    int *List = malloc(sizeof(int)*Tam);

    int i;
    for(i = 0;i<Tam;i++)
        List[i] = Tam - i;

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
        printf("\nOrdenado!");
    }
    Sleep(Speed*3);

    return(Erro);
}

void list_ordenar(int *List)
{
    int Tam = TAM;
    int i,j = 0;

    while(list_confere_ordem(List)){
        system("cls");
        list_print(List);
        //printf(" 1 | 2 | 3 | 4 | 5 | 6 | 7 | 8 | 9 | 10| 11| 12|\n");
        list_posicao(j);

        Sleep(Speed);
        if(List[j] > List[j+1]){


            list_trocar(List,j,j+1);
            j--;
            if(j< 0)
                j=0;
        }
        else
            j++;
    }

}

void list_posicao(int Pos)
{
    printf("%c",32);
    int j;
    for(j = 1;j<= Pos*4;j++)
        printf("%c",32);

    printf("%c",24);
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
