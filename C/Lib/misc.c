#include "Include/heredianet.h"

pthread_mutex_t xref_mtx = PTHREAD_MUTEX_INITIALIZER;
gdp_list_t *objs = NULL;

struct ref {
	char *name;
	int count;
};

void xref(char *name, int value) {
	pthread_mutex_lock(&xref_mtx);

	if (objs == NULL)
		objs = gdp_list_new();

	int i;
	int found = 0;
	struct ref *r = NULL;

	for (i = 0; i < gdp_list_count(objs); ++i)
	{
		r = (struct ref *)gdp_list_get(objs, i);
		if (strcmp(name, r->name) == 0) {
			r->count += value;
			found = 1;
			break;
		}
	}

	if (!found) {
		r = (struct ref *)malloc(sizeof(struct ref));
		r->name = strdup(name);
		r->count = value;

		gdp_list_add(objs , r);
	}

	pthread_mutex_unlock(&xref_mtx);
}

void incref(char *name) {
	xref(name, 1);
}

void decref(char *name) {
	xref(name, -1);
}

void dumpobjs() {
	pthread_mutex_lock(&xref_mtx);

	if (objs == NULL)
		objs = gdp_list_new();

	int i;

	for (i = 0; i < gdp_list_count(objs); ++i)
	{
		struct ref *r = (struct ref *)gdp_list_get(objs, i);
		printf("%s: %d\n", r->name, r->count);
	}

	pthread_mutex_unlock(&xref_mtx);
}

void* xcalloc(size_t num, size_t size) {
	register void *value = calloc(num, size);
	if (value == 0) {
		fputs("virtual memory exhausted.\n", stderr);
		exit(EXIT_FAILURE);
	}
	return value;
}

void* xmalloc(size_t size) {
	register void *value = malloc(size);
	if (value == 0) {
		fputs("virtual memory exhausted.\n", stderr);
		exit(EXIT_FAILURE);
	}
	return value;
}

void* xmalloc0(size_t size) {
	register void *value = xmalloc(size);
	memset(value, 0, size);
	return value;
}

void* xmalloc_dbg(size_t size, char *name) {
	incref(name);
	return xmalloc(size);
}

void* xmalloc0_dbg(size_t size, char *name) {
	incref(name);
	return xmalloc0(size);
}

void free_dbg(void* value, char *name) {
	decref(name);
	free(value);
}
