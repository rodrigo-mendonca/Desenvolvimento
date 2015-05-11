#include <iostream>
#include <stdio.h>
#include <math.h>
#include "include\Grafico.h"

using namespace std;
void MatrizVanderMonde();
void CalcErro();
int Fatorial(int);

int main()
{
    CalcErro();

    return 0;
}

void CalcErro(){
    int nOpcao=0;
    bool lParar = false;
    while(!lParar){
        printf("\nEscolha a Funcao!\n\n1-Sen(x)\n2-Cos(x)\n3-Exp(x)\nOpcao -> ");
        scanf("%d",&nOpcao);

        if(nOpcao > 0 and nOpcao < 4)
            lParar = true;
    }
    int nX, nI,nAte = 50;
    double nSen,nCos,nExp,nCalc;

    if(nOpcao==1){
        nI = 1;
        for(nX=1;nX<=nAte;nX++){
            nI = nI + 2;

            nSen  = sin(nX);
            nCalc = nX - pow(nX,nI)/Fatorial(nX);

            printf("%d|%d\n",nSen,nCalc);
        }
    }
    if(nOpcao==2){
        nI = 0;
        for(nX=1;nX<=nAte;nX++){
            nI = nI + 2;

            nCos  = cos(nX);
            nCalc = 1 - pow(nX,nI)/Fatorial(nX);
        }
    }
    if(nOpcao==3){
        nI=1;
        for(nX=1;nX<=nAte;nX++){
            nI = nI + 1;
            nExp  = exp(nX);
            nCalc = 1 + pow(nX,nI)/Fatorial(nX);
        }
    }
}

int Fatorial(int nX){
    int nN,nFat = 1;
    nN = nX;
    while(nN > 1)
    {
        nN--;
        nFat = nN * nFat;
    }
    nFat = nX * nFat;
    return nFat;
}
