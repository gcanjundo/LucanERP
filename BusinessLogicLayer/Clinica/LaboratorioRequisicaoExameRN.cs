using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer.Clinica;
using Dominio.Clinica;

namespace BusinessLogicLayer.Clinica
{
    public class LaboratorioRequisicaoExameRN
    {
         private static LaboratorioRequisicaoExameRN _instancia;

        private LaboratorioRequisicaoExameDAO dao;

        public LaboratorioRequisicaoExameRN()
        {
          dao = new LaboratorioRequisicaoExameDAO();
        }

        public static LaboratorioRequisicaoExameRN GetInstance() 
        {
            if (_instancia == null)
            {
                _instancia = new LaboratorioRequisicaoExameRN();
            }

            return _instancia;
        }

        public LaboratorioRequisicaoExameDTO Salvar(LaboratorioRequisicaoExameDTO dto)
        {
            return dao.Adicionar(dto); 
        }

        public LaboratorioRequisicaoExameDTO Excluir(LaboratorioRequisicaoExameDTO dto)
        {
            return dao.Excluir(dto);
        }

        public LaboratorioRequisicaoExameDTO ObterPorPK(LaboratorioRequisicaoExameDTO dto)
        {
            return dao.ObterPorPK(dto);
        }

        public List<LaboratorioRequisicaoExameDTO> ObterPorFiltro(LaboratorioRequisicaoExameDTO dto)
        {
            return dao.ObterPorFiltro(dto);
        }

        public LaboratorioRequisicaoExameDetalhesDTO GetAnalisesItem( int pQtd, LaboratorioExameDTO dto)
        {

            var Exame = LaboratorioExameRN.GetInstance().ObterPorPK(new LaboratorioExameDTO { Codigo = dto.Codigo });
            return new LaboratorioRequisicaoExameDetalhesDTO
            {
                NroOrdenacao = pQtd+1,
                Descricao = Exame.Referencia +" "+Exame.Descricao, 
                ExameID = Exame.Codigo,
                PrevisionDeliveryDate = DateTime.Today.AddDays(dto.DelieveryDeadLine), 
                Status = 1,
                PrecoUnitario = Exame.PrecoVenda,
                Desconto = Exame.Desconto,
                ValorDesconto = Exame.Desconto > 0 ? (Exame.Desconto * Exame.PrecoVenda)/100 : 0,
                ValorUtente = Exame.ValorUtente,
                ValorEntidade = Exame.ValorEntidade,
                ValorTotal = Exame.PrecoVenda - Exame.Desconto,
                
                
            };
        }


    }
}
