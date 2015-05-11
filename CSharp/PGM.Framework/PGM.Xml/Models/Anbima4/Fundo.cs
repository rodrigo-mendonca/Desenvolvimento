using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PGM.Xml.Models.Anbima4
{
    [XmlType("fundo")]
    public class Fundo
    {
        [XmlElement("header")]
        public List<Header> Header { get; set; }

        [XmlElement("caixa")]
        public List<Caixa> Caixa { get; set; }

        [XmlElement("cotas")]
        public List<Cotas> Cotas { get; set; }

        [XmlElement("despesas")]
        public List<Despesas> Despesas { get; set; }

        [XmlElement("outrasdespesas")]
        public List<OutrasDespesas> OutrasDespesas { get; set; }

        [XmlElement("provisao")]
        public List<Provisao> Provisao { get; set; }
    }
}
