#include <iostream>
#include "..\Classes\WinSockClass.h"

using namespace std;

int nPort = 90; // porta para se conectar com o servidor
WinSockClass oWinSock = WinSockClass(nPort);

int main()
{
    cout << "Esperando...!";
    if(oWinSock.IniciaServer()==0)
        return 0;

    oWinSock.AguardandoClient();
    Proxy *oProxy = (Proxy *)malloc(sizeof(Proxy));

    oWinSock.Receber((char *)oProxy,sizeof(Proxy));

    cout << "\nAviaoX - "         << oProxy->AviaoX;
    cout << "\nAviaoY - "         << oProxy->AviaoY;
    cout << "\nAviaoZ - "         << oProxy->AviaoZ;
    cout << "\nAnguloXY_Tiro - "  << oProxy->AnguloXY_Tiro;
    cout << "\nAnguloZ_Tiro - "   << oProxy->AnguloZ_Tiro;


    oProxy->AviaoX          = 20;
    oProxy->AviaoY          = 30;
    oProxy->AviaoZ          = 40;
    oProxy->AnguloXY_Tiro   = 50;
    oProxy->AnguloZ_Tiro    = 60;
    oWinSock.Enviar((char *)oProxy,sizeof(Proxy));

    cout << "\n" ;
    system("PAUSE");

    return 0;
}
