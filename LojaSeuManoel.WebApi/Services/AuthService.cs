using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BCrypt.Net;
using LojaSeuManoel.WebApi.Models;
using LojaSeuManoel.WebApi.Data;
using Microsoft.EntityFrameworkCore;

namespace LojaSeuManoel.WebApi.Services
{
    /// <summary>
    /// Serviço responsável pela autenticação e geração de tokens JWT
    /// </summary>
    public class AuthService
    {
        private readonly LojaContext _context;
        private readonly IConfiguration _configuration;
        
        public AuthService(LojaContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        
        /// <summary>
        /// Autentica um usuário e retorna um token JWT
        /// </summary>
        public async Task<(bool Success, string Token, string Message)> LoginAsync(string email, string senha)
        {
            try
            {
                var usuario = await _context.Usuarios
                    .FirstOrDefaultAsync(u => u.Email == email);
                    
                if (usuario == null)
                {
                    return (false, string.Empty, "Usuário não encontrado");
                }
                
                if (!BCrypt.Net.BCrypt.Verify(senha, usuario.SenhaHash))
                {
                    return (false, string.Empty, "Senha inválida");
                }
                
                var token = GerarToken(usuario);
                return (true, token, "Login realizado com sucesso");
            }
            catch (Exception ex)
            {
                return (false, string.Empty, $"Erro interno: {ex.Message}");
            }
        }
        
        /// <summary>
        /// Gera um token JWT para o usuário
        /// </summary>
        private string GerarToken(Usuario usuario)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration["JWT:Key"] ?? "sua-chave-secreta-muito-segura-com-pelo-menos-256-bits"));
                
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                new Claim(ClaimTypes.Name, usuario.Nome),
                new Claim(ClaimTypes.Email, usuario.Email),
                new Claim("usuario_id", usuario.Id.ToString())
            };
            
            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:Issuer"] ?? "LojaDoSeuManoel",
                audience: _configuration["JWT:Audience"] ?? "LojaDoSeuManoel",
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: credentials
            );
            
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        
        /// <summary>
        /// Cria hash da senha usando BCrypt
        /// </summary>
        public static string CriarHashSenha(string senha)
        {
            return BCrypt.Net.BCrypt.HashPassword(senha);
        }
        
        /// <summary>
        /// Valida se uma senha corresponde ao hash
        /// </summary>
        public static bool ValidarSenha(string senha, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(senha, hash);
        }
    }
}
