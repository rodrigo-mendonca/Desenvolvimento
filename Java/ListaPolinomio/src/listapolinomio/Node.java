/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package listapolinomio;

/**
 *
 * @author rmendonca
 */

public class Node {
    public double oCoef = 0;
    public double oExp  = 0;
    public Node oProx   = null;

    public double[] getDado() {
        return new double[]{oCoef,oExp};
    }
    
    public void setDado(double toCoef,double toExp) {
        oCoef = toCoef;
        oExp = toExp;
    }
    
    public void setDado(double[] toValores) {
        oCoef = toValores[0];
        oExp = toValores[1];
    }
    public Node getProx() {
        return oProx;
    }
    public void setProx(Node toProx) {
        oProx = toProx;
    }
}

