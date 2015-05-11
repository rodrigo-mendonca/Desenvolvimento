﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace SharpBook.Classes
{
    public class Perfil
    {
        public Image iFoto          = null;
        public String cNome         = "";
        public String cSobreNome    = "";
        public String cAniver       = "";
        public String cCidade       = "";
        public String cEstado       = "";
        public List<Filmes> lFilmes = new List<Filmes>();

        public Image SetImg(String tcCaminho)
        {
            iFoto = GetImg(tcCaminho, 140, 140);
            return iFoto;
        }

        public Image GetImg(String tcCaminho,int nMx,int nMy)
        {
            Image bFoto;

            try
            {
                 bFoto = new Bitmap(tcCaminho);
            }
            catch (Exception)
            {
                bFoto = iFoto;
            }

            if (bFoto == null)
            {
                bFoto = new Bitmap(nMx,nMy);
            }
            Bitmap oFoto = new Bitmap(nMx, nMy);
            Graphics loGfx = Graphics.FromImage(oFoto);
            int nM = (int) (nMx + nMy)/2;

            if (bFoto.Width > bFoto.Height)
            {
                int nConv = (bFoto.Height * nM / bFoto.Width);
                loGfx.DrawImage(bFoto, 0, ((nM / 2) - (nConv / 2)), nM, nConv);
            }

            if (bFoto.Width < bFoto.Height)
            {
                int nConv = (bFoto.Width * nM / bFoto.Height);
                loGfx.DrawImage(bFoto, ((nM / 2) - (nConv / 2)), 0, nConv, nM);
            }

            if (bFoto.Width == bFoto.Height)
            {
                loGfx.DrawImage(bFoto, 0, 0, nM, nM);
            }
            return oFoto;
        }
    }

    public class Filmes
    {
        public String cNome     = "";
        public String cAtores   = "";
        public String cComen    = "";

        public Filmes() { }
    }
}
