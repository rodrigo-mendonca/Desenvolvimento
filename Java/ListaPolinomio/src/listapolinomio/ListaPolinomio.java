/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package listapolinomio;

/**
 *
 * @author rmendonca
 */
public class ListaPolinomio {

    /**
     * @param args the command line arguments
     */
    public static void main(String[] args) {
      List oLista = new List().Add(new double[]{2,8},new double[]{5, 3});
      oLista = oLista.Add(new double[]{9,1},new double[]{4, 2});
      
      System.out.println("Conteudo da Lista:");
      System.out.println(oLista.toString());
    }
}
