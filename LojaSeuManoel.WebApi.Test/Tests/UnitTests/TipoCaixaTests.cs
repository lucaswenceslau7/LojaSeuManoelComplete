using Microsoft.VisualStudio.TestTools.UnitTesting;
using LojaSeuManoel.WebApi.Models;

namespace LojaSeuManoel.WebApi.Test.Tests.UnitTests
{
    [TestClass]
    public class TipoCaixaTests
    {
        [TestMethod]
        public void Volume_DeveCalcularCorretamente()
        {
            
            var tipoCaixa = new TipoCaixa
            {
                Nome = "Caixa Pequena",
                Altura = 30,
                Largura = 40,
                Comprimento = 80
            };

            
            int volume = tipoCaixa.Volume;

            
            Assert.AreEqual(96000, volume); // 30 * 40 * 80 = 96000
        }

        [TestMethod]
        public void ProdutoCabe_ComProdutoMenor_DeveRetornarTrue()
        {
            
            var tipoCaixa = new TipoCaixa
            {
                Nome = "Caixa Grande",
                Altura = 50,
                Largura = 60,
                Comprimento = 80
            };

            var produto = new Produto
            {
                Nome = "Produto Pequeno",
                Altura = 20,
                Largura = 30,
                Comprimento = 40
            };

            
            bool cabe = tipoCaixa.ProdutoCabe(produto);

            
            Assert.IsTrue(cabe);
        }

        [TestMethod]
        public void ProdutoCabe_ComProdutoMaior_DeveRetornarFalse()
        {
            
            var tipoCaixa = new TipoCaixa
            {
                Nome = "Caixa Pequena",
                Altura = 30,
                Largura = 40,
                Comprimento = 50
            };

            var produto = new Produto
            {
                Nome = "Produto Grande",
                Altura = 60,
                Largura = 70,
                Comprimento = 80
            };

            
            bool cabe = tipoCaixa.ProdutoCabe(produto);

            
            Assert.IsFalse(cabe);
        }

        [TestMethod]
        public void ProdutoCabe_ComProdutoExato_DeveRetornarTrue()
        {
            
            var tipoCaixa = new TipoCaixa
            {
                Nome = "Caixa Exata",
                Altura = 30,
                Largura = 40,
                Comprimento = 50
            };

            var produto = new Produto
            {
                Nome = "Produto Exato",
                Altura = 30,
                Largura = 40,
                Comprimento = 50
            };

            
            bool cabe = tipoCaixa.ProdutoCabe(produto);

            
            Assert.IsTrue(cabe);
        }

        [TestMethod]
        public void ProdutoCabe_ComRotacao_DeveRetornarTrue()
        {
            
            var tipoCaixa = new TipoCaixa
            {
                Nome = "Caixa Teste",
                Altura = 30,
                Largura = 40,
                Comprimento = 50
            };

            // Produto que só cabe se rotacionado
            var produto = new Produto
            {
                Nome = "Produto Rotacionado",
                Altura = 50, // Maior dimensão do produto
                Largura = 30, // Menor dimensão do produto
                Comprimento = 40 // Dimensão média do produto
            };

            
            bool cabe = tipoCaixa.ProdutoCabe(produto);

            
            Assert.IsTrue(cabe); // Deve caber porque o algoritmo considera rotações
        }

        [TestMethod]
        public void ProdutoCabe_ComUmaDimensaoMaior_DeveRetornarFalse()
        {
            
            var tipoCaixa = new TipoCaixa
            {
                Nome = "Caixa Teste",
                Altura = 30,
                Largura = 40,
                Comprimento = 50
            };

            var produto = new Produto
            {
                Nome = "Produto com uma dimensão muito grande",
                Altura = 60, // Esta dimensão é maior que qualquer da caixa
                Largura = 20,
                Comprimento = 30
            };

            
            bool cabe = tipoCaixa.ProdutoCabe(produto);

            
            Assert.IsFalse(cabe);
        }

        [TestMethod]
        public void ProdutoCabe_ComDimensoesZero_DeveRetornarTrue()
        {
            
            var tipoCaixa = new TipoCaixa
            {
                Nome = "Caixa Teste",
                Altura = 30,
                Largura = 40,
                Comprimento = 50
            };

            var produto = new Produto
            {
                Nome = "Produto Sem Volume",
                Altura = 0,
                Largura = 0,
                Comprimento = 0
            };

            
            bool cabe = tipoCaixa.ProdutoCabe(produto);

            
            Assert.IsTrue(cabe); // Produto sem volume sempre cabe
        }
    }
}
