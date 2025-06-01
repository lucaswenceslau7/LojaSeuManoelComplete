namespace LojaSeuManoel.WebApi.Models
{
    /// <summary>
    /// Representa uma caixa usada em um pedido especifico
    /// </summary>
    public class CaixaUtilizada
    {
        public int Id { get; set; }
        public int PedidoId { get; set; }
        public Pedido Pedido { get; set; } = null!;
          public int? TipoCaixaId { get; set; }
        public TipoCaixa? TipoCaixa { get; set; }
        
        public List<Produto> ProdutosDaCaixa { get; set; } = new();
        
        /// <summary>
        /// volume ocupado pelos produtos nesta caixa
        /// </summary>
        public decimal VolumeOcupado => ProdutosDaCaixa.Sum(p => p.Volume);
          /// <summary>
        /// calcula o percentual de ocupação da caixa
        /// </summary>
        public decimal PercentualOcupacao => TipoCaixa?.Volume > 0 ? (VolumeOcupado / TipoCaixa.Volume) * 100 : 0;
    }
}
