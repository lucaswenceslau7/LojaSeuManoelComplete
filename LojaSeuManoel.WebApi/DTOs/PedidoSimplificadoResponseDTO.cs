namespace LojaSeuManoel.WebApi.DTOs
{
    /// <summary>
    /// DTO para representar uma caixa utilizada na resposta simplificada
    /// </summary>
    public class CaixaSimplificadaDTO
    {
        /// <summary>
        /// ID da caixa utilizada (nome do tipo de caixa)
        /// </summary>
        public string? caixa_id { get; set; }
        
        /// <summary>
        /// Lista de nomes dos produtos colocados na caixa
        /// </summary>
        public List<string> produtos { get; set; } = new();
        
        /// <summary>
        /// Observação para casos especiais (produtos que não cabem)
        /// </summary>
        public string? observacao { get; set; }
    }
    
    /// <summary>
    /// DTO para representar um pedido na resposta simplificada
    /// </summary>
    public class PedidoSimplificadoDTO
    {
        /// <summary>
        /// ID do pedido
        /// </summary>
        public int pedido_id { get; set; }
        
        /// <summary>
        /// Lista de caixas utilizadas no pedido
        /// </summary>
        public List<CaixaSimplificadaDTO> caixas { get; set; } = new();
    }
    
    /// <summary>
    /// DTO para a resposta completa simplificada
    /// </summary>
    public class PedidosSimplificadoResponseDTO
    {
        /// <summary>
        /// Lista de pedidos processados
        /// </summary>
        public List<PedidoSimplificadoDTO> pedidos { get; set; } = new();
    }
}