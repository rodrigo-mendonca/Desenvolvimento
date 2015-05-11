#include "heredianet.h"

void* gdp_stack_new() {
    gdp_stack_t* s = (gdp_stack_t*)malloc(sizeof(gdp_stack_t));
    memset(s, 0, sizeof(gdp_stack_t));
    pthread_mutex_init(&(s->lock_obj), NULL);
    return s;
}

void gdp_stack_push(void* stack, void* value) {
    gdp_stack_t* s = (gdp_stack_t*)stack;

    gdp_node_t* node = (gdp_node_t*)malloc(sizeof(gdp_node_t));
    node->value = value;

    pthread_mutex_lock(&(s->lock_obj));

    node->next = s->top;
    s->top = node;
    s->len++;

    pthread_mutex_unlock(&(s->lock_obj));
}

void* gdp_stack_pop(void* stack) {
    gdp_stack_t* s = (gdp_stack_t*)stack;

    pthread_mutex_lock(&(s->lock_obj));

    if (s->len == 0) {
        pthread_mutex_unlock(&(s->lock_obj));
        return NULL;
    }

    gdp_node_t* node = s->top;
    void* value = node->value;
    s->top = node->next;
    s->len--;

    pthread_mutex_unlock(&(s->lock_obj));

    free(node);
    return value;
}
