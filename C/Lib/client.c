#include "Include/heredianet.h"

gdp_client_t* gdp_client_new(socket_t sock, gdp_addr_t addr) {
	gdp_client_t* client = NEW0(gdp_client_t, "CLIENT");
	strcpy(client->objsign, "CLIENT");
	client->sock = sock;
	client->addr = addr;
	client->state = IN_HEADER;
	client->_prebuffer = gdp_stream_writer_new(HEADERLENGTH);
	client->_onclose_callbacks = gdp_list_new();

	// chama no construtor para garantir existencia
	gdp_io_thread_inst();

	gdp_io_thread_add_client(client);

	return client;
}

void gdp_client_add_onclose(gdp_client_t* client, event_handler callback, void* state) {
	gdp_event_info_t* event_info = NEW0(gdp_event_info_t, "EVENT_INFO");
	event_info->callback = callback;
	event_info->state = state;

	gdp_list_add(client->_onclose_callbacks, event_info);
}

void gdp_client_remove_onclose(gdp_client_t* client, event_handler callback, void* state) {
	int i;

	for (i = 0; i < gdp_list_count(client->_onclose_callbacks); ++i)
	{
		gdp_event_info_t* ei = (gdp_event_info_t*)gdp_list_get(client->_onclose_callbacks, i);
		if (ei->callback == callback && ei->state == state) {
			gdp_list_remove(client->_onclose_callbacks, i);
			FREE(ei, "EVENT_INFO");
		}
	}
}

void gdp_client_send(gdp_client_t* client, char* buffer, int len) {
	char* buf_clone = NEWARRAY(char, len, "PACKET_BUFFER");
	memcpy(buf_clone, buffer, len);

	gdp_packet_t* pack = gdp_packet_new(client, buf_clone, len);
	gdp_io_thread_enqueue_send(pack);
}

gdp_packet_t* gdp_client_recv(gdp_client_t* client) {
	return gdp_io_thread_dequeue_recv();
}

int gdp_client_isclosed(gdp_client_t* client) {
	return client->_closed;
}

void gdp_client_close(gdp_client_t* client) {
	if (!client->_closed) {
		memset(client->objsign, 0, sizeof(client->objsign));
		client->_closed = 1;
	} else {
		printf("Fechou 2 vezes.\n");
		return;
	}

	if (client->_prebuffer)
		gdp_stream_writer_free(client->_prebuffer);

	if (client->_prepack)
		gdp_packet_free(client->_prepack);

	closesocket(client->sock);

	int i;

	for (i = 0; i < gdp_list_count(client->_onclose_callbacks); i++) {
		gdp_event_info_t* ei = (gdp_event_info_t*)gdp_list_get(client->_onclose_callbacks, i);
		ei->callback(client, ei->state);
	}

	for (i = gdp_list_count(client->_onclose_callbacks) - 1; i > 0; i--)
		gdp_list_remove(client->_onclose_callbacks, i);

	gdp_list_free(client->_onclose_callbacks);
	gdp_io_thread_remove_client(client);
	FREE(client, "CLIENT");
}

void gdp_client_internal_recv(gdp_client_t* client) {
	char *buffer = NEWARRAY(char, DEFAULT_BUFFER_SIZE, "BUFFER_RECV");
	int bytes_read = recv(client->sock, buffer, DEFAULT_BUFFER_SIZE, 0);

	if (bytes_read <= 0) {
		if (bytes_read != EWOULDBLOCK && bytes_read != EAGAIN) {
			gdp_client_close(client);
			FREE(buffer, "BUFFER_RECV");
			return;
		}
	} else {

		gdp_stream_reader_t* reader = gdp_stream_reader_new(buffer, bytes_read);
		while (gdp_stream_reader_pending(reader) > 0) {
			if (client->state == IN_HEADER) {
				int size_read_plus_pend =
					gdp_stream_writer_size(client->_prebuffer) + gdp_stream_reader_pending(reader);

				if (size_read_plus_pend < HEADERLENGTH) {
					int pending = gdp_stream_reader_pending(reader);
					char tmp[pending];

					gdp_stream_reader_read(reader, tmp, pending);
					gdp_stream_writer_append(client->_prebuffer, tmp, pending);
				} else {
					int size = HEADERLENGTH - gdp_stream_writer_size(client->_prebuffer);
					char tmp[size];

					gdp_stream_reader_read(reader, tmp, size);
					gdp_stream_writer_append(client->_prebuffer, tmp, size);

					char *cloned_buf = gdp_stream_writer_clone_buffer(client->_prebuffer);

					client->_prepack = gdp_packet_parse(
												client,
												cloned_buf,
												gdp_stream_writer_size(client->_prebuffer));
					FREE(cloned_buf, "STREAM_WRITER_CLONE_BUFFER")

					if (client->_prepack != NULL) {
						client->state = IN_PACKET;
						gdp_stream_writer_clear(client->_prebuffer);
					} else {
						gdp_client_close(client);
						FREE(buffer, "BUFFER_RECV");
						return;
					}
				}
			}

			if (client->state == IN_PACKET) {
				if (gdp_stream_reader_pending(reader) > 0) {
					int pending_in_packet = gdp_packet_pending(client->_prepack);
					char tmp[pending_in_packet];

					int bytes_to_write = gdp_stream_reader_read(reader, tmp, pending_in_packet);
					gdp_packet_append(client->_prepack, tmp, bytes_to_write);
				}

				if (gdp_packet_is_complete(client->_prepack)) {
					gdp_io_thread_enqueue_recv(client->_prepack);
					client->_prepack = NULL;
					client->state = IN_HEADER;
				}
			}
		}

		gdp_stream_reader_free(reader);

	}

	FREE(buffer, "BUFFER_RECV");
}

gdp_client_t* gdp_client_connect(const char* host, int port) {
	gdp_io_thread_assure_wsa();

	struct hostent *remote_host;
	struct sockaddr_in sockaddr;

	if (isalpha(*host)) {
		remote_host = gethostbyname(host);
	} else {
		sockaddr.sin_addr.s_addr = inet_addr(host);
		if (sockaddr.sin_addr.s_addr == INADDR_NONE) {
			return NULL;
		} else {
			remote_host = gethostbyaddr((char *) &(sockaddr.sin_addr), 4, AF_INET);
		}
	}

	if (remote_host != NULL) {
		if (remote_host->h_addrtype == AF_INET) {
			int i = 0;
			while (remote_host->h_addr_list[i] != 0) {
				sockaddr.sin_addr.s_addr = *(u_long *) remote_host->h_addr_list[i++];
			}
		} else {
			return NULL;
		}
	}
	
	sockaddr.sin_family = AF_INET;
	sockaddr.sin_port = htons(port);

	socket_t sock = socket(AF_INET, SOCK_STREAM, IPPROTO_TCP);
	if (sock == INVALID_SOCKET) {
		printf("Error at socket(): %d\n", WSAGetLastError());
		return NULL;
	}

	if (!connect(sock, (struct sockaddr *)&sockaddr, sizeof(sockaddr))) {
#ifdef WIN32
		u_long mode = 1;
		ioctlsocket(sock, FIONBIO, &mode);
	    setsockopt(sock, IPPROTO_TCP, TCP_NODELAY, &mode, sizeof mode);
#else
	    char on = 1;
	    ioctl(sock, FIONBIO, (char *)&on);
	    setsockopt(sock, IPPROTO_TCP, TCP_NODELAY, &on, sizeof on);
#endif


		gdp_addr_t addr;

		strcpy(addr.ip, inet_ntoa(sockaddr.sin_addr));
		addr.port = port;

		return gdp_client_new(sock, addr);
	}

	closesocket(sock);
	return NULL;
}
