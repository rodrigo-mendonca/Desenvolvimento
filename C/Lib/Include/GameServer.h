#ifndef GAMESERVER_H
#define GAMESERVER_H

#include "GameLib.h"

ALLEGRO_THREAD *thsend2server;
ALLEGRO_THREAD *threcv2server;
ALLEGRO_MUTEX *musend2server;
ALLEGRO_COND *condsend2server;

void gdp_send2server_init(void);
void *gdp_recv2server(ALLEGRO_THREAD *, void *);
void *gdp_send2server(ALLEGRO_THREAD *, void *);
void gdp_hit(int,int,int,PacketCharInfo *,int);
#endif
