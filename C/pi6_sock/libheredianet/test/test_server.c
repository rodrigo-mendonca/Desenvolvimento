#include "../include/heredianet.h"

int main(int argc, char **argv) {
	int i = 0;

	printf("%d, %d, %d\n", sizeof(char), sizeof(int), sizeof(unsigned int));

	gdp_server_t* server = gdp_server_new();
	printf("Iniciou server\n");
	gdp_server_listen(server, 34000);
	printf("Aguardando por clientes.\n");

	void show(gdp_client_t* client, int state) {
		printf("%s:%d -> %s\n", client->addr.ip, client->addr.port, state == 1 ? "FECHOU" : "ABRIU");
	};

	gdp_server_add_onopenclose(server, show);

	DWORD start = GetTickCount(), end = 0, j = 0;

	while (1) {
		gdp_packet_t* pack = gdp_server_recv(server);
		//if (pack) {
			if (strcmp(pack->objsign, "PACKET") != 0) {
				printf("Chegou pacote invalido.");
			}

			//printf("%d\t%s\n", i, gdp_packet_buffer(pack));

			char* buf = NEWARRAY(char, 64, "PACKET_BUFFER");
			sprintf(buf, "Caramba %d", i);
			gdp_server_send(server, pack->client, buf, strlen(buf) + 1);
			i++;

			gdp_packet_free(pack);
		//}
		
		end = GetTickCount();
		if (end - start > 5000) {
			printf("Packs/sec = %d/%f = %f\n", i-j, (end-start)/1000.0, (i-j)/((end-start)/1000.0));
			j = i;

			dumpobjs();

			start = GetTickCount();
		}
	}
	
	return 0;
}
