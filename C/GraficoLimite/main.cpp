#include <stdio.h>
#include <iostream>
#include <stdlib.h>
#include <graphics.h>
#include <math.h>

void DesenhaPonto (double,double);
void DesenhaLinha (double,double,double,double);
void DesenhaEscala (void);
double PixelToCode (int, char);
double f(double);
int CodeToPixel (double, char);
bool VerifEspecial(char *);

using namespace std;

//Propriedades
int nEscala = 100;
int nXTela  = 1000;
int nYTela  = 1000;
int nXCentro = nXTela / 2;
int nYCentro = nYTela / 2;
double nA, nB, nC;

double f(double nX){
    double nRet;
    // calcula a funcao
    nRet = 2 * pow(nX,2) + 1 * nX + 1;
    return nRet;
}

int main( )
{
    initwindow( nXTela , nYTela , "GraficoLimite" ); //Cria a tela do grafico
    setbkcolor(WHITE); // Muda a cor do fundo
    cleardevice(); // Limpa a tela
    DesenhaEscala(); // Desenha a escala de fundo
    double nIni,nFim,nH; //Variaveis de controle do inicio e fim do intervalo
    int nPart;
    
    //LE AS INFORMAÇÕES DO USUARIO
    cout << "Digite o inicio do intervalo.\n> ";
    cin >> nIni;
    cout << "Digite o final do intervalo.\n> ";
    cin >> nFim;
    cout << "Digite a divisao do intervalo.\n> ";
    cin >> nPart;
    cout << "-- Tabela Diferencial --\n" , "X   -  f(x)  -   Diferença -  Media X \n";
    
    // Cria os intevalos
    double nValores[nPart][4] ;
    
    // Zera todos os valores
    for(int nI = 0; nI < nPart;nI++)
    {
        nValores[nI][0] = 0;
        nValores[nI][1] = 0;
        nValores[nI][2] = 0;
        nValores[nI][3] = 0;
    }
    
    //seta valor H de divisão
    nH = (nFim - nIni) / nPart;

    //Monta Grafico e desenha a tabela de diferencias
    double nAux;
    for(int i = 0;i < nPart;i++)
    {
        nAux           = nIni + (i * nH);
        
	   	nValores[i][0] = nAux;
    	nValores[i][1] = f(nAux);
    	nValores[i][2] = (f(nAux + nH) - nValores[i][1]) / nH;
    	nValores[i][3] = (nAux * 2 + nH) / 2;
        
        // Desenha os pontos no grafico
        DesenhaPonto(nValores[i][0],nValores[i][1]); 
        DesenhaPonto(nValores[i][3],nValores[i][2]); 
        
        char cBuffer[80];
    
        sprintf( cBuffer, "%g   |   %g   |   %g   |   %g\n", nValores[i][0] , nValores[i][1], nValores[i][2], nValores[i][3]);
    
        cout <<  cBuffer; 
    }
    
    // Desenha as linhas que ligam os pontos
    for(int i = 0;i < nPart - 1;i++)
    {
        setcolor(BLUE); 
        DesenhaLinha(nValores[i][0], nValores[i][1], nValores[i+1][0], nValores[i+1][1]);
        setcolor(RED);  
        DesenhaLinha(nValores[i][3], nValores[i][2], nValores[i+1][3], nValores[i+1][2]);
    }

    while( !kbhit() ); 
	closegraph( );
	return(0);
}

void DesenhaPonto(double tnX,double tnY)
{
    int nX = CodeToPixel(tnX,'X');
    int nY = CodeToPixel(tnY,'Y');
    
    char cBuffer[30];
    
    sprintf( cBuffer, "(%g,%g)", tnX , tnY);
    
    settextstyle(2,1,4);
    outtextxy( nX + 20, nY - 2, cBuffer );
    putpixel(nX,nY, COLOR(0,0,0));	   
}

void DesenhaLinha(double tnX1,double tnY1,double tnX2,double tnY2)
{
    int nX1 = CodeToPixel(tnX1,'X');
    int nY1 = CodeToPixel(tnY1,'Y');
    int nX2 = CodeToPixel(tnX2,'X');
    int nY2 = CodeToPixel(tnY2,'Y');
    
    line(nX1,nY1,nX2,nY2);	   
}

void DesenhaEscala()
{   int nAux  = 0;
    int nLine = 2;
    
    for(int nI = 0;nI <= nXTela; nI++)
    {
            if(nAux == nI)
            {
                setcolor(BLACK);
                line(nI,nYCentro,nI,nYCentro - nLine);
                nAux += nEscala;
            }
            putpixel(nI,nYCentro, COLOR(0,0,0));
    }
    
    nAux = 0;
    for(int nI = 0;nI <= nYTela; nI++)
    {
            if(nAux == nI)
            {
                setcolor(BLACK);
                line(nXCentro,nI,nXCentro + nLine,nI);
                nAux += nEscala;
            }
            putpixel(nXCentro,nI, COLOR(0,0,0));
    }
}

double PixelToCode(int tnPixel, char tcTipo)
{
     int nCentro = 0;
     
     if(tcTipo == 'X'){nCentro = nXCentro;}
     else{nCentro = nYCentro;}
     
     if(tnPixel == nCentro){return(0);}
     
     double nCode = (double) ((tnPixel - nCentro) / nEscala);
     
     if(tcTipo == 'X'){return(nCode);}
     else{return(-nCode);}
}

int CodeToPixel(double tnCode, char tcTipo)
{
     int nCode = (int) round(tnCode * nEscala);
     int nCentro = 0;
     
     if(tcTipo == 'X'){nCentro = nXCentro;}
     else{nCentro = nYCentro;}
     
     if(tnCode == 0){return(nCentro);}
     
     if(tcTipo == 'X'){return(nCentro + nCode);}
     else{return(nCentro - nCode);}
}
