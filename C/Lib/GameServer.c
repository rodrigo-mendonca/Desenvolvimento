#include "Include/GameServer.h"

void gdp_send2server_init(void) {
    char* server_ip;
    listchars = calloc(CHARS,sizeof(Char*));
    memset(listchars, '\0', CHARS*sizeof(Char*));

    mudou = false;
    server_ip = gdp_files_quick_getstring("Configs//server.txt","server");
    client = gdp_client_connect(server_ip, 34000);
    free(server_ip);

    if(client == NULL){
         connecterro = 1; // erro de conexão
         return;
    }
    musend2server = al_create_mutex();
    condsend2server = al_create_cond();

    gdp_packet_t* packet = gdp_client_recv(client);
    PacketCharInfo *char_info = (PacketCharInfo*)gdp_packet_buffer(packet);

    nlocchar  = char_info->idchar;
    ntotchars = char_info->totchar;

    listchars[nlocchar] = malloc(sizeof(Char));
    gdp_load_char(opchar,listchars[nlocchar]);

    listchars[nlocchar]->obj.id     = nlocchar;
    listchars[nlocchar]->obj.idchar = nlocchar;
    listchars[nlocchar]->idmap      = opmap;

    // carrega o ambient
    gdp_load_ambient(opmap);
    listchars[nlocchar]->obj.x = ambient->ex;
    listchars[nlocchar]->obj.y = ambient->ey;

    al_lock_mutex(musend2server);
    mudou = true;
    al_signal_cond(condsend2server);
    al_unlock_mutex(musend2server);
}

void *gdp_recv2server(ALLEGRO_THREAD *th, void *arg) {
    while(true){
        if(client!=NULL){
            gdp_packet_t* packet = gdp_client_recv(client);
            PacketCharInfo *char_info = (PacketCharInfo*)gdp_packet_buffer(packet);

            ntotchars = char_info->totchar;
            ntotenemies = char_info->totenemies;

            if(listchars[char_info->idchar] == NULL){
                Char *aux = malloc(sizeof(Char));

                gdp_load_char(char_info->numchar,aux);

                listchars[char_info->idchar] = aux;
                ambient->info->infochar[char_info->idchar] = listchars[char_info->idchar]->info;
            }
            else{
                if(listchars[char_info->idchar]->dead==1 && char_info->idchar < 4){
                    free(listchars[char_info->idchar]);
                    Char *aux = malloc(sizeof(Char));

                    gdp_load_char(char_info->numchar,aux);

                    listchars[char_info->idchar] = aux;
                    ambient->info->infochar[char_info->idchar] = listchars[char_info->idchar]->info;
                }
            }

            if(listchars[char_info->idchar]->info->healt != char_info->healt){
                listchars[char_info->idchar]->info->healt   = char_info->healt;

                switch(char_info->dhit){
                    case GDPUP:
                        listchars[char_info->idchar]->obj.y -=1;
                        break;
                    case GDPDOWN:
                        listchars[char_info->idchar]->obj.y +=1;
                        break;
                    case GDPLEFT:
                        listchars[char_info->idchar]->obj.x -=1;
                        break;
                    case GDPRIGHT:
                        listchars[char_info->idchar]->obj.x +=1;
                        break;
                }
            }

            if(char_info->exit == 0){
                if(char_info->idchar != nlocchar && listchars[char_info->idchar]->obj.a !=4){
                    listchars[char_info->idchar]->dead          = 0;
                    listchars[char_info->idchar]->obj.id        = char_info->idchar;
                    listchars[char_info->idchar]->obj.idchar    = char_info->idchar;
                    listchars[char_info->idchar]->obj.a         = char_info->a;
                    listchars[char_info->idchar]->obj.d         = char_info->d;
                    listchars[char_info->idchar]->obj.x         = char_info->x;
                    listchars[char_info->idchar]->obj.y         = char_info->y;
                    listchars[char_info->idchar]->info->stamina = char_info->stamina;
                    listchars[char_info->idchar]->idmap         = char_info->idmap;
                }
            }
            else{
                listchars[char_info->idchar]->dead = 1;
            }
            gdp_packet_free(packet);
        }
    }
}

void *gdp_send2server(ALLEGRO_THREAD *th, void *arg) {
    PacketCharInfo *char_loc = (PacketCharInfo*)malloc(sizeof(PacketCharInfo));
    memset(char_loc, 0, sizeof(PacketCharInfo));
    int i;

    al_lock_mutex(musend2server);
    while (true) {
        if(client!=NULL){
            while (!mudou) {
                al_wait_cond(condsend2server, musend2server);
            }
            mudou = false;

            char_loc->numchar   = opchar;
            char_loc->idchar    = nlocchar;
            char_loc->a         = listchars[nlocchar]->obj.a;
            char_loc->d         = listchars[nlocchar]->obj.d;
            char_loc->x         = listchars[nlocchar]->obj.x  + listchars[nlocchar]->act[char_loc->a].rebatex;
            char_loc->w         = listchars[nlocchar]->obj.wd - listchars[nlocchar]->act[char_loc->a].rebatex;
            char_loc->y         = listchars[nlocchar]->obj.y  + listchars[nlocchar]->act[char_loc->a].rebatey;
            char_loc->h         = listchars[nlocchar]->obj.hd - listchars[nlocchar]->act[char_loc->a].rebatey;
            char_loc->healt     = listchars[nlocchar]->info->healt;
            char_loc->stamina   = listchars[nlocchar]->info->stamina;
            char_loc->damage    = listchars[nlocchar]->act[char_loc->a].damage;
            char_loc->exit      = 0;
            char_loc->idmap     = opmap;
            char_loc->totlifeless = listchars[nlocchar]->totlifeless;

            for(i=0;i<MAXCHARLIFELESS;i++){
                if(listchars[nlocchar]->listlifeless[i] != NULL){
                    if(listchars[nlocchar]->listlifeless[i]->dead==0){
                        char_loc->listlifeless[i].x = listchars[nlocchar]->listlifeless[i]->obj.x;
                        char_loc->listlifeless[i].y = listchars[nlocchar]->listlifeless[i]->obj.y;
                        char_loc->listlifeless[i].w = listchars[nlocchar]->listlifeless[i]->obj.wd;
                        char_loc->listlifeless[i].h = listchars[nlocchar]->listlifeless[i]->obj.hd;
                        char_loc->listlifeless[i].d = listchars[nlocchar]->listlifeless[i]->obj.d;
                        char_loc->listlifeless[i].damage = listchars[nlocchar]->listlifeless[i]->act[listchars[nlocchar]->listlifeless[i]->obj.a].damage;
                    }
                }
            }

            gdp_client_send(
                client,
                (char*)char_loc,
                sizeof(PacketCharInfo));

            // engenharia de emergencia
            int boss=boss_char_id;


            if(listchars[boss]!=NULL){
            char_loc->idchar    = boss;
            char_loc->a         = listchars[boss]->obj.a;
            char_loc->d         = listchars[boss]->obj.d;
            char_loc->x         = listchars[boss]->obj.x + listchars[boss]->act[char_loc->a].rebatex;
            char_loc->w         = listchars[boss]->obj.wd - listchars[boss]->act[char_loc->a].rebatex;
            char_loc->y         = listchars[boss]->obj.y  + listchars[boss]->act[char_loc->a].rebatey;
            char_loc->h         = listchars[boss]->obj.hd - listchars[boss]->act[char_loc->a].rebatey;
            char_loc->healt     = listchars[boss]->info->healt;
            char_loc->stamina   = listchars[boss]->info->stamina;
            char_loc->damage    = listchars[boss]->act[char_loc->a].damage;
            char_loc->exit      = 0;

            char_loc->idmap     =  listchars[boss]->idmap;
            char_loc->totlifeless = listchars[boss]->totlifeless;

            for(i=0;i<MAXCHARLIFELESS;i++){
                if(listchars[boss]->listlifeless[i] != NULL){

                    if(listchars[boss]->listlifeless[i]->dead==0){
                        char_loc->listlifeless[i].x = listchars[boss]->listlifeless[i]->obj.x;
                        char_loc->listlifeless[i].y = listchars[boss]->listlifeless[i]->obj.y;
                        char_loc->listlifeless[i].w = listchars[boss]->listlifeless[i]->obj.wd;
                        char_loc->listlifeless[i].h = listchars[boss]->listlifeless[i]->obj.hd;
                        char_loc->listlifeless[i].d = listchars[boss]->listlifeless[i]->obj.d;
                        char_loc->listlifeless[i].damage = listchars[boss]->listlifeless[i]->act[listchars[boss]->listlifeless[i]->obj.a].damage;
                    }
                }
            }
            }

            gdp_client_send(
                client,
                (char*)char_loc,
                sizeof(PacketCharInfo));

        }
    }

    al_unlock_mutex(musend2server);
}

void gdp_hit(int tx,int ty,int tdamage,PacketCharInfo *tchar,int td){
    int x,y,w,h;
    x = tchar->x;
    y = tchar->y;
    w = tchar->w+tchar->x;
    h = tchar->h+tchar->y;

    if(tx >= x &&
        tx <= w &&
        ty >= y &&
        ty <= h
       ){
        tchar->healt -= tdamage;
        tchar->dhit = td;
        switch(td){
            case GDPUP:
                tchar->y -=3;
                break;
            case GDPDOWN:
                tchar->y +=3;
                break;
            case GDPLEFT:
                tchar->x -=3;
                break;
            case GDPRIGHT:
                tchar->x +=3;
                break;
        }
    }
}
