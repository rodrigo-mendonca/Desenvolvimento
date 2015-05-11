using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PGM.Sys;

namespace PGM.Interfaces
{
    public interface IControllerLogin
    {
        bool ValidUser(string tLogin, string tPassword);

        void OpenConnection(PgmConnection tCon);
    }
}
