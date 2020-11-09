using DataAccessLayer.Seguranca;
using Dominio.Seguranca;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BusinessLogicLayer.Seguranca
{
    public class SaftRN
    {

        private static SaftRN _instancia;

        private SaftDAO dao;

        public SaftRN()
        {
            dao = new SaftDAO();
        }

        public static SaftRN GetInstance()
        {
            if (_instancia == null)
            {
                _instancia = new SaftRN();
            }

            return _instancia;
        }

        public static T Deserialize<T>(string xmlString)
        {
            TextReader tw = null;
            try
            {
                tw = new StreamReader(xmlString);

                System.Xml.Serialization.XmlSerializer x = new System.Xml.Serialization.XmlSerializer(typeof(T));
                T config = (T)x.Deserialize(tw);

                tw.Close();
                tw.Dispose();

                return config;
            }
            catch
            {
                if (tw != null)
                {
                    tw.Close();
                    tw.Dispose();
                }

                return default(T);
            }
        }

        public static string Serializa(object config)
        {
            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    using (TextWriter tw = new StreamWriter(ms))
                    {
                        System.Xml.Serialization.XmlSerializer x = new System.Xml.Serialization.XmlSerializer(config.GetType());
                        x.Serialize(tw, config);

                        tw.Flush();
                    }

                    return ms.ToString();
                }
            }
            catch
            {
                return string.Empty;
            }
        } 
         
    }
}
