#include "Include/Events.h"

void gdp_initevents(){
    event_queue = al_create_event_queue();
    al_register_event_source(event_queue, al_get_keyboard_event_source());
    al_register_event_source(event_queue, al_get_display_event_source(SCREEN));
}

void gdp_timer(){
    double nfsp = (double)1.0 / FPS;

    timer = al_create_timer(nfsp);
    al_register_event_source(event_queue, al_get_timer_event_source(timer));
    gdp_runtime();
}

bool gdp_readtime(){
    if(evento.type == ALLEGRO_EVENT_TIMER){
        return true;
    }

    return false;
}

bool gdp_readclose(){
    if(evento.type == ALLEGRO_EVENT_DISPLAY_CLOSE){
        nclose_game = 1;
        return true;
    }

    return false;
}

void gdp_pausetime(){
   al_stop_timer(timer);
}

void gdp_runtime(){
   al_start_timer(timer);
}

void gdp_readdirection(Object *tobj){
    ALLEGRO_KEYBOARD_STATE state;
    al_get_keyboard_state(&state);
    
    tobj->d2 = 0;
    if (al_key_down(&state, ALLEGRO_KEY_UP)) tobj->d2 |= GDPUP;
    if (al_key_down(&state, ALLEGRO_KEY_DOWN)) tobj->d2 |= GDPDOWN;
    if (al_key_down(&state, ALLEGRO_KEY_LEFT)) tobj->d2 |= GDPLEFT;
    if (al_key_down(&state, ALLEGRO_KEY_RIGHT)) tobj->d2 |= GDPRIGHT;

    if(evento.type == ALLEGRO_EVENT_KEY_DOWN){
        if(evento.keyboard.keycode == ALLEGRO_KEY_UP){
            tobj->d = GDPUP;
        }
        if(evento.keyboard.keycode == ALLEGRO_KEY_DOWN){
            tobj->d = GDPDOWN;
        }
        if(evento.keyboard.keycode == ALLEGRO_KEY_LEFT){
            tobj->d = GDPLEFT;
        }
        if(evento.keyboard.keycode == ALLEGRO_KEY_RIGHT){
            tobj->d = GDPRIGHT;
        }
    }

    if(evento.type == ALLEGRO_EVENT_KEY_UP){
        if(tobj->d2 == GDPUP) tobj->d = GDPUP;
        if(tobj->d2 == GDPDOWN) tobj->d = GDPDOWN;
        if(tobj->d2 == GDPLEFT) tobj->d = GDPLEFT;
        if(tobj->d2 == GDPRIGHT) tobj->d = GDPRIGHT;
    }
}

void gdp_readaction(Object *tobj, float *scale){
    if(evento.type == ALLEGRO_EVENT_KEY_DOWN){
        if(evento.keyboard.keycode == ALLEGRO_KEY_2){
            if (*scale<=2)
            *scale+=0.05;
        }

        if(evento.keyboard.keycode == ALLEGRO_KEY_1){
            if (*scale>=1)
              *scale-=0.05;
        }

        if(evento.keyboard.keycode == ALLEGRO_KEY_D){
            tobj->a2 |= 1;
        }
        if(evento.keyboard.keycode == ALLEGRO_KEY_F){
            tobj->a2 |= 2;
        }
    }

    if(evento.type == ALLEGRO_EVENT_KEY_UP){
        if(evento.keyboard.keycode == ALLEGRO_KEY_D){
            tobj->a2 &= ~1;
        }
        if(evento.keyboard.keycode == ALLEGRO_KEY_F){
            tobj->a2 &= ~2;
        }
    }

    if (tobj->a2 & 2)
        tobj->a = 3;
    else if (!tobj->d2)
        tobj->a = 0;
    else if (tobj->a2 & 1)
        tobj->a = 2;
    else
        tobj->a = 1;
}
