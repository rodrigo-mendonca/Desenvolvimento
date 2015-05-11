using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PGM.Interfaces
{
    public interface IPgmXml
    {
        /// <summary>
        /// Define um arquivo XML controlador
        /// </summary>
        /// <param name="tFileName">Caminho do arquivo XML</param>
        void SetXmlFile(string tFileName);

        /// <summary>
        /// Define um arquivo XML controlador
        /// </summary>
        /// <param name="tXml">Xml como texto</param>
        void SetXmlBuffer(string tXml);

        /// <summary>
        /// Serializa a classe escolhada para o caminho escolhido
        /// </summary>
        /// <param name="tObj">Objeto que será serializado</param>
        /// <param name="tFileName">Caminho que o XML será salvo</param>
        /// <returns></returns>
        void Serialize<T>(T tObj, string tFileName);

        /// <summary>
        /// Deserializa um XML para a classe escolhida
        /// </summary>
        /// <param name="tFileName">Caminho do arquivo XML</param>
        /// <returns>Retorna a classe escolhida</returns>
        T Deserialize<T>(string tFileName);

        /// <summary>
        /// Deserializa um XML para a classe escolhida
        /// </summary>
        /// <returns>Retorna a classe escolhida</returns>
        T Deserialize<T>();
    }
}
