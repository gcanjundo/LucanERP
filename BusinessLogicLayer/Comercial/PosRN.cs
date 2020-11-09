using DataAccessLayer.Comercial.POS;
using Dominio.Comercial;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Comercial
{
   public class PosRN
    {
        private static PosRN _instancia;
         private PosDAO dao;

        public PosRN()
        {
          dao = new PosDAO();
        }

        public static PosRN GetInstance() 
        {
            if (_instancia == null)
            {
                _instancia = new PosRN();
            }

            return _instancia;
        }


        public PosDTO CreateNewPOS(PosDTO dto)
        {
           return dto = dao.Adicionar(dto);
        }

        public List<PosDTO> ObterCaixaFechadas(PosDTO dto)
        {
            return dao.ObterCaixaFechadas(dto);
        }

         
        public List<PosDTO> GetRegistersDropDownList(PosDTO dto)
        {
            var lista = ObtePostosVendas(dto); 
            lista.Insert(0, new PosDTO(-1, "-SELECCIONE-")); 
            return lista;
        }

        public List<PosDTO> ObtePostosVendas(PosDTO dto)
        {
            return dao.ObterPorFiltro(dto);
        }

        public PosDTO GetPostOfSalesDetails(PosDTO dto)
        {
            return dao.ObterPorPK(dto);  
        }

        public string ChangeRESTPinCode(string pPosID, string pPinCode)
        {
            dao.ChangePosPinCode(new PosDTO
            {
                Codigo = int.Parse(pPosID),
                PinCode = pPinCode
            });

            return "alert('PIN Alterado com Sucesso'); CleanScreen()";
        }
    }
}
