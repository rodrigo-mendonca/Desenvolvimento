#ifndef STRUCTS_H
#define STRUCTS_H

#include "GDPilha.h"

#define GDPLEFT 1
#define GDPRIGHT 2
#define GDPUP 4
#define GDPDOWN 8
#define DIRECTIONS 4
#define CHARS 4

typedef struct _Object{
    int id;
    int x;
    int y;
    double w;
    double h;
    int wd;
    int hd;
    int d;
    unsigned int d2;
    int a;
    int lock;
    unsigned int a2;
}Object;

typedef struct _Action{
    int id;
    double fps;
    GDPilha *direction[DIRECTIONS];
    ALLEGRO_BITMAP *image;
    ALLEGRO_TIMER *time_sprite;
    ALLEGRO_EVENT_QUEUE *fila_timer;
    ALLEGRO_SAMPLE *sound;
    int stepx;
    int stepy;
    int charge;
    int damage;
    int lock;
}Action;

typedef struct _InfoChar{
    char *nome;
    int healtfull;
    int staminafull;
    int healt;
    int stamina;
}InfoChar;

typedef struct Char{
    Object obj;
    Action *act;
    InfoChar *info;
}Char;

typedef struct _Info{
    ALLEGRO_BITMAP *image;
    int w;
    int h;
    InfoChar *infochar[CHARS];
    ALLEGRO_FONT *fonte;
    ALLEGRO_COLOR color;
}Info;

typedef struct _Scene{
    Info *info;
    ALLEGRO_BITMAP *image;
    ALLEGRO_BITMAP *model;
    ALLEGRO_SAMPLE *musicback;
    int w;
    int h;
    int wd;
    int hd;
}Scene;

typedef struct _PacketCharInfo {
    int x, y;
} PacketCharInfo;


#endif
