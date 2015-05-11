#include <allegro5/allegro.h>
#include "Events.h"

void InitEvents(ALLEGRO_EVENT_QUEUE *toEventQueue)
{
    al_register_event_source(toEventQueue, al_get_keyboard_event_source());
    al_register_event_source(toEventQueue, al_get_display_event_source(oScreen));
}


void Timer(double tnTime,ALLEGRO_EVENT_QUEUE *toEventQueue){
    oTimer = al_create_timer(tnTime);
    al_register_event_source(toEventQueue, al_get_timer_event_source(oTimer));
    al_start_timer(oTimer);
}

int KeyDown(ALLEGRO_EVENT *toEvent)
{
    int nTecla = 0;

    if (toEvent->type == ALLEGRO_EVENT_KEY_DOWN)
    {
        nTecla = ReadKey(toEvent->keyboard.keycode);
    }
    return nTecla;
}

int KeyUp(ALLEGRO_EVENT *toEvent)
{
    int nTecla = 0;

    if (toEvent->type == ALLEGRO_EVENT_KEY_UP)
    {
        nTecla = ReadKey(toEvent->keyboard.keycode);
    }
    return nTecla;
}

int ReadKey(int tnCod)
{
    int nTecla = 0;
    switch(tnCod)
    {
        case ALLEGRO_KEY_UP:
            nTecla = 1;
            break;
        case ALLEGRO_KEY_DOWN:
            nTecla = 2;
            break;
        case ALLEGRO_KEY_LEFT:
            nTecla = 3;
            break;
        case ALLEGRO_KEY_RIGHT:
            nTecla = 4;
            break;
        case ALLEGRO_KEY_SPACE:
            nTecla = 32;
            break;
        case ALLEGRO_KEY_P:
            nTecla = 80;
            break;
    }
    return nTecla;
}






