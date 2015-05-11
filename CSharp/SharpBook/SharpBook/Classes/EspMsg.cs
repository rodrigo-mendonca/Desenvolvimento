using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpBook.Classes
{
    public class EspMsg : Msg
    {
        public Filmes fRecomend = null;

        public EspMsg(Usuario tuRemetente, Usuario tuDestinatario, String tcAssunto, Filmes tfFilme)
        {
            uRemetente      = tuRemetente;
            uDestinatario   = tuDestinatario;
            cAssunto        = tcAssunto;
            fRecomend       = tfFilme;
        }
    }
}
