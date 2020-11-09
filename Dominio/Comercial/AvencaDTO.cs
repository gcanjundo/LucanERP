namespace Dominio.Comercial
{

    public class AvencaDTO : RetornoDTO
    {
        
        
        public int ModeloID { get; set; }
        public bool IsPropinaEscolar { get; set; }
        public bool OnlyForWeekDays { get; set; }
        public bool IsTemplate { get; set; }
        public bool IncludeMonthReference { get; set; }
        public int FinalDocumentID { get; set; }
        public int SerieID { get; set; }
        public int PaymentTermsID { get; set; }
        

    }
}
