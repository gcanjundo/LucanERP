using BusinessLogicLayer.Comercial;
using DataAccessLayer.Clinica;
using DataAccessLayer.Comercial;
using Dominio.Clinica;
using Dominio.Geral;
using System.Collections.Generic;

namespace BusinessLogicLayer.Clinica
{
    public class LaboratorioExameRN
    {
        private static LaboratorioExameRN _instancia;

        private LaboratorioExameDAO dao;

        public LaboratorioExameRN()
        {
          dao = new LaboratorioExameDAO();
        }

        public static LaboratorioExameRN GetInstance() 
        {
            if (_instancia == null)
            {
                _instancia = new LaboratorioExameRN();
            }

            return _instancia;
        }

        public LaboratorioExameDTO Salvar(LaboratorioExameDTO dto) 
        {
            if(dto.Codigo <= 0)
            {
                new ArtigoDAO().Adicionar(dto);
            } 
             
            if (dao.Adicionar(dto).Sucesso)
                dto.MensagemErro = "alert('Exame Gravado com Sucesso'); window.location.href='ListaExamesLaboratoriais'";
            else
                dto.MensagemErro = "alert('Erro ao Gravar o Exame: "+dto.MensagemErro+"')";
            return dto; 
        }

        public LaboratorioExameDTO Excluir(LaboratorioExameDTO dto) 
        {
            return dao.Excluir(dto);
        }

        public List<LaboratorioExameDTO> ObterPorFiltro(LaboratorioExameDTO dto)
        {
            return dao.ObterPorFiltro(dto);
        }

        public List<LaboratorioExameDTO> GetExameForDropDowList()
        {     
            var lista = dao.ObterPorFiltro(new LaboratorioExameDTO { Designacao =string.Empty }); 
            lista.Insert(0, new LaboratorioExameDTO { Designacao = "-SELECCIONE-", Codigo = -1 }); 
            return lista;
        }

        public LaboratorioExameDTO ObterPorPK(LaboratorioExameDTO dto) 
        {
           return dao.ObterPorPK(dto);
        }

          
    }
}
