#include "Node.h"
typedef struct Lista{
      private:
      Node *oLista;
      public:
      Lista();      //Constructor
      void Insert();
      void Remove(int tnIndex);
      int Search(Node* & toItem);
}Lista;

Lista::Lista()            
{
}

void Lista::Insert()
{
     
}
void Lista::Remove(int tnIndex)
{ 
     nValor = tnValor; 
}
int Lista::Search(Node* & toItem)
{
    oNext = toItem; 
}
