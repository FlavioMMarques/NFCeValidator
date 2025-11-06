using System;

namespace NFCeValidator.Models
{
    public class NFCeInfo
    {
        public string ChaveAcesso { get; set; }
        public string Serie { get; set; }
        public string NumeroNFCe { get; set; }
        public decimal ValorTotal { get; set; }
        public DateTime? DataEmissao { get; set; }
        public string CFOP { get; set; }
        public string DocumentoDestinatario { get; set; }
        public string TipoDocumento { get; set; } // CNPJ ou CPF
        
        // Dados da View
        public bool ExisteNaView { get; set; }
        public decimal? ValorNaView { get; set; }
        public DateTime? DataNaView { get; set; }
        public string CFOPNaView { get; set; }
        public string DocumentoNaView { get; set; }
        public string StatusNaView { get; set; } // A=Ativa, C=Cancelada, I=Inutilizada
        
        // Flags de Validação
        public bool ValorDivergente { get; set; }
        public bool DataDivergente { get; set; }
        public bool CFOPDivergente { get; set; }
        public bool DocumentoDivergente { get; set; }
        public bool StatusIncorreto { get; set; }
        public bool ChaveDuplicada { get; set; }
        
        public string Status { get; set; }
        public string DetalhesValidacao { get; set; }
        public string CaminhoArquivo { get; set; }

        public NFCeInfo()
        {
            ChaveAcesso = string.Empty;
            Serie = string.Empty;
            NumeroNFCe = string.Empty;
            ValorTotal = 0;
            DataEmissao = null;
            CFOP = string.Empty;
            DocumentoDestinatario = string.Empty;
            TipoDocumento = string.Empty;
            
            ExisteNaView = false;
            ValorNaView = null;
            DataNaView = null;
            CFOPNaView = string.Empty;
            DocumentoNaView = string.Empty;
            StatusNaView = string.Empty;
            
            ValorDivergente = false;
            DataDivergente = false;
            CFOPDivergente = false;
            DocumentoDivergente = false;
            StatusIncorreto = false;
            ChaveDuplicada = false;
            
            Status = "Não Validado";
            DetalhesValidacao = string.Empty;
            CaminhoArquivo = string.Empty;
        }
        
        public int GetNivelGravidade()
        {
            // 0 = OK, 1 = Alerta, 2 = Erro
            if (!ExisteNaView || ChaveDuplicada || StatusIncorreto)
                return 2; // Erro
            
            if (ValorDivergente || CFOPDivergente || DataDivergente || DocumentoDivergente)
                return 1; // Alerta
            
            return 0; // OK
        }
    }
}
