#include <cstdlib>
#include <stdio.h>
#include <stdio.h>
#include "graficos.h"

#define MAX 100

using namespace std;

void PrintaMatriz(double M[][MAX], int nTamanho);
void LimpaMatriz(double M[][MAX], int nTamanho);
void Acerta_Diagonal(double M[][MAX],double I[][MAX],int nDIMENSAO);
double Fator(double M[][MAX],int nL1,int nL2,int nCOL);
void Cria_identidade(double I[][MAX], int nTAM);
int Ajust(double (*M)[MAX], double (*IDENT)[MAX],int tnLINHA, double tnMULT,int tnSOMA, int nDIMENSAO);
void InvertMatriz(double (*nMAT)[MAX],double (*nIDENT)[MAX],int tnTAM);
int CalcVander();

Graficos oGraf;

int CalcVander()
{
    int nOpcao=0;
    int nIni,nFim,nPre;
    bool lParar = false;
    
    oGraf.nEscala = 5;
    oGraf.nXTela = 1000;
    oGraf.nYTela = 1000;
    double nFun,nCalc;
    
    printf("Escolha um intervalo inicial -> ");
    scanf("%d",&nIni);
    
    printf("Escolha um intervalo final -> ");
    scanf("%d",&nFim);
    
    printf("Precisao -> ");
    scanf("%d",&nPre);  
    
    while(!lParar){
        printf("\nEscolha a Funcao!\n\n1-exp(x)\n2-sen(x)\n3-1/(1+x^2)\nOpcao -> ");
        scanf("%d",&nOpcao);

        if(nOpcao > 0 and nOpcao < 4)
            lParar = true;
    }
    oGraf.CriarTela();
    oGraf.DesenhaEscala();
    
    
}
int main(int argc, char *argv[])
{
    CalcVander();
    
    //le tamanho da matriz
    int nTAM = 0;
    double nInput;
    
    printf("Digite o tamanho da matriz quadrada:");
    scanf("%i",&nTAM);

    // cria matriz
    double nMAT[MAX][MAX];
    double nIDENT[MAX][MAX];
    
    LimpaMatriz(nMAT,MAX);
    
    for(int i=0; i<nTAM;i++)
    {
        for(int j=0;j<nTAM;j++)
        {
                printf("Valor da posição %i,%i :",i,j);
                scanf("%lf",&nInput);
                nMAT[i][j]=nInput;
        }
    }

    printf("Matriz:");
    PrintaMatriz(nMAT,nTAM);
    // inverte a matriz, a matriz inversa fica no lugar da identidade
    InvertMatriz(nMAT,nIDENT,nTAM);
    printf("\n");
    printf("Matriz Inversa:");
    PrintaMatriz(nIDENT,nTAM);
    
    printf("\n");
    system("PAUSE");
    return EXIT_SUCCESS;
}

void InvertMatriz(double (*nMAT)[MAX],double (*nIDENT)[MAX],int nTAM)
{    
    LimpaMatriz(nIDENT,MAX);
    Cria_identidade(nIDENT,MAX);
    
     //PRIMEIRO QUADRANTE
    int AUX=0;
    double nFATOR = 0;
    for(int H = 0; H < nTAM; H++)
   	{
    	for(int J = nTAM-1; J > H; J--)
   		{
    		nFATOR=Fator(nMAT, AUX ,J,H);
    	}
    	AUX++;
     }

     //SEGUNDO QUADRANTE
    AUX=nTAM;

    for(int H = nTAM-1; H > 0; H--)
    {
       AUX--;
      for(int J = 0; J < H;J++)
      {
            nFATOR=Fator(nMAT, AUX ,J,H);
      }
    }
    Acerta_Diagonal(nMAT,nIDENT,nTAM);
}


void PrintaMatriz(double M[][MAX], int nTamanho)
{
    double nOut;

     for(int i=0;i<nTamanho;i++)
     {
     printf("\n");
         for(int j=0; j < nTamanho;j++ )
         {
            nOut =  M[i][j];
            printf("%12.3f",nOut);
         }
     }
 }

double Fator(double M[][MAX],int nL1,int nL2,int nCOL)
{
    if(M[nL1,nCOL]==0)
        return 0;

	double nRET=(-(M[nL2][nCOL]))/M[nL1][nCOL];

	return nRET;
}

int Ajust(double (*M)[MAX], double (*IDENT)[MAX],int tnLINHA, double tnMULT,int tnSOMA, int nDIMENSAO)
{
    if (tnMULT==0)
       return 0;

    double N1,N2;

	for(int I = 0; I < nDIMENSAO; I++)
    {
        N1=M[tnLINHA][I];
        N2=M[tnSOMA][I];
        M[tnSOMA][I] = tnMULT*N1 + N2;
    }

	for(int I = 0; I < nDIMENSAO; I++)
    {
        N1=IDENT[tnLINHA][I];
        N2=IDENT[tnSOMA][I];
        IDENT[tnSOMA][I] = tnMULT*N1 + N2;
    }

    return 1;
}

void LimpaMatriz(double M[][MAX], int nTAMANHO)
{
    for(int n1=0; n1 < nTAMANHO;n1++)
    {
        for(int n2=0; n2<nTAMANHO; n2++)
        {
            M[n1][n2]=0;
        }
    }
}

void Cria_identidade(double I[][MAX], int nTAM)
{
    for(int x=0; x<nTAM;x++)
        I[x][x]=1;
}

void Acerta_Diagonal(double M[][MAX],double I[][MAX], int nDIMENSAO)
{
    for(int Y=0;Y<nDIMENSAO;Y++)
    {
        if (M[Y][Y] != 1)
		{
            for(int X = 0; X<nDIMENSAO;X++)
                I[Y][X]= I[Y][X] / M[Y][Y];

            M[Y][Y]	= 1;
		}
    }
}
