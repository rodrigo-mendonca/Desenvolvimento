#include "heredianet.h"

void onopenclose(gdp_client_t* client, int state) {
	printf("%s: %s:%d\n", state ? "DESCONECTOU":"CONECTOU", client->addr.ip, client->addr.port);
}

void main(void) {
	gdp_server_t* server = gdp_server_new();
	gdp_server_add_onopenclose(server, onopenclose);
	gdp_server_listen(server, 34000);

	while (1) {
		gdp_packet_t* packet = gdp_server_recv(server);
		if (packet) {
			gdp_server_broadcast_except(
				server,
				gdp_packet_buffer(packet),
				gdp_packet_size(packet),
				gdp_packet_client(packet));
		}
	}
}