#include "GameLib.h"

gdp_client_t* client = NULL;

void gdp_send2server_init(void) {
    mudou = false;
    client = gdp_client_connect("localhost", 34000);
    musend2server = al_create_mutex();
    condsend2server = al_create_cond();
}

void *gdp_send2server(ALLEGRO_THREAD *th, void *arg) {
    al_lock_mutex(musend2server);
    while (true) {
        while (!mudou) {
            al_wait_cond(condsend2server, musend2server);
        }

        mudou = false;

        PacketCharInfo *char_info = (PacketCharInfo*)malloc(sizeof(PacketCharInfo));
        memset(char_info, 0, sizeof(PacketCharInfo));
        char_info->x = listchars[0]->obj.x;
        char_info->y = listchars[0]->obj.y;

        gdp_client_send(client, (char*)char_info, sizeof(PacketCharInfo));
    }

    al_unlock_mutex(musend2server);
}

void gdp_init(){
    listchars = calloc(nChars,sizeof(Char*));
    ncanaisaudio = 3;

    // Inicializa a Allegro
    al_init();

    // Inicializa o add-on para utilização de imagens
    al_init_image_addon();

    // Inicializa o add-on para utilização de teclado
    al_install_keyboard();

    // Inicialização do add-on para uso de fontes
    al_init_font_addon();
    al_init_ttf_addon();

    // Inicialização do add-on para uso de sons
    al_install_audio();
    al_init_acodec_addon();
    al_reserve_samples(ncanaisaudio);

    // Configura a janela
    SCREEN = al_create_display(wigth, height);
    // define o titulo
    al_set_window_title(SCREEN, title);


    // cria thread para informar servidor sobre o que tá rolando
    gdp_send2server_init();

    thsend2server = al_create_thread(gdp_send2server, NULL);
    al_start_thread(thsend2server);
}

void gdp_close(){
    al_destroy_event_queue(event_queue);
    al_destroy_timer(timer);
    al_destroy_display(SCREEN);
}

void gdp_intro(){
    ALLEGRO_BITMAP *image   = NULL;
    ALLEGRO_FONT *fonte     = al_load_font(".//Fonts//font_intro.ttf", 45, 0);
    // musica de fundo
    ALLEGRO_SAMPLE *music   = al_load_sample(".//Songs//Menu//intro_music.ogg");
    //al_play_sample(music, 1.0, 0.0, 1.0, ALLEGRO_PLAYMODE_LOOP, NULL);
    // narrador
    ALLEGRO_SAMPLE *narrative   = al_load_sample(".//Songs//Menu//intro_narrative.ogg");
    //al_play_sample(narrative, 1.0, 0.0, 1.0, ALLEGRO_PLAYMODE_ONCE, NULL);

    char* ConfigFile = "Configs//Intro.txt";
    char* mold = "lin%i";
    char* cLine = NULL, *cLineAux=NULL;
    double speed = gdp_files_quick_getfloat(ConfigFile,"speed_text");

    int space = 45, xi=0, i=0;
    int lines = gdp_files_quick_getint(ConfigFile,"num_lines");
    char **texto = calloc(lines,sizeof(char*));
    double posicao = 0,limite = -height-(space*lines);

    for(xi=0;xi<lines;xi++)
    {
        cLine = calloc(10,sizeof(char*));

        sprintf(cLine,mold,xi+1);

        cLineAux = gdp_files_quick_getstring(ConfigFile, cLine);

        texto[xi] = cLineAux;

        free(cLine);
    }
    free(mold);
    free(ConfigFile);

    while(true){
        al_wait_for_event(event_queue, &evento);

        if(gdp_readclose()){
            break;
        }
        if(evento.type == ALLEGRO_EVENT_KEY_DOWN){
            break;
        }

        if(gdp_readtime()){
            gdp_clear();
            for(i=0;i<lines;i++)
                al_draw_textf(fonte, al_map_rgb(255, 255, 255), wigth / 2, (int)height+posicao+(space*i) ,ALLEGRO_ALIGN_CENTRE, "%s", texto[i]);

            al_flip_display();
            posicao -= speed;
            if(posicao < limite)
                break;
        }
    }

    free(texto);
    al_destroy_bitmap(image);
    al_destroy_sample(music);
    al_destroy_sample(narrative);
    al_destroy_font(fonte);
}

void gdp_loaging(){
    gdp_clear();
    char *texto;
    ALLEGRO_BITMAP *image   = NULL;
    ALLEGRO_FONT *fonte     = al_load_font(".//Fonts//font_menu.ttf", 30, 0);

    texto = "Carregando...";
    al_draw_textf(fonte, al_map_rgb(255, 255, 255), wigth-120, height-50,ALLEGRO_ALIGN_LEFT, "%s", texto);
    al_flip_display();

    free(texto);
    al_destroy_bitmap(image);
    al_destroy_font(fonte);
}

void gdp_clear(){
    // Preenchemos a janela de branco
    al_clear_to_color(setcolor(0, 0, 0,0));
}

ALLEGRO_COLOR setcolor(int tnRed,int tnGreen,int tnBlue,int tnAlfa){
    ALLEGRO_COLOR oRetorno;
    oRetorno.r = tnRed/255;
    oRetorno.g = tnGreen/255;
    oRetorno.b = tnBlue/255;
    oRetorno.a = tnAlfa/255;

    return oRetorno;
}

void gdp_splash(){
    int i;
    int ncond = 0,nlogo = 1;

    char *buf;
    ALLEGRO_BITMAP *image = NULL;
    ALLEGRO_THREAD *thread = NULL;

    thread = al_create_thread(gdp_splashclose,(void*) &ncond);
    al_start_thread(thread);

    for(i=0;i<nlogo;i++){
        buf = ".//Images//logo0.png";
        image = al_load_bitmap(buf);

        // se fechar a tela
        if(ncond==1){
            al_destroy_bitmap(image);
            break;
        }
        // se precionar qualquer botao
        if(ncond==2){
            al_destroy_bitmap(image);
            break;
        }

        gdp_fadein(image,1);
        al_rest(0.5);
        gdp_fadeout(image,1);

        al_destroy_bitmap(image);
    }
    free(buf);
}

void *gdp_splashclose(ALLEGRO_THREAD *thr, void *params){

    int *nclose = (int*)params;

    while(true){
        if(!al_is_event_queue_empty(event_queue)){
            al_wait_for_event(event_queue, &evento);

            if(gdp_readclose()){
                *nclose = 1;
                break;
            }
            if(evento.type == ALLEGRO_EVENT_KEY_DOWN){
                *nclose = 2;
                break;
            }
        }
    }
    return NULL;
}

void gdp_menu(){
    // Carregando o arquivo de fonte
    ALLEGRO_FONT *fonte = al_load_font(".//Fonts//font_menu.ttf", 100, 0);
    ALLEGRO_BITMAP *image = al_load_bitmap(".//Images//Menu.png");

    // inicia a musica
    ALLEGRO_SAMPLE *music= al_load_sample(".//Songs//Menu//music.ogg");
    al_play_sample(music, 1.0, 0.0, 1.0, ALLEGRO_PLAYMODE_LOOP, NULL);

    ALLEGRO_SAMPLE *musicsel= al_load_sample(".//Songs//Menu//musicsel.ogg");
    ALLEGRO_SAMPLE *musicconfirm= al_load_sample(".//Songs//Menu//musicconfirm.ogg");

    int options = 2;
    char **texto = calloc(options,sizeof(char*));
    texto[0] = "Novo Jogo";
    texto[1] = "Sair";
    char *confirm = ">";

    int nopcao = 1,nposicao = 220,nitensmenu = 2,nespaco = 80;

    while(true){
        al_wait_for_event(event_queue, &evento);

        if(gdp_readclose()){
            break;
        }

        if(evento.type == ALLEGRO_EVENT_KEY_DOWN){

            if(evento.keyboard.keycode == ALLEGRO_KEY_DOWN){
                al_play_sample(musicsel, 1.0, 0.0, 1.0, ALLEGRO_PLAYMODE_ONCE, NULL);
                nopcao++;

            }
            if(evento.keyboard.keycode == ALLEGRO_KEY_UP){
                al_play_sample(musicsel, 1.0, 0.0, 1.0, ALLEGRO_PLAYMODE_ONCE, NULL);
                nopcao--;
            }

            if(evento.keyboard.keycode == ALLEGRO_KEY_ENTER){
                al_play_sample(musicconfirm, 1.0, 0.0, 1.0, ALLEGRO_PLAYMODE_ONCE, NULL);
                al_rest(1.0);
                opmenu = nopcao;
                break;
            }
        }
        if(nopcao>nitensmenu)
                nopcao = nitensmenu;
        if(nopcao<=0)
                nopcao = 1;

        if(gdp_readtime()){
            gdp_clear();

            // imagem de fundo
            al_draw_scaled_bitmap(image,
                0, 0,
                al_get_bitmap_width(image),
                al_get_bitmap_height(image),
                0,
                0,
                wigth,
                height,
                0
            );

            // menu
            al_draw_textf(fonte, al_map_rgb(0, 0, 0), wigth / 2, nposicao+nespaco, ALLEGRO_ALIGN_CENTRE, "%s", texto[0]);

            al_draw_textf(fonte, al_map_rgb(0, 0, 0), wigth / 2, nposicao+nespaco*2, ALLEGRO_ALIGN_CENTRE, "%s", texto[1]);

            // selecao
            al_draw_textf(fonte, al_map_rgb(0, 0, 0), (wigth / 2)-230, nposicao+nopcao*nespaco, ALLEGRO_ALIGN_LEFT, "%s", confirm);

            al_flip_display();
        }

    }
    free(texto);
    al_destroy_sample(music);
    al_destroy_sample(musicsel);
    al_destroy_sample(musicconfirm);
    al_destroy_bitmap(image);
    al_destroy_font(fonte);
}

void gdp_game(){
    while(true){
        if (!al_is_event_queue_empty(event_queue)){
            al_wait_for_event(event_queue, &evento);

            if(gdp_readclose())
                break;

            if(gdp_readtime()){

            }
        }
    }
}

void gdp_fadein(ALLEGRO_BITMAP *image, int velocidade){
    int alfa;

    for (alfa = 0; alfa <= 255; alfa += velocidade)
    {
        gdp_clear();
        // imagem de fundo
        al_draw_tinted_scaled_bitmap(
            image,
            al_map_rgba(alfa, alfa, alfa, alfa),
            0, 0,
            al_get_bitmap_width(image),
            al_get_bitmap_height(image),
            0,
            0,
            wigth,
            height,
            0
        );
        al_flip_display();
        al_rest(0.005);
    }
}

void gdp_fadeout(ALLEGRO_BITMAP *image, int velocidade){
    int alfa;
    for (alfa = 255; alfa >= 0; alfa -= velocidade)
    {
        gdp_clear();
        // imagem de fundo
        al_draw_tinted_scaled_bitmap(image,al_map_rgba(alfa,alfa,alfa, alfa),
            0, 0,
            al_get_bitmap_width(image),
            al_get_bitmap_height(image),
            0,
            0,
            wigth,
            height,
            0
        );
        al_flip_display();
        al_rest(0.005);
    }
}

int gdp_colision(Object *tobj){
    if(tobj->y-ambient->info->h < 0)
        return(true);

    if((tobj->y+ (tobj->hd)) > height)
        return(true);

    ALLEGRO_COLOR color,colorwall;
    colorwall = al_map_rgb(0, 0, 0);

    double sx = (double) ambient->w / ambient->wd;
    double sy = (double) ambient->h / ambient->hd;

    int we    = (int) ambient->wd * sx;
    int he    = (int) ambient->hd * sy;

    int xup   = (int) ((tobj->x + tobj->wd) ) * sx;
    int yup   = (int) ((tobj->y-ambient->info->h)) * sy;

    int xdown = (int) (tobj->x) * sx;
    int ydown = (int) (((tobj->y-ambient->info->h) + tobj->hd)) * sy;

    if((xdown >= 0 && ydown >= 0 && xup <= we && yup <= he)){
        color = al_get_pixel(ambient->model, xdown, ydown);
        if(colorwall.r == color.r && colorwall.g == color.g && colorwall.b == color.b)
            return(true);

        color = al_get_pixel(ambient->model, xup, ydown);
        if(colorwall.r == color.r && colorwall.g == color.g && colorwall.b == color.b)
            return(true);
    }
    else
        return(true);

    return(false);
}

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

    gdp_movsprite(&tchar->obj,&oactions[acao]);

    oFrame = al_create_sub_bitmap(
              oactions[acao].image,
              oSprite->w*oSprite->ix,
              oSprite->h*oSprite->iy,
              oSprite->w,
              oSprite->h);

    tchar->obj.wd = (int) tchar->obj.w*oSprite->w;
    tchar->obj.hd = (int) tchar->obj.h*oSprite->h;

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

            if(oSprite->last==1 && tchar->obj.lock == 1){
                tchar->obj.lock = 0;
                tchar->obj.a = 0;
                tchar->obj.a2 &= ~2;
            }

            // verifica se deu dano em outro char
            gdp_damage(tchar);

            // gasta a stamina
            tchar->info->stamina += oactions[acao].charge;
            if(tchar->info->stamina>tchar->info->staminafull)
                tchar->info->stamina = tchar->info->staminafull;
            if(tchar->info->stamina < 0)
                tchar->info->stamina = 0;

            // muda de sprite
            gdp_popend(&oPilha);
            oactions[acao].direction[dir] = oPilha;

            al_flush_event_queue(oactions[acao].fila_timer);
        }
    }
    // mata o bitmap
    al_destroy_bitmap(oFrame);
}

void gdp_movsprite(Object *tobj,Action *tact){
    int x = tobj->x, y = tobj->y;
    if(tobj->d2 & GDPUP)
        tobj->y -= tact->stepy;
    if(tobj->d2 & GDPDOWN)
        tobj->y += tact->stepy;
    if(tobj->d2 & GDPLEFT)
        tobj->x -= tact->stepx;
    if(tobj->d2 & GDPRIGHT)
        tobj->x += tact->stepx;

    if(gdp_colision(tobj)){
        if(tobj->d2 & GDPUP)
            tobj->y += tact->stepy;
        if(tobj->d2 & GDPDOWN)
            tobj->y -= tact->stepy;
        if(tobj->d2 & GDPLEFT)
            tobj->x += tact->stepx;
        if(tobj->d2 & GDPRIGHT)
            tobj->x -= tact->stepx;
    }

    if (tobj->x != x || tobj->y != y) {
        al_lock_mutex(musend2server);
        mudou = true;
        al_signal_cond(condsend2server);
        al_unlock_mutex(musend2server);
    }
}

void gdp_loadchar(Char *ochar, char *file){
    int i,j, xi, nQTSprites=0;

    int ntamx,ntamy,nacao;
    double nfsp;
    char *image;
    char *sound;
    char *actionFile, *actionTagFile, *actionTagFileMold="act_%i";
    ALLEGRO_TIMER *chartimer;

    int numacao = gdp_files_quick_getint(file, "action_number");

    Action *oacoes;
    oacoes = calloc(numacao,sizeof(Action));
    GDPilha *opilha=NULL;
    Sprite *ospri;

    ochar->obj.id = gdp_files_quick_getint(file, "id");
    ochar->obj.w  = gdp_files_quick_getfloat(file, "scale_w");
    ochar->obj.h  = gdp_files_quick_getfloat(file, "scale_h");
    ochar->obj.x  = gdp_files_quick_getint(file, "posX");
    ochar->obj.y  = gdp_files_quick_getint(file, "posY");
    ochar->obj.d  = gdp_files_quick_getint(file, "direction");
    ochar->obj.a  = gdp_files_quick_getint(file, "ini_act");
    ochar->obj.lock = 0;
    ochar->obj.a2 = 0;
    ochar->obj.d2 = 0;

    // informacoes
    ochar->info = malloc(sizeof(InfoChar));
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
            for(j=(nQTSprites-1);j>=0;j--){
                ospri = malloc(sizeof(Sprite));

                if (nQTSprites==-1)
                {
                    ospri->ix = 0;
                    ospri->iy = 0;
                }else{
                    ospri->ix = j;
                    ospri->iy = i;
                }

                ospri->w = ntamx;
                ospri->h = ntamy;

                // marca ultimo sprite
                if(j==(nQTSprites-1))
                    ospri->last = 1;
                else
                    ospri->last = 0;

                gdp_push(&opilha,ospri);
            }
            oacoes[nacao].direction[i] = opilha;
        }

        free(image);
        free(sound);
        free(actionTagFile);
        free(actionFile);
    }

    ochar->act = oacoes;
}

void gdp_statuschar(Char *tchar){
    if(tchar->info->healt<=0){
        tchar->obj.a = 4;
        tchar->info->healt = 0;
    }
    if(tchar->info->stamina<=0){
        tchar->obj.a = 0;
    }
}

void gdp_ambient(Scene *tambient){
    char *image = "Stages//Teste//Images//region1.png";
    char *model = "Stages//Teste//Model//region1.png";
    char *sound = "Stages//Teste//Songs//musicback.ogg";

    tambient->info = malloc(sizeof(Info));
    gdp_configinfo(tambient->info);

    tambient->image  = al_load_bitmap(image);
    tambient->model  = al_load_bitmap(model);
    tambient->w      = al_get_bitmap_width(tambient->image);
    tambient->h      = al_get_bitmap_height(tambient->image);
    tambient->wd     = wigth;
    tambient->hd     = height - ambient->info->h;

    tambient->musicback = al_load_sample(sound);

    free(image);
    free(model);
    free(sound);
}

void gdp_configinfo(Info *tinfo){
    tinfo->fonte = al_load_font("Fonts//font_info.ttf", 12, 0);
    tinfo->h = 100;
    tinfo->w = wigth;
    tinfo->color = al_map_rgb(255,255,255);
}

void gdp_drawinfo(Info *tinfo){
    int i,space = (int) wigth / nChars;
    char *menu;
    char *cache = calloc(30,sizeof(char));

    for(i=0;i<nChars;i++){
        menu = "Saude: %d/%d";
        sprintf(cache,menu,
                tinfo->infochar[i]->healt,
                tinfo->infochar[i]->healtfull);
        al_draw_textf(tinfo->fonte, tinfo->color, 20+space*i, 10, ALLEGRO_ALIGN_LEFT, "%s",cache);

        free(menu);
        menu = "Cansaço: %d/%d";
        sprintf(cache,menu,
                tinfo->infochar[i]->stamina,
                tinfo->infochar[i]->staminafull
                );

        al_draw_textf(tinfo->fonte, tinfo->color, 20+space*i, 30, ALLEGRO_ALIGN_LEFT, "%s",cache);
        free(menu);
    }
    free(cache);
}
