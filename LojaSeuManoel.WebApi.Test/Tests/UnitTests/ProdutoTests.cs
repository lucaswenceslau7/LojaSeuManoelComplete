using Microsoft.VisualStudio.TestTools.UnitTesting;
using LojaSeuManoel.WebApi.Models;

namespace LojaSeuManoel.WebApi.Test.Tests.UnitTests
{
    [TestClass]
    public class ProdutoTests
    {
        [TestMethod]
        public void Volume_DeveCalcularCorretamente()
        {
            
            var produto = new Produto
            {
                Nome = "Produto Teste",
                Altura = 10,
                Largura = 20,
                Comprimento = 30
            };

            
            int volume = produto.Volume;

            
            Assert.AreEqual(6000, volume); // 10 * 20 * 30 = 6000
        }

        [TestMethod]
        public void Volume_ComDimensoesZero_DeveRetornarZero()
        {
            
            var produto = new Produto
            {
                Nome = "Produto Teste",
                Altura = 0,
                Largura = 20,
                Comprimento = 30
            };

            
            int volume = produto.Volume;

            
            Assert.AreEqual(0, volume);
        }

        [TestMethod]
        public void Volume_ComDimensaoUnitaria_DeveCalcularCorretamente()
        {
            
            var produto = new Produto
            {
                Nome = "Produto Teste",
                Altura = 1,
                Largura = 1,
                Comprimento = 1
            };

            
            int volume = produto.Volume;

            
            Assert.AreEqual(1, volume);
        }

        [TestMethod]
        public void Volume_ComDimensoesGrandes_DeveCalcularCorretamente()
        {
            
            var produto = new Produto
            {
                Nome = "Produto Teste",
                Altura = 100,
                Largura = 50,
                Comprimento = 80
            };

            
            int volume = produto.Volume;

            
            Assert.AreEqual(400000, volume); // 100 * 50 * 80 = 400000
        }
    }
}
