#include <iostream>
#include "..\Classes\WinSockClass.h"

using namespace std;

int nPort = 90; // porta para se conectar com o servidor
char cServer[20] = "localhost"; // endereço ou nome do servidor
WinSockClass oWinSock = WinSockClass(nPort);

int main()
{

        // inicia o client
	if(oWinSock.IniciaClient(cServer)==0)
        return 0;

    Proxy *oProxy = (Proxy *)malloc(sizeof(Proxy));
    system("PAUSE");
    oProxy->AviaoX          = 150;
    oProxy->AviaoY          = 250;
    oProxy->AviaoZ          = 350;
    oProxy->AnguloXY_Tiro   = 50;
    oProxy->AnguloZ_Tiro    = 40;

    oWinSock.Enviar((char *)oProxy,sizeof(Proxy));


    oWinSock.Receber((char *)oProxy,sizeof(Proxy));


    cout << "\nAviaoX - "         << oProxy->AviaoX;
    cout << "\nAviaoY - "         << oProxy->AviaoY;
    cout << "\nAviaoZ - "         << oProxy->AviaoZ;
    cout << "\nAnguloXY_Tiro - "  << oProxy->AnguloXY_Tiro;
    cout << "\nAnguloZ_Tiro - "   << oProxy->AnguloZ_Tiro;

    cout << "\nEnviado!\n";
    system("PAUSE");

    return 0;
}
