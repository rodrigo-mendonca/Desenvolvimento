using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PGM.Xml.Models.Anbima4
{
    [XmlType("arquivoposicao_4_01")]
    public class Anbima4
    {
        [XmlElement("fundo")]
        public Fundo Fundo { get; set; }
    }
}
