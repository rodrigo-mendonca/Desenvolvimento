#include <stdio.h>
#include <stdlib.h>
#include "Lib/GameLib.h"

void teste();
void configinfo(Info *);
void DrawInfo(Info *);
void gdp_hit(int ,int ,int ,Char *);

int wigth        = 800;
int height       = 600;
char *title      = "TheGame!!";
int nChars = 2;

int main(int argc, char **argv)
{
    // inicia o allegro
    gdp_init();
    // inicia os eventos
    gdp_initevents();

    // inicia o timer
    gdp_timer();

    // exibe splash
   //gdp_splash();

    // se fechou a tela não faz nada
    if(nclose_game==0){
        // exibe o menu
        //gdp_menu();
        opmenu = 1;
        // opção 1 inicia o jogo
        if(opmenu==1){
            //tela de loaging
            //gdp_intro();
            if(nclose_game==0){
                gdp_loaging();
                teste();
            }
            //gdp_game();
        }
    }
    gdp_close();

    return EXIT_SUCCESS;
}

void teste(){
    int i;
    char **FileChar = calloc(nChars,sizeof(char*));

    FileChar[0] = "Characters//James//Configs//james.txt";
    FileChar[1] = "Characters//Julios//Configs//julios.txt";

    ambient = malloc(sizeof(Scene));
    gdp_ambient(ambient);

    for(i=0;i<nChars;i++){
        listchars[i] = malloc(sizeof(Char));
        gdp_loadchar(listchars[i],FileChar[i]);
        ambient->info->infochar[i] = listchars[i]->info;
        listchars[i]->obj.id = i;
    }
    free(FileChar);

    // inicia a musica do cenario
    //al_play_sample(ambient->musicback, 1.0, 0.0, 0.4, ALLEGRO_PLAYMODE_LOOP, NULL);

    while(true){
        al_wait_for_event(event_queue, &evento);

        if(gdp_readclose())
            break;

        if(listchars[0]->obj.lock == 0){
            gdp_readaction(&listchars[0]->obj);
            gdp_readdirection(&listchars[0]->obj);
        }

        gdp_damagechar(listchars[0]);
        gdp_SOCKreadaction(&listchars[1]->obj);
        gdp_SOCKreaddirection(&listchars[1]->obj);
        gdp_SOCKdamagechar(listchars[1]);

        for(i=0;i<nChars;i++){
            gdp_statuschar(listchars[i]);

        }

        if(gdp_readtime()){
            gdp_clear();
            gdp_drawinfo(ambient->info);
            // imagem de fundo
            al_draw_scaled_bitmap(ambient->image,
                0, 0,
                ambient->w,
                ambient->h,
                0,
                ambient->info->h,
                ambient->wd,
                ambient->hd,
                0
            );

            if(listchars[0]->obj.y > listchars[1]->obj.y){
                gdp_drawchar(listchars[1]);
                gdp_drawchar(listchars[0]);
            }
            else{
                gdp_drawchar(listchars[0]);
                gdp_drawchar(listchars[1]);
            }

            al_flip_display();
        }
    }
}

void gdp_damage(Char *tchar){
    int i;

    for(i=0;i<nChars;i++){
        if(tchar->obj.id != listchars[i]->obj.id){
            if(tchar->obj.a == 3){
                switch(tchar->obj.d){
                    case GDPUP:
                        gdp_hit(tchar->obj.x,
                                tchar->obj.y,
                                tchar->act[tchar->obj.a].damage,
                                listchars[i]);

                        gdp_hit(tchar->obj.x+tchar->obj.wd,
                                tchar->obj.y,
                                tchar->act[tchar->obj.a].damage,
                                listchars[i]);
                        break;
                    case GDPDOWN:
                        gdp_hit(tchar->obj.x,
                                tchar->obj.y+tchar->obj.hd,
                                tchar->act[tchar->obj.a].damage,
                                listchars[i]);

                        gdp_hit(tchar->obj.x+tchar->obj.wd,
                                tchar->obj.y+tchar->obj.hd,
                                tchar->act[tchar->obj.a].damage,
                                listchars[i]);
                        break;

                    case GDPLEFT:
                        gdp_hit(tchar->obj.x,
                                tchar->obj.y,
                                tchar->act[tchar->obj.a].damage,
                                listchars[i]);

                        gdp_hit(tchar->obj.x,
                                tchar->obj.y+tchar->obj.hd,
                                tchar->act[tchar->obj.a].damage,
                                listchars[i]);
                        break;

                    case GDPRIGHT:
                        gdp_hit(tchar->obj.x+tchar->obj.wd,
                                tchar->obj.y,
                                tchar->act[tchar->obj.a].damage,
                                listchars[i]);
                        gdp_hit(tchar->obj.x+tchar->obj.wd,
                                tchar->obj.y+tchar->obj.hd,
                                tchar->act[tchar->obj.a].damage,
                                listchars[i]);

                        break;
                }
            }
        }
    }
}

void gdp_hit(int tx,int ty,int tdamage,Char *tchar){
     if(tx >= tchar->obj.x &&
        tx <= (tchar->obj.x+tchar->obj.wd) &&
        ty >= tchar->obj.y &&
        ty <= (tchar->obj.y+tchar->obj.hd)
       ){
            tchar->info->healt -= tdamage;

            ALLEGRO_BITMAP *blood = al_load_bitmap("Temp//blood.png");
            // imagem de fundo
            al_draw_scaled_bitmap(blood,
                0, 0,
                55,
                42,
                tchar->obj.x,
                tchar->obj.y,
                55,
                42,
                0
            );
            al_destroy_bitmap(blood);
       }
}
