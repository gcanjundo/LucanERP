using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominio.Geral;
using DataAccessLayer.Geral;

namespace BusinessLogicLayer.Geral
{   
    public class MotivoRN 
    {
        private static MotivoRN _instancia;

        private MotivoDAO dao;

        public MotivoRN()
        {
          dao = new MotivoDAO();
        }

        public static MotivoRN GetInstance() 
        {
            if (_instancia == null)
            {
                _instancia = new MotivoRN();
            }

            return _instancia;
        }

        public MotivoDTO Salvar(MotivoDTO dto)
        {
            if (dto.Codigo > 0)
            {
                return dao.Alterar(dto);
            }
            else 
            {
                return dao.Adicionar(dto);
            }
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

        public List<MotivoDTO> ObterMotivosAdmissao(MotivoDTO dto)
        {
            return ObterPorFiltro(dto);
        }

        public List<MotivoDTO> ObterMotivosDemissao(MotivoDTO dto)
        {
            return ObterPorFiltro(dto);
        }

        public List<MotivoDTO> ObterMotivosAlta(MotivoDTO dto) 
        {
            return ObterPorFiltro(dto);
        }

        public List<MotivoDTO> ObterMotivosInternamento(MotivoDTO dto) 
        {
            return ObterPorFiltro(dto);
        }

        
    }
}
