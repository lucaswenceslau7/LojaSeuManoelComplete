namespace LojaSeuManoel.WebApi.Models
{    /// <summary>
    /// Objeto representante das caixas disponivesis para envio de pedidos
    /// </summary>
    public class TipoCaixa
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public int Altura { get; set; }
        public int Largura { get; set; }
        public int Comprimento { get; set; }
        
        /// <summary>
        /// Volume
        /// </summary>
        public int Volume => Altura * Largura * Comprimento;
        
        /// <summary>
        /// Verifica se o volume cabe na caixa
        /// </summary>
        public bool ProdutoCabe(Produto produto)
        {
            // Verifica se o produto cabe considerando todas as rotações possíveis
            var dimensoesProduto = new[] { produto.Altura, produto.Largura, produto.Comprimento }.OrderBy(x => x).ToArray();
            var dimensoesCaixa = new[] { Altura, Largura, Comprimento }.OrderBy(x => x).ToArray();
            
            return dimensoesProduto[0] <= dimensoesCaixa[0] &&
                   dimensoesProduto[1] <= dimensoesCaixa[1] &&
                   dimensoesProduto[2] <= dimensoesCaixa[2];
        }
    }
}
