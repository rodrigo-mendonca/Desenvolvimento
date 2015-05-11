#include "..\include\Grafico.h"
#include <stdio.h>
#include <iostream>
#include <stdlib.h>
#include <graphics.h>
#include <math.h>

Grafico::Grafico()
{
    //ctor
}

Grafico::~Grafico()
{
    //dtor
}

void Grafico::CriarGrafico(){
    nXCentro = nAltura / 2;
    nYCentro = nLargura / 2;

    initwindow( nLargura , nAltura , "Grafico" ); //Cria a tela do grafico
    setbkcolor(WHITE); // Muda a cor do fundo
    cleardevice(); // Limpa a tela
//    DesenhaEscala(); // Desenha a escala de fundo
}
/*
void Grafico::DesenhaPonto(double tnX,double tnY)
{
    int nX = CodeToPixel(tnX,'X');
    int nY = CodeToPixel(tnY,'Y');

    char cBuffer[30];

    sprintf( cBuffer, "(%g,%g)", tnX , tnY);

    settextstyle(2,1,4);
    outtextxy( nX + 20, nY - 2, cBuffer );
    putpixel(nX,nY, COLOR(0,0,0));
}

void Grafico::DesenhaLinha(double tnX1,double tnY1,double tnX2,double tnY2)
{
    int nX1 = CodeToPixel(tnX1,'X');
    int nY1 = CodeToPixel(tnY1,'Y');
    int nX2 = CodeToPixel(tnX2,'X');
    int nY2 = CodeToPixel(tnY2,'Y');

    line(nX1,nY1,nX2,nY2);
}

void Grafico::DesenhaEscala()
{   int nAux  = 0;
    int nLine = 2;

    for(int nI = 0;nI <= nLargura; nI++)
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
    for(int nI = 0;nI <= nAltura; nI++)
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

double Grafico::PixelToCode(int tnPixel, char tcTipo)
{
     int nCentro = 0;

     if(tcTipo == 'X'){nCentro = nXCentro;}
     else{nCentro = nYCentro;}

     if(tnPixel == nCentro){return(0);}

     double nCode = (double) ((tnPixel - nCentro) / nEscala);

     if(tcTipo == 'X'){return(nCode);}
     else{return(-nCode);}
}

int Grafico::CodeToPixel(double tnCode, char tcTipo)
{
     int nCode = (int) round(tnCode * nEscala);
     int nCentro = 0;

     if(tcTipo == 'X'){nCentro = nXCentro;}
     else{nCentro = nYCentro;}

     if(tnCode == 0){return(nCentro);}

     if(tcTipo == 'X'){return(nCentro + nCode);}
     else{return(nCentro - nCode);}
}
*/
