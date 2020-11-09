using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer.Comercial.Stock;
using Dominio.Geral; 
namespace BusinessLogicLayer.Comercial.Stock
{   
    public class MotivoMovimentoRN 
    {
        private static MotivoMovimentoRN _instancia;

        private MotivoDAO dao;

        public MotivoMovimentoRN()
        {
          dao = new MotivoDAO();
        }

        public static MotivoMovimentoRN GetInstance() 
        {
            if (_instancia == null)
            {
                _instancia = new MotivoMovimentoRN();
            }

            return _instancia;
        }

        public MotivoDTO Salvar(MotivoDTO dto)
        {
            return dao.Adicionar(dto);
        }

        public MotivoDTO Excluir(MotivoDTO dto)
        {
            return dao.Eliminar(dto);
        }

        public List<MotivoDTO> ObterPorFiltro(MotivoDTO dto)
        {
            return dao.ObterPorFiltro(dto);
        }

        public MotivoDTO ObterPorPK(MotivoDTO dto)
        {
            return dao.ObterPorPK(dto);
        }  
    }
}
