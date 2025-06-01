using LojaSeuManoel.WebApi.Models;
using LojaSeuManoel.WebApi.Data;
using Microsoft.EntityFrameworkCore;

namespace LojaSeuManoel.WebApi.Services
{
    /// <summary>
    /// Service responsavel pela logica de empacotamento dos produtos
    /// </summary>
    public class EmpacotamentoService
    {
        private readonly LojaContext _context;

        public EmpacotamentoService(LojaContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Processa uma lista de pedidos e determina o empacotamento otimizado
        /// </summary>
        public async Task<List<Pedido>> ProcessarPedidosAsync(List<Pedido> pedidos)
        {
            var tiposCaixa = await _context.TiposCaixa
                .OrderBy(tc => tc.Altura * tc.Largura * tc.Comprimento) // Ordena por volume crescente para tentar usar caixas menores primeiro
                .ToListAsync();

            foreach (var pedido in pedidos)
            {
                await ProcessarPedidoAsync(pedido, tiposCaixa);
            }

            return pedidos;
        }        /// <summary>
        /// Processa um pedido individual
        /// </summary>
        private Task ProcessarPedidoAsync(Pedido pedido, List<TipoCaixa> tiposCaixa)
        {
            // Ordena produtos por volume decrescente (algoritmo First Fit Decreasing)
            // Começamos sempre pelos produtos de maior volume (ex: Monitor 60.000 cm³, Microfone 2.500 cm³, etc.)
            var produtosOrdenados = pedido.Produtos
                .OrderByDescending(p => p.Altura * p.Largura * p.Comprimento)
                .ToList();
                
            foreach (var produto in produtosOrdenados)
            {
                bool produtoEmpacotado = false;

                // Tenta colocar o produto em uma caixa já existente
                foreach (var caixaExistente in pedido.CaixasUtilizadas)
                {
                    if (ProdutoCabeNaCaixa(produto, caixaExistente))
                    {
                        caixaExistente.ProdutosDaCaixa.Add(produto);
                        produtoEmpacotado = true;
                        break;
                    }
                }
                // Se não coube em nenhuma caixa existente, cria uma nova
                if (!produtoEmpacotado)
                {
                    var tipoCaixaAdequada = EncontrarMenorCaixaQueCabe(produto, tiposCaixa);

                    if (tipoCaixaAdequada == null)
                    {
                        // Produto não cabe em nenhuma caixa - cria uma "caixa especial" sem tipo
                        var caixaEspecial = new CaixaUtilizada
                        {
                            TipoCaixaId = null, // Indica que não há caixa adequada
                            TipoCaixa = null,
                            ProdutosDaCaixa = new List<Produto> { produto }
                        };
                        pedido.CaixasUtilizadas.Add(caixaEspecial);
                    }
                    else
                    {
                        var novaCaixa = new CaixaUtilizada
                        {
                            TipoCaixaId = tipoCaixaAdequada.Id,
                            TipoCaixa = tipoCaixaAdequada,
                            ProdutosDaCaixa = new List<Produto> { produto }
                        };
                        pedido.CaixasUtilizadas.Add(novaCaixa);
                    }
                }
            }

            return Task.CompletedTask;
        }
        /// <summary>
        /// Verifica se um produto cabe em uma caixa já utilizada
        /// </summary>
        private bool ProdutoCabeNaCaixa(Produto produto, CaixaUtilizada caixaUtilizada)
        {
            // Se não há tipo de caixa, não cabe
            if (caixaUtilizada.TipoCaixa == null)
                return false;

            // Verifica se o produto cabe fisicamente na caixa
            if (!caixaUtilizada.TipoCaixa.ProdutoCabe(produto))
                return false;

            // Verifica se há espaço volumétrico (simplificação - na realidade seria mais complexo)
            var volumeDisponivel = caixaUtilizada.TipoCaixa.Volume - caixaUtilizada.VolumeOcupado;
            return produto.Volume <= volumeDisponivel;
        }

        /// <summary>
        /// Encontra a menor caixa que pode acomodar o produto
        /// </summary>
        private TipoCaixa? EncontrarMenorCaixaQueCabe(Produto produto, List<TipoCaixa> tiposCaixa)
        {
            return tiposCaixa.FirstOrDefault(tc => tc.ProdutoCabe(produto));
        }
    }
}
