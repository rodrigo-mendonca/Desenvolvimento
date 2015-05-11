using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using PGM.Extensions.FoxPro;
using PGM.Interfaces;

namespace PGM.Xml
{
    public class PgmXml : IPgmXml
    {
        MemoryStream XmlBuffer;

        public PgmXml()
        { 
        
        }

        public void SetXmlFile(string tFileName)
        {
            string cXml = "";
            cXml = cXml.FileToStr(tFileName);
            SetXmlBuffer(cXml);
        }

        public void SetXmlBuffer(string tXml)
        {
            // converte para bytes
            byte[] byteArray = Encoding.UTF8.GetBytes(tXml);
            // cria um stream dos bytes
            XmlBuffer = new MemoryStream(byteArray);
        }

        public void Serialize<T>(T tObj,string tFileName)
        {
            XmlSerializer oSerializer = new XmlSerializer(typeof(T));
            TextWriter oWriter = new StreamWriter(tFileName);
            oSerializer.Serialize(oWriter, tObj);
            oWriter.Close();
        }

        public T Deserialize<T>(string tFileName)
        {
            SetXmlBuffer(tFileName.FileToStr(tFileName));

            return Deserialize<T>();
        }

        public T Deserialize<T>()
        { 
            T lReturn;
            XmlSerializer oSerializer = new XmlSerializer(typeof(T));

            lReturn = (T)oSerializer.Deserialize(XmlBuffer);

            return lReturn;
        }
    }
}
