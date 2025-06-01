using Microsoft.AspNetCore.Mvc;
using LojaSeuManoel.WebApi.Services;
using LojaSeuManoel.WebApi.DTOs;

namespace LojaSeuManoel.WebApi.Controllers
{
    /// <summary>
    /// Controller responsável pela autenticação
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;
        
        public AuthController(AuthService authService)
        {
            _authService = authService;
        }
        
        /// <summary>
        /// Realiza o login do usuário
        /// </summary>
        /// <param name="loginDto">Dados de login do usuário</param>
        /// <returns>Token JWT se o login for bem-sucedido</returns>
        [HttpPost("login")]
        public async Task<ActionResult<LoginResponseDTO>> Login([FromBody] LoginDTO loginDto)
        {
            try
            {
                if (string.IsNullOrEmpty(loginDto.Usuario) || string.IsNullOrEmpty(loginDto.Senha))
                {
                    return BadRequest(new LoginResponseDTO
                    {
                        Sucesso = false,
                        Mensagem = "Usuário e senha são obrigatórios"
                    });
                }
                
                var resultado = await _authService.LoginAsync(loginDto.Usuario, loginDto.Senha);
                
                if (!resultado.Success)
                {
                    return Unauthorized(new LoginResponseDTO
                    {
                        Sucesso = false,
                        Mensagem = resultado.Message
                    });
                }
                
                return Ok(new LoginResponseDTO
                {
                    Sucesso = true,
                    Token = resultado.Token,
                    Usuario = loginDto.Usuario,
                    Expiracao = DateTime.UtcNow.AddHours(2),
                    Mensagem = "Login realizado com sucesso"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new LoginResponseDTO
                {
                    Sucesso = false,
                    Mensagem = $"Erro interno do servidor: {ex.Message}"
                });
            }
        }
    }
}
