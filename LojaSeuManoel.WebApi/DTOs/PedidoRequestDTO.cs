using System.Text.Json.Serialization;

namespace LojaSeuManoel.WebApi.DTOs
{    /// <summary>
    /// DTO para receber as dimensões de um produto
    /// </summary>
    public class DimensoesDTO
    {
        [JsonPropertyName("altura")]
        public int Altura { get; set; }
        
        [JsonPropertyName("largura")]
        public int Largura { get; set; }
        
        [JsonPropertyName("comprimento")]
        public int Comprimento { get; set; }
    }/// <summary>
    /// DTO para receber um produto em um pedido
    /// </summary>
    public class ProdutoDTO
    {
        [JsonPropertyName("produto_id")]
        public string? ProdutoId { get; set; }
        
        [JsonPropertyName("dimensoes")]
        public DimensoesDTO Dimensoes { get; set; } = new();
    }      /// <summary>
    /// DTO para receber um pedido
    /// </summary>
    public class PedidoDTO
    {
        [JsonPropertyName("pedido_id")]
        public int PedidoId { get; set; }
        
        [JsonPropertyName("produtos")]
        public List<ProdutoDTO> Produtos { get; set; } = new();
    }
      /// <summary>
    /// DTO para receber a lista de pedidos
    /// </summary>
    public class PedidosRequestDTO
    {
        [JsonPropertyName("pedidos")]
        public List<PedidoDTO> Pedidos { get; set; } = new();
    }
}
