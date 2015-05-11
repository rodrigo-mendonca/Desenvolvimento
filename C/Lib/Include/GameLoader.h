#ifndef GAMELOADER_H
#define GAMELOADER_H

#include "Structs.h"
#include "FileManager.h"
#include "GameLib.h"
#define itoa(i, s, l) sprintf(s, "%d", i)

void gdp_load_char(int, Char*);
void gdp_load_ambient(int);
void gdp_load_gates(char*);
void gdp_load_Lifeless(int, Lifeless *);

#endif // GAMELOADER_H
