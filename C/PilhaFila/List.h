typedef struct MyPilha
{
  int nValor;
  struct MyPilha *oProximo;

} Pilha;


void PUSH(Pilha **paramPilha, int nValue);
void POP(Pilha **paramPilha);
void POPEND(Pilha **paramPilha);
int TOP(Pilha **paramPilha);
int SizePilha(Pilha **paramPilha);
