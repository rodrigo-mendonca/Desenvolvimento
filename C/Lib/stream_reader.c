#include "Include/heredianet.h"

gdp_stream_reader_t* gdp_stream_reader_new(char* buffer, int len) {
	gdp_stream_reader_t* reader = NEW0(gdp_stream_reader_t, "STREAM_READER");
	reader->buffer = buffer;
	reader->index = 0;
	reader->buffer_length = len;

	return reader;
}

void gdp_stream_reader_free(gdp_stream_reader_t* reader) {
	FREE(reader, "STREAM_READER");
}

int gdp_stream_reader_pending(gdp_stream_reader_t* reader) {
	return max(reader->buffer_length - reader->index, 0);
}

int gdp_stream_reader_read(gdp_stream_reader_t* reader, char* buffer, int size) {
	if (reader->index >= reader->buffer_length)
		return 0;

	if (size <= 0 || size > gdp_stream_reader_pending(reader))
		size = gdp_stream_reader_pending(reader);

	memcpy(buffer, reader->buffer + reader->index, size);

	reader->index += size;
	return size;
}
