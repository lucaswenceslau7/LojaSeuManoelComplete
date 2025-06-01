using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using LojaSeuManoel.WebApi.Data;
using LojaSeuManoel.WebApi.DTOs;
using Microsoft.EntityFrameworkCore;

namespace LojaSeuManoel.WebApi.Controllers
{
    /// <summary>
    /// Controller para gerenciamento dos tipos de caixas disponíveis
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [Authorize]
    public class CaixasController : ControllerBase
    {
        private readonly LojaContext _context;
        
        public CaixasController(LojaContext context)
        {
            _context = context;
        }
        
        /// <summary>
        /// Obtém todos os tipos de caixas disponíveis
        /// </summary>
        /// <returns>Lista de tipos de caixas com suas dimensões</returns>
        [HttpGet]
        public async Task<ActionResult> ObterTiposCaixa()        {
            try
            {
                var tiposCaixa = await _context.TiposCaixa
                    .Select(tc => new
                    {
                        id = tc.Id,
                        nome = tc.Nome,
                        dimensoes = new DimensoesResponseDTO
                        {
                            Altura = tc.Altura,
                            Largura = tc.Largura,
                            Comprimento = tc.Comprimento
                        }
                    })
                    .ToListAsync();
                    
                return Ok(tiposCaixa);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { 
                    sucesso = false, 
                    mensagem = $"Erro interno do servidor: {ex.Message}" 
                });            }
        }
    }
}
