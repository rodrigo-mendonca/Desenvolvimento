using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PGM.Sys;
using PGM.Interfaces;
using PGM.SQL.Repositories;
using PGM.SQL.Models;
using PGM.Extensions.Pgm;

namespace PGM.Controllers
{
    public class ControllerLogin : IControllerLogin
    {
        public bool ValidUser(string tLogin, string tPassword)
        {
            ISysRepository<Usuario> oRepo = (ISysRepository<Usuario>)PgmInjector.GetInstance<ISysRepository<Usuario>>();
            // busca o usuario no banco
            IList<Usuario> oListUsu = oRepo.Where(c => c.Login == tLogin);

            // Usuario não cadastrado
            if (oListUsu.Count == 0)
                return false;

            Usuario oUsu = oListUsu[0];

            PgmGlobal.UserCurrent       = oUsu.Login.Trim();
            PgmGlobal.UserCurrentId     = oUsu.PkId;
            PgmGlobal.UserPassCurrent   = oUsu.Senha.Trim();
            PgmGlobal.UserCurrentGrupId = oUsu.Grupo;
            PgmGlobal.UserMaster        = oUsu.Grupo == 1;
            // verifica se a senha está correta
            if (tPassword.Encrypt() != PgmGlobal.UserPassCurrent)
                return false;

            return true;
        }

        public void OpenConnection(PgmConnection tCon)
        {
            PgmGlobal.DbConnectName = tCon.ConnectionName;
            
        }
    }
}
