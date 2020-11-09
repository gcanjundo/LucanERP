using DataAccessLayer.Comercial.Restauracao;
using Dominio.Comercial.Restauracao;
using System.Collections.Generic;

namespace BusinessLogicLayer.Comercial.Restauracao
{
    public class MesaRN
    {
        private static MesaRN _instancia;

        private MesaDAO dao;

        public MesaRN()
        {
          dao = new MesaDAO();
        }

        public static MesaRN GetInstance() 
        {
            if (_instancia == null)
            {
                _instancia = new MesaRN();
            }

            return _instancia;
        }

        public MesaDTO Salvar(MesaDTO dto)
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

        public bool Excluir(MesaDTO dto)
        {
            return dao.Eliminar(dto);
        }

        public List<MesaDTO> ObterPorFiltro(MesaDTO dto)
        {
            return dao.ObterPorFiltro(dto);
        }

        public MesaDTO ObterPorPK(MesaDTO dto)
        {
            return dao.ObterPorPK(dto);
        }

        public void Desocupar(MesaDTO dto)
        {
            dao.Desocupar(dto);
        }

        public void Ocupar(MesaDTO dto)
        {
           dao.Ocupar(dto);
        }

        
        
    }
}
