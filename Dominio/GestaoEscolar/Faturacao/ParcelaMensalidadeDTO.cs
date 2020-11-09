using Dominio.Geral;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dominio.GestaoEscolar.Faturacao
{
    public class MensalidadeDTO : ArtigoDTO
    {
        public string ModalidadeCobranca { get; set; }
        public int DiaLimite { get; set; }
        public string InicioCobranca { get; set; }
        public string TerminoCobranca { get; set; }
        public int ExternalEntity { get; set; }
        public bool IsExternalFee { get; set; }
    }
    public class ParcelaMensalidadeDTO:RetornoDTO
    {    
        public MensalidadeDTO Mensalidade { get; set; }
        public string Data { get; set; }  
        public int Mes { get; set; }
        public decimal ValorUnitario { get; set; }
        public bool CobraMulta { get; set; }
        public DateTime DataLimite { get; set; }
        public bool Activa { get; set; } 

        public ParcelaMensalidadeDTO()
        {

        }

        public ParcelaMensalidadeDTO(int pCodigo)
        {
            Codigo = pCodigo;
        }

        public ParcelaMensalidadeDTO(int pCodigo, MensalidadeDTO pMensalidade, string pDescricao, string pData, int pMes)
        {
            Codigo = pCodigo;
            Descricao = pDescricao;
            Mensalidade = pMensalidade;
            Data = pData;
            Mes = pMes;
        }
    }

    public class ListaParcelasMensalidadesDTO : List<ParcelaMensalidadeDTO> 
    {
    
    }
}
