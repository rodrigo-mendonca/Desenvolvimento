package Externo;

import java.io.FileNotFoundException;
import java.io.IOException;
import operacoesmatriz.Matriz;
import operacoesmatriz.MatrizComum;
import operacoesmatriz.MatrizSparsa;

public class TesteV1 {
    
    static boolean existeSparsa = true;
    
    static Matriz m1;
    static Matriz m2;
    static Matriz m3;
    static Matriz m4;
    static Matriz felix;

    public static void inicializaMatrizesDeArquivo() throws FileNotFoundException, IOException{
        m1=new MatrizComum();
        m3=new MatrizComum();
        if(existeSparsa){
            m2 = new MatrizSparsa();
            m4 = new MatrizSparsa();
            felix = new MatrizSparsa();
        } else {
            m2 = new MatrizComum();
            m4 = new MatrizComum();
            felix = new MatrizComum();
        }
        
        m1.LerDoArquivo("m1.mtx");
        m2.LerDoArquivo("m2.mtx");
        m3.LerDoArquivo("m3.mtx");
        m4.LerDoArquivo("m4.mtx");
        felix.LerDoArquivo("felix.mtx");
    }

    public static void main (String [] args) throws FileNotFoundException, IOException{
        inicializaMatrizesDeArquivo();
        
        if (testeSoma() == false)
            System.out.println("Falhou no testeSoma");
        if (testeSomaEscalar() == false)
            System.out.println("Falhou no testeSomaEscalar");
        if (testeSomaAssociativa() == false)
            System.out.println("Falhou no testeSomaAssociativa");
        if (testeMultiplicarEscalar() == false)
            System.out.println("Falhou no testeMultiplicarEscalar");
        if (testeTranspor() == false)
            System.out.println("Falhou no testeTraspor");
        if (testeClassificarLinha() == false)
            System.out.println("Falhou no testeClassificarLinha");
        if (testeMultiplicar() == false)
            System.out.println("Falhou no testeMultiplicar");
    }
    
    static double obterSoma(Matriz m){
        double k = 0;
        for (int i = 0; i < m.NumeroLinhas(); i++) {
            for (int j = 0; j < m.NumeroColunas(); j++) {
                k+=m.Posicao(i, j);
            }
        }
        return k;
    }
    
    static boolean testeSoma(){
        Matriz soma1 = m1.Somar(m2);
        if( m1.Posicao(2, 3) != 4) //Não deve alterar a matriz m1
            return false;
        if( soma1.Posicao(1, 2) != 9)
            return false;
        if( soma1.Posicao(2, 1) != 15)
            return false;
        if( obterSoma(soma1)!=obterSoma(m1)+obterSoma(m2)) //a soma de todos os elementos deve ser igual
            return false;
        return true;
    }
    
    static boolean testeSomaEscalar(){
        Matriz copiaM3 = m3.SomarEscalar(0);
        if(comparaDuasMatrizes(copiaM3, m3) == false)
            return false;
        double escalar = 5.7;
        Matriz somaEscalar1 = m3.SomarEscalar(escalar);
        if( m3.Posicao(2, 3) != 7) //Não deve alterar a matriz m1
            return false;
        if( somaEscalar1.Posicao(1, 2) != 2+escalar)
            return false;
        if( somaEscalar1.Posicao(2, 1) != 2+escalar)
            return false;
        //se somar um escalar a uma matriz, eu somo mxn vezes o escalar à matriz
        
        if( obterSoma(somaEscalar1) - (obterSoma(m3)+escalar*m3.NumeroColunas()*m3.NumeroLinhas()) > 0.0000001 )
            return false;
        return true;
    }
    static boolean testeSomaAssociativa(){
        Matriz soma1 = m1.Somar(m2); //soma1 = m1 + m2
        Matriz soma2 = soma1.Somar(m1); //soma2 = m1 + m2 + m1
        if( obterSoma(soma2)!=obterSoma(m1)+obterSoma(m2)+obterSoma(m1)) //a soma de todos os elementos deve ser igual
            return false;
        Matriz soma3 = m2.Somar(m1).Somar(m1); //soma3 = m2+m1+m1
        if( obterSoma(soma2)!=obterSoma(soma3)) //a soma de todos os elementos deve ser igual
            return false;
        
        return comparaDuasMatrizes(soma3, soma2);
    }
    
    static boolean testeMultiplicarEscalar(){
        Matriz multEscalar1 = m3.MultiplicarEscalar(0);
        if(obterSoma(multEscalar1) != 0)
            return false;
        Matriz multEscalar2 = m3.MultiplicarEscalar(4);
        Matriz soma1 = m3.Somar(m3);
        Matriz soma2 = soma1.Somar(soma1);
        return comparaDuasMatrizes(soma2, multEscalar2);
    }
    
    static boolean testeTranspor(){
        Matriz m4t = m4.Transpor();
        Matriz m4rec = m4t.Transpor();
        //matriz trasposta duas vezes é a mesma matriz
        if(comparaDuasMatrizes(m4rec, m4) == false)
            return false;
        if(m4t.Posicao(6, 4) != m4.Posicao(4, 6))
            return false;
        //a soma dos elementos de uma matriz e da trasposta é igual
        Matriz m2t = m2.Transpor();
        if(obterSoma(m2t)!=obterSoma(m2))
            return false;
        //(m1 trasposta + m2 trasposta) trasposta = m1 + m2
        Matriz m1t = m1.Transpor();
        if(comparaDuasMatrizes( m1t.Somar(m2t).Transpor(), m1.Somar(m2)) ==false )
            return false;
        
        Matriz felixt = felix.Transpor();
        if(felixt.Posicao(6, 4)!=1)
            return false;
        if(felix.Posicao(6, 4)!=0)
            return false;
        //felixt.toBitmap("c:/felixt.png");
        return true;
    }
    
    static boolean testeClassificarLinha(){
        Matriz m4c3 = m4.ClassificarPorLinha(3);
        //Os valores das matrizes permanecem
        if(obterSoma(m4) != obterSoma(m4c3))
            return false;
        // a linha 3 tem que estar ordenada em ordem crescente
        for(int i=1;i<m4.NumeroColunas();i++){
            if(m4c3.Posicao(3, i) < m4c3.Posicao(3, i-1))
                return false;
        }
        // a coluna 3 da matriz m4 tem que ser igual à coluna 8 da m4c3 pois o 7 é o maior elemento
        for(int i=1;i<m4.NumeroLinhas();i++){
            if(m4c3.Posicao(i, 8) != m4.Posicao(i, 3))
                return false;
        }
        
        return true;
    }
    
    static boolean testeMultiplicar(){
        Matriz id9 = criaMatrizIdentidadeQuadrada(m4.NumeroLinhas());
        //Multiplicar pela identidade
        if(comparaDuasMatrizes(m4, m4.Multiplicar(id9))==false)
            return false;
        if(comparaDuasMatrizes(m4, id9.Multiplicar(m4))==false)
            return false;

        // (m2 x m3) x m4 = m2 x (m3 x m4)
        Matriz p23_4 = m2.Multiplicar(m3).Multiplicar(m4);
        Matriz p2_34 = m2.Multiplicar(m3.Multiplicar(m4));
        if(comparaDuasMatrizes(p23_4, p2_34)==false)
            return false;
        
        Matriz p23 = m2.Multiplicar(m3);
        //verificando alguns resultados
        if(p23.Posicao(0, 0) != 36)
            return false;
        if(p23.Posicao(1, 1) != 15)
            return false;
        if(p23.Posicao(2, 2) != 13)
            return false;
        if(p23.Posicao(1, 6) != 37)
            return false;
        
        return true;
    }
    
    static Matriz criaMatrizIdentidadeQuadrada(int size){
        
        Matriz m  = new MatrizComum();
        m.CriarMatriz(size, size);
        for(int i=0;i<size;i++)
            for(int j=0;j<size;j++)
                if(i==j)
                    m.Posicao(i, j, 1);
                else
                    m.Posicao(i, j, 0);
        return m;
    }
    
    static boolean comparaDuasMatrizes(Matriz a, Matriz b){
        if(a.NumeroColunas()!= b.NumeroColunas())
            return false;
        if(a.NumeroLinhas()!= b.NumeroLinhas())
            return false;
        
        for (int i = 0; i < a.NumeroLinhas(); i++) {
            for (int j = 0; j < a.NumeroColunas(); j++) {
                if(a.Posicao(i, j)!=b.Posicao(i, j))
                    return false;
            }
        }
        return true;
    }
    
    
}
