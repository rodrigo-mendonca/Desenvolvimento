 #define FPS 60

#include <allegro5/allegro.h>
#include <allegro5/allegro_image.h>
#include <allegro5/allegro_font.h>
#include <allegro5/allegro_ttf.h>

#include <stdio.h>
#include <stdlib.h>

#include "GameLib.h"
#include "Principal.h"
#include "Sounds.h"
#include "Testes.h"

ALLEGRO_EVENT_QUEUE *oEventQueue = NULL,oSpriteTime = NULL;
ALLEGRO_EVENT oEvento;
ALLEGRO_DISPLAY *oScreen;

ALLEGRO_FONT *ttf_font;

void Pause();
void Game(int);

struct Sprite *oNinja,*oRyu,*oLink;
struct MovSprite *oMovSprite;

int nWigth   = 640;
int nHeigth  = 480;
char *cTitle = "Jogos!!";
bool lPause = false;
int nFrame = 0;
int nTecla=0,nTeclaUp = 0,nTeclaDown = 0;

int main()
{
    // inicia a lib e allegro
    GameLib_Init(oScreen);
    // cria uma lista de eventos
    oEventQueue = al_create_event_queue();
    // inicia todos os eventos
    InitEvents(oEventQueue);

    // inicia o evento do timer
    double nTime = (double)1.0 / FPS;
    Timer(nTime,oEventQueue);

    al_init_ttf_addon();
    ttf_font = al_load_ttf_font("comic.TTF", 48, 0);

    SpriteNinja();
    SpriteRyu();
    SpriteLink();
    oMovSprite = malloc(sizeof(struct MovSprite));

    oMovSprite->oSprite = oLink;
    oMovSprite->nH      = nHeigth;
    oMovSprite->nW      = nWigth;
    oMovSprite->nStep   = 1;

    while(true){
        al_wait_for_event(oEventQueue, &oEvento);

        // fechar a tela
        if(oEvento.type == ALLEGRO_EVENT_DISPLAY_CLOSE)
            break;

        if (oEvento.type == ALLEGRO_EVENT_KEY_DOWN)
        {
            nTeclaUp = 0;
            nTeclaDown = ReadKey(oEvento.keyboard.keycode);
            oMovSprite->nTipo = nTeclaDown;
        }

        if (oEvento.type == ALLEGRO_EVENT_KEY_UP)
        {
            nTeclaUp = ReadKey(oEvento.keyboard.keycode);
            if(nTeclaDown==nTeclaUp){
                nTeclaDown = 0;
                oMovSprite->nTipo = 0;
            }
        }
        // pause
        if(nTeclaUp==80){
            lPause = !lPause;
        }

        // verifica se precisa desenhar na tela
        if(oEvento.type == ALLEGRO_EVENT_TIMER)
        {
            // LIMPA A TELA
            ClearScreen();

            // RODA O JOGO
            Game(nFrame);

            if(lPause){
                Pause();
            }
            else{
                // faz a contagem de FPS
                nFrame++;
                if(nFrame==FPS)
                    nFrame = 0;
            }
            // Atualiza a tela
            al_flip_display();
        }
        nTeclaUp = 0;
    }
    // mata objetos
    al_destroy_display(oScreen);
    al_destroy_event_queue(oEventQueue);

    return 0;
}

void Pause(){
    al_draw_text(ttf_font, al_map_rgb(0, 0,0), nWigth / 2, (nHeigth-48) / 2, ALLEGRO_ALIGN_CENTRE, "Pause");
}

void Game(int tnFrame){
    if(!lPause){
        // MOVIMENTA O SRITE
        MoveSprite(oMovSprite);
    }
    // DESENHA O SRITE
    if(nTeclaDown==32)
        DrawSprite(tnFrame,oNinja);
    if(nTeclaUp==0 && nTeclaDown!=32)
        DrawSprite(tnFrame,oLink);
}
