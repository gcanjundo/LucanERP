using Dominio.Geral;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Oficina
{
    
    public class OrdemServicoDTO:RetornoDTO
    {
        public OrdemServicoDTO()
        {

        }

        public OrdemServicoDTO(int pOrdeID)
        {
            WorkOrderID = pOrdeID;
        }
        public int WorkOrderID { get; set; }
        public string WorkDesgination { get; set; }
        public int DocumentID { get; set; }
        public int SerieID { get; set; }
        public int OsType { get; set; }
        public string Reference { get; set; }
        public string DocumentNumber { get; set; }
        public int DocumentStatus { get; set; }
        public int TechnicianID { get; set; }
        public int VehicleID { get; set; }
        public decimal InitialKM { get; set; }
        public int OsStatus { get; set; }
        public string BookingUser { get; set; }
        public DateTime BookingDate { get; set; }
        public int CheckInTechnicianID { get; set; }
        public DateTime CheckInDate { get; set; }
        public string DriverPerson { get; set; }
        public string DriverContact { get; set; }
        public DateTime OsBeginDate { get; set; }
        public DateTime OsEndDate { get; set; } 
        public int CheckOutTechnicianID { get; set; }
        public DateTime CheckOutDate { get; set; }
        public bool Actived { get; set; }
        public string Matricula { get; set; }
        public string FreeNotes  { get; set; }
        public VeiculoDTO Veiculo { get; set; }
        public List<NoteDTO> Notas { get; set; }
        public DateTime PredictionClosing { get; set; }
        public List<OrderDetailDTO> OrderDetailsList { get; set; }
        public CheckListDTO CheckListOrder { get; set; }
        public List<IncidentDTO> IncidentsList { get; set; }
        public decimal ProductListSubTotal { get; set; }
        public decimal ServiceListSubTotal { get; set; }
        public decimal OrderSubTotal { get; set; }
        public int BillingEntityID { get; set; }
        public int InvoiceID { get; set; }
        public string BillingEntityDesignation { get; set; }
        public string CheckInTechnicianName { get; set; }
        public string CheckOutTechnicianName { get; set; }
        public string WorkOrderTechnicianName { get; set; }
    }

    public class CheckListDTO:RetornoDTO
    {
        public CheckListDTO()
        {

        }

        public CheckListDTO(int pOrdeID)
        {
            OrderServiceID = pOrdeID;
        }
        public int OrderServiceID { get; set; }
        public DateTime CheckListDate { get; set; }
        public bool HasMacaco { get; set; }
        public bool HasChaveRoda { get; set; }
        public bool HasManivela { get; set; }
        public bool HasSocorro { get; set; } 
        public bool HasPiscaFrontalLE { get; set; }
        public bool HasPiscaFrontalLD { get; set; }
        public bool HasPiscaTraseiroLE { get; set; }
        public bool HasPiscaTraseiroLD { get; set; }
        public bool HasStopLE { get; set; }
        public bool HasStopLD { get; set; }
        public bool HasFarolLE { get; set; }
        public bool HasFarolLD { get; set; }
        public bool HasSimboloFrontal { get; set; }
        public bool HasSimboloTraseiro { get; set; }
        public bool HasRetrovisorLE { get; set; }
        public bool HasRetrovisorLD { get; set; }
        public bool HasRetrovisorInterior { get; set; }
        public bool HasRadio { get; set; }
        public bool HasTapeteFrontalLE { get; set; }
        public bool HasTapeteFrontalLD { get; set; }
        public bool HasTapeteTraseiroLE { get; set; }
        public bool HasTapeteTraseiroLD { get; set; }
        public bool HasTapeteTraseiroMeio { get; set; }
        public bool HasIsqueiro { get; set; }
        public bool HasElevadorFrontalLE { get; set; }
        public bool HasElevadorFrontalLD { get; set; }
        public bool HasElevadorTraseiroLE { get; set; }
        public bool HasElevadorTraseiroLD { get; set; }
        public string Observacoes { get; set; }
        public string NivelCombustivel { get; set; }
        public bool HasColete { get; set; }
        public bool HasTriangulo { get; set; } 

    }
    public class OrderDetailDTO:RetornoDTO
    {
        public int OrderID { get; set; } 
        public int DetailID { get; set; }
        public int ItemID { get; set; }
        public int TechnicianID { get; set; }
        public DateTime ActionStart { get; set; }
        public DateTime ActionEnd { get; set; }
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal SubTotal { get; set; } 
        public bool External { get; set; }
        
        public DateTime EndPrevision { get; set; } 
        public string StatusDescription { get; set; }
        public decimal Discount { get; set; }
        public decimal Tax { get; set; }
        public string Notes { get; set; }
        public string ItemType { get; set; }
        public int WorshopLocationID { get; set; }
        public int WareHouseID { get; set; }
        public int InvoiceID { get; set; }
    }

    public class IncidentDTO:RetornoDTO
    {
        public int OrderServiceID { get; set; }
        public DateTime IncidentDate { get; set; }
        public int TechnicianID { get; set; }
        public string TechnicianName { get; set; }
        public string Origem { get; set; } 
    }


    public class NoteDTO:RetornoDTO
    {
        public int OrderServiceID { get; set; }
        public DateTime NoteDate { get; set; }
        public int TechnicianID { get; set; }
        public string TechnicianName { get; set; }
        public string NoteCode { get; set; } 
        public bool IsVisivel { get; set; }
    }
}
