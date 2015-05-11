#include "Include/GameLoader.h"

void gdp_load_char(int id, Char *ochar){
    int i,j, xi, nQTSprites=0;

    int ntamx,ntamy,nacao;
    double nfsp;
    char *file;
    char *image;
    char *sound;
    char *actionFile, *actionTagFile, *actionTagFileMold="act_%i";
    ALLEGRO_TIMER *chartimer;

    char buffer[1];
    itoa(id,buffer,10);
    file = gdp_files_quick_getstring("Configs//Chars.txt",buffer);

    int numacao = gdp_files_quick_getint(file, "action_number");

    Action *oacoes;
    oacoes = calloc(numacao,sizeof(Action));
    GDPilha *opilha=NULL;
    Sprite *ospri;

    ochar->dead = 0;
    ochar->totlifeless = 0;
    ochar->listlifeless  = calloc(10,sizeof(Lifeless*));

    ochar->obj.type = 1;
    ochar->obj.id   = gdp_files_quick_getint(file, "id");
    ochar->obj.w    = gdp_files_quick_getfloat(file, "scale_w");
    ochar->obj.h    = gdp_files_quick_getfloat(file, "scale_h");
    ochar->obj.x    = gdp_files_quick_getint(file, "posX");
    ochar->obj.y    = gdp_files_quick_getint(file, "posY");
    ochar->obj.d    = gdp_files_quick_getint(file, "direction");
    ochar->obj.a    = gdp_files_quick_getint(file, "ini_act");
    ochar->obj.lock = 0;
    ochar->obj.a2   = 0;
    ochar->obj.d2   = 0;

    // informacoes
    ochar->info = malloc(sizeof(InfoChar));
    ochar->info->name         = gdp_files_quick_getstring(file, "name");
    ochar->info->healtfull    = gdp_files_quick_getint(file, "healtfull");
    ochar->info->staminafull  = gdp_files_quick_getint(file, "staminafull");
    ochar->info->healt = ochar->info->healtfull;
    ochar->info->stamina = ochar->info->staminafull;

    ALLEGRO_BITMAP *pri;
    for (xi=0;xi<numacao;xi++)
    {
        actionTagFile = calloc(10,sizeof(char));
        sprintf(actionTagFile,actionTagFileMold,xi);
        actionFile = gdp_files_quick_getstring(file, actionTagFile);

        //configura ações
        nacao = gdp_files_quick_getint(actionFile,"id");
        image=gdp_files_quick_getstring(actionFile,"image");
        pri = al_load_bitmap(image);
        if (pri == NULL) {
            printf("Imagem %s, configurada em %s nao encontrada.", image, actionFile);
            exit(1);
        }
        ntamx = gdp_files_quick_getint(actionFile,"size_x");
        ntamy = gdp_files_quick_getint(actionFile,"size_y");

        oacoes[nacao].id            = nacao;
        oacoes[nacao].image         = pri;
        oacoes[nacao].stepx         = gdp_files_quick_getint(actionFile,"stepx");
        oacoes[nacao].stepy         = gdp_files_quick_getint(actionFile,"stepy");
        oacoes[nacao].fps           = gdp_files_quick_getint(actionFile,"fps");
        oacoes[nacao].lifelessid    = gdp_files_quick_getint(actionFile,"lifelessid");
        oacoes[nacao].rebatex       = gdp_files_quick_getint(actionFile,"rebatex");
        oacoes[nacao].rebatey       = gdp_files_quick_getint(actionFile,"rebatey");
        oacoes[nacao].fila_timer    = al_create_event_queue();

        sound = gdp_files_quick_getstring(actionFile,"sound");

        if (strcmp(sound,"NULL")!=0)
            oacoes[nacao].sound = al_load_sample(sound);

        oacoes[nacao].charge = gdp_files_quick_getint(actionFile,"charge");
        oacoes[nacao].damage = gdp_files_quick_getint(actionFile,"damage");
        oacoes[nacao].lock = gdp_files_quick_getint(actionFile,"lock");

        nfsp = (double)1.0 / oacoes[nacao].fps;
        chartimer = al_create_timer(nfsp);
        al_register_event_source(oacoes[nacao].fila_timer, al_get_timer_event_source(chartimer));
        al_start_timer(chartimer);

        nQTSprites = gdp_files_quick_getint(actionFile,"qt_sprites");

        for(i=0;i<4;i++){
            opilha=NULL;

            if (nQTSprites==-1)
            {
                ospri = malloc(sizeof(Sprite));
                ospri->ix = 0;
                ospri->iy = 0;
                ospri->first = 1;
                ospri->last  = 1;
                gdp_push(&opilha,ospri);
            }

            for(j=(nQTSprites-1);j>=0;j--){
                ospri = malloc(sizeof(Sprite));
                ospri->ix = j;
                ospri->iy = i;
                ospri->w = ntamx;
                ospri->h = ntamy;

                ospri->last = 0;
                ospri->first = 0;

                 // marca ultimo sprite
                if(j==(nQTSprites-1)){
                    ospri->last = 1;
                }
                if(j==0){
                    ospri->first = 1;
                }
                gdp_push(&opilha,ospri);
            }
            oacoes[nacao].direction[i] = opilha;
        }

        free(image);
        free(sound);
        free(actionTagFile);
        free(actionFile);

    }
    free(file);
    ochar->act = oacoes;
}

void gdp_load_ambient(int id){
    opmap = id;
     // arquivo com informa??o d todos os mapas
    char *MainConfigFile = "Configs//Ambients.txt";

    //carrega arquivo de configura??o
    char *mold = "map%i";
    char *tag = calloc(10,sizeof(char));

    char *ConfigFile;
    char *image;
    char *model;
    char *sound;

    //carrega ambient a partir do mapa passado
    sprintf(tag,mold,id);

    ConfigFile = gdp_files_quick_getstring(MainConfigFile,tag);
    image = gdp_files_quick_getstring(ConfigFile,"image");
    model = gdp_files_quick_getstring(ConfigFile,"model");
    sound = gdp_files_quick_getstring(ConfigFile,"sound");

    if (ambient != NULL) {
        free(ambient->gates);
        free(ambient);
    }

    ambient = malloc(sizeof(Scene));
    ambient->info = malloc(sizeof(Info));
    gdp_configinfo(ambient->info);

    ambient->id     = id;
    ambient->image  = al_load_bitmap(image);
    ambient->model  = al_load_bitmap(model);
    al_lock_bitmap(ambient->model, al_get_bitmap_format(ambient->model),ALLEGRO_LOCK_READONLY);

    ambient->w      = al_get_bitmap_width(ambient->image);
    ambient->h      = al_get_bitmap_height(ambient->image);
    ambient->wd     = wigth;
    ambient->hd     = height;

    ambient->ex     = gdp_files_quick_getint(ConfigFile,"ex");
    ambient->ey     = gdp_files_quick_getint(ConfigFile,"ey");

    gdp_load_gates(ConfigFile);

    ambient->musicback = al_load_sample(sound);

    free(image);
    free(model);
    free(sound);

    free(tag);
    free(ConfigFile);

    int i;
    for(i=0;i<ntotchars;i++){
        if(listchars[i] !=NULL)
            ambient->info->infochar[i] = listchars[i]->info;
    }
    // inicia a musica do cenario
    al_play_sample(ambient->musicback, 1.0, 0.0, 1.0, ALLEGRO_PLAYMODE_LOOP, NULL);


}

void gdp_load_gates(char* filename){
    int n;
    int qt_gates = gdp_files_quick_getint(filename, "num_gates");
    char *tag, *buff;

    ambient->qt_gates = qt_gates;
    ambient->gates = (Gate*)calloc(qt_gates, sizeof(Gate));

    for (n=1; n<=qt_gates; n++)
    {
        tag  = "gate%i_x1"; buff=calloc(10,sizeof(char));
        sprintf(buff, tag, n);
        ambient->gates[n-1].x1 = gdp_files_quick_getint(filename,buff);
        free(buff);

        tag  = "gate%i_y1";buff=calloc(10,sizeof(char));
        sprintf(buff, tag, n);
        ambient->gates[n-1].y1 = gdp_files_quick_getint(filename,buff);
        free(buff);

        tag  = "gate%i_x2";buff=calloc(10,sizeof(char));
        sprintf(buff, tag, n);
        ambient->gates[n-1].x2 = gdp_files_quick_getint(filename,buff);
        free(buff);

        tag  = "gate%i_y2";buff=calloc(10,sizeof(char));
        sprintf(buff, tag, n);
        ambient->gates[n-1].y2 = gdp_files_quick_getint(filename,buff);
        free(buff);

        tag  = "gate%i_map";buff=calloc(10,sizeof(char));
        sprintf(buff, tag, n);
        ambient->gates[n-1].ambientId = gdp_files_quick_getint(filename,buff);
        free(buff);

        tag  = "gate%i_ex";buff=calloc(10,sizeof(char));
        sprintf(buff, tag, n);
        ambient->gates[n-1].ex = gdp_files_quick_getint(filename,buff);
        free(buff);

        tag  = "gate%i_ey";buff=calloc(10,sizeof(char));
        sprintf(buff, tag, n);
        ambient->gates[n-1].ey = gdp_files_quick_getint(filename,buff);
        free(buff);

    }
}

void gdp_load_Lifeless(int id, Lifeless *olifeless){
    int i,j, xi, nQTSprites=0;

    int ntamx,ntamy,nacao;
    double nfsp;
    char *file;
    char *image;
    char *sound;
    char *actionFile, *actionTagFile, *actionTagFileMold="act_%i";
    ALLEGRO_TIMER *chartimer;

    char buffer[1];
    itoa(id,buffer,10);
    file = gdp_files_quick_getstring("Configs//Lifeless.txt",buffer);

    int numacao = gdp_files_quick_getint(file, "action_number");

    Action *oacoes;
    oacoes = calloc(numacao,sizeof(Action));
    GDPilha *opilha=NULL;
    Sprite *ospri;

    olifeless->obj.type = 2;
    olifeless->obj.id = gdp_files_quick_getint(file, "id");
    olifeless->obj.w  = gdp_files_quick_getfloat(file, "scale_w");
    olifeless->obj.h  = gdp_files_quick_getfloat(file, "scale_h");
    olifeless->obj.x  = gdp_files_quick_getint(file, "posX");
    olifeless->obj.y  = gdp_files_quick_getint(file, "posY");
    olifeless->obj.d  = gdp_files_quick_getint(file, "direction");
    olifeless->obj.a  = gdp_files_quick_getint(file, "ini_act");
    olifeless->obj.lock = 0;
    olifeless->obj.a2 = 0;
    olifeless->obj.d2 = 0;

    ALLEGRO_BITMAP *pri;
    for (xi=0;xi<numacao;xi++)
    {
        actionTagFile = calloc(10,sizeof(char));
        sprintf(actionTagFile,actionTagFileMold,xi);
        actionFile = gdp_files_quick_getstring(file, actionTagFile);

        //configura ações
        nacao = gdp_files_quick_getint(actionFile,"id");
        image=gdp_files_quick_getstring(actionFile,"image");
        pri = al_load_bitmap(image);
        ntamx = gdp_files_quick_getint(actionFile,"size_x");
        ntamy = gdp_files_quick_getint(actionFile,"size_y");

        oacoes[nacao].id     = nacao;
        oacoes[nacao].image  = pri;
        oacoes[nacao].stepx  = gdp_files_quick_getint(actionFile,"stepx");
        oacoes[nacao].stepy  = gdp_files_quick_getint(actionFile,"stepy");
        oacoes[nacao].fps    = gdp_files_quick_getint(actionFile,"fps");
        oacoes[nacao].fila_timer = al_create_event_queue();

        sound = gdp_files_quick_getstring(actionFile,"sound");

        if (strcmp(sound,"NULL")!=0)
            oacoes[nacao].sound = al_load_sample(sound);

        oacoes[nacao].damage = gdp_files_quick_getint(actionFile,"damage");
        oacoes[nacao].lock = gdp_files_quick_getint(actionFile,"lock");

        nfsp = (double)1.0 / oacoes[nacao].fps;
        chartimer = al_create_timer(nfsp);
        al_register_event_source(oacoes[nacao].fila_timer, al_get_timer_event_source(chartimer));
        al_start_timer(chartimer);

        nQTSprites = gdp_files_quick_getint(actionFile,"qt_sprites");

        for(i=0;i<4;i++){
            opilha=NULL;

            if (nQTSprites==-1)
            {
                ospri = malloc(sizeof(Sprite));
                ospri->ix = 0;
                ospri->iy = 0;
                ospri->first = 1;
                ospri->last  = 1;
                gdp_push(&opilha,ospri);
            }

            for(j=(nQTSprites-1);j>=0;j--){
                ospri = malloc(sizeof(Sprite));
                ospri->ix = j;
                ospri->iy = i;
                ospri->w = ntamx;
                ospri->h = ntamy;

                ospri->last  = 0;
                ospri->first = 0;

                 // marca ultimo sprite
                if(j==(nQTSprites-1)){
                    ospri->last = 1;
                }
                if(j==0){
                    ospri->first = 1;
                }
                gdp_push(&opilha,ospri);
            }
            oacoes[nacao].direction[i] = opilha;
        }

        free(image);
        free(sound);
        free(actionTagFile);
        free(actionFile);

    }
    free(file);
    olifeless->act = oacoes;
}
