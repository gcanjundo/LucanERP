using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominio.GestaoEscolar.Pedagogia
{
    public class EncarregadoDTO:RetornoDTO
    {
        private AlunoDTO _encAluno;

        public AlunoDTO EncAluno
        {
            get { return _encAluno; }
            set { _encAluno = value; }
        }
        private string _encNome="";

        public string EncNome
        {
            get { return _encNome; }
            set { _encNome = value; }
        }
        private string _encTelefone;

        public string EncTelefone
        {
            get { return _encTelefone; }
            set { _encTelefone = value; }
        }
        private string _encTelemovel;

        public string EncTelemovel
        {
            get { return _encTelemovel; }
            set { _encTelemovel = value; }
        }
        private string _encParentesco;

        public string EncParentesco
        {
            get { return _encParentesco; }
            set { _encParentesco = value; }
        }

        private string _encEmail;

        public string EncEmail
        {
            get { return _encEmail; }
            set { _encEmail = value; }
        }
    }

    public class ListaEncarregadosDTO : List<EncarregadoDTO> 
    {
    
    }
}
