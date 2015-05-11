#include <stdio.h>
#include <stdlib.h>
#include <string.h>

/* run this program using the console pauser or add your own getch, system("pause") or input loop */
int MultiMod(int tnA,int tnB,int tnN);
void Mdc(int tnA,int tnB);
int MdcAux(int tnA,int tnB);
void Euclides(int tnA,int tnB);
void TabelaEuclides(int tnA,int tnB);
void TableVeryCrazy(int tnA,int tnB);
void TabelaZ(int tnZ);

int main(int argc, char *argv[]) {
	int nExe = 5,nA,nB,nZ;
	nA = 48;
	nB = 30;
	nZ = 9;
	
	/*
	1* - Calculo de MDC
	2 - Calcula a tabela Zn(Escolher qual N em nZ)
	3* - Calculo de MDC com Euclides
	4* - Tabela com base em Euclides
	5* - Tabela muito louca
	
	
	* nExe 1,3,4,5 usando as variaveis nA e nB para o calculo
	
	*/	
	
	switch(nExe){
		case 1:
			Mdc(nA,nB);
			break;
		case 2:
			TabelaZ(nZ);
			break;
		case 3:
			Euclides(nA,nB);
			break;
		case 4:
			TabelaEuclides(nA, nB);
			break;
		case 5:
			TableVeryCrazy(nA,nB);
			break;
	}
	system("pause");
	return 0;
}

int MultiMod(int tnA,int tnB,int tnN){
	return (tnA*tnB)%tnN;
}

void TabelaZ(int tnZ){
	int nT = tnZ+1;
	int nMatriz[nT][nT+1];
	
	int nI = 0,nJ=0,nMM=0;
	nMatriz[0][0] = 0;
	for(nI=0;nI<nT;nI++){
		nMatriz[0][nI+1] = nI;
		nMatriz[nI+1][0] = nI;
	}
	for(nI=0;nI<nT-1;nI++){
		for(nJ=0;nJ<nT-1;nJ++){
			nMM= MultiMod(nI,nJ,tnZ);
			nMatriz[nI+1][nJ+1] = nMM;
		}
	}
	printf("Tabela Z%i: \n\n",tnZ);
	for(nI=0;nI<nT;nI++){
		for(nJ=0;nJ<nT;nJ++){
			printf("|%i",nMatriz[nI][nJ]);
		}
	printf("\n");
	}
}

void Euclides(int tnA,int tnB){
	int nMax,nMin,nMod;
	
	if(tnA>tnB){
		nMax=tnA;
		nMin=tnB;
	}
	else{
		nMax=tnB;
		nMin=tnA;
	}
	
	nMod = nMax%nMin;
	while(nMod!=0){
		nMax=nMin;
		nMin=nMod;
		nMod = nMax%nMin;
	}
	printf("O MDC(por Euclides) de %i e %i e %i\n\n",tnA,tnB,nMin);
}

void Mdc(int tnA,int tnB){
    int nRes = MdcAux(tnA,tnB);
    printf("O MDC de %i e %i e %i\n\n",tnA,tnB,nRes);
}

int MdcAux(int tnA,int tnB)
{
    if(tnB == 0) return tnA;
    else
    return MdcAux(tnB,tnA%tnB); 
}

void TabelaEuclides(int tnA, int tnB) {
	int nI, nJ;
	
	printf("a = %d, b = %d\n\n", tnA, tnB);
	
	printf(" x\\y|");
	for (nI = -4; nI < 5; nI++)
		printf("%5d", nI);
	
	printf("\n");
	
	for (nI = 0; nI < 10; nI++)
		printf("_____");
	
	printf("\n");
	
	for (nI = -4; nI < 5; nI++) {
		printf("%4d|", nI);
		for (nJ = -4; nJ < 5; nJ++)
			printf("%5d", tnA*nI + tnB*nJ);
		printf("\n");
	}
}


void TableVeryCrazy(int tnA,int tnB){
    printf("Dividendo =   Quociente  *  Divisor  +  Resto    h    k\n");
    printf("_______________________________________________________\n");

    if (tnB > tnA) {
       int nT = tnA;
       tnA 	  = tnB;
       tnB 	  = nT;
    }

    int nH2 = 0, nH1 = 1;
    int nK2 = 1, nK1 = 0;

    int nD = tnB;
    int nQ = tnA / tnB;
    int nR = tnA % tnB;
    int nH = nQ*nH1 + nH2;
    int nK = nQ*nK1 + nK2;

    printf("%9d = %11d  *  %7d  +  %5d %4d %4d\n", tnA, nQ, nD, nR, nH, nK);

    while (nR > 0) {
       int nNr = nD % nR;
       nQ  = nD / nR;
       
       nH2 = nH1;
       nH1 = nH;
       nH  = nQ*nH1 + nH2;

       nK2 = nK1;
       nK1 = nK;
       nK  = nQ*nK1 + nK2;

       printf("%9d = %11d  *  %7d  +  %5d %4d %4d\n", nD, nQ, nR, nNr, nH, nK);

       nD = nR;
       nR = nNr;
    }

    printf("\n%d * %d  +  %d * %d   =  %d\n", nH, nK1, nK, nH1, nH*nK1 + nK*nH1);
    printf("%d * %d  -  %d * %d   =  %d\n", nH, nK1, nK, nH1, nH*nK1 - nK*nH1);
}

