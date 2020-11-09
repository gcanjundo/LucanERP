namespace Dominio.Comercial
{
    public class ComissaoDTO:TabelaGeral
    {
        public ComissaoDTO()
        {

        }

        public ComissaoDTO(int pCodigo) 
        {
            Codigo = pCodigo;
        }

        public ComissaoDTO(int pCodigo, string pDescricao)
        {
            Codigo = pCodigo;
            Descricao = pDescricao;
        }

        public ComissaoDTO(int pCodigo, string pDescricao, decimal pValor)
        {
            Codigo = pCodigo;
            Descricao = pDescricao;
            Valor=pValor;
        }

        public ComissaoDTO(int pCodigo, string pDescricao, decimal pValor, int pEstado)
        {
            Codigo = pCodigo;
            Descricao = pDescricao;
            Valor = pValor;
            Estado = pEstado;
        }

        public ComissaoDTO(int pCodigo, string pDescricao, decimal pValor, int pEstado, bool pSucesso, string pMensagem)
        {
            Codigo = pCodigo;
            Descricao = pDescricao;
            Valor = pValor;
            Estado = pEstado;
            MensagemErro = pMensagem;
            Sucesso = pSucesso;
        } 

        public decimal Valor { get; set; }
    }
}
