#ifndef GAMEMENU_H
#define GAMEMENU_H

#include "GameLib.h"

void gdp_menu();
void gdp_intro();
void gdp_loaging();
void gdp_splash();
void gdp_selchar();
void *gdp_splashclose(ALLEGRO_THREAD *, void *);
void gdp_erro(char *);

#endif // GAMEMENU_H