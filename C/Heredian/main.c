#include <stdio.h>
#include <stdlib.h>
#include "../Lib/Include/GameLib.h"

void configobj();

int wigth        = 800;
int height       = 600;
char *title      = "Heredian";

int main(int argc, char **argv)
{
    // inicia o allegro
    gdp_init();
    // inicia os eventos
    gdp_initevents();
    // inicia o timer
    gdp_timer();
    // exibe splash
    gdp_splash();
    // intro
    gdp_intro();

    // se fechou a tela não faz nada
    if(nclose_game==0){
        // exibe o menu
        gdp_menu();
        // opção 1 inicia o jogo
        if(opmenu==1){
            if(nclose_game==0){
                //tela de loaging
                gdp_loaging();
	            gdp_game();
            }
        }
    }
    gdp_close();

    return EXIT_SUCCESS;
}
