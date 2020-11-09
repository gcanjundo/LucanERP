using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominio.Seguranca
{
    public class FormularioDTO:ModuloDTO
    { 
        public ModuloDTO Modulo { get; set; }
        public FormularioDTO()
        {

        }

        public FormularioDTO(int pCodigo, string pTitulo, string pLink)
        {
            base.Codigo = pCodigo;
            Descricao = pTitulo;
            Link = pLink;
        }
    }

    
}
