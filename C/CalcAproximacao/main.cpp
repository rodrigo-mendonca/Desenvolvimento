#include <stdio.h>
#include <math.h>
#include <iostream>
#include <stdlib.h>
#define PI 3.14159265359

// metodos
void Menu();
void Teste();
void PontoMedio();
void Trapezio();
void Simpson1_3();
void Simpson3_8();
void PontoMedioComp();
void TrapezioComp();
void CalcParts(double *);
double f(double);
double fl(double);
double integral(double,double);

// variaveis
int nEscolha = -1,nFuncao,nIni,nFim,nPart;

void Teste()
{
    nIni    = 0;
    nFim    = 1;
    nFuncao = 1;
    nPart   = 100;

    printf("Ponto Medio\n");
    PontoMedio();

    printf("\nTrapezio\n");
    Trapezio();

    printf("\nSimpson 1/3\n");
    Simpson1_3();

    printf("\nSimpson3/8\n");
    Simpson3_8();

    printf("\nPonto Medio Composto\n");
    PontoMedioComp();

    printf("\nTrapezio Composto\n");
    TrapezioComp();
}

int main()
{
    // funcao de teste
    Teste();
    nEscolha = -1;
    nFuncao  = 0;

    while(nEscolha!=0){
        // exibe o menu para o usuario
        Menu();

        // executa de acordo com a escolha
        switch(nEscolha){
            case 1:
                printf("\nResultados\n");
                PontoMedio();
                break;
            case 2:
                printf("\nResultados\n");
                Trapezio();
                break;
            case 3:
                printf("\nResultados\n");
                Simpson1_3();
                break;
            case 4:
                printf("\nResultados\n");
                Simpson3_8();
                break;
            case 5:
                printf("\nResultados\n");
                PontoMedioComp();
                break;
            case 6:
                printf("\nResultados\n");
                TrapezioComp();
                break;
        }
        nFuncao=0,nIni=0,nFim=0;nEscolha=-1;
    }
    return 0;
}

void Menu()
{
    printf("\n\nEscolha uma opcao.\n\n");
    printf("1 - Regra do ponto medio\n");
    printf("2 - Regra do trapezio\n");
    printf("3 - Regra 1/3 - de Simpson\n");
    printf("4 - Regra 3/8 - de Simpson\n");
    printf("5 - Regra do ponto medio composta\n");
    printf("6 - Regra do trapezio composta\n");
    printf("0 - Sair\n");

    while(nEscolha < 0 || nEscolha > 6){
        printf("Opcao ->");
        scanf("%d",&nEscolha);
    }
    if(nEscolha==0)
        return;

    printf("\nEscolha uma Funcao.\n\n");
    printf("1 - exp(x)\n");
    printf("2 - sin(x)\n");
    printf("3 - cos(x)\n");

    while(nFuncao < 1 || nFuncao > 6){
        printf("Opcao ->");
        scanf("%d",&nFuncao);
    }

    printf("\nIntervalo Inicial A ->");
    scanf("%d",&nIni);

    printf("\nIntervalo Final B ->");
    scanf("%d",&nFim);

    nPart = 1;
    if(nEscolha>3){
        printf("\nNumero de partes ->");
        scanf("%d",&nPart);
        if(nPart<=0)
            nPart = 1;
    }
}

// f(x)
double f(double nX)
{
    double nRetorno = 0;

    switch(nFuncao){
        case 1:
            nRetorno = exp(nX);
            break;
        case 2:
            nRetorno = sin(nX);
            break;
        case 3:
            nRetorno = cos(nX);
            break;
    }
    return(nRetorno);
}

//f'(x)
double fl(double nX)
{
    double nRetorno = 0;

    switch(nFuncao){
        case 1:
            nRetorno = exp(nX);
            break;
        case 2:
            nRetorno = -cos(nX);
            break;
        case 3:
            nRetorno = sin(nX);
            break;
    }
    return(nRetorno);
}

double integral(double nA,double nB){
    return fl(nB)-fl(nA);
}

void PontoMedio()
{
    double nEqua,nApro,nDif,nMed;
    nEqua = integral(nIni,nFim);

    nMed = (nFim - nIni);
    nApro = nMed * f(nMed);

    nDif = fabs(nEqua - nApro);

    printf("Aproximacao = %f\n",nApro);
    printf("Equacao     = %f\n",nEqua);
    printf("Dif.        = %f\n",nDif);
}

void Trapezio()
{
    double nEqua,nApro,nDif,nMed;
    nEqua = integral(nIni,nFim);

    nMed = (nFim - nIni);
    nApro = (f(nIni)+f(nFim))*nMed/2;

    nDif = fabs(nEqua - nApro);

    printf("Aproximacao = %f\n",nApro);
    printf("Equacao     = %f\n",nEqua);
    printf("Dif.        = %f\n",nDif);
}

void Simpson1_3()
{
    double nEqua,nApro,nDif,nMed;
    nEqua = integral(nIni,nFim);
    double nH = (double)(nFim - nIni)/2,nC;

    nC    = nIni+nH;
    nApro = (nH/3)*(f(nIni) + (4*f(nC)) + f(nFim));

    nDif  = fabs(nEqua - nApro);

    printf("Aproximacao = %f\n",nApro);
    printf("Equacao     = %f\n",nEqua);
    printf("Dif.        = %f\n",nDif);
}

void Simpson3_8()
{
    double nEqua,nApro,nDif,nMed;
    int nN;
    nEqua = integral(nIni,nFim);
    // forca para multiplo de 3
    nN = nPart;
    if((nN % 3) == 0)
        nN = 3*((nN/3)+1);

    double nH = (double)(nFim - nIni)/nN,nC;
    double nM = 0,nNM = 0,nX = 0;

    nX = nIni + nH;
    int nI;
    for(nI=1;nI<nN;nI++){
        if((nI % 3) == 0)
            nM  = nM + f(nX);
        else
            nNM = nNM + f(nX);
        nX = nX + nH;
    }

    nApro = ((nH*3)/8) * (f(nIni)+f(nFim)+(3*nM)+(2*nNM));
    nDif = fabs(nEqua - nApro);

    printf("Aproximacao = %f\n",nApro);
    printf("Equacao     = %f\n",nEqua);
    printf("Dif.        = %f\n",nDif);
}

void PontoMedioComp()
{
    double nParts[nPart];
    CalcParts(nParts);

    double nEqua,nApro = 0,nDif,nMed;
    nEqua = integral(nIni,nFim);

    int nI=0;
    for(nI=1;nI<nPart;nI++){
        nMed = (nParts[nI] - nParts[nI-1]);
        nApro = nApro+(f(nParts[nI]))*nMed;
    }

    nDif = fabs(nEqua - nApro);

    printf("Aproximacao = %f\n",nApro);
    printf("Equacao     = %f\n",nEqua);
    printf("Dif.        = %f\n",nDif);
}

void TrapezioComp()
{
    double nParts[nPart];
    CalcParts(nParts);

    double nEqua,nApro = 0,nDif,nMed;
    nEqua = integral(nIni,nFim);

    int nI=0;
    for(nI=1;nI<nPart;nI++){
        nMed = (nParts[nI] - nParts[nI-1]);
        nApro = nApro+(f(nParts[nI-1])+f(nParts[nI]))*nMed/2;
    }

    nDif = fabs(nEqua - nApro);

    printf("Aproximacao = %f\n",nApro);
    printf("Equacao     = %f\n",nEqua);
    printf("Dif.        = %f\n",nDif);
}

void CalcParts(double * tnParts){
    int nJ;
    double nInt;

    for(nJ=1;nJ<=nPart;nJ++){
        nInt = (double) (nFim - nIni)/nPart;
        nInt = nInt*nJ;
        nInt = nIni + nInt;
        tnParts[nJ-1] = nInt;
    }
}
