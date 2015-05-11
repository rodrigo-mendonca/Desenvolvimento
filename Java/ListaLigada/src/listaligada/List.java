/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package listaligada;

/**
 *
 * @author rmendonca
 */
public class List {
        private Node oRaiz = new Node();;
	
	/* 
	 * construtor da Lista
	 */
	public List(){           
	}

	/*
	 * metodo que insere um dado na lista 
	 */
        
        public List Add(Object... toValores){		
            
            for(Object oL : toValores)
            {
                Add(oL);
            }
            return(this);
	}
        
	public List Add(Object toValor){		
            Node oAux = oRaiz;

            Node oNewNode = new Node();		
            oNewNode.setDado(toValor);

            while(oAux.getProx() != null){
                    oAux = oAux.getProx();
            }
            oAux.setProx(oNewNode);
            
            return(this);
	}

	/*
	 * metodo que insere um dado na lista na posicao informada
	 */
	public List Add(int tnPos, Object toValor){
		if(tnPos < 0 || tnPos > Size()){
			System.out.println("Posicao inv�lida!");
			return(this);
		}		
		Node oAux = oRaiz;
		
		Node oNewNode = new Node();
		oNewNode.setDado(toValor);
		
		int nCont = 0;
		while(oAux.getProx() != null && nCont < tnPos){
			oAux = oAux.getProx();
			nCont++;
		}		
		
		oNewNode.setProx(oAux.getProx());
		oAux.setProx(oNewNode);
                
                return(this);
	}
	
	/*
	 * metodo que verifica se o objeto informado ja esta na lista
	 */
	public boolean Contains(Object toValor){
            Node oAux = oRaiz;		
            while(oAux.getProx() != null){
                if(oAux.getProx().getDado().equals(toValor)){
                        return true;
                }			
                oAux = oAux.getProx();
            }
            return false;
	}
	
	/*
	 * metodo que remove um objeto da lista na posicao informada e retorna este objeto
	 */
	public Object Remove(int tnPos){
            if(tnPos < 0 || tnPos > Size()){
                return null;
            }

            Node oAux = oRaiz;
            int nCont = 0;

            while(nCont < tnPos && oAux.getProx() != null){
                    oAux = oAux.getProx();
                    nCont++;
            }

            Node oAserRemovido = oAux.getProx();		
            if(oAserRemovido != null){
                oAux.setProx(oAserRemovido.getProx());
                return oAserRemovido.getDado();
            }
            else{
                oAux.setProx(null);
                return null;
            }
	}
	
	/*
	 * m�todo que remove da lista o dado informado
	 */
	public boolean Remove(Object toValor){		
	    Node oAux = oRaiz;		
            while(oAux.getProx() != null){
                if(oAux.getProx().getDado().equals(toValor)){
                        Node oPosterior = oAux.getProx().getProx();

                        oAux.setProx(oPosterior);
                        return true;
                }			
                oAux = oAux.getProx();
            }
            return false;
	}

	/*
	 * metodo que retorna um dado na posicao informada
	 */
	public Object Get(int nI){
            int nCont = -1;
            Node oAux = oRaiz;
            while(nCont < nI && oAux != null){
                oAux = oAux.getProx();
                nCont++;
            }
            if(oAux != null)
                return oAux.getDado();
            else
                return null;
	}

	/*
	 * metodo que calcula o tamanho da lista
	 */
	public int Size(){
            Node oAux = oRaiz;
            int nCont = 0;
            while(oAux != null){
                oAux = oAux.getProx();
                if(oAux != null)
                        nCont++;
            }
            return nCont;
	}
	
	/*
	 * metodo que verifica se a lista esta vazia
	 */
	public boolean isEmpty(){
            if(oRaiz.getProx() == null)
                return true;
            else
                return false;
	}
	
	/*
	 * metodo que elimina todos os dados da lista
	 */
	public void Clear(){
            oRaiz.setProx(null);
	}
	
	/*
	 *  metodo que imprime todos os objetos da lista
	 */
	public String toString(){
            String cText = "[";
            Node oAux = oRaiz.getProx();
            while(oAux != null){
                    cText += oAux.getDado().toString()+", ";			
                    oAux = oAux.getProx();
            }
            if(cText.charAt(cText.length()-1) == ' ')
                    cText = cText.substring(0, cText.length()-2);
            cText+="]";
            return cText;
	}
}
