using Dominio.Seguranca;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Seguranca
{
    public class SystemLanguageRN
    {

        private static SystemLanguageRN _instancia;

         
        public SystemLanguageRN()
        {
           
        }

        public static SystemLanguageRN GetInstance()
        {
            if (_instancia == null)
            {
                _instancia = new SystemLanguageRN();
            }

            return _instancia;
        }

        public LangauageDTO GetLanguage(CultureInfo pCurrentCulture, ResourceManager pRm)
        {
            return new LangauageDTO(pCurrentCulture, pRm); 
        }
    }
}
