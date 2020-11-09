using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominio.Geral;
using DataAccessLayer.Geral;



namespace BusinessLogicLayer.Geral
{
    public class MoedaRN 
    {
        private static MoedaRN _instancia;

        private MoedaDAO dao;

        public MoedaRN()
        {
          dao = new MoedaDAO();
        }

        public static MoedaRN GetInstance() 
        {
            if (_instancia == null)
            {
                _instancia = new MoedaRN();
            }

            return _instancia;
        }

        // Actualiza o Cambio
        public MoedaDTO Salvar(MoedaDTO dto)
        {
            return dao.Adicionar(dto);  
        } 

        public List<MoedaDTO> ObterPorFiltro(MoedaDTO dto)
        {
            List<MoedaDTO> lista =  dao.ObterPorFiltro(dto);
            return lista;
        } 

        public bool Excluir(MoedaDTO dto)
        {
            return dao.Eliminar(dto);
        }
    }
}
