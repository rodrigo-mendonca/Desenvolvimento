#include "heredianet.h"

gdp_packet_t* gdp_packet_new_from_air(gdp_client_t* client, int size) {
	gdp_packet_t* packet = NEW0(gdp_packet_t, "PACKET");
	strcpy(packet->objsign, "PACKET");
	packet->client = client;
	packet->size = size;
	packet->buffer = NEWARRAY0(char, size, "PACKET_BUFFER");
	
	return packet;
}

gdp_packet_t* gdp_packet_new(gdp_client_t* client, char* buffer, int len) {
	gdp_packet_t* packet = NEW0(gdp_packet_t, "PACKET");
	strcpy(packet->objsign, "PACKET");
	packet->client = client;
	packet->buffer = buffer;
	packet->size = len;
	packet->current_size = len;
	
	return packet;
}

void gdp_packet_free(gdp_packet_t* packet) {
	if (!packet) {
		AQUIF("Tentando FREE no pacote NULL.\n", packet);
	} else if (strncmp(packet->objsign, "PACKET", 6) != 0) {
		AQUIF("FREE: esperando pacote mas nao veio. (%p)\n", packet);
	} else {
		memset(packet->objsign, 0, sizeof(packet->objsign));
	}

	if (packet->buffer)
		FREE(packet->buffer, "PACKET_BUFFER");
	gdp_packet_free_but_buffer(packet);
}

void gdp_packet_free_but_buffer(gdp_packet_t* packet) {
	FREE(packet, "PACKET");
}

void gdp_packet_append(gdp_packet_t* packet, const char* buffer, int len) {
	memcpy(packet->buffer + packet->current_size, buffer, len);
	packet->current_size += len;
}

int gdp_packet_is_complete(gdp_packet_t* packet) {
	return packet->current_size == packet->size;
}

int gdp_packet_pending(gdp_packet_t* packet) {
	return packet->size - packet->current_size;
}

int gdp_packet_size(gdp_packet_t* packet) {
	return packet->size;
}

char* gdp_packet_buffer(gdp_packet_t* packet) {
	return packet->buffer;
}

gdp_packet_t* gdp_packet_parse(gdp_client_t* client, const char* buffer, int len) {
	if (memcmp(buffer, "GDP\0", 4) != 0) {
		fprintf(stderr, "Invalid signature. 0x%2x%2x%2x%2x.\n", *buffer, *(buffer+1), *(buffer+2), *(buffer+3));
		return NULL;
	}
	
	int size = (int)*(buffer + 4);
	if (MAXPACKETLENGTH < size) {
		fprintf(stderr, "Invalid buffer. Size '%d' greater than '%d'.\n", size, MAXPACKETLENGTH);
		return NULL;
	}
		
	return gdp_packet_new_from_air(client, size);
}
