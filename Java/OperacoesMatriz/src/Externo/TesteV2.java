package Externo;

import java.io.FileNotFoundException;
import java.io.IOException;
import operacoesmatriz.Matriz;
import operacoesmatriz.MatrizComum;
import operacoesmatriz.MatrizSparsa;

public class TesteV2 {
   
    static boolean existeSparsa = false;
    
    static Matriz m5;
    static Matriz m6;
    static Matriz m7;
    static Matriz m8;
    
    public static void inicializaMatrizesDeArquivo() throws FileNotFoundException, IOException{
        m5=new MatrizComum();
        m7=new MatrizComum();
        if(existeSparsa){
            m6 = new MatrizSparsa();
            m8 = new MatrizSparsa();
        } else {
            m6 = new MatrizComum();
            m8 = new MatrizComum();
        }
        
        m5.LerDoArquivo("m5.mtx");
        m6.LerDoArquivo("m6.mtx");
        m7.LerDoArquivo("m7.mtx");
        m8.LerDoArquivo("m8.mtx");

    }
    
    public static void main (String [] args) throws FileNotFoundException, IOException{
        inicializaMatrizesDeArquivo();
        if (testeDeterminante() == false)
            System.out.println("Falhou no testeDeterminante");
        if (testeDeterminanteZero() == false)
            System.out.println("Falhou no testeDeterminanteZero");
        if (testeDeterminanteProdutoMatrizes() == false)
            System.out.println("Falhou no testeDeterminanteProdutoMatrizes");
        if (testePropDeteterminanteTransposta() == false)
            System.out.println("Falhou no testePropDeteterminanteTransposta");
        
    }
    
    static boolean testeDeterminante(){
        double det = m5.CalcularDeterminante();
        if(det!=5)
            return false;
        det = m7.CalcularDeterminante();
        if(det!=-1)
            return false;
        return true;
    }

    static boolean testeDeterminanteZero(){
        //O determinante de uma matriz com uma linha ou coluna com todos os elementos zero é zero.
        double det = m6.CalcularDeterminante();
        if(det!=0)
            return false;
        det = m8.CalcularDeterminante();
        if(det!=0)
            return false;
        return true;
    }

    static boolean testePropDeteterminanteTransposta(){
        //O determinante de uma matriz é igual ao determinante da transposta
        double det = m5.CalcularDeterminante();
        double detTrasposta = m5.Transpor().CalcularDeterminante();
        if(det!=detTrasposta)
            return false;
        det = m7.CalcularDeterminante();
        detTrasposta = m7.Transpor().CalcularDeterminante();
        if(det!=detTrasposta)
            return false;
        return true;
    }
    
    static boolean testeDeterminanteProdutoMatrizes(){
        double detM6 = m6.CalcularDeterminante();
        double detM7 = m7.CalcularDeterminante();
        
        if(m6.Multiplicar(m7).CalcularDeterminante() != detM6*detM7)
            return false;
        return true;
    }
}
