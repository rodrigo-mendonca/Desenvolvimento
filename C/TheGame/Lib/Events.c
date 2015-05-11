#include "GameLib.h"

void gdp_initevents()
{
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
    if(evento.type == ALLEGRO_EVENT_KEY_DOWN){
        if(evento.keyboard.keycode == ALLEGRO_KEY_UP){
            tobj->d = GDPUP;
            tobj->d2 |= GDPUP;
        }
        if(evento.keyboard.keycode == ALLEGRO_KEY_DOWN){
            tobj->d = GDPDOWN;
            tobj->d2 |= GDPDOWN;
        }
        if(evento.keyboard.keycode == ALLEGRO_KEY_LEFT){
            tobj->d = GDPLEFT;
            tobj->d2 |= GDPLEFT;
        }
        if(evento.keyboard.keycode == ALLEGRO_KEY_RIGHT){
            tobj->d = GDPRIGHT;
            tobj->d2 |= GDPRIGHT;
        }
    }

    if(evento.type == ALLEGRO_EVENT_KEY_UP){
        if(evento.keyboard.keycode == ALLEGRO_KEY_UP){
            tobj->d2 &= ~GDPUP;
        }
        if(evento.keyboard.keycode == ALLEGRO_KEY_DOWN){
            tobj->d2 &= ~GDPDOWN;
        }
        if(evento.keyboard.keycode == ALLEGRO_KEY_LEFT){
            tobj->d2 &= ~GDPLEFT;
        }
        if(evento.keyboard.keycode == ALLEGRO_KEY_RIGHT){
            tobj->d2 &= ~GDPRIGHT;
        }

        if(tobj->d2 == GDPUP) tobj->d = GDPUP;
        if(tobj->d2 == GDPDOWN) tobj->d = GDPDOWN;
        if(tobj->d2 == GDPLEFT) tobj->d = GDPLEFT;
        if(tobj->d2 == GDPRIGHT) tobj->d = GDPRIGHT;
    }
}

void gdp_readaction(Object *tobj){
    if(evento.type == ALLEGRO_EVENT_KEY_DOWN){
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

void gdp_damagechar(Char *tchar){
    if(evento.type == ALLEGRO_EVENT_KEY_DOWN){
        if(evento.keyboard.keycode == ALLEGRO_KEY_A){
            if(tchar->info->healt>0)
                tchar->info->healt -= 10;
        }
        if(evento.keyboard.keycode == ALLEGRO_KEY_ESCAPE){
            tchar->info->healt = 100;
        }
    }
}

void gdp_SOCKreaddirection(Object *tobj){

}

void gdp_SOCKreadaction(Object *tobj){

}

void gdp_SOCKdamagechar(Char *tchar){

}
