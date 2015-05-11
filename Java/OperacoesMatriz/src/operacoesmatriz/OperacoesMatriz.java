/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package operacoesmatriz;

import java.io.FileNotFoundException;
import java.io.IOException;

/**
 *
 * @author rmendonca
 */
public class OperacoesMatriz {

    /**
     * @param arp[;gs the command line arguments
     */
    public static void main(String[] args) throws FileNotFoundException, IOException {
        MatrizSparsa oMC = new MatrizSparsa();
        MatrizSparsa oMC2 = new MatrizSparsa();
        //double nResult = oMC.CalcularDeterminante();
        //oMC.LerDoArquivo("m1.mtx");
        
        oMC.Posicao(0, 0, 3);
        oMC.Posicao(0, 1, 5);
        oMC.Posicao(0, 2, 7);
        oMC.Posicao(1, 0, 6);
        oMC.Posicao(1, 1, 2);
        oMC.Posicao(1, 2, 9);
        oMC.Posicao(2, 0, 8);
        oMC.Posicao(2, 1, 2);
        oMC.Posicao(2, 2, 1);
        
        
        oMC2.Posicao(0, 0, 0);
        oMC2.Posicao(0, 1, 2);
        oMC2.Posicao(0, 2, 4);
        oMC2.Posicao(1, 0, 6);
        oMC2.Posicao(1, 1, 2);
        oMC2.Posicao(1, 2, 6);
        oMC2.Posicao(2, 0, 1);
        oMC2.Posicao(2, 1, 2);
        oMC2.Posicao(2, 2, 4);
        
        
        oMC =  (MatrizSparsa) oMC.Multiplicar(oMC2);
        
        System.out.println( oMC.toString() );
        
        
        
        
        
    }
}
