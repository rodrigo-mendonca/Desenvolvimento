/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package operacoesmatriz;

/**
 *
 * @author rmendonca
 */
public class MatrizSparsa extends Matriz {
    NoEsparsa oEsparsa = null;
    
    public MatrizSparsa()
    {
        oEsparsa = new NoEsparsa(null,null);
    }
    public void CriarMatriz(int tnLinhas,int tnColunas){
        oEsparsa = new NoEsparsa(null,null);
        Posicao(tnLinhas-1,tnColunas-1,0);
    }
    @Override
    public double Posicao(int nI, int nJ) {
        NoEsparsa oAux = oEsparsa;
        while(!(oAux.nLinha == nI)){
            if(oAux.oProxLinha==null){
                oAux.oProxLinha = new NoEsparsa(oAux,null);
            }
            oAux = oAux.oProxLinha;
        }
        while(!(oAux.nColuna == nJ)){
            if(oAux.oProxColuna==null){
                oAux.oProxColuna = new NoEsparsa(null,oAux);
            }
            oAux = oAux.oProxColuna;
        }
        return(oAux.nValor);
    }
    @Override
    public void Posicao(int nI, int nJ,double nK) {
        NoEsparsa oAux = oEsparsa;
        while(!(oAux.nLinha == nI)){
            if(oAux.oProxLinha==null){
                oAux.oProxLinha = new NoEsparsa(oAux,null);
            }
            oAux = oAux.oProxLinha;
        }
        while(!(oAux.nColuna == nJ)){
            if(oAux.oProxColuna==null){
                oAux.oProxColuna = new NoEsparsa(null,oAux);
            }
            oAux = oAux.oProxColuna;
        }
        oAux.nValor = nK;
    }
    @Override
    public int NumeroLinhas() {
        NoEsparsa oAux = oEsparsa;
        while(oAux.oProxLinha!=null){
            oAux = oAux.oProxLinha;
        }
        return(oAux.nLinha+1);
    }
    @Override
    public int NumeroColunas() {
        NoEsparsa oAux = oEsparsa;
        while(oAux.oProxColuna!=null){
            oAux = oAux.oProxColuna;
        }
        return(oAux.nColuna+1);
    }
    @Override
    public Matriz ClassificarPorLinha(int tnIndex) {
        return(this);
    }
    @Override
    public Matriz ClassificarPorColuna(int tnIndex) {
        return(this);
    }
    @Override
    public Matriz Somar(Matriz toSoma) {
        double nResult = 0;
        MatrizSparsa oRetorno = new MatrizSparsa();
        
        for (int i = 0; i < NumeroLinhas(); i++) {
            for (int j = 0; j < NumeroColunas(); j++) {
                nResult = this.Posicao(i, j) + toSoma.Posicao(i, j);
                oRetorno.Posicao(i, j, nResult);
            }
        }

        return(oRetorno);
    }
    @Override
    public Matriz SomarEscalar(double tnEscalar) {
        double nResult = 0;
        MatrizSparsa oRetorno = new MatrizSparsa();
        
        for (int i = 0; i < NumeroLinhas(); i++) {
            for (int j = 0; j < NumeroColunas(); j++) {
                nResult = this.Posicao(i, j) + tnEscalar;
                oRetorno.Posicao(i, j, nResult);
            }
        }

        return(oRetorno);
    }
  
    
        @Override
    public Matriz Multiplicar(Matriz toMult) {
        
        double nResult ;
        MatrizSparsa oRetorno = new MatrizSparsa();
        
    
        for(int lnXI=0;lnXI < NumeroLinhas(); lnXI++)
        {
           for(int lnXII=0;lnXII < toMult.NumeroColunas() ;lnXII++)
           {
              nResult = 0;
              for(int lnXIII=0;lnXIII < NumeroLinhas(); lnXIII++)
              {
                nResult += ( Posicao(lnXI,lnXIII) * toMult.Posicao(lnXIII,lnXII) );
                oRetorno.Posicao(lnXI,lnXII, nResult);

              }
           }
        }

        return(oRetorno);
    }

    
    
    @Override
    public Matriz MultiplicarEscalar(double tnEscalar) {
        double nResult = 0;
        MatrizSparsa oRetorno = new MatrizSparsa();
        
        for (int i = 0; i < NumeroLinhas(); i++) {
            for (int j = 0; j < NumeroColunas(); j++) {
                nResult = this.Posicao(i, j) * tnEscalar;
                oRetorno.Posicao(i, j, nResult);
            }
        }

        return(oRetorno);
    }
    
    @Override
    public Matriz Transpor(){
        double nAux[][] = new double[NumeroColunas()][NumeroLinhas()];
        MatrizSparsa oRetorno = new MatrizSparsa();
        int i,j;
        
        for (i = 0; i < nAux.length; i++) {
            for (j = 0; j < nAux[i].length; j++) {
                nAux[i][j] =  Posicao(j,i);
            }
        }
        for (i = 0; i < nAux.length; i++) {
            for (j = 0; j < nAux[0].length; j++) {
                oRetorno.Posicao( i, j, nAux[i][j]);
            }
        }
        
        return(oRetorno);
    }
}