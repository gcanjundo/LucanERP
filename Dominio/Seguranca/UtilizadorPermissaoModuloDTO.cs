using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominio.Seguranca
{
    public class UtilizadorPermissaoModuloDTO:RetornoDTO
    {


        public ModuloDTO Modulo { get; set; }
        public UtilizadorDTO User { get; set; }
        
        private int _utilizadorPermModAcesso;

        public int UtilizadorPermModAcesso
        {
            get { return _utilizadorPermModAcesso; }
            set { _utilizadorPermModAcesso = value; }
        }

        private int _utilizadorPermModVisibilidade;

        public int UtilizadorPermModVisibilidade
        {
            get { return _utilizadorPermModVisibilidade; }
            set { _utilizadorPermModVisibilidade = value; }
        }

        private int _utilizadorPermModAutorizar;
       

        public UtilizadorPermissaoModuloDTO(ModuloDTO pModulo, UtilizadorDTO pUtilizador)
        {
            // TODO: Complete member initialization
            this.Modulo = pModulo;
            this.User = pUtilizador;
        }

        public UtilizadorPermissaoModuloDTO()
        {
            // TODO: Complete member initialization
        }

        public int UtilizadorPermModAutorizar
        {
            get { return _utilizadorPermModAutorizar; }
            set { _utilizadorPermModAutorizar = value; }
        }
    }
     
}
