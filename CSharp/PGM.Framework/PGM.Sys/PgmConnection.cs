using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PGM.Sys
{
    public class PgmConnection
    {
        public string ConnectionName { get; set; }
        public string ConnectionDisplayName { get; set; }

        public PgmConnection(string Display, string Name)
        {
            ConnectionName = Name;
            ConnectionDisplayName = Display;
        }
    }
}
