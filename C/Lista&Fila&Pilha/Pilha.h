#include "Pilha.h"

typedef struct Pilha{
      private:
      Node* oTop;
      
      public:
      Pilha();//Constructor
      void Push(int nDado);
      int Pop();
      int Top();
      int Size();
      bool IsEmpty();
      
}Pilha;

Pilha::Pilha()            
{
     
}

void Pilha::Push(int nDado)
{
        // cria um novo item com o topo como anterior
        Node* oAux = new Node();
        oAux.SetNext(oTop);
        // altera o valor do novo topo
        oAux.SetValue(toDado);
        // coloca o novo item no topo
        oTop = oAux;
}
int Pilha::Pop()
{ 
    return(0);
}
int Pilha::Top()
{
    return(0);
}
int Pilha::Size()
{
    return(0);
}
bool Pilha::IsEmpty() 
{
    return(true);
}

