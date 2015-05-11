#include "Include/heredianet.h"

gdp_stream_writer_t* gdp_stream_writer_new(int maxsize) {
	gdp_stream_writer_t* writer = NEW0(gdp_stream_writer_t, "STREAM_WRITER");
	writer->buffer = NEWARRAY0(char, maxsize, "STREAM_WRITER_BUFFER");
	writer->maxsize = maxsize;
	writer->size = 0;

	return writer;
}

void gdp_stream_writer_free(gdp_stream_writer_t* writer) {
	FREE(writer->buffer, "STREAM_WRITER_BUFFER");
	FREE(writer, "STREAM_WRITER");
}

int gdp_stream_writer_append(gdp_stream_writer_t* writer, const char* buffer, int len) {
	if (writer->size + len > writer->maxsize) {
		len = writer->maxsize - writer->size;
	}

	memcpy(writer->buffer + writer->size, buffer, len);

	writer->size += len;
	return len;
}

char* gdp_stream_writer_buffer(gdp_stream_writer_t* writer) {
	return writer->buffer;
}

char* gdp_stream_writer_clone_buffer(gdp_stream_writer_t* writer) {
	char* clone = NEWARRAY0(char, gdp_stream_writer_size(writer), "STREAM_WRITER_CLONE_BUFFER");
	memcpy(clone, gdp_stream_writer_buffer(writer), gdp_stream_writer_size(writer));
	return clone;
}

int gdp_stream_writer_size(gdp_stream_writer_t* writer) {
	return writer->size;
}

void gdp_stream_writer_clear(gdp_stream_writer_t* writer) {
	memset(writer->buffer, 0, writer->size);
	writer->size = 0;
}
