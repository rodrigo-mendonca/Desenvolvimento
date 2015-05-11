#include "GameLib.h"

#ifndef EVENTS_H
#define EVENTS_H

#include <allegro5/allegro.h>
#include "Structs.h"

ALLEGRO_TIMER *timer;
ALLEGRO_EVENT evento;
ALLEGRO_EVENT_QUEUE *event_queue;

ALLEGRO_THREAD *thsend2server;
ALLEGRO_THREAD *threcv2server;
ALLEGRO_MUTEX *musend2server;
ALLEGRO_COND *condsend2server;
bool mudou;


void gdp_initevents();
void gdp_timer();
bool gdp_readtime();
bool gdp_readclose();
void gdp_pausetime();
void gdp_runtime();

void gdp_readaction(Object *,float *);
void gdp_readdirection(Object*);

#endif
