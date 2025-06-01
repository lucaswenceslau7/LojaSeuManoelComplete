namespace LojaSeuManoel.WebApi.DTOs
{    /// <summary>
    /// DTO para retornar as dimensões de um produto na resposta
    /// </summary>
    public class DimensoesResponseDTO
    {
        public int Altura { get; set; }
        public int Largura { get; set; }
        public int Comprimento { get; set; }
    }
    
    /// <summary>
    /// DTO para retornar um produto na resposta
    /// </summary>
    public class ProdutoResponseDTO
    {
        public int Id { get; set; }
        public DimensoesResponseDTO Dimensoes { get; set; } = new();
    }
    
    /// <summary>
    /// DTO para retornar uma caixa utilizada
    /// </summary>
    public class CaixaUtilizadaResponseDTO
    {
        public int Id { get; set; }
        public string TipoCaixa { get; set; } = string.Empty;
        public DimensoesResponseDTO Dimensoes { get; set; } = new();
        public List<ProdutoResponseDTO> Produtos { get; set; } = new();
        public decimal VolumeOcupado { get; set; }
        public decimal VolumeTotal { get; set; }
        public decimal PercentualOcupacao { get; set; }
    }
    
    /// <summary>
    /// DTO para retornar um pedido processado
    /// </summary>
    public class PedidoResponseDTO
    {
        public int Id { get; set; }
        public List<CaixaUtilizadaResponseDTO> Caixas { get; set; } = new();
        public decimal VolumeTotal { get; set; }
        public int QuantidadeCaixas { get; set; }
    }
    
    /// <summary>
    /// DTO para retornar a resposta completa dos pedidos
    /// </summary>
    public class PedidosResponseDTO
    {
        public List<PedidoResponseDTO> Pedidos { get; set; } = new();
        public bool Sucesso { get; set; }
        public string Mensagem { get; set; } = string.Empty;
    }
}
