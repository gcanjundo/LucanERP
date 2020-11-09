namespace Dominio.Contabilidade
{
    public class PlanoContaDTO:TabelaGeral
    { 
        public string Conta { get; set; }  
        public int ContaPai { get; set; }  
        public string Classe { get; set; } // R - RAZÃO OU PRIMEIRO GRAU; I - INTEGRADORA OU 2ºGRAU; M - MOVIMENTO OU 3º GRAU 
        public string Natureza { get; set; }  
        public string RubricaPai { get; set; }
        public int AnoExercicio { get; set; }


    }
}
