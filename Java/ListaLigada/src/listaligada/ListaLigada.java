/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package listaligada;

import java.sql.Time;
import java.util.ArrayList;

/**
 *
 * @author rmendonca
 */
public class ListaLigada {
    
    public static void main(String[] args) {

      List oLista = new List().Add("a","B",4,"22/01/1989",5,Time.valueOf("18:10:02"));		

      System.out.println("Conteudo da Lista:");
      System.out.println(oLista.toString());

      System.out.println("Lista Invertida:");   
      System.out.println(Invert(oLista).toString());

      System.out.println("Ponto Medio:");
      System.out.println(Pmed(oLista));

      System.out.println("Matriz em lista:");
      oLista = Matriz2Lista(new Object[][]{ {"a",1},{"b",2},{"c",3}  });

      for (int nI = 0; nI < oLista.Size(); nI++) {
         List oAux = (List) oLista.Get(nI);
         
         System.out.println(oAux.Get(0) + "|" +oAux.Get(1));
      }
    }

     /*
     * 
     * Inversor de lista
     */
    public static List Invert(List toLista)
    {
        ArrayList<Object> oNodes = new ArrayList<Object>();

        for (int i = 0; i < toLista.Size(); i++) {
            oNodes.add(toLista.Get(i));
        }

        toLista.Clear();
        int nLen = oNodes.toArray().length;

        for (int nXi = nLen; nXi > 0; nXi--)
        {
            toLista.Add(oNodes.get(nXi-1));
        }

        return toLista;
    }
     /*
     * 
     * Ponto Medio
     */
     public static Object Pmed(List toLista)
     {
         Object oRetorno;

         int nMid = (int) toLista.Size()/2;
         oRetorno = toLista.Get(nMid);               

         return oRetorno;
    }
     /*
     * 
     * Lista Bidimensional
     */
     public static List Matriz2Lista(Object[][] toMatriz)
     {
         List oRetorno = new List();

         for (int nI = 0; nI < toMatriz.length; nI++) {
             List oAux = new List();

             for (int nJ = 0; nJ < toMatriz[nI].length ; nJ++) {  
               oAux.Add(toMatriz[nI][nJ]);
            }
            oRetorno.Add(oAux);  
         }
         return oRetorno;
     }
}