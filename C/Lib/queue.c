#include "Include/heredianet.h"

void* gdp_queue_new() {
	gdp_queue_t* q = NEW0(gdp_queue_t, "QUEUE");
    pthread_mutex_init(&(q->lock_obj), NULL);

    return q;
}

void gdp_queue_destroy(void* queue) {
    gdp_queue_t* q = (gdp_queue_t*)queue;
	while (q->len > 0) {
		gdp_queue_deq(queue);
	}
	pthread_mutex_destroy(&(q->lock_obj));
	FREE(queue, "QUEUE");
}

void gdp_queue_enq(void* queue, void* value) {
    gdp_queue_t* q = (gdp_queue_t*)queue;
    gdp_node_t* newnode = NEW(gdp_node_t, "QUEUE_NODE");
    newnode->next = NULL;
    newnode->value = value;

    pthread_mutex_lock(&(q->lock_obj));

    if (q->len > 0) {
        q->head->next = newnode;
    } else {
        q->tail = newnode;
    }

    q->head = newnode;
    q->len++;

    pthread_mutex_unlock(&(q->lock_obj));
}

void* gdp_queue_deq(void* queue) {
    gdp_queue_t* q = (gdp_queue_t*)queue;

    pthread_mutex_lock(&(q->lock_obj));

    if (q->len == 0) {
        pthread_mutex_unlock(&(q->lock_obj));
        return NULL;
    }

    gdp_node_t* node = q->tail;
    q->tail = node->next;
    void* value = node->value;
    q->len--;

    pthread_mutex_unlock(&(q->lock_obj));

    FREE(node, "QUEUE_NODE");
    return value;
}
