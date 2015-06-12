#include <unistd.h>
#include <stdio.h>
#include <time.h>
#include <stdlib.h>
#define TAM 10;

void list_print(int *);
void list_ordenar(int *);
int list_confere_ordem(int *);
void inverte(int*,int,int);
void troca(int *,int,int);
void limpar();
void espera(int);
int buscatroca(int *);

int Speed = 100;

int main()
{
    int random = 1; // define se deve usar um numero aleatorio ou pedir um numero para o usuario
    int input;
    int Tam = TAM;
    int *List = (int*)malloc(sizeof(int)*Tam);
    srand (time(NULL));

    int i;
    for(i = 0;i<Tam;i++){
        
        if(random)
            List[i] = (int) 1 + (rand() % 20);
        else{
            printf("Digite o numero %d\n-",i);
            scanf("%i",&input);
            List[i] = input;
        }
    }
    list_ordenar(List);

    return 0;
}

void list_print(int *List)
{
    int Tam = TAM;
    int i;
    for(i=0;i<Tam;i++)
    {
        char str[4];
        sprintf(str, "%d", List[i]);

        printf("%3s|",str);
    }
    printf("\n");
}

int list_confere_ordem(int *List)
{
    int Tam = TAM;
    int i;
    int Erro = 1;
    for(i=1;i<Tam;i++)
    {
        if(List[i-1] > List[i])
        {
            Erro = 0;
            break;
        }
    }
    if(!Erro){
        printf("\nNao Ordenado!\n");
    }
    else{
        printf("\nOrdenado!\n\n");
    }
    espera(Speed*3);

    return(Erro);
}

void list_ordenar(int *List)
{
    int i,tro = 0,inv = 0;
    int n = TAM;

    // ordena com uma troca
    if(buscatroca(List))
        tro = 1;
    limpar();
    list_print(List);
    // se nao consegui ordena aplica a inversa 1 ou n vezes ate ordenars
    while(!list_confere_ordem(List)){
        int ini = -1,fim = -1;
        
        limpar();
        
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

        espera(Speed);
        // muda uma subsequencia
        inverte(List,ini,fim);

        inv++;
    }
    printf("Total de Operacoes\n");
    printf("Troca:%i\n",tro);
    printf("Inversa:%i\n",inv);
}

int buscatroca(int *List){
    int i;
    int n = TAM;
    int atual = 0;

    int ini = -1,fim = -1;
    for(i=1;i<n;i++){
        if(ini < 0){
            if(List[i-1] > List[i])
                ini = i-1;
        }

        if(fim < 0 && ini > -1){
            if(List[ini] < List[i] && ini !=i - 1)
                fim = i - 1;
        }
    }

    if(ini == -1)
        ini = 0;
    if(fim == -1)
        fim = n-1;

    troca(List,ini,fim);
    
    return list_confere_ordem(List);
}

void inverte(int *List,int Ini,int Fim)
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

void limpar(){
    #if defined(WIN32) || defined(_WIN32) || defined(__WIN32) && !defined(__CYGWIN__)
        system("cls");
    #else
        system("clear");
    #endif
}

void espera(int Tempo){

    #if defined(WIN32) || defined(_WIN32) || defined(__WIN32) && !defined(__CYGWIN__)
        Sleep(Tempo);
    #else
        usleep(Tempo*1000);
    #endif
}
