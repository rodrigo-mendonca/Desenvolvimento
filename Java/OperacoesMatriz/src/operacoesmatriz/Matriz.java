/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package operacoesmatriz;

import java.awt.Color;
import java.awt.image.BufferedImage;
import java.io.*;
import javax.imageio.ImageIO;
import javax.swing.JFileChooser;
import javax.swing.filechooser.FileNameExtensionFilter;

/**
 *
 * @author rmendonca
 */
public abstract class Matriz {
    public abstract void CriarMatriz(int tnLinhas,int tnColunas);
    public abstract double Posicao(int nI,int nJ);
    public abstract void Posicao(int nI,int nJ,double nK);
    public abstract int NumeroLinhas();
    public abstract int NumeroColunas();
    public abstract Matriz ClassificarPorLinha(int tnIndex);
    public abstract Matriz ClassificarPorColuna(int tnIndex);
    public abstract Matriz Somar(Matriz toSoma);
    public abstract Matriz SomarEscalar(double tnEscalar);
    public abstract Matriz Multiplicar(Matriz toMult);
    public abstract Matriz MultiplicarEscalar(double tnEscalar);
    public abstract Matriz Transpor();
        
    public double CalcularDeterminante() {
        double nDeterm = 0,nMult = 1;
        int nAux=0;
        int nQ = 0;
        if(NumeroColunas()==2){
            nQ = 1;
        }
        // faz a soma
        for (int i = 0; i < NumeroColunas()-nQ; i++) {
            nAux = i;
            for (int j = 0; j < NumeroLinhas(); j++) {
                nMult*=this.Posicao(j,nAux);
                if (nAux==NumeroColunas()-1) {
                    nAux=-1;
                }
                nAux++;
            }
            nDeterm+=nMult;
            nMult=1;
        }
        //Faz a subtracao
        for (int i = NumeroColunas()-1; i >= nQ ; i--) {
            nAux = i;
            for (int j = 0; j < NumeroLinhas(); j++) {
                nMult*=this.Posicao(j,nAux);
                if (nAux==0) {
                    nAux=NumeroColunas();
                }
                nAux--;
            }
            nDeterm-=nMult;
            nMult=1;
        }
        return(nDeterm);
    }
    
    public void SalvarEmArquivo(){
        
    }
    public void LerDoArquivo(String tcCaminho) throws FileNotFoundException, IOException{
        JFileChooser oFileChooser = new JFileChooser();
        FileNameExtensionFilter oFilter = new FileNameExtensionFilter(
        ".mtx files", "mtx");
        
        oFileChooser.setFileFilter(oFilter);
        File oArquivo = null;
        
        if (tcCaminho.isEmpty()) {
            if(oFileChooser.showOpenDialog(null) != JFileChooser.APPROVE_OPTION)
                return;
            
            oArquivo      = oFileChooser.getSelectedFile();
        }
        else{
            oArquivo      = new File(tcCaminho);
        }
        
        FileReader oRead = new FileReader(oArquivo);
        BufferedReader oBuffer = new BufferedReader(oRead);
        
        //lista a primeira linha
        String cLinha = oBuffer.readLine()+";";
        int nLinha=0,nColuna=0;
        
        nLinha = Integer.parseInt(cLinha.substring(0,cLinha.indexOf(";")));
        nColuna= Integer.parseInt(cLinha.substring(cLinha.indexOf(";")+1,cLinha.length()-1));
        this.CriarMatriz(nLinha, nColuna);

        nLinha=0;
        nColuna=0;
        String cParte = "",cNum ="";
        //lista as linhas seguintes
        while(oBuffer.ready()){
            cLinha=oBuffer.readLine() + ";";
            for (int i = 0; i < cLinha.length(); i++) {
                cParte=cLinha.substring(i,i+1);
                if(cParte.equals(";")){
                    this.Posicao(nLinha, nColuna, Double.parseDouble(cNum));
                    nColuna++;
                    cNum = "";
                }
                else{
                    cNum+=cParte;
                }
            }
            nLinha++;
            nColuna=0;
        }
    }
    
    @Override
    public String toString(){
        StringBuilder cRes = new StringBuilder();
        for(int i=0;i<NumeroLinhas();i++){
            for (int j = 0; j < NumeroColunas(); j++) {
                cRes.append(Posicao(i, j)).append(";");
            }
            cRes.append('\n');
        }
        return cRes.toString();
    }
    public void toBitmap(String caminhoPNG){
        int nHeight = NumeroLinhas();
        int nWidth  = NumeroColunas();
        BufferedImage oBit = new BufferedImage(nHeight, nWidth, BufferedImage.TYPE_INT_ARGB);
        for (int i=0;i<nHeight;i++){
            for (int j=0;j<nWidth;j++){
                if(Posicao(i,j)==0)
                    oBit.setRGB(j, i, Color.WHITE.getRGB());
                else if(Posicao(i,j)==1)
                    oBit.setRGB(j, i, Color.BLACK.getRGB());
                else
                    oBit.setRGB(j, i, (int)Posicao(i, j));
            }
        }
        File oFile = new File(caminhoPNG);
        try {
            ImageIO.write(oBit, "png", oFile);
        } catch (IOException e) {
            e.printStackTrace();
        }
    }


}
