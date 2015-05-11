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
#include "GDPilha.h"
#include "Events.h"
#include "FileManager.h"
#include "../../libheredianet/include/heredianet.h"
#define FPS 60

// Variável representando a janela principal
ALLEGRO_DISPLAY *SCREEN;

char *title;
int wigth;
int height;
Scene *ambient;
int nChars;
Char **listchars;

int opmenu;
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
int gdp_colision(Object *);
void gdp_drawchar(Char*);
void gdp_splash();
void *gdp_splashclose(ALLEGRO_THREAD *, void *);
void *gdp_fillblocks(ALLEGRO_THREAD *thr, void *params);
void gdp_ambient(Scene *);
void gdp_movsprite(Object *,Action *);
void gdp_loadchar(Char *, char *);
void gdp_statuschar(Char *);
void gdp_drawinfo(Info *tinfo);
void gdp_configinfo(Info *tinfo);
void gdp_damage(Char *);

#endif
