#ifndef GAMELIB_H
#define GAMELIB_H

#include <stdio.h>
#include <stdlib.h>
#include <allegro5/allegro.h>
#include <allegro5/allegro_image.h>
#include <allegro5/allegro_font.h>
#include <allegro5/allegro_ttf.h>
#include <allegro5/allegro_audio.h>
#include <allegro5/allegro_acodec.h>
#include <allegro5/allegro_primitives.h>

#include "Structs.h"
#include "GDPilha.h"
#include "heredianet.h"
#include "Events.h"
#include "GameServer.h"
#include "GameLoader.h"
#include "GameMenu.h"
#include "GameDraw.h"

#define FPS 60
//#define al_play_sample(a,b,c,d,e,f)

// Variável representando a janela principal
ALLEGRO_DISPLAY *SCREEN;

char *title;
int wigth;
int height;
Scene *ambient;
int ntotchars;
int ntotenemies;
int nlocchar;
int ntotlifeless;
int connecterro;
float scale;
int boss_char_id;

Char **listchars;
Lifeless **listlifeless;
gdp_client_t* client;

// opções
int opmenu;
int opchar;
int opmap;

int ncanaisaudio;
int nclose_game;

ALLEGRO_COLOR setcolor(int,int,int,int);
void gdp_init();
void gdp_close();
void gdp_clear();
void gdp_menu();
void gdp_intro();
void gdp_loaging();
void gdp_game();
void gdp_fadein(ALLEGRO_BITMAP *,int);
void gdp_fadeout(ALLEGRO_BITMAP *,int);
int gdp_colision(Object *,Action*);
void gdp_drawchar(Char*);
void gdp_splash();
void *gdp_splashclose(ALLEGRO_THREAD *, void *);
void gdp_movsprite(Object *,Action *);
void gdp_statuschar(Char *);
void gdp_drawinfo(Info *tinfo);
void gdp_configinfo(Info *tinfo);
void gdp_update_camera(Char *, float , float , float );
void gdp_valid_ambient_change(Object*, Action*);
int idlifeless();

#endif
