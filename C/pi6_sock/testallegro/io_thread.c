#include "heredianet.h"


static gdp_io_thread_t* instance = NULL;
static pthread_once_t instance_once_control = PTHREAD_ONCE_INIT;
static pthread_once_t wsastartup_once_control = PTHREAD_ONCE_INIT;

int gdp_io_thread_getsockets(fd_set* set) {
	FD_ZERO(set);

	int i, fdmax = 0;

	for (i = 0; i < gdp_list_count(instance->servers); i++) {
		gdp_server_t* server = (gdp_server_t*)gdp_list_get(instance->servers, i);
		FD_SET(server->sock, set);
		
		if (fdmax < server->sock)
			fdmax = server->sock;
	}
			
	for (i = 0; i < gdp_list_count(instance->clients); i++) {
		gdp_client_t* client = (gdp_client_t*)gdp_list_get(instance->clients, i);
		FD_SET(client->sock, set);

		if (fdmax < client->sock)
			fdmax = client->sock;
	}

	return fdmax;
}

void gdp_io_thread_close(void) {
	instance->_running = 0;
	exit(1);

	int i;

	for (i = 0; i < gdp_list_count(instance->servers); i++) {
		gdp_server_close(gdp_list_get(instance->servers, i));
	}
	
	gdp_list_free(instance->servers);
	instance->servers = NULL;
		
	for (i = 0; i < gdp_list_count(instance->clients); i++) {
		gdp_client_close(gdp_list_get(instance->clients, i));
	}
	
	gdp_list_free(instance->clients);
	instance->clients = NULL;
	
	pthread_once_t reset = PTHREAD_ONCE_INIT;
	memcpy(&instance_once_control, &reset, sizeof(reset));
}

gdp_packet_t* gdp_io_thread_dequeue_recv(void) {
	return (gdp_packet_t*)gdp_queue_deq(instance->_queue_recv);
}

void gdp_io_thread_enqueue_send(gdp_packet_t* pack) {
	gdp_queue_enq(instance->_queue_send, pack);
}

void gdp_io_thread_enqueue_recv(gdp_packet_t* pack) {
	gdp_queue_enq(instance->_queue_recv, pack);
}

void gdp_io_thread_remove_server(gdp_server_t *server) {
	gdp_list_remove_item(instance->servers, server);
	if (gdp_list_count(instance->servers) + gdp_list_count(instance->clients) == 0)
		gdp_io_thread_close();
}

void gdp_io_thread_remove_client(gdp_client_t *client) {
	gdp_list_remove_item(instance->clients, client);
	if (gdp_list_count(instance->servers) + gdp_list_count(instance->clients) == 0)
		gdp_io_thread_close();
}

void gdp_io_thread_add_server(gdp_server_t* server) {
	gdp_list_add(instance->servers, server);
}

void gdp_io_thread_add_client(gdp_client_t* client) {
	gdp_list_add(instance->clients, client);
}

int gdp_io_thread_internal_send(socket_t sock, const char *data, int length) {
	int sent = 0;
	
	while (sent < length) {
		int sent_now = send(sock, data + sent, length - sent, 0);
		if (sent_now == 0) {
			return 0;
		} else if (sent_now > 0) {
			sent += sent_now;
		} else {
			printf("Send retornou menos que 0 = %d. SOCKET fechado.\n", sent_now);
			return 0;
		}
	}
	
	return sent;
}

void gdp_io_thread_process_recv(void) {
	fd_set sockets;
	FD_ZERO(&sockets);

	int fdmax = gdp_io_thread_getsockets(&sockets);
	if (fdmax == 0) {
		printf("fdmax: %d\n", fdmax);
		return;
	}

	struct timeval tv = { 0, 30 };

	if (select(fdmax+1, &sockets, NULL, NULL, &tv) > 0)
	{
		int i;
		for (i = 0; i < gdp_list_count(instance->servers); i++) {
			gdp_server_t* server = (gdp_server_t*)gdp_list_get(instance->servers, i);
			if (server && FD_ISSET(server->sock, &sockets)) {
				gdp_server_accept(server);
			}
		}

		for (i = 0; i < gdp_list_count(instance->clients); i++) {
			gdp_client_t* client = (gdp_client_t*)gdp_list_get(instance->clients, i);
			if (client && FD_ISSET(client->sock, &sockets)) {
				if (!gdp_client_isclosed(client)) {
					gdp_client_internal_recv(client);
				}
			}
		}
	}
}

void gdp_io_thread_process_send(void) {
	gdp_packet_t* pack;
	while (pack = (gdp_packet_t*)gdp_queue_deq(instance->_queue_send)) {
		gdp_client_t* client = pack->client;

		if (gdp_list_indexof(instance->clients, client) < 0) {
			gdp_packet_free(pack);
			continue;
		}
		
		char header[8];
		int size = gdp_packet_size(pack);
		memcpy(header, "GDP", 4);
		memcpy(header+4, &size, 4);
		
		if (!gdp_client_isclosed(client)) {
			if (!gdp_io_thread_internal_send(client->sock, header, 8))
				gdp_client_close(client);
		}
			
		if (!gdp_client_isclosed(client)) {
			if (!gdp_io_thread_internal_send(client->sock, gdp_packet_buffer(pack), size))
				gdp_client_close(client);
		}
		
		gdp_packet_free(pack);
	}
}

void* gdp_io_thread_run(void* state) {
	while (instance->_running) {
		gdp_io_thread_process_recv();
		gdp_io_thread_process_send();
		Sleep(1);
	}
}

void gdp_io_thread_inst_once(void) {
	if (!instance) {
		instance = gdp_io_thread_new();
		pthread_create(&(instance->thread), NULL, gdp_io_thread_run, NULL);
	}
}

gdp_io_thread_t* gdp_io_thread_inst() {
	pthread_once(&instance_once_control, gdp_io_thread_inst_once);
	return instance;
}

void gdp_io_thread_assure_wsa_once() {
#ifdef WIN32
	int res;

	WSADATA wsaData;
	res = WSAStartup(MAKEWORD(2,2), &wsaData);
	if (res != 0) {
		printf("WSAStartup failed: %d\n", res);
		return;
	}
#endif
}

void gdp_io_thread_assure_wsa() {
	pthread_once(&wsastartup_once_control, gdp_io_thread_assure_wsa_once);
}

gdp_io_thread_t* gdp_io_thread_new() {
	gdp_io_thread_t* io_thread = NEW0(gdp_io_thread_t, "IO_THREAD");
	io_thread->servers = gdp_list_new();
	io_thread->clients = gdp_list_new();
	io_thread->_queue_send = gdp_queue_new();
	io_thread->_queue_recv = gdp_queue_new();
	io_thread->_running = 1;
}
