#include <stdio.h>
#include <iostream>
#include <stdlib.h>
#include <graphics.h>
#include <math.h>
#include <windows.h>

using namespace std;

// lista de metodos usados
void   DesenhaPonto  (double,double);
void   DesenhaLinha  (double,double,double,double);
void   DesenhaEscala (void);
double PixelToCode   (int, char);;
double Round         (double);
int    CodeToPixel   (double, char);
bool   VerifEspecial (char *);
// funcoes
double f             (double);
double fl            (double,double);

//Propriedades
int nEscala      = 40; // Defini o espasamentos dfas coordenadas
int nXTela       = 1000; // Largura da Tela
int nYTela       = 1000; // Altura da tela
int nXCentro     = nXTela / 2; // Ponto central da largura
int nYCentro     = nYTela / 2; // Ponto central da altura
int nDiv         = 100; // Espaço entre os pontos

// configuração das funcoes
double nIni      = -10; // começo da funcao
double nFim      = 10; // final da funcao
int nChute       = 0; // numero dado pelo usuario
double nPrecisao = 3; // para cada 0 equivate a uma casa decimal
int nTeste       = 0; // numero do teste, 0 desativa o teste

void Config()
{
     switch(nTeste){
          case 1:
               nChute = -1;
               break;
          case 2:
               nChute = 4;
               break;
          case 3:
               nChute = 2;
               nIni   = 2;
               nFim   = 3;
               nPrecisao = 5;
               break;
          case 4:
               nChute = 1;
               nIni   = 0;
               nFim   = 2;
               nPrecisao = 4;
               break;
          case 5:
               nChute = 1;
               break;
          default:
               nChute = 0;
               break;
    }
}

double f(double nX){
    double nRet;
    // calcula a funcao
    switch(nTeste){
          case 1:
               nRet      = (pow(nX,5)) + 2;
               break;
          case 2:
               nRet      = pow(nX,6) - pow(nX,4) + 3 * pow(nX,3) - 2 * nX;
               break;
          case 3:
               nRet      = 2 * pow(nX,3) - 6 * pow(nX,2) + 3 * nX + 1;
               break;
          case 4:
               nRet      = pow(nX,7) - 3 * pow(nX,5) + 4 * pow(nX,2) - 2;
               break;
          case 5:
               nRet      = pow(nX,3) - 3 * nX + 6;
               break;
          default:
               nRet      = cos(tan(nX));
               break;
    }
    return nRet;
}

double fl(double tnX,double tnH){
    double nRet;
    // calcula a funcao derivada
    nRet = (f(tnX + tnH) - f(tnX)) / tnH;
    return nRet;
}

int main( )
{
    Config(); //configura os parametros iniciais
    initwindow( nXTela , nYTela , "MetNewton" ); //Cria a tela do grafico
    setbkcolor(WHITE); // Muda a cor do fundo
    cleardevice(); // Limpa a tela
    DesenhaEscala(); // Desenha a escala de fundo
    double nPonto1,nPonto2 = 0,nH;
    //seta valor H de divisão
    nH = (nFim - nIni) / nDiv;

    //Monta Grafico
    for(int i = 1;i <= nDiv;i++)
    {
        nPonto1           = nIni + ((i - 1) * nH);
        nPonto2           = nIni + (i * nH);
        // Desenha os pontos no grafico
        setcolor(BLUE);  
        DesenhaLinha(nPonto1, f(nPonto1), nPonto2, f(nPonto2));
    }
    
    //LE AS INFORMAÇÕES DO USUARIO, SE NÃO FOR TESTE
    if(nTeste == 0){
        cout << "Digite o X inicial.\n> ";
        cin >> nChute;
    }
    
    char cBuffer[100]; //STRING DA TABELA
    double nCoor[2][2];//MATRIZ DE COORDENADAS
    nCoor[1][0] = nChute;
    
    // titulo da tabela
    cout << "X         f(x)            x - f(x)/f'(x)\n";
    
    while(nCoor[1][0] != nCoor[0][0])
    {
         nCoor[0][0] = Round(nCoor[1][0]);
         nCoor[0][1] = Round(f(nCoor[0][0]));
         nCoor[1][0] = Round(nCoor[0][0] - (f(nCoor[0][0])/fl(nCoor[0][0],nH)));
         nCoor[1][1] = 0;
         
         // Exibe uma tabela dos valores calculados
         sprintf( cBuffer, "%g         %g             %g\n", nCoor[0][0] , nCoor[0][1], nCoor[1][0]);
         cout << cBuffer;
         
        // Desenha o linha
        setcolor(RED);  
        DesenhaLinha(nCoor[0][0], nCoor[0][1], nCoor[1][0], nCoor[1][1]);
        
        //cria uma lentidao entre cada linha
        Sleep(300);
    }
    while( !kbhit() ); 
	closegraph( );
	return(0);
}

double Round(double tnNum)
{
       // O NUMERO VEZES DEZ ELEVADO A PRECISAO, TRUNCA AS CASAS DECIMAIS E DIVITE POR DEZ ELEVADO A PRECISAO
       return round(tnNum * pow(10,nPrecisao)) / pow(10,nPrecisao);
}

// metodos de desenho a baixo
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

    for(int nI = 0;nI <= nXTela;nI++)
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
