using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpBook.Classes
{
    public class Msg
    {
        // private metodos
        private DateTime dData = DateTime.Now;
        private String cMenssage = "";
        private bool lLida = false;
        // public metodos
        public String cAssunto = "";
        public Usuario uRemetente    = null;
        public Usuario uDestinatario = null;

        public Msg()
        { 
        
        }
        public Msg(Usuario tuRemetente,Usuario tuDestinatario,String tcAssunto,String tcMessage)
        { 
            uRemetente    = tuRemetente;
            uDestinatario = tuDestinatario;
            cAssunto      = tcAssunto;
            cMenssage     = tcMessage;
        }
        public Msg(Usuario tuRemetente, Usuario tuDestinatario, String tcAssunto, String tcMessage, bool tlLida)
        {
            uRemetente      = tuRemetente;
            uDestinatario   = tuDestinatario;
            cAssunto        = tcAssunto;
            cMenssage       = tcMessage;
            SetLida(tlLida);
        }
        public void SetLida(bool tlLida)
        {
            lLida = tlLida;
        }
        public String LerMsg()
        {
            lLida = true;
            return(cMenssage);
        }
        public String LerAssunto()
        {
            return (cAssunto);
        }
    }
}
