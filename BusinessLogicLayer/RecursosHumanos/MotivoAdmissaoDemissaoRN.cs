using System.Collections.Generic;
using DataAccessLayer.RecursosHumanos;
using Dominio.Geral;

namespace BusinessLogicLayer.RecursosHumanos
{
    public class MotivoAdmissaoDemissaoRN
    {
        private static MotivoAdmissaoDemissaoRN _instancia;

        private MotivoAdmissaoDemissaoDAO dao;

        public MotivoAdmissaoDemissaoRN()
        {
          dao= new MotivoAdmissaoDemissaoDAO();
        }

        public static MotivoAdmissaoDemissaoRN GetInstance() 
        {
            if (_instancia == null)
            {
                _instancia = new MotivoAdmissaoDemissaoRN();
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

        public List<MotivoDTO> ListaMotivoAdmissaoDemissaos(string descricao)
        {
            if (descricao == null)
            {
                descricao = "";
            }
            return dao.ObterPorFiltro(new MotivoDTO(0,descricao));
        }

        public MotivoDTO ObterPorPK(MotivoDTO dto) 
        {
           return dao.ObterPorPK(dto);
        }
    }
}
