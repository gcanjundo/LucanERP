using DataAccessLayer.Geral;
using Dominio.Tesouraria;
using Dominio.Geral;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Tesouraria
{
    public class BancoRN
    {
        private static BancoRN _instancia;

        private EntidadeDAO dao;

        public BancoRN()
        {
            dao = new EntidadeDAO();
        }

        public static BancoRN GetInstance()
        {
            if (_instancia == null)
            {
                _instancia = new BancoRN();
            }

            return _instancia;
        }

        public BancoDTO Salvar(BancoDTO dto)
        {
            return (BancoDTO)dao.Adicionar(dto);
        }

        public EntidadeDTO Excluir(BancoDTO dto)
        {
            EntidadeDTO entity = new EntidadeDTO();
            entity.Codigo = dto.Codigo;
            return dao.Excluir(entity);
        }

        public List<BancoDTO> ObterPorFiltro(BancoDTO dto)
        {     
            return dao.ObterBancos(dto);
        }

         

        public BancoDTO ObterPorPK(BancoDTO dto)
        {
            EntidadeDTO entity = dto;
            return (BancoDTO) dao.ObterEntidadePorPK(entity);
        }
    }
}
