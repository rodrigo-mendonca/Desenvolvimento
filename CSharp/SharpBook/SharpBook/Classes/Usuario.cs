﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;

namespace SharpBook.Classes
{
    public class Usuario : Perfil
    {
        public String cLogin = "";
        public String cSenha = "";       

        public List<Usuario> lAmigos = new List<Usuario>();
        public List<Msg> lMsgs       = new List<Msg>();

        public Usuario()
        { 
        }
        public Usuario(String tcLogin)
        {
            cLogin = tcLogin;
        }
        public Usuario(String tcLogin, String tcSenha)
        {
            cLogin = tcLogin;
            cSenha = tcSenha;
        }

        public Usuario PreConfig()
        {
            String cImage = Directory.GetCurrentDirectory();

            try
            {
                cImage = cImage+"\\Imagens\\" + cLogin + ".jpg";
                iFoto = SetImg(cImage);
            }
            catch (Exception)
            {
            }
            
            cNome = cLogin;
            cCidade = "São Paulo";
            cEstado = "SP";

            return this;
        }

    }
}
