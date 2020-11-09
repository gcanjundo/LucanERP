using DataAccessLayer.Oficina;
using Dominio.Oficina;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Oficina
{
    public class MarcacaoRN
    {
        private static MarcacaoRN _instancia;

        private MarcacaoDAO dao;

        public MarcacaoRN()
        {
            dao = new MarcacaoDAO();
        }

        public static MarcacaoRN GetInstance()
        {
            if (_instancia == null)
            {
                _instancia = new MarcacaoRN();
            }

            return _instancia;
        }

        public MarcacaoDTO Salvar(MarcacaoDTO dto)
        {
            return dao.Adicionar(dto);
        }

        public MarcacaoDTO Anular(MarcacaoDTO dto)
        {
            return dao.Excluir(dto);
        }

        public List<MarcacaoDTO> ObterPorFiltro(MarcacaoDTO dto)
        {
            return dao.ObterPorFiltro(dto);
        }

        public MarcacaoDTO ObterPorPK(MarcacaoDTO dto)
        {
            return ObterPorFiltro(dto).FirstOrDefault();
        }
    }
}
