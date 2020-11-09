using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominio.Seguranca
{
    public class UtilizadorPermissaoFormularioDTO:RetornoDTO
    {
        

        private FormularioDTO _userPermFormFormulario;

        public FormularioDTO UserPermFormFormulario
        {
            get { return _userPermFormFormulario; }
            set { _userPermFormFormulario = value; }
        }

        private UtilizadorDTO _userPermFormUtilizador;

        public UtilizadorDTO UserPermFormUtilizador
        {
            get { return _userPermFormUtilizador; }
            set { _userPermFormUtilizador = value; }
        }


        private int _userPermFormInclusao=0;

        public int UserPermFormInclusao
        {
            get { return _userPermFormInclusao; }
            set { _userPermFormInclusao = value; }
        }

        private int _userPermFormAlteracao=0;

        public int UserPermFormAlteracao
        {
            get { return _userPermFormAlteracao; }
            set { _userPermFormAlteracao = value; }
        }

        private int _userPermFormExclusao=0;

        public int UserPermFormExclusao
        {
            get { return _userPermFormExclusao; }
            set { _userPermFormExclusao = value; }
        }

        private int _userPermFormConsulta=0;

        public int UserPermFormConsulta
        {
            get { return _userPermFormConsulta; }
            set { _userPermFormConsulta = value; }
        }

        private int _userPermFormImpressao=0;

        public int UserPermFormImpressao
        {
            get { return _userPermFormImpressao; }
            set { _userPermFormImpressao = value; }
        }

        private int _userPermFormAcesso=0;

        public int UserPermFormAcesso
        {
            get { return _userPermFormAcesso; }
            set { _userPermFormAcesso = value; }
        }

        private string dsFormulario="";

        public string DsFormulario
        {
            get { return dsFormulario; }
            set { dsFormulario = value; }
        }
    }

    
}
