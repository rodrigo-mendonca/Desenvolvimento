using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PGMProcessing
{
    public class PgmImage
    {
        public byte[][] oMatriz;
        public int[] CountColors = new int[256];
        int[] AccumColors = new int[256];
        public int nWidth, nHeight;

        public PgmImage(byte[][] tMatriz,int tWidth,int tHeight)
        { 
            oMatriz = tMatriz;
            nWidth = tWidth;
            nHeight = tHeight;

            int nCor = 0;
            for (int i = 0; i < nHeight; i++)
            {
                for (int j = 0; j < nWidth; j++)
                {
                    nCor = oMatriz[i][j];
                    CountColors[nCor]++;
                }
            }
        }

        public int[] GetAccumColors()
        {
            AccumColors = (int[])CountColors.Clone();
            for (int i = 1; i < 256; i++)
            {
                AccumColors[i] = AccumColors[i] + AccumColors[i - 1];
            }
            return (AccumColors);
        }

    }
}
