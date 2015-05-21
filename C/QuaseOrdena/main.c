#include <unistd.h>
#include <stdio.h>
#include <windows.h>

#define TAM 10;


void print_list(int *);
void ordenar_list(int *);
void confere_ordem_list(int *);

int main()
{
    int Tam = TAM;
    int *List = malloc(sizeof(int)*Tam);

    int i;
    for(i = 0;i<Tam;i++)
        List[i] = i;

    for(i = 0;i< Tam ;i++)
    {
        print_list(List);
        //printf(" 1 | 2 | 3 | 4 | 5 | 6 | 7 | 8 | 9 | 10| 11| 12|\n");
        printf("%c",32);

        int j;
        for(j = 1;j<=i*4;j++)
            printf("%c",32);

        printf("%c",24);

        Sleep(500);
        system("cls");
    }

    return 0;
}

void print_list(int *List)
{
    int Tam = TAM;
    int i;
    for(i=0;i<Tam;i++)
    {
        printf(" %i |",List[i]);
    }
    printf("\n");
}

void ordenar_list(int *List)
{

}

void confere_ordem_list(int *List)
{

}
