namespace LojaSeuManoel.WebApi.Models
{
    /// <summary>
    /// Representa o pedido passando uma lista de produtos e lista de caixas utilizadas
    /// </summary>
    public class Pedido
    {
        public int Id { get; set; }
        public DateTime DataCriacao { get; set; } = DateTime.UtcNow;
        public List<Produto> Produtos { get; set; } = new();
        public List<CaixaUtilizada> CaixasUtilizadas { get; set; } = new();
        
        /// <summary>
        /// volume total de todos os produtos do pedido
        /// </summary>
        public decimal VolumeTotal => Produtos.Sum(p => p.Volume);
    }
}
