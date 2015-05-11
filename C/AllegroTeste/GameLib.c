#include <allegro5/allegro.h>
#include <allegro5/allegro_image.h>
#include "GameLib.h"
#include <stdlib.h>

void GameLib_Init(){
    // Inicializa a Allegro
    al_init();

    // Inicializa o add-on para utilização de imagens
    al_init_image_addon();
    // Inicializa o add-on para utilização de teclado
    al_install_keyboard();
    // Configura a janela
    oScreen = al_create_display(nWigth, nHeigth);

    al_set_window_title(oScreen, cTitle);

}

void GameLib_Close(){
    // Inicializa a Allegro
    al_init();

    // Inicializa o add-on para utilização de imagens
    al_init_image_addon();
    // Inicializa o add-on para utilização de teclado
    al_install_keyboard();
    // Configura a janela
    oScreen = al_create_display(nWigth, nHeigth);

    al_set_window_title(oScreen, cTitle);
}

void ClearScreen(){
    // Preenchemos a janela de branco
    al_clear_to_color(SetColor(255, 255, 255,1));
}

void Wait(double tnFPS){
    double nFPS;
    nFPS = (double) 1/tnFPS;
    al_rest(nFPS);
}

void DrawSprite(int tnFrame,struct Sprite *toSprite){

    al_wait_for_event(oEventQueue, &oEvento);


    ALLEGRO_BITMAP *oFrame = NULL;

    // EXTRAI O SPRITE
    oFrame = al_create_sub_bitmap(toSprite->oImg,
                                  toSprite->nXFrame*toSprite->nIXFrame,
                                  toSprite->nYFrame*toSprite->nIYFrame,
                                  toSprite->nXFrame,
                                  toSprite->nYFrame);

    // DESENHA O SPRITE
    al_draw_scaled_bitmap(oFrame,
        0, 0,
        toSprite->nXFrame,
        toSprite->nYFrame,
        toSprite->nX,
        toSprite->nY,
        toSprite->nW,
        toSprite->nH,
        0
    );

    // MATA A IMAGEM
    al_destroy_bitmap(oFrame);
}

ALLEGRO_COLOR SetColor(int tnRed,int tnGreen,int tnBlue,int tnAlfa){
    ALLEGRO_COLOR oRetorno = al_map_rgba(tnRed,tnGreen,tnBlue,tnAlfa);
    return oRetorno;
}

void MoveSprite(struct MovSprite *toMovSprite){
    int nColisao;

    switch(toMovSprite->nTipo){
        case 1: // para cima
            nColisao = toMovSprite->oSprite->nY - toMovSprite->nStep;

            if(nColisao >= 0)
                toMovSprite->oSprite->nY = toMovSprite->oSprite->nY - toMovSprite->nStep;
            break;

        case 2: //  para baixa
            nColisao = toMovSprite->oSprite->nY + toMovSprite->oSprite->nH + toMovSprite->nStep;

            if(nColisao <= toMovSprite->nH)
                toMovSprite->oSprite->nY = toMovSprite->oSprite->nY + toMovSprite->nStep;
            break;

        case 3: // para a esquerda
            nColisao = toMovSprite->oSprite->nX - toMovSprite->nStep;

            if(nColisao >= 0)
                toMovSprite->oSprite->nX = toMovSprite->oSprite->nX - toMovSprite->nStep;

            break;

        case 4: // para a direita
            nColisao = toMovSprite->oSprite->nX + toMovSprite->oSprite->nW + toMovSprite->nStep ;

            if(nColisao <= toMovSprite->nW)
                toMovSprite->oSprite->nX = toMovSprite->oSprite->nX + toMovSprite->nStep;
            break;

    }
}
