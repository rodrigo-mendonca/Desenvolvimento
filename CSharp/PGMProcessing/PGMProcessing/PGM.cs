using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.IO;

namespace PGMProcessing
{
    public struct FSNum
    {
        public const double Right = 7F / 16F;
        public const double DownLeft = 3F / 16F;
        public const double Down = 5F / 16F;
        public const double DownRight = 1F / 16F;
    }

    public class PGM
    {
        int nVersion = -1;
        List<PgmImage> lVersions = new List<PgmImage>();
        public string cFielSource;

        public int[] GetColorList()
        {
            return lVersions[nVersion].CountColors;
        }

        public int[] GetColorAccumList()
        {
            return lVersions[nVersion].GetAccumColors();
        }

        public int GetWidth()
        {
            return lVersions[nVersion].nWidth;
        }

        public int GetHeight()
        {
            return lVersions[nVersion].nHeight;
        }

        public bool IsEmpty()
        {
            return lVersions.Count == 0;
        }

        public bool IsFirstVerison()
        {
            return nVersion == 0;
        }

        public bool IsLastVerison()
        {
            return (lVersions.Count-1) == nVersion;
        }

        public Image GetPgmBitmap()
        {
            // cria um bimap para retorno do tamanho da imagem
            Bitmap oImg = new Bitmap(1, 1);

            // se existir alguma versão
            if (lVersions.Count > 0)
            {
                PgmImage oPgm = lVersions[nVersion];
                // cria um bimap para retorno do tamanho da imagem
                oImg = new Bitmap(oPgm.nWidth, oPgm.nHeight);

                // pega a versao atual
                byte[][] oAtu = oPgm.oMatriz;
                for (int i = 0; i < oPgm.nHeight; i++)
                {
                    for (int j = 0; j < oPgm.nWidth; j++)
                    {
                        int nNum = oAtu[i][j];
                        // cria um pixel de acordo com a matriz
                        oImg.SetPixel(j, i, Color.FromArgb(nNum, nNum, nNum));
                    }
                }
            }
            return (oImg);
        }

        public int NumberOption(ref BinaryReader tRead,int Lim)
        {
            char cDig;
            string cOp = "";
            int nByte = tRead.ReadByte();
            while (nByte != Lim)
            {
                cDig = (char)nByte;

                if (!char.IsDigit(cDig))
                    return 0;

                cOp += cDig;
                nByte = tRead.ReadByte();
            }

            return Convert.ToInt16(cOp);
        }

        public void OpenFile()
        {
            OpenFileDialog oOpen = new OpenFileDialog();
            oOpen.Filter = "Pgm files (*.pgm)|*.pgm";
            oOpen.ShowDialog();

            if (oOpen.FileName != "")
            {
                // guarda o caminho para depois salvar
                cFielSource = oOpen.FileName;

                lVersions.Clear();
                nVersion = -1;
                OpenFile(oOpen.FileName);
            }
        }
        public void OpenFile(string FileName)
        {
            if (FileName != "")
            {
                using (FileStream oFileStream = new FileStream(FileName, FileMode.Open))
                {
                    BinaryReader oReader = new BinaryReader(oFileStream);

                    // o arquivo começa com P5 e um enter
                    if (oReader.ReadChar() == 'P' && oReader.ReadChar() == '5' && oReader.ReadByte() == 10)
                    {
                        // faz a leitura da largura da imagem
                        int nWidth = NumberOption(ref oReader, 32);
                        // faz a leitua da altura
                        int nHeight = NumberOption(ref oReader, 10);

                        // caso não tiver altura ou largura definido exibe um erro
                        if (nWidth == 0 || nHeight == 0)
                        {
                            MessageBox.Show("Arquivo com o formato inválido");
                            return;
                        }

                        int Gray = NumberOption(ref oReader, 10);

                        byte[][] oScreen = NewMatriz(nWidth, nHeight);

                        int k = 0;
                        // preenche a matriz com os dados
                        for (int j = 0; j < nHeight; j++)
                        {
                            for (int i = 0; i < nWidth; i++)
                            {
                                oScreen[j][i] = oReader.ReadByte();
                                k++;
                            }
                        }
                        // adiciona uma nova versão
                        AddVersion(oScreen, nWidth, nHeight);
                    }
                    else
                    {
                        MessageBox.Show("Arquivo com o formato inválido");
                        return;
                    }
                }
            }
        }

       public void SaveFile()
        {
            SaveAsFile(cFielSource);
        }

        public void SaveAsFile(string tFielSource)
        {
            string cFile = tFielSource;

            if (cFile == "")
            {
                SaveFileDialog oSave = new SaveFileDialog();
                oSave.Filter = "Pgm|*.pgm";
                oSave.ShowDialog();

                cFile = oSave.FileName;
            }

            if (cFile == "")
                return;

            // pega a versão atual
            PgmImage oPgm = lVersions[nVersion];
            byte[][] oAtu = oPgm.oMatriz;

            using (FileStream oFileStream = new FileStream(cFile, FileMode.OpenOrCreate))
            {
                oFileStream.Flush();
                BinaryWriter oWriter = new BinaryWriter(oFileStream);
                oWriter.Write("P5\n".ToCharArray());
                oWriter.Write((oPgm.nWidth.ToString() + " ").ToCharArray());
                oWriter.Write((oPgm.nHeight.ToString() + "\n255\n").ToCharArray());

                for (int j = 0; j < oPgm.nHeight; j++)
                {
                    oWriter.Write(oAtu[j]);
                }
                oWriter.Close();
            }
        }

        public void Undo()
        {
            if (nVersion > 0)
                nVersion--;
        }

        public void Redo()
        {
            if (nVersion < (lVersions.Count-1))
                nVersion++;
        }

        public void RotateLeft()
        {
            if (lVersions.Count != 0)
            {
                LMatriz();
            }
        }

        public void RotateRight()
        {
            if (lVersions.Count != 0)
            {
                RMatriz();
            }
        }

        public void ColorReduction(int tRedu)
        {
            PgmImage oPgm = lVersions[nVersion];
            byte[][] oAtu = (byte[][])oPgm.oMatriz.Clone();
            byte[][] oNewMatriz = NewMatriz(oPgm.nWidth, oPgm.nHeight);

            for (int i = 0; i < oPgm.nHeight; i++)
                for (int j = 0; j < oPgm.nWidth; j++)
                    oNewMatriz[i][j] = Reduction(oAtu[i][j], tRedu);

            AddVersion(oNewMatriz, oPgm.nWidth, oPgm.nHeight);
        }

        public void FloydSteinberg(int tRedu)
        {
            PgmImage oPgm = lVersions[nVersion];
            byte[][] oAtu = NewMatriz(oPgm.oMatriz,oPgm.nWidth, oPgm.nHeight); 
            byte[][] oNewMatriz = NewMatriz(oPgm.nWidth, oPgm.nHeight);

            double nErro = 0;

            for (int i = 0; i < oPgm.nHeight; i++)
            {
                for (int j = 0; j < oPgm.nWidth; j++)
                {
                    // faz a redução da cor
                    oNewMatriz[i][j] = Reduction(oAtu[i][j], tRedu);

                    // verifica o valor do erro
                    nErro = (double)oAtu[i][j] - (double)oNewMatriz[i][j];

                    if (j < (oPgm.nWidth - 1))
                        oAtu[i][j + 1] = ColorNormal(oAtu[i][j + 1] + nErro * FSNum.Right);

                    if (i < (oPgm.nHeight - 1))
                    {
                        oAtu[i + 1][j] = ColorNormal(oAtu[i + 1][j] + nErro * FSNum.Down);

                        if (j < (oPgm.nWidth - 1))
                            oAtu[i + 1][j + 1] = ColorNormal(oAtu[i + 1][j + 1] + nErro * FSNum.DownRight);

                        if (j > 0)
                            oAtu[i + 1][j - 1] = ColorNormal(oAtu[i + 1][j - 1] + nErro * FSNum.DownLeft);
                    }
                }
            }

            AddVersion(oNewMatriz, oPgm.nWidth, oPgm.nHeight);
        }

        public void FiltroMedia(int tSize)
        {
            PgmImage oPgm = lVersions[nVersion];
            byte[][] oAtu = lVersions[nVersion].oMatriz;
            byte[][] oNewMatriz = NewMatriz(oPgm.nWidth, oPgm.nHeight);

            for (int i = 0; i < oPgm.nHeight; i++)
                for (int j = 0; j < oPgm.nWidth; j++)
                    oNewMatriz[i][j] = (byte)Proximity(oAtu, i, j, tSize,"Media"); ;

            AddVersion(oNewMatriz, oPgm.nWidth, oPgm.nHeight);
        }

        public void FiltroMediana(int tSize)
        {
            PgmImage oPgm = lVersions[nVersion];
            byte[][] oAtu = lVersions[nVersion].oMatriz;
            byte[][] oNewMatriz = NewMatriz(oPgm.nWidth, oPgm.nHeight);

            for (int i = 0; i < oPgm.nHeight; i++)
                for (int j = 0; j < oPgm.nWidth; j++)
                    oNewMatriz[i][j] = (byte)Proximity(oAtu, i, j, tSize, "Mediana"); ;

            AddVersion(oNewMatriz, oPgm.nWidth, oPgm.nHeight);
        }

        public int Proximity(byte[][] tMatriz, int tY, int tX, int tSize, string tTipo)
        {
            Func<double, double, double, double>
                F = (nX, nY, nR) => (1F / (2F * Math.PI * Math.Pow(0, 2))) * Math.Pow(Math.E, ((-(Math.Pow(nX, 2) + Math.Pow(nY, 2))) / (2 * Math.Pow(0, 2))));

            return Proximity(tMatriz, tY, tX, tSize, tTipo,F);
        }

        public void EqualizeColors()
        {
            PgmImage oPgm = lVersions[nVersion];
            byte[][] oNewMatriz = NewMatriz(oPgm.oMatriz, oPgm.nWidth, oPgm.nHeight);
            int Min, Max,Cor;
            int[] Accum = oPgm.GetAccumColors();

            // busca a quantidade minima
            Min = Accum.Min();
            // a ultima posicao sempre tem o valor acumulado maximo
            Max = Accum[255];

            for (int i = 0; i < oPgm.nHeight; i++)
            {
                for (int j = 0; j < oPgm.nWidth; j++)
                {
                    Cor = oNewMatriz[i][j];
                    oNewMatriz[i][j] = (byte) Math.Round((((double)Accum[Cor] - Min) / (Max - Min)) * 255);
                }
            }

            AddVersion(oNewMatriz, oPgm.nWidth, oPgm.nHeight);
        }

        public void Gaussiano(int tSize, double tDesvio)
        {
            PgmImage oPgm = lVersions[nVersion];
            byte[][] oAtu = lVersions[nVersion].oMatriz;
            byte[][] oNewMatriz = NewMatriz(oPgm.nWidth, oPgm.nHeight);

            Func<double, double, double, double>
                F = (nX, nY, nR) => (1F / (2F * Math.PI * tDesvio * tDesvio)) * 
                                    Math.Exp(((-(nX * nX + nY * nY)) / (2F * tDesvio * tDesvio)));

            for (int i = 0; i < oPgm.nHeight; i++)
                for (int j = 0; j < oPgm.nWidth; j++)
                    oNewMatriz[i][j] = (byte)Proximity(oAtu, i, j, tSize, "Gaussiano",F); ;

            AddVersion(oNewMatriz, oPgm.nWidth, oPgm.nHeight);
        }

        public void Laplaciano(int tSize, double tDesvio)
        {
            PgmImage oPgm = lVersions[nVersion];
            byte[][] oAtu = lVersions[nVersion].oMatriz;
            byte[][] oNewMatriz = NewMatriz(oPgm.nWidth, oPgm.nHeight);

            Func<double, double, double, double>
                F = (nX, nY, nR) => -(1F / (Math.PI * Math.Pow(tDesvio, 4))) * 
                                     (1F - ((nX * nX + nY * nY) / (2F * tDesvio * tDesvio))) * 
                                     Math.Exp(((-(nX * nX + nY * nY)) / (2F * tDesvio * tDesvio)));

            for (int i = 0; i < oPgm.nHeight; i++)
                for (int j = 0; j < oPgm.nWidth; j++)
                    oNewMatriz[i][j] = Proximity(oAtu, i, j, tSize, "Laplaciano", F);

            AddVersion(oNewMatriz, oPgm.nWidth, oPgm.nHeight);
        }

        private byte Proximity(byte[][] tMatriz, int tY, int tX, int tSize, string tTipo,Func<double, double, double, double> F)
        {
            double Soma = 0;
            int nC = 0;
            int IniY = tY - (tSize - 1) / 2;
            int IniX = tX - (tSize - 1) / 2;
            int nW = tMatriz[0].Length, nH = tMatriz.Length;
            int[] List = new int[(tSize * tSize)];

            for (int i = IniY; i < (IniY + tSize); i++)
                for (int j = IniX; j < (IniX + tSize); j++)
                    if (i >= 0 && i < nH && j >= 0 && j < nW)
                    {
                        if (tTipo == "Media")
                            Soma += tMatriz[i][j];
                        if (tTipo == "Mediana")
                            List[nC] = tMatriz[i][j];
                        if (tTipo == "Gaussiano" || tTipo == "Laplaciano")
                        {
                            double nRet = (((double)tMatriz[i][j])) * F(Math.Abs(tY - i), Math.Abs(tX - j), tSize);
                            Soma += nRet;
                        }

                        nC++;
                    }

            double Rd = 0;

            if (tTipo == "Media")
                Rd = Soma / nC;

            if (tTipo == "Gaussiano")
                Rd = Soma;

            if (tTipo == "Laplaciano")
                Rd = ColorNormal(tMatriz[tY][tX] - Soma);

            if (tTipo == "Mediana")
            {
                List = Sort(List, nC);
                int Med1, Med2;

                // PARA IMPAR
                if (nC % 2 == 1)
                {
                    Med1 = (nC - 1) / 2;
                    Rd = List[Med1];
                }
                else // PARA PAR
                {
                    Med1 = List[nC / 2 - 1];
                    Med2 = List[nC / 2];
                    Rd = (Med1 + Med2) / 2;
                }
            }

            return (byte)Math.Round(Rd);
        }

        private byte Reduction(byte tAtu,int tRedu)
        {
            byte nK;
            double nP, nRedu = (double)(tRedu - 1);

            nP = Math.Round(tAtu * nRedu / 255);
            nK = (byte)Math.Round(255 * nP / nRedu);

            return Convert.ToByte(nK);
        }

        public void ErosaoMorfologica(int tSize)
        {
            PgmImage oPgm = lVersions[nVersion];
            byte[][] oAtu = lVersions[nVersion].oMatriz;
            byte[][] oNewMatriz = NewMatriz(oPgm.nWidth, oPgm.nHeight);

            string cFielSource = "Janela.txt";
            char[] Janela;

            using (FileStream oFileStream = new FileStream(cFielSource, FileMode.Open))
            {
                BinaryReader oReader = new BinaryReader(oFileStream);
                int count = (int)oReader.BaseStream.Length;
                Janela = oReader.ReadChars(count).Where(c => c != '\n' && c != '\r').ToArray();
            }

            Func<int, int, char,int> F = (A, V, C) => V - ((int)char.GetNumericValue(C)) < A ? V - ((int)char.GetNumericValue(C)) : A;

            for (int i = 0; i < oPgm.nHeight; i++)
                for (int j = 0; j < oPgm.nWidth; j++)
                    oNewMatriz[i][j] = FJanela(oAtu, j, i, tSize, Janela, F);

            AddVersion(oNewMatriz, oPgm.nWidth, oPgm.nHeight);
        }

        public void DilatacaoMorfologica(int tSize)
        {
            PgmImage oPgm = lVersions[nVersion];
            byte[][] oAtu = lVersions[nVersion].oMatriz;
            byte[][] oNewMatriz = NewMatriz(oPgm.nWidth, oPgm.nHeight);

            string cFielSource = "Janela.txt";
            char[] Janela;

            using (FileStream oFileStream = new FileStream(cFielSource, FileMode.Open))
            {
                BinaryReader oReader = new BinaryReader(oFileStream);
                int count = (int)oReader.BaseStream.Length;
                Janela = oReader.ReadChars(count).Where(c => c != '\n' && c != '\r').ToArray();
            }

            Func<int, int, char, int> F = (A, V, C) => V + ((int)char.GetNumericValue(C)) > A ? V + ((int)char.GetNumericValue(C)) : A;

            for (int i = 0; i < oPgm.nHeight; i++)
                for (int j = 0; j < oPgm.nWidth; j++)
                    oNewMatriz[i][j] = FJanela(oAtu, j, i, tSize, Janela, F);

            AddVersion(oNewMatriz, oPgm.nWidth, oPgm.nHeight);
        }

        private byte FJanela(byte[][] tMatriz, int tX, int tY, int tSize, char[] Janela, Func<int, int, char, int> F)
        { 
            int IniY = tY - (tSize - 1) / 2;
            int IniX = tX - (tSize - 1) / 2;
            int nW = tMatriz[0].Length, nH = tMatriz.Length;

            int nAnt = tMatriz[tY][tX];

            int ind = 0;
            for (int i = IniY; i < (IniY + tSize); i++)
                for (int j = IniX; j < (IniX + tSize); j++)
                {
                    if (i >= 0 && i < nH && j >= 0 && j < nW)
                    {
                        if (Janela[ind] != 'I')
                            nAnt = F(nAnt, tMatriz[i][j],Janela[ind]);
                    }
                    ind++;
                }

            byte Rd = (byte)ColorNormal(nAnt);

            return(Rd);
        }

        private byte ColorNormal(double tCor)
        {
            return (byte)(tCor > 255 ? 255 : (tCor < 0 ? 0 : tCor));
        }

        private void RMatriz()
        {
            PgmImage oPgm = lVersions[nVersion];
            byte[][] oMatriz = oPgm.oMatriz;
            byte[][] oNewMatriz = NewMatriz(oPgm.nHeight, oPgm.nWidth);

            for (int i = 0; i < oPgm.nHeight; i++)
                for (int j = 0; j < oPgm.nWidth; j++)
                    oNewMatriz[j][(oPgm.nHeight - 1) - i] = oMatriz[i][j];

            AddVersion(oNewMatriz, oPgm.nHeight, oPgm.nWidth);
        }

        private void LMatriz()
        {
            PgmImage oPgm = lVersions[nVersion];
            byte[][] oMatriz = oPgm.oMatriz;
            byte[][] oNewMatriz = NewMatriz(oPgm.nHeight, oPgm.nWidth);

            for (int i = 0; i < oPgm.nHeight; i++)
                for (int j = 0; j < oPgm.nWidth; j++)
                    oNewMatriz[(oPgm.nWidth - 1) - j][i] = oMatriz[i][j];

            AddVersion(oNewMatriz, oPgm.nHeight, oPgm.nWidth);
        }

        private byte[][] NewMatriz(int tWidth, int tHeight)
        {
            byte[][] Retorno;
            Retorno = new byte[tHeight][];

            for (int i = 0; i < tHeight; i++)
                Retorno[i] = new byte[tWidth];

            return Retorno;
        }

        private byte[][] NewMatriz(byte[][] tBytes, int tWidth, int tHeight)
        {
            byte[][] Retorno;
            Retorno = new byte[tHeight][];

            for (int i = 0; i < tHeight; i++)
                Retorno[i] = (byte[])tBytes[i].Clone();

            return Retorno;
        }

        public void AddVersion(byte[][] tMatriz, int tWidth, int tHeight)
        {
            nVersion++;
            if (nVersion < lVersions.Count)
                lVersions.RemoveRange(nVersion, lVersions.Count - nVersion);

            lVersions.Add(new PgmImage(tMatriz, tWidth, tHeight));
        }

        public int[] Sort(int[] tList,int tTam)
        {
            int Aux;
            for (int i = 0; i < tTam-1; i++)
            {
                for (int j = i+1; j< tTam; j++)
                {
                    if(tList[i] > tList[j])
                    {
                        Aux = tList[j];
                        tList[j] = tList[i];
                        tList[i] = Aux;
                    }
                }
            }

            return tList;
        }

        public void Fourier(char Tipo)
        {
            string Temp = System.IO.Path.GetTempPath() + Guid.NewGuid().ToString() + ".pgm";
            string Save = System.IO.Path.GetTempPath() + Guid.NewGuid().ToString() + ".pgm";

            SaveAsFile(Save);

            string program = @"C:\Users\rmendonca\Documents\GitHub\Desenvolvimento\C\Fourier\bin\Debug\Fourier.exe";

            ProcessStartInfo _info =
            new ProcessStartInfo(program, Save + " " + Temp + " " + Tipo);

            _info.RedirectStandardOutput = true;
            _info.UseShellExecute = false;
            _info.CreateNoWindow = true;

            Process _p = new Process();
            _p.StartInfo = _info;
            _p.Start();

            string _processResults = _p.StandardOutput.ReadToEnd();
            // abre o arquivo salvo adiconando como uma nova versão
            OpenFile(_processResults);
        }
    }
}
