#ifndef GDPILHA_H
#define GDPILHA_H

typedef struct _Sprite{
    int w;
    int h;
    int ix;
    int iy;
    int last;
    int first;
}Sprite;

typedef struct _GDPilha{
  Sprite *oObject;
  struct _GDPilha *oProximo;
} GDPilha;

void gdp_push(GDPilha **, Sprite*);
void gdp_pop(GDPilha **);
void gdp_popend(GDPilha **);
Sprite* gdp_top(GDPilha **);
int gdp_stacksize(GDPilha **);

#endif
