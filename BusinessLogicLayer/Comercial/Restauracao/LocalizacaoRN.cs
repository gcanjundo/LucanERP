using DataAccessLayer.Comercial.Restauracao;
using Dominio.Comercial.Restauracao;
using System.Collections.Generic;

namespace BusinessLogicLayer.Comercial.Restauracao
{
    public class LocalizacaoRN
    {
        private static LocalizacaoRN _instancia;

        private LocalizacaoDAO dao;

        public LocalizacaoRN()
        {
          dao = new LocalizacaoDAO();
        }

        public static LocalizacaoRN GetInstance() 
        {
            if (_instancia == null)
            {
                _instancia = new LocalizacaoRN();
            }

            return _instancia;
        }

        public LocalizacaoDTO Salvar(LocalizacaoDTO dto)
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

        public LocalizacaoDTO Excluir(LocalizacaoDTO dto)
        {
            return dao.Eliminar(dto);
        }

        public List<LocalizacaoDTO> ObterPorFiltro(LocalizacaoDTO dto)
        {
            return dao.ObterPorFiltro(dto);
        }

        public LocalizacaoDTO ObterPorPK(LocalizacaoDTO dto)
        {
            return dao.ObterPorPK(dto);
        }
    }
}
