using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominio.Geral;
using DataAccessLayer.Geral;

namespace BusinessLogicLayer.Geral
{   
    public class PrinterRN 
    {
        private static PrinterRN _instancia;

        private PrinterDAO dao;

        public PrinterRN()
        {
          dao = new PrinterDAO();
        }

        public static PrinterRN GetInstance() 
        {
            if (_instancia == null)
            {
                _instancia = new PrinterRN();
            }

            return _instancia;
        }

        public PrinterDTO Salvar(PrinterDTO dto)
        {   
             return dao.Adicionar(dto);
        }

        public void Remover(PrinterDTO dto)
        {
             
        }

        public List<PrinterDTO> ObterPorFiltro(PrinterDTO dto)
        {
            return dao.ObterPorFiltro();
        }

        public PrinterDTO ObterPorPK(PrinterDTO dto)
        {
            return ObterPorFiltro(dto).Where(t=>t.Codigo == dto.Codigo).ToList().SingleOrDefault();
        }


        public void Excluir(List<ProductPrinterDTO> printerList)
        {
            foreach(var printer in printerList)
            {
                dao.AddProductPrinter(printer);
            }
        }

        public void Excluir(ProductPrinterDTO dto)
        {
            dao.DeleteProductPrinter(dto);
        }

        
    }
}
