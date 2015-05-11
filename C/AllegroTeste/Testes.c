#include <stdlib.h>

#include <allegro5/allegro.h>
#include <allegro5/allegro_image.h>

#include "GameLib.h"

struct Sprite *oNinja,*oRyu,*oLink;


void SpriteNinja(){
    oNinja = malloc(sizeof(struct Sprite));

    ALLEGRO_BITMAP *oImg = al_load_bitmap("Frames\\ninjaue.png");

    oNinja->oImg     = oImg;

    oNinja->oTimer   = al_create_timer(tnTime);
    al_register_event_source(toEventQueue, al_get_timer_event_source(oTimer));

    oNinja->nX       = 50;
    oNinja->nY       = 200;
    oNinja->nW       = 138;
    oNinja->nH       = 152;
    oNinja->nXFrame  = 38;
    oNinja->nYFrame  = 52;
    oNinja->nIXFrame = 0;
    oNinja->nIYFrame = 0;
    oNinja->nTFrame  = 13;
}

void SpriteRyu(){
    oRyu = malloc(sizeof(struct Sprite));

    ALLEGRO_BITMAP *oImg = al_load_bitmap("Frames\\Ryuzito2.jpg");

    oRyu->oImg     = oImg;
    oRyu->nFPS     = 5;
    oRyu->nX       = 400;
    oRyu->nY       = 50;
    oRyu->nW       = 138;
    oRyu->nH       = 152;
    oRyu->nXFrame  = 65;
    oRyu->nYFrame  = 84;
    oRyu->nIXFrame = 0;
    oRyu->nIYFrame = 0;
    oRyu->nTFrame  = 17;
}

void SpriteLink(){
    oLink = malloc(sizeof(struct Sprite));

    ALLEGRO_BITMAP *oImg = al_load_bitmap("Frames\\Zeldinha.png");

    oLink->oImg     = oImg;
    oLink->nFPS     = 7;
    oLink->nX       = 50;
    oLink->nY       = 200;
    oLink->nW       = 102;
    oLink->nH       = 76;
    oLink->nXFrame  = 102;
    oLink->nYFrame  = 76;
    oLink->nIXFrame = 0;
    oLink->nIYFrame = 0;
    oLink->nTFrame  = 8 ;
}

