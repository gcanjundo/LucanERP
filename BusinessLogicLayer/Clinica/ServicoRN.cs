using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominio.Clinica;
using DataAccessLayer.Clinica;
using Dominio.Geral;

namespace BusinessLogicLayer.Clinica
{
    public class ServicoRN
    {

        private static ServicoRN _instancia;

        private ServicoDAO dao;

        public ServicoRN()
        {
          dao = new ServicoDAO();
        }

        public static ServicoRN GetInstance() 
        {
            if (_instancia == null)
            {
                _instancia = new ServicoRN();
            }

            return _instancia;
        }

        public ArtigoDTO Salvar(ArtigoDTO dto) 
        {
            return dao.Adicionar(dto);
        }

        public ServicoDTO Excluir(ServicoDTO dto) 
        {
            return dao.Eliminar(dto);
        }

        public List<ServicoDTO> ObterPorFiltro(ServicoDTO dto)
        {
            return dao.ObterPorFiltro(dto);
        }

        public List<ServicoDTO> ListaServicos(string descricao, int tipo)
        {
            if (descricao==null) 
            {
                descricao = "";
            }
            return dao.ObterPorFiltro(new ServicoDTO { Descricao ="" });
        }

        public ServicoDTO ObterPorPK(ServicoDTO dto) 
        {
            return ObterPorFiltro(dto)[0];
        }

         
    }
}
