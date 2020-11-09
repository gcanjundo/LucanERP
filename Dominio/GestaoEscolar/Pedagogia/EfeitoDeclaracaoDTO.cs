using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominio.GestaoEscolar.Pedagogia
{
    public class EfeitoDeclaracaoDTO : RetornoDTO
    {

        public EfeitoDeclaracaoDTO()
        {

        }

        private int _efeCodigo = 0;

        public int EfeCodigo
        {
            get { return _efeCodigo; }
            set { _efeCodigo = value; }
        }
        private string _efeDescricao = "";

        public string EfeDescricao
        {
            get { return _efeDescricao; }
            set { _efeDescricao = value; }
        }
        private String efeTipo = "";

        public String EfeTipo
        {
            get { return efeTipo; }
            set { efeTipo = value; }
        }

        public EfeitoDeclaracaoDTO(int codigo, string descricao, string tipo)
        {
            this.EfeCodigo = codigo;
            this.EfeDescricao = descricao;
            this.EfeTipo = tipo;
        }

    }
}

