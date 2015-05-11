/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package listaligada;

/**
 *
 * @author rmendonca
 */

public class Node {
    public Object oValor = null;
    public Node oProx   = null;

    public Object getDado() {
        return oValor;
    }
    public void setDado(Object toValor) {
        oValor = toValor;
    }
    public Node getProx() {
        return oProx;
    }
    public void setProx(Node toProx) {
        oProx = toProx;
    }
}

