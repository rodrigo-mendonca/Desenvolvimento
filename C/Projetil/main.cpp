#include <cstdlib>
#include <iostream>
#include <math.h>
#include <String.h>
#include <fstream>

using namespace std;

// metodos
double LerGraus();
double d2r(int nGraus);
string CalculaPontos();

// variaveis
const double nG = 9.80665; // aceleraçao da gravidade
double nGrausGiro,nGrausBala,nSpeed,nPasso;

int main(int argc, char *argv[])
{
    // lê angulo de disparo 
    cout << "Digite o Angulo de disparo.>";
    nGrausGiro = LerGraus();
    cout << "Digite o Angulo da bala.>";
    // lê angulo da bala
    nGrausBala = LerGraus();
    cout << "Digite a velocidade.>";
    // lê velocidade
    cin >> nSpeed;
    cout << "Espacamentos entre os pontos.>";
    // lê espaçãmentos entre os pontos
    cin >> nPasso;
   
    string cFile = CalculaPontos();
    //cout << cFile;
    
    //ofstream myFile("Exporte.txt");
    //myFile << cFile;
    //myFile.close();
    
    system("PAUSE");
    return EXIT_SUCCESS;
}

string CalculaPontos()
{
    char cRetorno[] = "",cAux[] = "";
    //cordenadas basicas
    float nX=0,nY=0,nZ=0;
    //velocidades
    float nVx=0,nVy=0,nVz=0;
    //valores iniciais dos eichos
    float nXi=0,nYi=0,nZi=0;
    //Auxiliar
    float nRaiz=0;
    while(nZ >= 0 || nX==0)
    {
    	//calcula o proximo X
    	nX = nX + nPasso;
    	
    	// pela equação da reta acha o Y
     	nY = tan(nGrausGiro) * nX;
     	
     	//velocidade no plano
     	nVy = cos(nGrausBala) * nSpeed;
     	
     	//calcula dimensão do vetor resultante para calculo da altura
     	nRaiz = sqrt(pow(nX,2)+pow(nY,2));
     	
     	//calculo de balistica
    	nZ = nRaiz * tan(nGrausBala) - (nG*pow(nRaiz,2))/(2*(pow(nVy,2)));
        
    	//sprintf(cAux,"point %f,%f,%f \n",nX,nY,nZ);
    	//strcat(cRetorno, cAux);
     }
     return(cRetorno);
}

/*
  
  FUNÇÔES ALXILIARES
  
*/

double LerGraus()
{
    int nGraus;
    bool lOK = false;
    
    while(!lOK)
    {
        
        cin >> nGraus;
        
        if ( !(nGraus < 0 || nGraus > 360) )
              lOK = true;     
    }
        
    
    return d2r(nGraus);
        
}

double d2r(int nGraus)
{
       return (nGraus/180)* M_PI;
}

