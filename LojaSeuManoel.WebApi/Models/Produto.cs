namespace LojaSeuManoel.WebApi.Models
{    /// <summary>
    /// Objeto Produto
    /// </summary>
    public class Produto
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public int Altura { get; set; }
        public int Largura { get; set; }
        public int Comprimento { get; set; }
        
        /// <summary>
        /// Caucula o volume
        /// </summary>
        public int Volume => Altura * Largura * Comprimento;
        
        public int PedidoId { get; set; }
        public Pedido Pedido { get; set; } = null!;
    }
}
