using Microsoft.VisualStudio.TestTools.UnitTesting;
using LojaSeuManoel.WebApi.Services;

namespace LojaSeuManoel.WebApi.Test.Tests.UnitTests
{
    [TestClass]
    public class AuthServiceTests
    {
        [TestMethod]
        public void CriarHashSenha_DeveCriarHashValido()
        {
            
            string senha = "minhasenha123";

            
            string hash = AuthService.CriarHashSenha(senha);

            
            Assert.IsNotNull(hash);
            Assert.IsTrue(hash.Length > 0);
            Assert.AreNotEqual(senha, hash); // Hash deve ser diferente da senha original
        }

        [TestMethod]
        public void ValidarSenha_ComSenhaCorreta_DeveRetornarTrue()
        {
            
            string senha = "minhasenha123";
            string hash = AuthService.CriarHashSenha(senha);

            
            bool resultado = AuthService.ValidarSenha(senha, hash);

            
            Assert.IsTrue(resultado);
        }

        [TestMethod]
        public void ValidarSenha_ComSenhaIncorreta_DeveRetornarFalse()
        {
   
            string senhaCorreta = "minhasenha123";
            string senhaIncorreta = "senhaerrada";
            string hash = AuthService.CriarHashSenha(senhaCorreta);

            bool resultado = AuthService.ValidarSenha(senhaIncorreta, hash);

            Assert.IsFalse(resultado);
        }

        [TestMethod]
        public void CriarHashSenha_ComSenhasDiferentes_DeveCriarHashesDiferentes()
        {
            
            string senha1 = "senha123";
            string senha2 = "senha456";

            
            string hash1 = AuthService.CriarHashSenha(senha1);
            string hash2 = AuthService.CriarHashSenha(senha2);

            
            Assert.AreNotEqual(hash1, hash2);
        }

        [TestMethod]
        public void ValidarSenha_ComHashInvalido_DeveRetornarFalse()
        {
            
            string senha = "minhasenha123";
            string hashInvalido = "hash_invalido";

             
            Assert.ThrowsException<BCrypt.Net.SaltParseException>(() =>
            {
                AuthService.ValidarSenha(senha, hashInvalido);
            });
        }
    }
}