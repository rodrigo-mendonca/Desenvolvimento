#include "Include/GameLib.h"

void gdp_init(){
    // aloca limites de exibicao
    listlifeless = calloc(LIFELESS,sizeof(Lifeless*));
    listchars = calloc(CHARS,sizeof(Char*));

    // configurações iniciais
    ncanaisaudio = 3; // numero de canais de audio
    connecterro  = 0; //nao existe erro
    ntotlifeless = 0; // numero de objetos
    ntotchars    = 4; // numero de personagens
    ntotenemies  = 0; // numero de inimigos
    opmap        = 1; // mapa escolhido
    scale        = 1.5; //escala do mapa
    boss_char_id = gdp_files_quick_getint("Configs//server.txt","boss_char_id"); //id do boss

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

    //inicia addons de primitivas
    al_init_primitives_addon();

	// Configura a janela
	 al_set_new_display_flags(ALLEGRO_FULLSCREEN_WINDOW|ALLEGRO_FULLSCREEN);
	SCREEN = al_create_display(wigth, height);
	// define o titulo
	al_set_window_title(SCREEN, title);

    ambient = NULL;
}

void gdp_close(){
	al_destroy_event_queue(event_queue);
	al_destroy_timer(timer);
	al_destroy_display(SCREEN);
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

int comparar_char(const LayeredObject *a, const LayeredObject *b) {
    // move os NULLs para o fim
    int ya = INT_MAX;
    int yb = INT_MAX;

    if (a->type != OBJTYPE_NONE) ya = a->y;
    if (b->type != OBJTYPE_NONE) yb = b->y;
    return ya - yb;
}

void gdp_game(){
    gdp_send2server_init();

    if(connecterro == 1){
        gdp_erro("Não foi possível conectar ao servidor! :/");
        return;
    }
    int i,a=-1,d=-1,x=-1,y=-1;

    thsend2server = al_create_thread(gdp_send2server, NULL);
    al_start_thread(thsend2server);

    threcv2server = al_create_thread(gdp_recv2server, NULL);
    al_start_thread(threcv2server);

    while(true){
        listchars[nlocchar]->idmap = opmap;

        al_wait_for_event(event_queue, &evento);

        if(gdp_readclose())
            break;

        if(listchars[nlocchar]->obj.lock == 0 && listchars[nlocchar]->obj.a !=4){
            gdp_readaction(&listchars[nlocchar]->obj,&scale);
            gdp_readdirection(&listchars[nlocchar]->obj);
        }

        for(i=0;i<ntotchars;i++){
            if(listchars[i]!=NULL)
                gdp_statuschar(listchars[i]);
        }

        if (gdp_readtime() && al_is_event_queue_empty(event_queue)) {
		    if (a != listchars[nlocchar]->obj.a || d != listchars[nlocchar]->obj.d2 ||
		    	x != listchars[nlocchar]->obj.x || y != listchars[nlocchar]->obj.y) {
		        al_lock_mutex(musend2server);
		        mudou = true;
		        al_signal_cond(condsend2server);
		        al_unlock_mutex(musend2server);
		    }
            gdp_clear();

            ALLEGRO_TRANSFORM camera;
            al_identity_transform(&camera);
            gdp_update_camera(listchars[nlocchar], height, wigth, scale);

            // imagem de fundo
            al_draw_scaled_bitmap(ambient->image,
                0, 0,
                ambient->w,
                ambient->h,
                0,
                0, //ambient->info->h
                ambient->wd,
                ambient->hd,
                0
            );

            //mostra portoes
            for (i=0;i<(ambient->qt_gates);i++)
            {
                 al_draw_filled_rectangle
                 (
                    (ambient->gates[i].x1),
                    (ambient->gates[i].y1),
                    (ambient->gates[i].x2),
                    (ambient->gates[i].y2),
                    al_map_rgba(255, 98, 100,0)
                  );
            }

            int size = CHARS + LIFELESS;
            LayeredObject *sorted = (LayeredObject*)calloc(size, sizeof(LayeredObject));
            memset(sorted, 0, size * sizeof(LayeredObject));

            for (i = 0; i < CHARS; i++) {
                if (listchars[i] && listchars[i]->idmap == opmap) {
                    sorted[i].type = OBJTYPE_ENEMY_OR_CHAR;
                    sorted[i].y = listchars[i]->obj.y + listchars[i]->obj.h;
                    sorted[i].arr_idx = i;
                }
            }

            for (i = CHARS; i < CHARS + LIFELESS; i++) {
                if (listlifeless[i - CHARS] && listlifeless[i - CHARS]->idmap == opmap) {
                    sorted[i].type = OBJTYPE_LIFELESS;
                    sorted[i].y = listlifeless[i - CHARS]->obj.y + listlifeless[i - CHARS]->obj.h;
                    sorted[i].arr_idx = i - CHARS;
                }
            }

            qsort(sorted, size, sizeof(LayeredObject), (void*)comparar_char);

            for (i = 0; i < size; i++) {
                if (sorted[i].type == OBJTYPE_NONE)
                    break;

                if (sorted[i].type == OBJTYPE_ENEMY_OR_CHAR) {
                    if(listchars[sorted[i].arr_idx] != NULL){
                        if(listchars[sorted[i].arr_idx]->dead==0){
                                gdp_drawchar(listchars[sorted[i].arr_idx]);
                        }
                    }
                    /*
                    if (listchars[sorted[i].arr_idx]->dead==1) {
                        free(listchars[sorted[i].arr_idx]);
                        listchars[sorted[i].arr_idx] = NULL;
                    }
                    */
                } else {
                    if(listlifeless[sorted[i].arr_idx] != NULL){
                        if(listlifeless[sorted[i].arr_idx]->dead==0){
                            gdp_drawlifeless(listlifeless[sorted[i].arr_idx]);
                        }
                    }
                    /*
                    if (listlifeless[sorted[i].arr_idx]->dead==1) {
                        free(listlifeless[sorted[i].arr_idx]);
                        listlifeless[sorted[i].arr_idx] = NULL;
                    }
                    */
                }
            }

            free(sorted);

            al_use_transform(&camera);
            gdp_drawinfo(ambient->info);
            al_flip_display();
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

int gdp_colision(Object *tobj,Action *act){
    if(tobj->y < 0)
		return(true);

	if((tobj->y+ (tobj->hd)) > height)
		return(true);

	ALLEGRO_COLOR color,colorwall;
	colorwall = al_map_rgb(0, 0, 0);

	double sx = (double) ambient->w / ambient->wd;
	double sy = (double) ambient->h / ambient->hd;

	int we    = (int) ambient->wd * sx;
	int he    = (int) ambient->hd * sy;

	int xup   = (int) ((tobj->x + tobj->wd-act->rebatex) ) * sx;
    int yup   = (int) ((tobj->y+act->rebatey)) * sy;

	int xdown = (int) (tobj->x+act->rebatex) * sx;
    int ydown = (int) (((tobj->y) + tobj->hd-act->rebatey)) * sy;

	if((xdown >= 0 && ydown >= 0 && xup <= we && yup <= he)){
		if(tobj->d != GDPRIGHT){
			color = al_get_pixel(ambient->model, xdown, ydown);
			if(colorwall.r == color.r && colorwall.g == color.g && colorwall.b == color.b)
				return(true);
		}
		if(tobj->d != GDPLEFT){
			color = al_get_pixel(ambient->model, xup, ydown);
			if(colorwall.r == color.r && colorwall.g == color.g && colorwall.b == color.b)
				return(true);
		}
	}
	else
		return(true);

	return(false);
}

void gdp_movsprite(Object *tobj,Action *tact){
	if(tobj->d2 & GDPUP)
		tobj->y -= tact->stepy;
	if(tobj->d2 & GDPDOWN)
		tobj->y += tact->stepy;
	if(tobj->d2 & GDPLEFT)
		tobj->x -= tact->stepx;
	if(tobj->d2 & GDPRIGHT)
		tobj->x += tact->stepx;

    // valida colisao
	if(gdp_colision(tobj,tact)){
		if(tobj->d2 & GDPUP)
			tobj->y += tact->stepy;
		if(tobj->d2 & GDPDOWN)
			tobj->y -= tact->stepy;
		if(tobj->d2 & GDPLEFT)
			tobj->x += tact->stepx;
		if(tobj->d2 & GDPRIGHT)
			tobj->x -= tact->stepx;
	}

    if(tobj->id == nlocchar && tobj->type == 1){
        // valida troca de mapa
        gdp_valid_ambient_change(tobj,tact);
    }
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

void gdp_configinfo(Info *tinfo){
	tinfo->fonte = al_load_font("Fonts//font_info.TTF", 12, 0);
	tinfo->h = 100;
	tinfo->w = wigth;
	tinfo->color = al_map_rgb(255,255,255);
	tinfo->infochar = calloc(CHARS,sizeof(InfoChar*));
}

void gdp_update_camera(Char *tchar, float theight, float twidth, float tscale)
{
    ALLEGRO_TRANSFORM camera;
    al_identity_transform(&camera);

    float charX,charY, scaledCharX, scaledCharY,
        charW, charH;

    charW = (float)tchar->obj.wd;
    charH = (float)tchar->obj.hd;

    charX=(float)tchar->obj.x;
    charY=(float)tchar->obj.y;

    scaledCharX= -((charX*tscale )-(ambient->wd/2.0) + (charW/2) );
    scaledCharY= -((charY*tscale )-(ambient->hd/2.0) + (charH/2));

    if (scaledCharX > 0)
        scaledCharX = 0;

    if (scaledCharY > ambient->info->h)
        scaledCharY = ambient->info->h;

    if (scaledCharY <= -( (tscale-1)*(ambient->hd) ) )
        scaledCharY = -( (tscale-1)*(ambient->hd) ) ;

    if (scaledCharX <= -( (tscale-1)*(ambient->wd) ) )
        scaledCharX = -( (tscale-1)*(ambient->wd) ) ;

     al_build_transform(&camera, scaledCharX, scaledCharY, tscale,tscale,0);
     al_use_transform(&camera);
}

void gdp_valid_ambient_change(Object *tobj, Action *tact)
{
    int n,x,y,w,h;
    x = tobj->x; //+ tact->rebatex;
    y = tobj->y; //+ tact->rebatey;
    w = tobj->wd; //- tact->rebatex;
    h = tobj->hd; // - tact->rebatey;



      //valida se passou por alguma porta
    for (n=0; n<(ambient->qt_gates);n++)
    {
        Gate oGate = ambient->gates[n];
        if (   (x + w ) >= (oGate.x1)
            && (x )  <= (oGate.x2)
            && (y + h)  >= (oGate.y1)
            && (y + h)  <= (oGate.y2))
        {

            // passou pelo portao
            tobj->x     = (ambient->gates[n].ex);
            tobj->y     = (ambient->gates[n].ey);

            al_destroy_sample(ambient->musicback);
            //al_destroy_sample(ambient->musicback);
            gdp_load_ambient( (ambient->gates[n].ambientId) );
            //al_play_sample(ambient->musicback, 1.0, 0.0, 1.0, ALLEGRO_PLAYMODE_LOOP, NULL);
        }
    }
}

int idlifeless(){
    int i,r=0;
    for(i=0;i<LIFELESS;i++){
        if(listlifeless[i] == NULL){
            r = i;
            break;
        }
        if(listlifeless[i]->dead==1){
            r = i;
            break;
        }
    }
    return(r);
}
