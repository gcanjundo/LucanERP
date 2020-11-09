using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Comercial.Stock
{
    public class InventarioDTO:RetornoDTO
    {
        public int InventoryID { get; set; }

        public string Reference { get; set; }

        public DateTime InventoryDate { get; set; }

        public DateTime RegistrationDate { get; set; }

        public int EmployeeID { get; set; }

        public int WarehouseID { get; set; }

        public string InventoryStatus { get; set; } // F - Fechado; C - EM CONTAGEM; A - ANULADO;

        public List<ItemMovimentoStockDTO> InventoryItemsList { get; set; }

        public string Localizacao { get; set; }
    }
}
