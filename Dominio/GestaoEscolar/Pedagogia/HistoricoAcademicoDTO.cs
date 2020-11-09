using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominio.GestaoEscolar.Pedagogia
{
    public class HistoricoAcademicoDTO
    {
        private int _histCodigo;

        public int HistCodigo
        {
            get { return _histCodigo; }
            set { _histCodigo = value; }
        }

    }

    public class ListaHistoricoAcademicoDTO : List<HistoricoAcademicoDTO> 
    {
    
    }
}
