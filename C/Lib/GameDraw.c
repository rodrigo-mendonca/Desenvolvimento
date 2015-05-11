#include "Include/GameDraw.h"

void gdp_drawchar(Char *tchar){
    ALLEGRO_EVENT charevento;

    int acao = tchar->obj.a;
    int dir = tchar->obj.d;

    if (dir == 1) dir = 1;
    if (dir == 2) dir = 2;
    if (dir == 4) dir = 3;
    if (dir == 8) dir = 0;

    GDPilha *oPilha = NULL;
    Sprite* oSprite;
    Action *oactions;
    ALLEGRO_BITMAP *oFrame = NULL;

    oactions = tchar->act;

    // se existir, reproduz o som
    if(oactions[acao].sound!=NULL)
        al_play_sample(oactions[acao].sound, 1.0, 0.0, 1.0, ALLEGRO_PLAYMODE_ONCE, NULL);

    oPilha = oactions[acao].direction[dir];
    oSprite = gdp_top(&oPilha);

    if(oactions[acao].lock==1)
        tchar->obj.lock = 1;

    tchar->obj.wd = (int) tchar->obj.w*oSprite->w;
    tchar->obj.hd = (int) tchar->obj.h*oSprite->h;

    gdp_movsprite(&tchar->obj,&oactions[acao]);

    oFrame = al_create_sub_bitmap(
              oactions[acao].image,
              oSprite->w*oSprite->ix,
              oSprite->h*oSprite->iy,
              oSprite->w,
              oSprite->h);

    // desenha o sprite
    al_draw_scaled_bitmap(
        oFrame,
        0,
        0,
        oSprite->w,
        oSprite->h,
        tchar->obj.x,
        tchar->obj.y,
        tchar->obj.wd,
        tchar->obj.hd,
        0
    );

    // verifica se deu tempo para troca de sprite
    if(!al_is_event_queue_empty(oactions[acao].fila_timer)){
        al_wait_for_event(oactions[acao].fila_timer, &charevento);

        // muda o sprite
        if(charevento.type == ALLEGRO_EVENT_TIMER){
            // verica se pode libera a movimentacao
            if(tchar->obj.lock==1 && oSprite->last == 1){
                tchar->obj.lock = 0;
                tchar->obj.a = 0;
                tchar->obj.a2 &= ~2;
            }
            // se existir alguma magia , cria o objeto
            if(oactions[acao].lifelessid > 0 && oSprite->first == 1 && tchar->totlifeless<MAXCHARLIFELESS){
                int id = idlifeless();

                listlifeless[id] = malloc(sizeof(Lifeless));

                gdp_load_Lifeless(oactions[acao].lifelessid, listlifeless[id]);
                ntotlifeless++;

                listlifeless[id]->dead       = 0;
                listlifeless[id]->obj.idchar = tchar->obj.id;
                listlifeless[id]->obj.id     = id;
                listlifeless[id]->idmap      = opmap;
                listlifeless[id]->obj.d2     = tchar->obj.d;
                listlifeless[id]->obj.d      = tchar->obj.d;

                listlifeless[id]->obj.x = tchar->obj.x;
                listlifeless[id]->obj.y = tchar->obj.y;

                tchar->listlifeless[tchar->totlifeless] = listlifeless[id];
                tchar->totlifeless++;
            }

            // se for o ultimo sprite e o objeto não estiver travado, gasta stamina
            if(oSprite->last == 1 || tchar->obj.lock==0){
                // gasta a stamina
                tchar->info->stamina += oactions[acao].charge;
                if(tchar->info->stamina>tchar->info->staminafull)
                    tchar->info->stamina = tchar->info->staminafull;
                if(tchar->info->stamina < 0)
                    tchar->info->stamina = 0;
            }

            // muda de sprite
            gdp_popend(&oPilha);
            oactions[acao].direction[dir] = oPilha;

            al_flush_event_queue(oactions[acao].fila_timer);
        }
    }
    al_destroy_bitmap(oFrame);
}

void gdp_drawinfo(Info *tinfo){
    int i,space = (int) wigth / 4;
    char *menu;
    char *cache = calloc(30,sizeof(char));
    ALLEGRO_BITMAP *image  = al_load_bitmap("Images//info.png");

    for(i=0;i<ntotchars;i++){
        if(tinfo->infochar[i] != NULL){
            // desenha o sprite
            al_draw_scaled_bitmap(
                image,
                0,
                0,
                255,
                147,
                space*i,
                0,
                space,
                100,
                0
            );

            menu = "Nome: %s";
            sprintf(cache,menu,
                    tinfo->infochar[i]->name);
            al_draw_textf(tinfo->fonte, tinfo->color, 20+space*i, 15, ALLEGRO_ALIGN_LEFT, "%s",cache);

            menu = "Saude: %d/%d";
            sprintf(cache,menu,
                    tinfo->infochar[i]->healt,
                    tinfo->infochar[i]->healtfull);
            al_draw_textf(tinfo->fonte, tinfo->color, 20+space*i, 35, ALLEGRO_ALIGN_LEFT, "%s",cache);

            menu = "Cansaço: %d/%d";
            sprintf(cache,menu,
                    tinfo->infochar[i]->stamina,
                    tinfo->infochar[i]->staminafull
                    );

            al_draw_textf(tinfo->fonte, tinfo->color, 20+space*i, 55, ALLEGRO_ALIGN_LEFT, "%s",cache);
        }
    }
    al_destroy_bitmap(image);
    free(cache);
}

void gdp_drawlifeless(Lifeless *tlifeless){
    ALLEGRO_EVENT evento;

    int acao = tlifeless->obj.a;
    int dir = tlifeless->obj.d;

    if (dir == 1) dir = 1;
    if (dir == 2) dir = 2;
    if (dir == 4) dir = 3;
    if (dir == 8) dir = 0;

    GDPilha *oPilha = NULL;
    Sprite* oSprite;
    Action *oactions;
    ALLEGRO_BITMAP *oFrame = NULL;

    oactions = tlifeless->act;
    tlifeless->dead = 0;
    // se existir, reproduz o som
    if(oactions[acao].sound!=NULL)
        al_play_sample(oactions[acao].sound, 1.0, 0.0, 1.0, ALLEGRO_PLAYMODE_ONCE, NULL);

    oPilha = oactions[acao].direction[dir];
    oSprite = gdp_top(&oPilha);

    int x,y;
    x = tlifeless->obj.x;
    y = tlifeless->obj.y;

    tlifeless->obj.wd = (int) tlifeless->obj.w*oSprite->w;
    tlifeless->obj.hd = (int) tlifeless->obj.h*oSprite->h;
    // verifica se deu dano em algum char
    gdp_movsprite(&tlifeless->obj,&oactions[acao]);

    // se objeto colidir desaparece
    if(tlifeless->obj.x == x && tlifeless->obj.y == y && oactions[acao].lock == 0){
        tlifeless->dead = 1;
        listchars[tlifeless->obj.idchar]->totlifeless--;
        return;
    }

    oFrame = al_create_sub_bitmap(
              oactions[acao].image,
              oSprite->w*oSprite->ix,
              oSprite->h*oSprite->iy,
              oSprite->w,
              oSprite->h);

    // desenha o sprite
    al_draw_scaled_bitmap(
        oFrame,
        0,
        0,
        oSprite->w,
        oSprite->h,
        tlifeless->obj.x,
        tlifeless->obj.y,
        tlifeless->obj.wd,
        tlifeless->obj.hd,
        0
    );

    // verifica se deu tempo para troca de sprite
    if(!al_is_event_queue_empty(oactions[acao].fila_timer)){
        al_wait_for_event(oactions[acao].fila_timer, &evento);

        // muda o sprite
        if(evento.type == ALLEGRO_EVENT_TIMER){
            // muda de sprite
            gdp_popend(&oPilha);
            oactions[acao].direction[dir] = oPilha;

            al_flush_event_queue(oactions[acao].fila_timer);
        }
    }
    al_destroy_bitmap(oFrame);
}
