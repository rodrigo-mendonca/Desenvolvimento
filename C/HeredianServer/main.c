#include <stdio.h>
#include <stdlib.h>
#include "../Lib/Include/heredianet.h"
#include "../Lib/Include/GameLib.h"
#include <math.h>

int connectclient(gdp_client_t* );
int desconnectclient(gdp_client_t* );
void gdp_damagechar(PacketCharInfo*);
void gdp_damageinimigo(PacketCharInfo*);
void gdp_dirdamage(PacketCharInfo *,PacketCharInfo *);

typedef struct _ListClients {
    int id;
    int inuse;
    gdp_client_t* client;
    PacketCharInfo charInfo;
} ListClients;

int nTotChars = 0;
ListClients listclients[4];
gdp_server_t* server;
ALLEGRO_THREAD *tbroadcast;
PacketCharInfo *inimgosCharInfo;
int qt_inimigos;
ALLEGRO_BITMAP **models;
int qt_inimigos, width, height, boss_num;


void init() {
    memset(listclients, 0, sizeof listclients);
 // Inicializa a Allegro
	al_init();

	// Inicializa o add-on para utilização de imagens
	al_init_image_addon();
}

void mysleep(int miliseconds) {
#ifdef WIN32
    Sleep(miliseconds);
#else
    usleep(miliseconds * 1000);
#endif
}

void onopenclose(gdp_client_t* client, int state) {
    int nidchar;

    if(!state){
        PacketCharInfo *char_info = &listclients[nTotChars].charInfo;
        nidchar=connectclient(client);
        nTotChars+=1;

        char_info->totchar = 4;
        char_info->idchar  = nidchar;

        gdp_server_send(
            server,
            client,
            (char*)char_info,
            sizeof(PacketCharInfo));

        int i;
        for (i = 0; i < qt_inimigos; i++) {
            inimgosCharInfo[i].totchar = nTotChars;
            inimgosCharInfo[i].totenemies = qt_inimigos;
            gdp_server_send(
                server,
                client,
                (char*)&inimgosCharInfo[i],
                sizeof(PacketCharInfo));
        }
    }
    else{
        PacketCharInfo *char_info;
        nidchar=desconnectclient(client);
        char_info = &listclients[nidchar].charInfo;
        nTotChars-=1;
        char_info->totchar=nTotChars;
        char_info->idchar=nidchar;
        char_info->exit = 1;

        gdp_server_broadcast(
            server,
            (char*)char_info,
            sizeof(PacketCharInfo));

        memset(char_info, 0, sizeof(PacketCharInfo));
    }
    printf("%s: %s:%d id:%i\n", state ? "DESCONECTOU":"CONECTOU", client->addr.ip, client->addr.port,nidchar);
    printf("Total %i\n",nTotChars);
}

int connectclient(gdp_client_t* client){
    int i,free = 0;
    for(i=0;i<4;i++){
        if(listclients[i].inuse == 0){
            listclients[i].id = i;
            listclients[i].inuse    = 1;
            listclients[i].client   = client;
            free = i;
            break;
        }
    }
    return free;
}

int desconnectclient(gdp_client_t* client){
    int i,free = -1;
    for(i=0;i<4;i++){
        if (listclients[i].inuse == 1) {
            if(listclients[i].client->addr.ip == client->addr.ip){
                listclients[i].inuse = 0;
                free = listclients[i].id;
                break;
            }
        }
    }
    return free;
}


void inicializar_ambients()
{
    int i;
    char tag[5];
    char *file = "Configs//Ambients.txt", *model;
    int qt_maps = gdp_files_quick_getint(file, "qt_maps");

    width   = gdp_files_quick_getint("Configs//Config.txt","width");
    height = gdp_files_quick_getint("Configs//Config.txt","height");
    boss_num = gdp_files_quick_getint("Configs//Config.txt","boss_num");

    models = (ALLEGRO_BITMAP**) xmalloc(sizeof(ALLEGRO_BITMAP*)*qt_maps);

    for (i=1; i<=qt_maps; i++){
        sprintf(tag, "map%i", i);
        model=gdp_files_quick_getstring(file,tag);
        models[i-1] = al_load_bitmap(model);
        free(model);
    }

}

int enemy_colision(PacketCharInfo *enemy){
      if(enemy->y < 0)
          return(true);

        if( (enemy->y + (enemy->h)) > height)
            return(true);

        ALLEGRO_COLOR color,colorwall;
        colorwall = al_map_rgb(0, 0, 0);
        int Map = enemy->idmap-1;

        double sx = (double) al_get_bitmap_width( models[Map] ) / width;
        double sy = (double) al_get_bitmap_height( models[Map] ) / height;

        int we = (int)width * sx;
        int he = (int)height * sy;

        int xup = (int) ((enemy->x + enemy->w) ) * sx;
        int yup = (int) ((enemy->y)) * sy;

        int xdown = (int) (enemy->x) * sx;
        int ydown = (int) (((enemy->y) + enemy->h)) * sy;

        if((xdown >= 0 && ydown >= 0 && xup <= we && yup <= he)){
                if(enemy->d != GDPRIGHT){
                        color = al_get_pixel(models[Map], xdown, ydown);
                        if(colorwall.r == color.r && colorwall.g == color.g && colorwall.b == color.b)
                                return(true);
                }
                if(enemy->d != GDPLEFT){
                        color = al_get_pixel(models[Map], xup, ydown);
                        if(colorwall.r == color.r && colorwall.g == color.g && colorwall.b == color.b)
                                return(true);
                }
        }
        else
                return(true);

        return(false);
}

void* broadcast(ALLEGRO_THREAD *t, void *state) {
    while (1) {
        gdp_packet_t* packet = gdp_server_recv(server);
        if (packet)
        {
            PacketCharInfo *char_info = (PacketCharInfo*)gdp_packet_buffer(packet);

            char_info->totchar    = nTotChars;
            char_info->totenemies = qt_inimigos;

            if(listclients[char_info->idchar].charInfo.healt==0){
                listclients[char_info->idchar].charInfo.healt = char_info->healt;
                listclients[char_info->idchar].charInfo.x = char_info->x;
                listclients[char_info->idchar].charInfo.y = char_info->y;
            }

            char_info->healt = listclients[char_info->idchar].charInfo.healt;
            char_info->dhit  = listclients[char_info->idchar].charInfo.dhit;

            gdp_damagechar(char_info);
            gdp_damageinimigo(char_info);

            int i,x,y,w,h,d,damage;
            x = char_info->x;
            y = char_info->y;
            w = char_info->w;
            h = char_info->h;
            d = char_info->d;
            damage = char_info->damage;

            for(i=0;i<char_info->totlifeless;i++){
                char_info->x      = char_info->listlifeless[i].x;
                char_info->y      = char_info->listlifeless[i].y;
                char_info->w      = char_info->listlifeless[i].w;
                char_info->h      = char_info->listlifeless[i].h;
                char_info->d      = char_info->listlifeless[i].d;
                char_info->damage = char_info->listlifeless[i].damage;
                gdp_damagechar(char_info);
                gdp_damageinimigo(char_info);
            }

            if(char_info->healt<0)
                char_info->healt = 0;

            char_info->x = x;
            char_info->y = y;
            char_info->w = w;
            char_info->h = h;
            char_info->d = d;
            char_info->damage = damage;

            memcpy(&listclients[char_info->idchar].charInfo , char_info, sizeof(PacketCharInfo));

            gdp_server_broadcast(
                server,
                (char*)char_info,
                sizeof(PacketCharInfo));

            gdp_packet_free(packet);
        }
    }
    return NULL;
}

void iniciar_broadcast() {
    tbroadcast = al_create_thread(broadcast, NULL);
    al_start_thread(tbroadcast);
}

void carregar_inimigos() {
    int i;
    char *file = "Configs//Enemies.txt";
    qt_inimigos = gdp_files_quick_getint(file, "qt_inimigos");

    inimgosCharInfo = xmalloc(qt_inimigos * sizeof(PacketCharInfo));
    memset(inimgosCharInfo, 0, qt_inimigos * sizeof(PacketCharInfo));

    for (i = 0; i < qt_inimigos; i++) {
        char tag[20];
        int n = i+1;
        int nChar;


        sprintf(tag, "%i_x", n);
        inimgosCharInfo[i].x = gdp_files_quick_getint(file,tag);

        sprintf(tag, "%i_y", n);
        inimgosCharInfo[i].y = gdp_files_quick_getint(file, tag);

        sprintf(tag, "%i_a", n);
        inimgosCharInfo[i].a = gdp_files_quick_getint(file, tag);

        sprintf(tag, "%i_d", n);
        inimgosCharInfo[i].d = gdp_files_quick_getint(file, tag);

        sprintf(tag, "%i_num", n);
        inimgosCharInfo[i].numchar = gdp_files_quick_getint(file, tag);
        nChar=inimgosCharInfo[i].numchar;

        sprintf(tag, "%ivision", nChar);
        inimgosCharInfo[i].vision = gdp_files_quick_getint("Configs//EnemiesConf.txt", tag);

        sprintf(tag, "%istep", nChar);
        inimgosCharInfo[i].step = gdp_files_quick_getint("Configs//EnemiesConf.txt", tag);

        sprintf(tag, "%idamage", nChar);
        inimgosCharInfo[i].damage = gdp_files_quick_getint("Configs//EnemiesConf.txt", tag);

        inimgosCharInfo[i].idchar = i+4;
        inimgosCharInfo[i].exit = false;

        sprintf(tag, "%i_helt", n);
        inimgosCharInfo[i].healt = gdp_files_quick_getint(file, tag);

        sprintf(tag, "%i_stamina", n);
        inimgosCharInfo[i].stamina = gdp_files_quick_getint(file, tag);

        sprintf(tag, "%i_idmap", n);
        inimgosCharInfo[i].idmap = gdp_files_quick_getint(file, tag);

        sprintf(tag, "%i_width", n);
        inimgosCharInfo[i].w = gdp_files_quick_getint(file, tag);

        sprintf(tag, "%i_height", n);
        inimgosCharInfo[i].h = gdp_files_quick_getint(file, tag);

    }



    for (i = 0; i < qt_inimigos; i++) {
        inimgosCharInfo[i].totchar = nTotChars;
        inimgosCharInfo[i].totenemies = qt_inimigos;
        gdp_server_broadcast(
            server,
            (char*)&inimgosCharInfo[i],
            sizeof(PacketCharInfo));
    }
}

int calcula_distancia(PacketCharInfo p1,PacketCharInfo p2)
{
    int nDist;
    nDist = sqrt( pow(p2.x-p1.x,2) + pow(p2.y-p1.y,2));
    return nDist;
}

void inimigar_game(){
    int i,j, nID=0, nMDist,nDist ;
    int difX, difY, lock=0 ;
    int probability;

    while(true){

        //if(nTotChars==0) // espirito maligno aqui
        //    continue;

        for (i = 0; i < qt_inimigos; i++) {
            nMDist=-1;nDist=0;

            if (inimgosCharInfo[i].exit==1)
                continue;

            if (inimgosCharInfo[i].healt<=0){
                inimgosCharInfo[i].exit=1;
                inimgosCharInfo[i].totchar = nTotChars;
                inimgosCharInfo[i].totenemies = qt_inimigos;


                gdp_server_broadcast(
                    server,
                    (char*)&inimgosCharInfo[i],
                    sizeof(PacketCharInfo));

                continue;
            }

            for (j=0; j<4; j++){
                if (listclients[j].inuse == 1){
                    nDist = calcula_distancia(inimgosCharInfo[i], listclients[j].charInfo);
                    if (nDist < nMDist || nMDist==-1){
                        nMDist = nDist;
                        nID = j;
                    }
                }
            }


            if (nMDist <= inimgosCharInfo[i].vision ){


                if (inimgosCharInfo[i].idmap == listclients[nID].charInfo.idmap){
                    inimgosCharInfo[i].a = 1;
                    difX = abs(inimgosCharInfo[i].x-listclients[nID].charInfo.x);
                    difY = abs(inimgosCharInfo[i].y-listclients[nID].charInfo.y);

                    // verifica se da dano
                    if (
                         (inimgosCharInfo[i].x + inimgosCharInfo[i].w ) >= (listclients[nID].charInfo.x)
                            && (inimgosCharInfo[i].x )  <= (listclients[nID].charInfo.x + listclients[nID].charInfo.w)
                            && (inimgosCharInfo[i].y )  >= (listclients[nID].charInfo.y)
                            && (inimgosCharInfo[i].y - inimgosCharInfo[i].h)  <= (listclients[nID].charInfo.y + listclients[nID].charInfo.h))
                    {
                        probability = rand()%10+1;
                        //if (probability==5)
                        //{
                            gdp_damagechar(&inimgosCharInfo[i]);
                        //}
                    }



                    // movimentaç~~ao do boss é diferente
                    if (boss_num==inimgosCharInfo[i].numchar)
                    {

                       if (
                           (abs(inimgosCharInfo[i].x - listclients[nID].charInfo.x) < inimgosCharInfo[i].step + inimgosCharInfo[i].w) &&  inimgosCharInfo[i].x <= listclients[nID].charInfo.x ||
                           (abs(inimgosCharInfo[i].y - listclients[nID].charInfo.y) < inimgosCharInfo[i].step + inimgosCharInfo[i].h &&  inimgosCharInfo[i].y <= listclients[nID].charInfo.y)
                        )
                        {
                              if (lock>0)
                            {
                                lock--;
                                continue;
                            }

                            inimgosCharInfo[i].a = 3;
                            lock=20;

                             if (difX>=difY)
                            {
                                if(inimgosCharInfo[i].x < listclients[nID].charInfo.x)
                                    inimgosCharInfo[i].d = GDPRIGHT;
                                else
                                    inimgosCharInfo[i].d = GDPLEFT;
                            }
                            if (difX<difY)
                            {
                                 if(inimgosCharInfo[i].y < listclients[nID].charInfo.y)
                                    inimgosCharInfo[i].d = GDPDOWN;
                                else
                                    inimgosCharInfo[i].d = GDPUP;

                            }
                        }
                        else
                        {

                            if (lock>0)
                            {
                                lock--;
                                continue;
                            }

                            if (difX>=difY)
                            {
                                if(inimgosCharInfo[i].x < listclients[nID].charInfo.x)
                                {
                                    inimgosCharInfo[i].x+=inimgosCharInfo[i].step;
                                    inimgosCharInfo[i].d = GDPRIGHT;
                                }
                                else
                                {
                                    inimgosCharInfo[i].x-=inimgosCharInfo[i].step;
                                    inimgosCharInfo[i].d = GDPLEFT;
                                }

                            }
                            if (difX<difY)
                            {
                                 if(inimgosCharInfo[i].y < listclients[nID].charInfo.y)
                                {
                                    inimgosCharInfo[i].y+=inimgosCharInfo[i].step;
                                    inimgosCharInfo[i].d = GDPDOWN;
                                }
                                else
                                {
                                    inimgosCharInfo[i].y-=inimgosCharInfo[i].step;
                                    inimgosCharInfo[i].d = GDPUP;
                                }

                            }
                        }



                        inimgosCharInfo[i].totchar = nTotChars;
                        inimgosCharInfo[i].totenemies = qt_inimigos;
                        gdp_server_broadcast(
                            server,
                            (char*)&inimgosCharInfo[i],
                            sizeof(PacketCharInfo));

                    continue;
                    }


                    // verifica movimentação X
                    if(inimgosCharInfo[i].x < listclients[nID].charInfo.x){
                        inimgosCharInfo[i].x+=inimgosCharInfo[i].step;

                        inimgosCharInfo[i].d = GDPRIGHT;
                        if (enemy_colision(&inimgosCharInfo[i]))
                            inimgosCharInfo[i].x-=inimgosCharInfo[i].step-1;

                        if (difX>difY)
                            inimgosCharInfo[i].d = GDPLEFT;
                        else{
                            if(inimgosCharInfo[i].y < listclients[nID].charInfo.y)
                                inimgosCharInfo[i].d =GDPDOWN;
                            else
                                inimgosCharInfo[i].d = GDPUP;
                        }

                    }else{
                        if (   abs(inimgosCharInfo[i].x - listclients[nID].charInfo.x) > inimgosCharInfo[i].step*2)
                            inimgosCharInfo[i].x-=inimgosCharInfo[i].step;

                           inimgosCharInfo[i].d = GDPLEFT;
                          if (enemy_colision(&inimgosCharInfo[i]))
                            inimgosCharInfo[i].x+=inimgosCharInfo[i].step*2;

                        if (difX>difY)
                            inimgosCharInfo[i].d = GDPRIGHT;
                        else{
                            if(inimgosCharInfo[i].y < listclients[nID].charInfo.y)
                                inimgosCharInfo[i].d =GDPDOWN;
                            else
                                inimgosCharInfo[i].d = GDPUP;
                        }

                    }

                    // verifica movimentação Y
                    if(inimgosCharInfo[i].y < listclients[nID].charInfo.y){
                        inimgosCharInfo[i].y+=inimgosCharInfo[i].step;

                        inimgosCharInfo[i].d = GDPDOWN;
                        if (enemy_colision(&inimgosCharInfo[i]))
                            inimgosCharInfo[i].y-=inimgosCharInfo[i].step*2;

                        if (difY>difX)
                            inimgosCharInfo[i].d = GDPDOWN;
                        else{
                            if(inimgosCharInfo[i].x < listclients[nID].charInfo.x)
                                inimgosCharInfo[i].d =GDPRIGHT;
                            else
                                inimgosCharInfo[i].d = GDPLEFT;
                        }


                    }else{
                        if (abs(inimgosCharInfo[i].y - listclients[nID].charInfo.y) > inimgosCharInfo[i].step*2)
                            inimgosCharInfo[i].y-=inimgosCharInfo[i].step;

                        inimgosCharInfo[i].d = GDPUP;
                        if (enemy_colision(&inimgosCharInfo[i]))
                            inimgosCharInfo[i].y+=inimgosCharInfo[i].step*2;

                        if (difY>difX)
                            inimgosCharInfo[i].d = GDPUP;
                        else{
                            if(inimgosCharInfo[i].x < listclients[nID].charInfo.x)
                                inimgosCharInfo[i].d =GDPRIGHT;
                            else
                                inimgosCharInfo[i].d = GDPLEFT;
                        }
                    }
                }
            }
            else
            {
                if ( inimgosCharInfo[i].a != 0){
                     inimgosCharInfo[i].a = 0;
                }
            }


            inimgosCharInfo[i].totchar = nTotChars;
            inimgosCharInfo[i].totenemies = qt_inimigos;
            gdp_server_broadcast(
                server,
                (char*)&inimgosCharInfo[i],
                sizeof(PacketCharInfo));

        }

        mysleep(80);
    }
}

void gdp_damagechar(PacketCharInfo *tchar){
	int i;

	for(i=0;i<nTotChars;i++){
		if(listclients[i].inuse){
            if(tchar->damage > 0 && tchar->idchar != listclients[i].charInfo.idchar){
                if(tchar->idmap == listclients[i].charInfo.idmap)
                    gdp_dirdamage(tchar,&listclients[i].charInfo);
            }
        }
    }
}

void gdp_damageinimigo(PacketCharInfo *tchar){
	int i;

	for(i=0;i<qt_inimigos;i++){
        if(tchar->damage > 0 && tchar->idchar != inimgosCharInfo[i].idchar){
            if(tchar->idmap == inimgosCharInfo[i].idmap)
                gdp_dirdamage(tchar,&inimgosCharInfo[i]);
        }
    }
}

void gdp_dirdamage(PacketCharInfo *tchar,PacketCharInfo *tchardam){
    int x,y,w,h,d,wm,hm;

    x = tchar->x;
    y = tchar->y;
    w = tchar->w + x;
    h = tchar->h + y;
    d = tchar->d;
    wm = x+(int)tchar->w/2;
    hm = y+(int)tchar->h/2;

    int probability = rand()%10+1;
    if (probability<5)
    {
        return;
    }

    switch(d){
        case GDPUP:
            gdp_hit(wm,
                    y,
                    tchar->damage,
                    tchardam,d);

            break;

        case GDPDOWN:
            gdp_hit(wm,
                    h,
                    tchar->damage,
                    tchardam,d);

            break;

        case GDPLEFT:
            gdp_hit(x,
                    hm,
                    tchar->damage,
                    tchardam,d);

            break;

        case GDPRIGHT:
            gdp_hit(w,
                    hm,
                    tchar->damage,
                    tchardam,d);
            //printf("w=%i hm=%i charh=%i y=%i\n",w,hm,tchar->h,y);
            break;
    }
}

int main(int argc, char **argv) {
    init();

	server = gdp_server_new();
	gdp_server_add_onopenclose(server, onopenclose);
	gdp_server_listen(server, 34000);
    inicializar_ambients();
    printf("Heredian Server!\n");

    iniciar_broadcast();
    carregar_inimigos();
    inimigar_game();

    return EXIT_SUCCESS;
}
