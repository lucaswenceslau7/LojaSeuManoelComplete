using LojaSeuManoel.WebApi.Models;
using LojaSeuManoel.WebApi.Data;
using LojaSeuManoel.WebApi.DTOs;
using Microsoft.EntityFrameworkCore;

namespace LojaSeuManoel.WebApi.Services
{
    /// <summary>
    /// Serviço principal para processamento dos pedidos requisitados
    /// </summary>
    public class PedidoService
    {
        private readonly LojaContext _context;
        private readonly EmpacotamentoService _empacotamentoService;
        
        public PedidoService(LojaContext context, EmpacotamentoService empacotamentoService)
        {
            _context = context;
            _empacotamentoService = empacotamentoService;
        }
        
        /// <summary>
        /// Processa uma lista de pedidos e retorna o resultado do empacotamento
        /// </summary>
        public async Task<PedidosResponseDTO> ProcessarPedidosAsync(PedidosRequestDTO request)
        {
            try
            {
                // Converte DTOs em entidades
                var pedidos = new List<Pedido>();
                
                foreach (var pedidoDto in request.Pedidos)
                {
                    var pedido = new Pedido();
                      foreach (var produtoDto in pedidoDto.Produtos)
                    {
                        var produto = new Produto
                        {
                            Nome = produtoDto.ProdutoId ?? "Produto sem nome",
                            Altura = produtoDto.Dimensoes.Altura,
                            Largura = produtoDto.Dimensoes.Largura,
                            Comprimento = produtoDto.Dimensoes.Comprimento,
                            Pedido = pedido
                        };
                        
                        pedido.Produtos.Add(produto);
                    }
                    
                    pedidos.Add(pedido);
                }
                
                // Processa o empacotamento
                var pedidosProcessados = await _empacotamentoService.ProcessarPedidosAsync(pedidos);
                
                // Passa para o banco de dados 
                _context.Pedidos.AddRange(pedidosProcessados);
                await _context.SaveChangesAsync();
                
                // Converte para DTO de resposta caso sucesso
                var response = new PedidosResponseDTO
                {
                    Sucesso = true,
                    Mensagem = "Pedidos processados com sucesso",
                    Pedidos = pedidosProcessados.Select(ConvertToResponseDTO).ToList()
                };
                
                return response;
            }
            catch (Exception ex)
            {
                return new PedidosResponseDTO
                {
                    Sucesso = false,
                    Mensagem = $"Erro ao processar pedidos: {ex.Message}",
                    Pedidos = new List<PedidoResponseDTO>()
                };
            }
        }
        
        /// <summary>
        /// Obtém todos os pedidos processados
        /// </summary>
        public async Task<List<PedidoResponseDTO>> ObterTodosPedidosAsync()
        {
            var pedidos = await _context.Pedidos
                .Include(p => p.Produtos)
                .Include(p => p.CaixasUtilizadas)
                    .ThenInclude(c => c.TipoCaixa)
                .Include(p => p.CaixasUtilizadas)
                    .ThenInclude(c => c.ProdutosDaCaixa)
                .ToListAsync();
                
            return pedidos.Select(ConvertToResponseDTO).ToList();        }
        
        /// <summary>
        /// Converte uma entidade Pedido para DTO de resposta
        /// </summary>
        private PedidoResponseDTO ConvertToResponseDTO(Pedido pedido)
        {
            return new PedidoResponseDTO
            {
                Id = pedido.Id,
                VolumeTotal = pedido.VolumeTotal,
                QuantidadeCaixas = pedido.CaixasUtilizadas.Count,                Caixas = pedido.CaixasUtilizadas.Select(c => new CaixaUtilizadaResponseDTO
                {
                    Id = c.Id,
                    TipoCaixa = c.TipoCaixa?.Nome ?? "N/A",
                    Dimensoes = new DimensoesResponseDTO
                    {
                        Altura = c.TipoCaixa?.Altura ?? 0,
                        Largura = c.TipoCaixa?.Largura ?? 0,
                        Comprimento = c.TipoCaixa?.Comprimento ?? 0
                    },
                    VolumeTotal = c.TipoCaixa?.Volume ?? 0,
                    VolumeOcupado = c.VolumeOcupado,
                    PercentualOcupacao = c.PercentualOcupacao,
                    Produtos = c.ProdutosDaCaixa.Select(p => new ProdutoResponseDTO
                    {
                        Id = p.Id,
                        Dimensoes = new DimensoesResponseDTO
                        {
                            Altura = p.Altura,
                            Largura = p.Largura,
                            Comprimento = p.Comprimento
                        }
                    }).ToList()
                }).ToList()
            };
        }
        
        /// <summary>
        /// Obtém todos os pedidos no formato simplificado
        /// </summary>
        public async Task<PedidosSimplificadoResponseDTO> ObterPedidosSimplificadoAsync()
        {
            var pedidos = await _context.Pedidos
                .Include(p => p.Produtos)
                .Include(p => p.CaixasUtilizadas)
                    .ThenInclude(c => c.TipoCaixa)
                .Include(p => p.CaixasUtilizadas)
                    .ThenInclude(c => c.ProdutosDaCaixa)
                .ToListAsync();
                
            return new PedidosSimplificadoResponseDTO
            {
                pedidos = pedidos.Select(ConvertToSimplifiedDTO).ToList()
            };
        }

        /// <summary>
        /// Converte uma entidade Pedido para DTO simplificado
        /// </summary>
        private PedidoSimplificadoDTO ConvertToSimplifiedDTO(Pedido pedido)
        {
            var pedidoSimplificado = new PedidoSimplificadoDTO
            {
                pedido_id = pedido.Id,
                caixas = new List<CaixaSimplificadaDTO>()
            };

            foreach (var caixa in pedido.CaixasUtilizadas)
            {
                var caixaSimplificada = new CaixaSimplificadaDTO
                {
                    caixa_id = caixa.TipoCaixa?.Nome,
                    produtos = caixa.ProdutosDaCaixa.Select(p => p.Nome).ToList()
                };

                // Se não há tipo de caixa, significa que os produtos não cabem
                if (caixa.TipoCaixa == null)
                {
                    caixaSimplificada.observacao = "Produto não cabe em nenhuma caixa disponível.";
                }

                pedidoSimplificado.caixas.Add(caixaSimplificada);
            }

            return pedidoSimplificado;
        }
    }
}
