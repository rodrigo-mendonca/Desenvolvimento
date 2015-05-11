/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package operacoesmatriz;

/**
 *
 * @author rmendonca
 */
public class NoEsparsa {
    double nValor         = 0;
    int nLinha            = 0;
    int nColuna           = 0;
    NoEsparsa oProxLinha  = null;
    NoEsparsa oProxColuna = null;
    
    public NoEsparsa(NoEsparsa toLinha,NoEsparsa toColuna){
        if (toLinha!=null) {
            nLinha  = toLinha.nLinha+1;
            nColuna = toLinha.nColuna;
        }
        if(toColuna!=null){
            nColuna = toColuna.nColuna+1;
            nLinha  = toColuna.nLinha;
        }
    }
}
