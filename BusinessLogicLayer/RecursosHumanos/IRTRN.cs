using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominio.RecursosHumanos;
using DataAccessLayer.RecursosHumanos;

namespace BusinessLogicLayer.RecursosHumanos
{
    public class IRTRN
    {
        private static IRTRN _instancia;

        private IRTDAO dao;

        public IRTRN()
        {
          dao= new IRTDAO();
        }

        public static IRTRN GetInstance() 
        {
            if (_instancia == null)
            {
                _instancia = new IRTRN();
            }

            return _instancia;
        }

        public EscaloesIRTDTO Salvar(EscaloesIRTDTO dto) 
        {
            return dao.Adicionar(dto);
        }

        public EscaloesIRTDTO Excluir(EscaloesIRTDTO dto) 
        {
            return dao.Eliminar(dto);
        }

        public List<EscaloesIRTDTO> ObterPorFiltro()
        {
            return dao.ObterPorFiltro();
        }

        
        public EscaloesIRTDTO ObterPorPK(EscaloesIRTDTO dto) 
        {
           return dao.ObterPorPK(dto);
        }

        public EscaloesIRTDTO ValorIRT(decimal SalarioBase)
        {
             
            var Escaloes = ObterPorFiltro();
            var dto = Escaloes.Where(t => t.SalarioMinimo <= SalarioBase && t.SalarioMaximo >= SalarioBase).ToList().FirstOrDefault();


            if (dto == null)
            {
                dto = Escaloes.Where(t => t.SalarioMinimo <= SalarioBase && t.SalarioMaximo == 0).ToList().FirstOrDefault();
            }

            if(dto.SalarioMaximo > 0)
            {
                dto.ValorExcesso = SalarioBase - dto.ValorExcesso;
                dto.ValorExcesso = dto.ValorExcesso < 0 ? dto.ValorExcesso * (1) : dto.ValorExcesso;

                dto.ValorDescontar = dto.ValorMinimoDesconto + ((dto.PercentualDesconto/100) * dto.ValorExcesso);
            }

            return dto;
        }
    }
}
