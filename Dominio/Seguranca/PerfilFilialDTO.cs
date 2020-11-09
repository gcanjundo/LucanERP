using Dominio.Geral;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominio.Seguranca
{
    public class PerfilEmpresaDTO:RetornoDTO
    {

        public EmpresaDTO Sucursal { get; set; }
        private PerfilDTO _perfil;

        public PerfilDTO Perfil
        {
            get { return _perfil; }
            set { _perfil = value; }
        }

        public PerfilEmpresaDTO() 
        {
        
        }

        public PerfilEmpresaDTO(int pPerfil, int pSucursal) 
        {
            Perfil = new PerfilDTO { Codigo = pPerfil };
            Sucursal = new EmpresaDTO(pSucursal);
        }
    }

    
}
