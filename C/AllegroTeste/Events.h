#include <allegro5/allegro.h>

ALLEGRO_TIMER *oTimer = NULL;
ALLEGRO_DISPLAY *oScreen;
ALLEGRO_EVENT oKeyEvento;

void InitEvents(ALLEGRO_EVENT_QUEUE *);
void Timer(double,ALLEGRO_EVENT_QUEUE *);
int KeyUp(ALLEGRO_EVENT*);
int KeyDown(ALLEGRO_EVENT*);
int ReadKey(int tnCod);
