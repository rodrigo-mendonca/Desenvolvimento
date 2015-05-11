#include "Include/GameMenu.h"

//#define al_play_sample(a,b,c,d,e,f)

void gdp_intro(){
    ALLEGRO_BITMAP *image   = NULL;
    ALLEGRO_FONT *fonte     = al_load_font(".//Fonts//font_intro.TTF", 20, 0);
    // musica de fundo
    ALLEGRO_SAMPLE *music   = al_load_sample(".//Songs//Intro//intro_music.ogg");
    al_play_sample(music, 1.0, 0.0, 1.0, ALLEGRO_PLAYMODE_LOOP, NULL);
    // narrador
    ALLEGRO_SAMPLE *narrative   = al_load_sample(".//Songs//Intro//intro_narrative.ogg");
    al_play_sample(narrative, 2.0, 0.0, 1.0, ALLEGRO_PLAYMODE_ONCE, NULL);

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

    while(true){
        al_wait_for_event(event_queue, &evento);

        if(gdp_readclose()){
            break;
        }
        if(evento.type == ALLEGRO_EVENT_KEY_DOWN){
            break;
        }

        if(gdp_readtime() && al_is_event_queue_empty(event_queue)){
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

    al_destroy_bitmap(image);
    al_destroy_font(fonte);
}

void gdp_erro(char *msgerro){
    gdp_clear();
    ALLEGRO_BITMAP *image   = NULL;
    ALLEGRO_FONT *fonte     = al_load_font(".//Fonts//font_info.TTF", 20, 0);

    al_draw_textf(fonte, al_map_rgb(255, 255, 255), wigth/2, height/2,ALLEGRO_ALIGN_CENTER, "%s", msgerro);
    al_flip_display();

    while(true){
        al_wait_for_event(event_queue, &evento);

        if(gdp_readclose()){
            break;
        }
        if(evento.type == ALLEGRO_EVENT_KEY_DOWN){
            break;
        }
    }

    free(msgerro);
    al_destroy_bitmap(image);
    al_destroy_font(fonte);
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
    if (!fonte) {
		printf("Nao carregou fonte\n");
		exit(1);
    }

    ALLEGRO_BITMAP *image = al_load_bitmap(".//Images//Menu.png");

    if (!image) {
		printf("Nao abriu imagem\n");
		exit(1);
    }

    // inicia a musica
    ALLEGRO_SAMPLE *music= al_load_sample(".//Songs//Menu//music.ogg");
    al_play_sample(music, 1.0, 0.0, 1.0, ALLEGRO_PLAYMODE_LOOP, NULL);

    ALLEGRO_SAMPLE *musicsel= al_load_sample(".//Songs//Menu//musicsel.ogg");
    ALLEGRO_SAMPLE *musicconfirm= al_load_sample(".//Songs//Menu//musicconfirm.ogg");

    int options = 2,i;
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

        if(gdp_readtime() && al_is_event_queue_empty(event_queue)){
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
            for(i=1;i<=options;i++)
                al_draw_textf(fonte, al_map_rgb(0, 0, 0), wigth / 2, nposicao+nespaco*i, ALLEGRO_ALIGN_CENTRE, "%s", texto[i-1]);

            // selecao
            al_draw_textf(fonte, al_map_rgb(0, 0, 0), (wigth / 2)-230, nposicao+nopcao*nespaco, ALLEGRO_ALIGN_LEFT, "%s", confirm);

            al_flip_display();
        }

    }

    if(opmenu==1)
        gdp_selchar();

    free(texto);
    al_destroy_sample(music);
    al_destroy_sample(musicsel);
    al_destroy_sample(musicconfirm);
    al_destroy_bitmap(image);
    al_destroy_font(fonte);
}

void gdp_selchar(){
    // Carregando o arquivo de fonte
    ALLEGRO_FONT *fonte = al_load_font(".//Fonts//font_menu.ttf", 100, 0);
    ALLEGRO_BITMAP *image = al_load_bitmap(".//Images//select.png");

    ALLEGRO_SAMPLE *musicsel= al_load_sample(".//Songs//Menu//musicsel.ogg");
    ALLEGRO_SAMPLE *musicconfirm= al_load_sample(".//Songs//Menu//musicconfirm.ogg");

    int options = 4,i;
    char **texto = calloc(options,sizeof(char*));
    texto[0] = "James";
    texto[1] = "Julios";
    texto[2] = "Japa";
    texto[3] = "Gauss";
    char *confirm = ">";

    int nopcao = 0,nposicao = 120,nitensmenu = 3,nespaco = 80;

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
                opchar = nopcao;
                break;
            }
        }
        if(nopcao>nitensmenu)
                nopcao = nitensmenu;
        if(nopcao<=0)
                nopcao = 0;

        if(gdp_readtime() && al_is_event_queue_empty(event_queue)){
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
            for(i=0;i<options;i++)
                al_draw_textf(fonte, al_map_rgb(0, 0, 0), wigth / 2, nposicao+nespaco*i, ALLEGRO_ALIGN_CENTRE, "%s", texto[i]);

            // selecao
            al_draw_textf(fonte, al_map_rgb(0, 0, 0), (wigth / 2)-230, nposicao+nopcao*nespaco, ALLEGRO_ALIGN_LEFT, "%s", confirm);

            al_flip_display();
        }

    }
    free(texto);
    al_destroy_sample(musicsel);
    al_destroy_sample(musicconfirm);
    al_destroy_bitmap(image);
    al_destroy_font(fonte);
    opchar++;
}
