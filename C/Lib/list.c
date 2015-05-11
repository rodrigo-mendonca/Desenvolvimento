#include "Include/heredianet.h"

gdp_list_t* gdp_list_new()
{
	gdp_list_t *ls = NEW0(gdp_list_t, "LIST");
	ls->available = 8;
	ls->items = NEWARRAY(void*, ls->available, "LIST_BUFFER");
	pthread_mutex_init(&(ls->lock_obj), NULL);
	return ls;
}

void gdp_list_add(gdp_list_t* list, void* item)
{
	pthread_mutex_lock(&(list->lock_obj));

	if (list->count + 1 > list->available)
	{
		void **newbuf;
		int i;
		list->available *= 2;

		newbuf = NEWARRAY(void*, list->available, "LIST_BUFFER");
		for (i = 0; i < list->count; i++)
			newbuf[i] = list->items[i];

		FREE(list->items, "LIST_BUFFER");
		list->items = newbuf;
	}

	list->items[list->count] = item;
	list->count++;

	pthread_mutex_unlock(&(list->lock_obj));
}

void* gdp_list_get(gdp_list_t* list, int index)
{
	void* item = NULL;

	pthread_mutex_lock(&(list->lock_obj));

	if (index < list->count)
		item = list->items[index];

	pthread_mutex_unlock(&(list->lock_obj));

	return item;
}

void gdp_list_set(gdp_list_t* list, int index, void* item)
{
	pthread_mutex_lock(&(list->lock_obj));

	if (index < list->count)
		list->items[index] = item;

	pthread_mutex_unlock(&(list->lock_obj));
}

void gdp_list_remove_item(gdp_list_t* list, void* item) {
	int i = gdp_list_indexof(list, item);
	if (i > -1) {
		gdp_list_remove(list, i);
	}
}

void gdp_list_remove(gdp_list_t* list, int index)
{
	pthread_mutex_lock(&(list->lock_obj));

	// shift items after index to the left
	if (index < list->count)
	{
		if (index + 1 < list->count)
		{
			int i;
			for (i = index; i < list->count; i++)
			{
				list->items[i] = list->items[i+1];
			}
		}

		list->count--;
	}

	// reduce allocated memory if necessary
	if (list->available > 8)
	{
		if (list->available / 2 > list->count)
		{
			void** newbuf;
			int i;

			newbuf = NEWARRAY(void*, list->available / 2, "LIST_BUFFER");
			for (i = 0; i < list->count; i++)
				newbuf[i] = list->items[i];

			FREE(list->items, "LIST_BUFFER");
			list->items = newbuf;
		}
	}

	pthread_mutex_unlock(&(list->lock_obj));
}

int gdp_list_count(gdp_list_t* list)
{
	return list->count;
}

void gdp_list_foreach(gdp_list_t* list, void (*eachfn)(void *))
{
	int i;

	pthread_mutex_lock(&(list->lock_obj));

	for (i = 0; i < list->count; i++)
	{
		eachfn(list->items[i]);
	}

	pthread_mutex_unlock(&(list->lock_obj));
}

int gdp_list_indexof(gdp_list_t* list, void* item)
{
	int i;

	pthread_mutex_lock(&(list->lock_obj));

	for (i = 0; i < list->count; i++)
	{
		if (list->items[i] == item) {
			pthread_mutex_unlock(&(list->lock_obj));
			return i;
		}
	}

	pthread_mutex_unlock(&(list->lock_obj));
	return -1;
}

void gdp_list_foreach_arg(gdp_list_t* list, void (*eachfn)(void *, void *), void* arg)
{
	int i;

	pthread_mutex_lock(&(list->lock_obj));

	for (i = 0; i < list->count; i++)
	{
		eachfn(list->items[i], arg);
	}

	pthread_mutex_unlock(&(list->lock_obj));
}

void gdp_list_free(gdp_list_t* list) {
	int i;
	pthread_mutex_t mutexzero = PTHREAD_MUTEX_INITIALIZER;

	while ((i = gdp_list_count(list)) > 0) {
		gdp_list_remove(list, i-1);
	}

	FREE(list->items, "LIST_BUFFER");
	list->items = NULL;
	pthread_mutex_destroy(&(list->lock_obj));
	list->lock_obj = mutexzero;
	FREE(list, "LIST");
}
