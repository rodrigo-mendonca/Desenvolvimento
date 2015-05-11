#include "Include/heredianet.h"

void gdp_server_add_onopenclose(gdp_server_t* server, onopenclose_callback callback) {
	gdp_list_add(server->_onopenclose_callbacks, callback);
}

gdp_packet_t* gdp_server_recv(gdp_server_t* server) {
	return gdp_io_thread_dequeue_recv();
}

void gdp_server_send(gdp_server_t* server, gdp_client_t* client, char* buffer, int len) {
	char* buf_clone = NEWARRAY(char, len, "PACKET_BUFFER");
	memcpy(buf_clone, buffer, len);

	gdp_packet_t* pack = gdp_packet_new(client, buf_clone, len);

	gdp_server_send_packet(server, pack);
}

void gdp_server_broadcast(gdp_server_t* server, char* buffer, int len) {
	gdp_server_broadcast_except(server, buffer, len, NULL);
}

void gdp_server_broadcast_except(gdp_server_t* server, char* buffer, int len, gdp_client_t* except_client) {
	int i;
	for (i = 0; i < gdp_list_count(server->clients); i++) {
		gdp_client_t* client = (gdp_client_t*)gdp_list_get(server->clients, i);

		if (client == except_client)
			continue; // ignore this client

		gdp_server_send(server, client, buffer, len);
	}
}

void gdp_server_send_packet(gdp_server_t* server, gdp_packet_t* pack) {
	gdp_io_thread_enqueue_send(pack);
}

void gdp_server_close(gdp_server_t* server) {
	int i;

	for (i = 0; i < gdp_list_count(server->clients); i++) {
		gdp_client_t* client = (gdp_client_t*)gdp_list_get(server->clients, i);
		if (!gdp_client_isclosed(client)) {
			gdp_client_close(client);
		}
	}

	closesocket(server->sock);

	gdp_list_free(server->clients);
	gdp_list_free(server->_onopenclose_callbacks);
	gdp_io_thread_remove_server(server);

	FREE(server, "SERVER");
}

void gdp_server_onclose_client(void* c, void* state) {
	gdp_client_t* client = (gdp_client_t*)c;
	gdp_server_t* server = (gdp_server_t*)state;

	int i;
	for (i = 0; i < gdp_list_count(server->_onopenclose_callbacks); i++) {
		onopenclose_callback callback = gdp_list_get(server->_onopenclose_callbacks, i);
		callback(client, 1);
	}

	gdp_list_remove_item(server->clients, client);
}

void gdp_server_accept(gdp_server_t* server) {
	gdp_addr_t addr;
	struct sockaddr_in sin;
	memset(&sin, 0, sizeof(sin));

	socklen_t sinlen = sizeof(sin);
	socket_t clientsocket = accept(server->sock, (struct sockaddr*)&sin, &sinlen);

	strcpy(addr.ip, "IPv6");
	addr.port = ntohs(sin.sin_port);

	if (sin.sin_family == AF_INET) {
		strcpy(addr.ip, inet_ntoa(sin.sin_addr));
	}

	gdp_client_t* client = gdp_client_new(clientsocket, addr);

	gdp_client_add_onclose(client, gdp_server_onclose_client, server);
	gdp_list_add(server->clients, client);

	int i = 0;
	for (i = 0; i < gdp_list_count(server->_onopenclose_callbacks); i++) {
		onopenclose_callback callback = gdp_list_get(server->_onopenclose_callbacks, i);
		callback(client, 0);
	}
}

void gdp_server_listen(gdp_server_t* server, int port) {
	gdp_io_thread_assure_wsa();

	socket_t sock = socket(AF_INET, SOCK_STREAM, IPPROTO_TCP);
	if (sock == INVALID_SOCKET) {
		printf("Error at socket(): %d\n", WSAGetLastError());
		exit(1);
	}

	u_long mode = 1;
    char on = 1;

#ifdef WIN32
	ioctlsocket(sock, FIONBIO, &mode);
    setsockopt(sock, IPPROTO_TCP, TCP_NODELAY, &mode, sizeof(mode));
    setsockopt(sock, SOL_SOCKET, SO_REUSEADDR, &mode, sizeof(int));
#else
    ioctl(sock, FIONBIO, (char *)&on);
    setsockopt(sock, IPPROTO_TCP, TCP_NODELAY, &on, sizeof(on));
    setsockopt(sock, SOL_SOCKET, SO_REUSEADDR, &mode, sizeof(mode));
#endif

	struct sockaddr_in sin;
	memset(&sin, 0, sizeof(sin));

	sin.sin_family = AF_INET;
	sin.sin_addr.s_addr = inet_addr("0.0.0.0");		// INADDR_ANY;
	sin.sin_port = htons(port);

	if (bind(sock, (struct sockaddr*)&sin, sizeof(sin)) == SOCKET_ERROR) {
		printf("bind failed with error: %d\n", WSAGetLastError());
		closesocket(sock);
		WSACleanup();
		return;
	}

	if (listen(sock, 10) == SOCKET_ERROR) {
		printf("Listen failed with error: %d\n", WSAGetLastError());
		closesocket(sock);
		WSACleanup();
		return;
	}

	server->sock = sock;

	gdp_io_thread_inst();
	gdp_io_thread_add_server(server);
}

gdp_server_t* gdp_server_new(void) {
	gdp_server_t* server = NEW0(gdp_server_t, "SERVER");
	server->clients = gdp_list_new();
	server->_onopenclose_callbacks = gdp_list_new();
	return server;
}

