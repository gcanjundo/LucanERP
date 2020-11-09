using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominio.Seguranca
{
    public class PermissaoModuloDTO:RetornoDTO
    {
         

         
        public ModuloDTO Modulo { get; set; } 
        public PerfilDTO Perfil { get; set; }
 
        public int Acesso { get; set; }
 
        public int Visibilidade { get; set; } 
         
        public int Autorizar { get; set; }

    }


}
