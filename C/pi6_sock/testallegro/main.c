#include <stdio.h>
#include <stdlib.h>
#include <allegro5\allegro.h>

#include "heredianet.h"

struct {
	bool pressing_left;
	bool pressing_right;
	bool pressing_up;
	bool pressing_down;
	bool pressing_space;
} ctrl_state = {false, false, false, false, false};

ALLEGRO_DISPLAY *display = NULL;
ALLEGRO_TIMER *timer = NULL;
ALLEGRO_EVENT event;
ALLEGRO_EVENT_QUEUE *queue = NULL;
bool done = false;
bool should_redraw = false;

ALLEGRO_THREAD *local_char_thread;
ALLEGRO_THREAD *remote_events_thread;

ALLEGRO_BITMAP *bolas[3];
gdp_client_t* client;

#define FRAME_RATE 30
#define CHECKABORT(x) if (!(x)) { printf("%s:%d\n", __FUNCTION__, __LINE__); abort(); }

typedef struct EVENT {
	ALLEGRO_MUTEX *mutex;
	ALLEGRO_COND *cond;
	bool signaled;
} EVENT;

typedef struct CHARACTER {
	float x, y;
	int bola;
} CHARACTER;

typedef struct CHARMOVE {
	float x, y;
} CHARMOVE;

typedef struct CHARCONNECTED {
	float x, y;
	int bola;
} CHARCONNECTED;

typedef struct CHARCHANGEBALL {
	int bola;
} CHARCHANGEBALL;

typedef struct GDPPROTOCOL {
	int type;
	union {
		CHARMOVE char_move;
		CHARCONNECTED char_connected;
		CHARCHANGEBALL char_change_ball;
	};
} GDPPROTOCOL;

CHARACTER main_char = { 0, 0, 0 };
CHARACTER remote_char = { 0, 0, 0 };
CHARACTER remote_chars[3] = { 0 };

bool remote_connected = false;

#define EVENTINIT { NULL, NULL, false }

EVENT evt = EVENTINIT;
ALLEGRO_MUTEX *init_mutex = NULL;

void event_init() {
	init_mutex = al_create_mutex();
}

void event_assure(EVENT *evt) {
	if (!evt->mutex) {
		al_lock_mutex(init_mutex);
		if (!evt->mutex) {
			evt->mutex = al_create_mutex();
			evt->cond = al_create_cond();
		}
		al_unlock_mutex(init_mutex);
	}
}

void event_signal(EVENT *evt) {
	event_assure(evt);

	al_lock_mutex(evt->mutex);
	evt->signaled = true;
	al_signal_cond(evt->cond);
	al_unlock_mutex(evt->mutex);
}

void event_wait_reset(EVENT *evt) {
	event_assure(evt);

	al_lock_mutex(evt->mutex);
	while (!evt->signaled)
		al_wait_cond(evt->cond, evt->mutex);
	evt->signaled = false;
	al_signal_cond(evt->cond);
	al_unlock_mutex(evt->mutex);
}

void redraw() {
	al_draw_bitmap(bolas[main_char.bola], main_char.x, main_char.y, 0);

	if (remote_connected)
		al_draw_bitmap(bolas[remote_char.bola], remote_char.x, remote_char.y, 0);
}

void exec_local_char(ALLEGRO_THREAD *thread, void* arg) {
	while (1) {
		event_wait_reset(&evt);
		if (ctrl_state.pressing_left) main_char.x -= 1;
		if (ctrl_state.pressing_right) main_char.x += 1;
		if (ctrl_state.pressing_down) main_char.y += 1;
		if (ctrl_state.pressing_up) main_char.y -= 1;
		should_redraw = true;

		GDPPROTOCOL *protocol = NEW0(GDPPROTOCOL, "PACKET_BUFFER");
		protocol->type = 0;
		protocol->char_move.x = main_char.x;
		protocol->char_move.y = main_char.y;

		gdp_client_send(client, protocol, sizeof(GDPPROTOCOL));
	}
}

void exec_remote_events(ALLEGRO_THREAD *thread, void* arg) {
	while (1) {
		gdp_packet_t* packet = gdp_client_recv(client);
		if (packet) {
			GDPPROTOCOL *proto = (GDPPROTOCOL *)gdp_packet_buffer(packet);

			switch (proto->type) {
			case 0:
				remote_char.x = proto->char_move.x;
				remote_char.y = proto->char_move.y;
				break;
			case 1:
				remote_connected = true;
				remote_char.x = proto->char_connected.x;
				remote_char.y = proto->char_connected.y;
				remote_char.bola = proto->char_connected.bola;
				break;
			case 2:
				remote_char.bola = proto->char_change_ball.bola;
				break;
			}

			should_redraw = true;

			proto = NULL;
			gdp_packet_free(packet);
		}
	}
}

int main()
{
	event_init();

    CHECKABORT(al_init());
    CHECKABORT(al_init_image_addon());
    CHECKABORT(al_init_font_addon());
    CHECKABORT(al_init_ttf_addon());
	CHECKABORT(al_init_primitives_addon());
	CHECKABORT(al_install_mouse());
	CHECKABORT(al_install_keyboard());

	CHECKABORT(client = gdp_client_connect("cas419480d16119", 34000));

	GDPPROTOCOL *protocol = NEW0(GDPPROTOCOL, "PACKET_BUFFER");
	protocol->type = 1;
	protocol->char_connected.x = main_char.x;
	protocol->char_connected.y = main_char.y;
	protocol->char_connected.bola = main_char.bola;

	gdp_client_send(client, protocol, sizeof(GDPPROTOCOL));

	remote_events_thread = al_create_thread(exec_remote_events, NULL);
	local_char_thread = al_create_thread(exec_local_char, NULL);

	al_start_thread(local_char_thread);
	al_start_thread(remote_events_thread);

	CHECKABORT(bolas[0] = al_load_bitmap("bolaazul.bmp"));
	CHECKABORT(bolas[1] = al_load_bitmap("bolaverde.bmp"));
	CHECKABORT(bolas[2] = al_load_bitmap("bolavermelha.bmp"));

	timer = al_create_timer(1.0/FRAME_RATE);
	al_start_timer(timer);

	al_set_new_display_flags(ALLEGRO_RESIZABLE);

    CHECKABORT(display = al_create_display(640, 480));
    al_clear_to_color(al_map_rgb(0xff, 0xff, 0xff));

	al_show_mouse_cursor(display);
	al_set_window_title(display,"Example");

	queue = al_create_event_queue();

	al_register_event_source(queue, (ALLEGRO_EVENT_SOURCE*)al_get_keyboard_event_source());
	al_register_event_source(queue, (ALLEGRO_EVENT_SOURCE*)al_get_mouse_event_source());
	al_register_event_source(queue, (ALLEGRO_EVENT_SOURCE*)timer);
	al_register_event_source(queue, (ALLEGRO_EVENT_SOURCE*)display);

	while (!done) {
		al_wait_for_event(queue, &event);

		if (al_event_queue_is_empty(queue)) {
			al_rest(0);
		}

		switch(event.type) {
		case ALLEGRO_EVENT_TIMER:
			break;
		case ALLEGRO_EVENT_DISPLAY_RESIZE:
			al_acknowledge_resize(event.display.source);
			break;
		case ALLEGRO_EVENT_DISPLAY_SWITCH_IN:
			break;
		case ALLEGRO_EVENT_DISPLAY_CLOSE:
			break;
		case ALLEGRO_EVENT_KEY_DOWN:
			printf("KEYDOWN: %d\n", event.keyboard.keycode);
			switch (event.keyboard.keycode) {
			case 82:
				ctrl_state.pressing_left = true;
				break;
			case 83:
				ctrl_state.pressing_right = true;
				break;
			case 84:
				ctrl_state.pressing_up = true;
				break;
			case 85:
				ctrl_state.pressing_down = true;
				break;
			case 75:
				ctrl_state.pressing_space = true;
				break;
			}
			break;
		case ALLEGRO_EVENT_KEY_CHAR:
			switch (event.keyboard.keycode) {
			case 59:
				done = true;
				break;
			case 75:
				main_char.bola = ++main_char.bola % 3;

				{
					GDPPROTOCOL *protocol = NEW0(GDPPROTOCOL, "PACKET_BUFFER");
					protocol->type = 2;
					protocol->char_change_ball.bola = main_char.bola;

					gdp_client_send(client, protocol, sizeof(GDPPROTOCOL));
				}
				break;
			}

			break;
		case ALLEGRO_EVENT_KEY_UP:
			printf("KEYUP: %d\n", event.keyboard.keycode);
			switch (event.keyboard.keycode) {
			case 82:
				ctrl_state.pressing_left = false;
				break;
			case 83:
				ctrl_state.pressing_right = false;
				break;
			case 84:
				ctrl_state.pressing_up = false;
				break;
			case 85:
				ctrl_state.pressing_down = false;
				break;
			case 75:
				ctrl_state.pressing_space = false;
				break;
			}
			break;
		}

		event_signal(&evt);

		if (should_redraw) {
			redraw();
			al_flip_display();
			should_redraw = false;
		}
	}

    return 0;
}
