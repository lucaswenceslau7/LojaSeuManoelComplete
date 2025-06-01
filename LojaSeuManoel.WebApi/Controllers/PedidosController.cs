using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using LojaSeuManoel.WebApi.Services;
using LojaSeuManoel.WebApi.DTOs;

namespace LojaSeuManoel.WebApi.Controllers
{
    /// <summary>
    /// Controller responsável pelo processamento de pedidos e empacotamento
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [Authorize] // Requer autenticação para todos os endpoints
    public class PedidosController : ControllerBase
    {
        private readonly PedidoService _pedidoService;
        
        public PedidosController(PedidoService pedidoService)
        {
            _pedidoService = pedidoService;
        }
        
        /// <summary>
        /// Processa uma lista de pedidos e retorna o empacotamento otimizado
        /// </summary>
        /// <param name="request">Lista de pedidos com produtos e suas dimensões</param>
        /// <returns>Resultado do empacotamento com as caixas utilizadas</returns>
        [HttpPost]
        public async Task<ActionResult<PedidosResponseDTO>> ProcessarPedidos([FromBody] PedidosRequestDTO request)
        {
            try
            {
                if (request == null || !request.Pedidos.Any())
                {
                    return BadRequest(new PedidosResponseDTO
                    {
                        Sucesso = false,
                        Mensagem = "É necessário enviar pelo menos um pedido",
                        Pedidos = new List<PedidoResponseDTO>()
                    });
                }
                
                // Valida se todos os pedidos têm produtos
                foreach (var pedido in request.Pedidos)
                {
                    if (!pedido.Produtos.Any())
                    {
                        return BadRequest(new PedidosResponseDTO
                        {
                            Sucesso = false,
                            Mensagem = "Todos os pedidos devem conter pelo menos um produto",
                            Pedidos = new List<PedidoResponseDTO>()
                        });
                    }
                    
                    // Valida dimensões dos produtos
                    foreach (var produto in pedido.Produtos)
                    {
                        if (produto.Dimensoes.Altura <= 0 || 
                            produto.Dimensoes.Largura <= 0 || 
                            produto.Dimensoes.Comprimento <= 0)
                        {
                            return BadRequest(new PedidosResponseDTO
                            {
                                Sucesso = false,
                                Mensagem = "Todas as dimensões dos produtos devem ser maiores que zero",
                                Pedidos = new List<PedidoResponseDTO>()
                            });
                        }
                    }
                }
                
                var resultado = await _pedidoService.ProcessarPedidosAsync(request);
                
                if (!resultado.Sucesso)
                {
                    return BadRequest(resultado);
                }
                
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new PedidosResponseDTO
                {
                    Sucesso = false,
                    Mensagem = $"Erro interno do servidor: {ex.Message}",
                    Pedidos = new List<PedidoResponseDTO>()
                });
            }
        }          /// <summary>
        /// Obtém todos os pedidos processados no formato simplificado
        /// </summary>
        /// <returns>Lista de pedidos no formato simplificado</returns>
        [HttpGet]
        public async Task<ActionResult<PedidosSimplificadoResponseDTO>> ObterPedidos()
        {
            try
            {
                var pedidos = await _pedidoService.ObterPedidosSimplificadoAsync();
                return Ok(pedidos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { 
                    sucesso = false, 
                    mensagem = $"Erro interno do servidor: {ex.Message}" 
                });
            }
        }
    }
}
