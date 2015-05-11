/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package fila.pilha;

/**
 *
 * @author rmendonca
 */
public class Item {
    Object oDado = null;
    Item oAnt = null;
            
    public Item()
    {
    
    }
    public Item(Item toAnt)
    {
        oAnt = toAnt;
    }
    public Item(Object toDado)
    {
        oDado = toDado;
    }
    public Object GetValue()
    {
        return(oDado);
    }
    public void SetValue(Object toDado)
    {
        oDado = toDado;
    }
}