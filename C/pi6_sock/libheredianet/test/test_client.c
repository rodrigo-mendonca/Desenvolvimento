#include "../include/heredianet.h"

int main(int argc, char **argv) {
	int i;
	char *data = "Celso Venancio Leite";
	if (argc > 1) {
		data = argv[1];
	}

	printf("Packet data: %s\n", data);

	gdp_client_t* client = gdp_client_connect("localhost", 34000);
 
	for (i = 0; ; i = ++i % INT_MAX) {
		gdp_client_send(client, strdup(data), strlen(data)+1);
		gdp_packet_t* pack = gdp_client_recv(client);
		
		//if (pack) {
			//printf("%s\n", gdp_packet_buffer(pack));
			gdp_packet_free(pack);
		//}
	}
	
	gdp_client_close(client);
	
	return 0;
}
