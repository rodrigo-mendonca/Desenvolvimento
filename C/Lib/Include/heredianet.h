#ifndef HEREDIANET_H_INCLUDED
#define HEREDIANET_H_INCLUDED

#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <assert.h>
#include <ctype.h>
#include <pthread.h>



#ifdef WIN32
#   include <winsock2.h>
#else
#define max(a, b) (a >= b ? a : b)
#   include <unistd.h>
#   include <errno.h>
#   include <netdb.h>
#   include <sys/ioctl.h>
#   include <sys/socket.h>
#   include <netinet/in.h>
#   include <netinet/ip.h>
#	include <netinet/tcp.h>
#   include <arpa/inet.h>
#endif

#define API(name, params, return) return name params

#ifdef DEBUGGING
#	define NEW(type, name) (type*)xmalloc_dbg(sizeof(type), name)
#	define NEW0(type, name) (type*)xmalloc0_dbg(sizeof(type), name)

#	define NEWARRAY(type, count, name) (type*)xmalloc_dbg(count * sizeof(type), name)
#	define NEWARRAY0(type, count, name) (type*)xmalloc0_dbg(count * sizeof(type), name)

#	define FREE(value, name) free_dbg(value, name);
#else
#	define NEW(type, name) (type*)xmalloc(sizeof(type))
#	define NEW0(type, name) (type*)xmalloc0(sizeof(type))

#	define NEWARRAY(type, count, name) (type*)xmalloc(count * sizeof(type))
#	define NEWARRAY0(type, count, name) (type*)xmalloc0(count * sizeof(type))

#	define FREE(value, name) free(value); value = NULL;
#endif

#define HEADERLENGTH 8
#define MAXPACKETLENGTH 1024
#define DEFAULT_BUFFER_SIZE 512

#ifdef WIN32
#	define socket_t SOCKET
#	define EWOULDBLOCK WSAEWOULDBLOCK
#	ifndef EAGAIN
#		define EAGAIN WSAEWOULDBLOCK
#	endif
#   define socklen_t int
#else
#	define socket_t int
#   define closesocket  close

#   define INVALID_SOCKET -1
#   define SOCKET_ERROR -1
#   define WSAGetLastError() errno
#   define WSACleanup()
#endif

typedef void (*event_handler)(void *, void*);

struct gdp_list {
    int count;
    int available;
    void** items;
    pthread_mutex_t lock_obj;
};

typedef struct gdp_list gdp_list_t;

struct gdp_node {
    struct gdp_node* next;
    void* value;
};

typedef struct gdp_node gdp_node_t;

struct gdp_queue {
    int len;
    gdp_node_t* head;
    gdp_node_t* tail;

    pthread_mutex_t lock_obj;
};

typedef struct gdp_queue gdp_queue_t;

struct gdp_stack {
    int len;
    gdp_node_t* top;

    pthread_mutex_t lock_obj;
};

typedef struct gdp_stack gdp_stack_t;

typedef struct gdp_stream_reader {
	char *buffer;
	int index;
	int buffer_length;
} gdp_stream_reader_t;


typedef struct gdp_stream_writer {
	char* buffer;
	int maxsize;
	int size;
} gdp_stream_writer_t;


typedef struct gdp_addr {
	char ip[15];
	int port;
} gdp_addr_t;

typedef struct gdp_client {

	char objsign[7];
	socket_t sock;
	gdp_addr_t addr;
	int _closed;
	int state;
	gdp_stream_writer_t* _prebuffer;
	struct gdp_packet* _prepack;
	gdp_list_t* _onclose_callbacks;

} gdp_client_t;

typedef struct gdp_packet {
	char objsign[7];
	gdp_client_t* client;
	char* buffer;
	int size;
	int current_size;
} gdp_packet_t;

typedef struct gdp_event_info {
	void *state;
	event_handler callback;
} gdp_event_info_t;

enum {
	IN_HEADER = 1,
	IN_PACKET
};

typedef struct gdp_io_thread {

	pthread_t thread_send;
	pthread_t thread_recv;

	pthread_mutex_t send_mutex;
	pthread_cond_t send_cond;

	pthread_mutex_t recv_mutex;
	pthread_cond_t recv_cond;

	gdp_queue_t* _queue_send;
	gdp_queue_t* _queue_recv;
	gdp_list_t* servers;
	gdp_list_t* clients;
	int _running;

} gdp_io_thread_t;

typedef struct gdp_server {
	socket_t sock;
	gdp_list_t* _onopenclose_callbacks;
	gdp_list_t* clients;

} gdp_server_t;

typedef void (*onopenclose_callback)(gdp_client_t*, int);

API(gdp_client_add_onclose, (gdp_client_t* client, event_handler callback, void* state), void);
API(gdp_client_close, (gdp_client_t* client), void);
API(gdp_client_connect, (const char* host, int port), gdp_client_t*);
API(gdp_client_internal_recv, (gdp_client_t* client), void);
API(gdp_client_isclosed, (gdp_client_t* client), int);
API(gdp_client_new, (socket_t sock, gdp_addr_t addr), gdp_client_t*);
API(gdp_client_recv, (gdp_client_t* client), gdp_packet_t*);
API(gdp_client_remove_onclose, (gdp_client_t* client, event_handler callback, void* state), void);
API(gdp_client_send, (gdp_client_t* client, char* buffer, int len), void);
API(gdp_io_thread_add_client, (gdp_client_t* client), void);
API(gdp_io_thread_add_server, (gdp_server_t* server), void);
API(gdp_io_thread_close, (void), void);
API(gdp_io_thread_dequeue_recv, (void), gdp_packet_t*);
API(gdp_io_thread_enqueue_recv, (gdp_packet_t* pack), void);
API(gdp_io_thread_enqueue_send, (gdp_packet_t* pack), void);
API(gdp_io_thread_getsockets, (fd_set* set), int);
API(gdp_io_thread_inst, (void), gdp_io_thread_t*);
API(gdp_io_thread_inst_once, (void), void);
API(gdp_io_thread_internal_send, (socket_t sock, const char *data, int length), int);
API(gdp_io_thread_new, (void), gdp_io_thread_t*);
API(gdp_io_thread_process_recv, (void), void);
API(gdp_io_thread_process_send, (gdp_packet_t *packet), void);
API(gdp_io_thread_remove_client, (gdp_client_t *client), void);
API(gdp_io_thread_remove_server, (gdp_server_t *server), void);
API(gdp_io_thread_run_recv, (void* state), void*);
API(gdp_io_thread_run_send, (void* state), void*);
API(gdp_list_add, (gdp_list_t* list, void* item), void);
API(gdp_list_count, (gdp_list_t* list), int);
API(gdp_list_foreach, (gdp_list_t* list, void (*eachfn)(void *)), void);
API(gdp_list_foreach_arg, (gdp_list_t* list, void (*eachfn)(void *, void *), void* arg), void);
API(gdp_list_free, (gdp_list_t* list), void);
API(gdp_list_get, (gdp_list_t* list, int index), void*);
API(gdp_list_indexof, (gdp_list_t* list, void* item), int);
API(gdp_list_new, (void), gdp_list_t*);
API(gdp_list_remove, (gdp_list_t* list, int index), void);
API(gdp_list_remove_item, (gdp_list_t* list, void* item), void);
API(gdp_list_set, (gdp_list_t* list, int index, void* item), void);
API(gdp_packet_append, (gdp_packet_t* packet, const char* buffer, int len), void);
API(gdp_packet_buffer, (gdp_packet_t* packet), char*);
API(gdp_packet_client, (gdp_packet_t* packet), gdp_client_t*);
API(gdp_packet_free, (gdp_packet_t* packet), void);
API(gdp_packet_free_but_buffer, (gdp_packet_t* packet), void);
API(gdp_packet_is_complete, (gdp_packet_t* packet), int);
API(gdp_packet_new, (gdp_client_t* client, char* buffer, int len), gdp_packet_t*);
API(gdp_packet_new_from_air, (gdp_client_t* client, int size), gdp_packet_t*);
API(gdp_packet_parse, (gdp_client_t* client, const char* buffer, int len), gdp_packet_t*);
API(gdp_packet_pending, (gdp_packet_t* packet), int);
API(gdp_packet_size, (gdp_packet_t* packet), int);
API(gdp_queue_deq, (void* queue), void*);
API(gdp_queue_destroy, (void* queue), void);
API(gdp_queue_enq, (void* queue, void* value), void);
API(gdp_queue_new, (void), void*);
API(gdp_server_accept, (gdp_server_t* server), void);
API(gdp_server_add_onopenclose, (gdp_server_t* server, onopenclose_callback callback), void);
API(gdp_server_broadcast, (gdp_server_t* server, char* buffer, int len), void);
API(gdp_server_broadcast_except, (gdp_server_t* server, char* buffer, int len, gdp_client_t* except_client), void);
API(gdp_server_close, (gdp_server_t* server), void);
API(gdp_server_listen, (gdp_server_t* server, int port), void);
API(gdp_server_new, (void), gdp_server_t*);
API(gdp_server_recv, (gdp_server_t* server), gdp_packet_t*);
API(gdp_server_send, (gdp_server_t* server, gdp_client_t* client, char* buffer, int len), void);
API(gdp_server_send_packet, (gdp_server_t* server, gdp_packet_t* pack), void);
API(gdp_stack_new, (void), void*);
API(gdp_stack_pop, (void* stack), void*);
API(gdp_stack_push, (void* stack, void* value), void);
API(gdp_stream_reader_free, (gdp_stream_reader_t* reader), void);
API(gdp_stream_reader_new, (char* buffer, int len), gdp_stream_reader_t*);
API(gdp_stream_reader_pending, (gdp_stream_reader_t* reader), int);
API(gdp_stream_reader_read, (gdp_stream_reader_t* reader, char* buffer, int size), int);
API(gdp_stream_writer_append, (gdp_stream_writer_t* writer, const char* buffer, int len), int);
API(gdp_stream_writer_buffer, (gdp_stream_writer_t* writer), char*);
API(gdp_stream_writer_clear, (gdp_stream_writer_t* writer), void);
API(gdp_stream_writer_clone_buffer, (gdp_stream_writer_t* writer), char*);
API(gdp_stream_writer_free, (gdp_stream_writer_t* writer), void);
API(gdp_stream_writer_new, (int maxsize), gdp_stream_writer_t*);
API(gdp_stream_writer_size, (gdp_stream_writer_t* writer), int);

API(xcalloc, (size_t num, size_t size), void*);
API(xmalloc, (size_t size), void*);
API(xmalloc0, (size_t size), void*);
API(xmalloc_dbg, (size_t size, char* name), void*);
API(xmalloc0_dbg, (size_t size, char* name), void*);

API(free_dbg, (void* value, char* name), void);
API(dumpobjs, (void), void);


// metodos
void gdp_client_close(gdp_client_t*);
gdp_client_t* gdp_client_connect(const char*, int);
void gdp_io_thread_assure_wsa();


#define AQUI printf("CHEGOU AQUI %s:%d:%s\n", __FILE__, __LINE__, __FUNCTION__ );
#define AQUIF(fmt, args) printf("CHEGOU AQUI %s:%d:%s\n   " fmt "\n", __FILE__, __LINE__, __FUNCTION__, args);

#endif // HEREDIANET_H_INCLUDED
