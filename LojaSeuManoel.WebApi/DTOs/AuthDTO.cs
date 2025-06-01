namespace LojaSeuManoel.WebApi.DTOs
{
    /// <summary>
    /// DTO para login do usuário
    /// </summary>
    public class LoginDTO
    {
        public string Usuario { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;
    }
    
    /// <summary>
    /// DTO para resposta do login
    /// </summary>
    public class LoginResponseDTO
    {
        public string Token { get; set; } = string.Empty;
        public string Usuario { get; set; } = string.Empty;
        public DateTime Expiracao { get; set; }
        public bool Sucesso { get; set; }
        public string Mensagem { get; set; } = string.Empty;
    }
}
