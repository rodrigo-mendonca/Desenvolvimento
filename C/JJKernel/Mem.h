#define IniMem
#define FimMem

typedef struct MyMem
{
  int nIniMem;
  int nTam;
  struct MyMem *oProx;        
       
} Memoria;

extern Memoria *oMemory;
