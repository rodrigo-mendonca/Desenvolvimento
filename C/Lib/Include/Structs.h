#ifndef STRUCTS_H
#define STRUCTS_H

#include "GDPilha.h"
#include <allegro5/allegro.h>
#include <allegro5/allegro_image.h>
#include <allegro5/allegro_font.h>
#include <allegro5/allegro_ttf.h>
#include <allegro5/allegro_audio.h>
#include <allegro5/allegro_acodec.h>
#define GDPLEFT 1
#define GDPRIGHT 2
#define GDPUP 4
#define GDPDOWN 8
#define DIRECTIONS 4
#define CHARS 30
#define LIFELESS 20
#define MAXCHARLIFELESS 5

typedef struct _Object{
    int id;
    int idchar;
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
    int type;
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
    int lifelessid;
    int rebatex,rebatey;
}Action;

typedef struct _Lifeless{
    Object obj;
    Action *act;
    int idmap;
    int dead;
}Lifeless;

typedef struct _InfoChar{
    char *name;
    int healtfull;
    int staminafull;
    int healt;
    int stamina;
}InfoChar;

typedef struct _Gate {
    int x1,y1, x2, y2;
    int ambientId;
    int ex,ey;
} Gate;

typedef struct Char{
    Object obj;
    Action *act;
    InfoChar *info;
    int idmap;
    int dead;
    int totlifeless;
    Lifeless **listlifeless;
}Char;

typedef struct _Info{
    ALLEGRO_BITMAP *image;
    int w;
    int h;
    InfoChar **infochar;
    ALLEGRO_FONT *fonte;
    ALLEGRO_COLOR color;
}Info;

typedef struct _Scene{
    int id;
    Info *info;
    ALLEGRO_BITMAP *image;
    ALLEGRO_BITMAP *model;
    ALLEGRO_SAMPLE *musicback;
    int w;
    int h;
    int wd;
    int hd;
    int ex;
    int ey;
    int qt_gates;
    Gate *gates;
}Scene;

typedef struct _PacketLifelessInfo {
    short x,y,w,h;
    short d;
    short damage;
} PacketLifelessInfo;

typedef struct _PacketCharInfo {
    short x,y,w,h;
    short a,d,dhit;
    short numchar;
    short idchar;
    short totchar;
    short totenemies;
    short exit;
    short healt,stamina;
    short damage;
    short idmap;
    short totlifeless;
	short step;
    short vision;
    PacketLifelessInfo listlifeless[MAXCHARLIFELESS];
} PacketCharInfo;

typedef enum LayeredObjectType {
    OBJTYPE_NONE = 0,
    OBJTYPE_ENEMY_OR_CHAR = 1,
    OBJTYPE_LIFELESS = 2
} LayeredObjectType;

typedef struct LayeredObject {
    LayeredObjectType type;
    int arr_idx;
    int y;
} LayeredObject;

#ifdef al_draw_textf
#	define al_draw_textf(fonte, cor, x, y, flags, formato, texto)
#endif

#ifdef malloc
#	define malloc(size) xmalloc(size)
#	define calloc(num, size) xcalloc(num, size)
#endif

#endif
