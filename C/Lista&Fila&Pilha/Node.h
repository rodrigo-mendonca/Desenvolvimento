typedef struct Node{
      private:
      Node* oNext;
      int nValor;
      
      public:
      Node();      //Constructor
      int GetValue() const;
      void SetValue(int tnValor);
      void SetNext(Node* toItem);
}Node;

Node::Node()            
{}
int Node::GetValue() const
   { return nValor; }
void Node::SetValue(int tnValor)
   { nValor = tnValor; }
void Node::SetNext(Node* toItem)
   { oNext = toItem; }

