/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package operacoesmatriz;

/**
 *
 * @author rmendonca
 */
public class MatrizComum extends Matriz{
    double[][] nMatriz = null;
    
    public MatrizComum(){
    }
    public MatrizComum(int tnLinhas,int tnColunas)
    {
        nMatriz = new double[tnLinhas][tnColunas];
    }
    @Override
    public void CriarMatriz(int tnLinhas,int tnColunas){
        nMatriz = new double[tnLinhas][tnColunas];
    }
    @Override
    public double Posicao(int nI, int nJ) {
        return(nMatriz[nI][nJ]);
    }
    @Override
    public void Posicao(int nI, int nJ,double nK) {
        nMatriz[nI][nJ] = nK;
    }
    @Override
    public int NumeroLinhas() {
        return(nMatriz.length);
    }

    @Override
    public int NumeroColunas() {
        return(nMatriz[0].length);
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
        MatrizComum oRetorno = new MatrizComum(NumeroLinhas(),NumeroColunas());
        
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
        MatrizComum oRetorno = new MatrizComum(NumeroLinhas(),NumeroColunas());
        
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
        MatrizComum oRetorno = new MatrizComum( NumeroLinhas(), toMult.NumeroColunas() );
        
    
        for(int lnXI=0;lnXI < NumeroLinhas(); lnXI++)
        {
           for(int lnXII=0;lnXII< toMult.NumeroColunas() ;lnXII++)
           {
              nResult = 0;
              for(int lnXIII=0;lnXIII < NumeroLinhas(); lnXIII++)
              {
                nResult += ( Posicao(lnXI,lnXIII) * toMult.Posicao(lnXIII,lnXII) );
                oRetorno.Posicao(lnXI, lnXII, nResult);

              }
           }
        }

        return(oRetorno);
    }
    
    

    
    
    @Override
    public Matriz MultiplicarEscalar(double tnEscalar) {
        double nResult = 0;
        MatrizComum oRetorno = new MatrizComum(NumeroLinhas(),NumeroColunas());
        
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
        MatrizComum oRetorno = new MatrizComum(NumeroColunas(),NumeroLinhas());
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