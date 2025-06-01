using Microsoft.VisualStudio.TestTools.UnitTesting;
using LojaSeuManoel.WebApi.Models;
using System;

namespace LojaSeuManoel.WebApi.Test.Tests.UnitTests
{
    [TestClass]
    public class PedidoTests
    {
        [TestMethod]
        public void VolumeTotal_ComUmProduto_DeveCalcularCorretamente()
        {
            
            var pedido = new Pedido();
            var produto = new Produto
            {
                Nome = "Produto 1",
                Altura = 10,
                Largura = 20,
                Comprimento = 30
            };
            pedido.Produtos.Add(produto);

            
            decimal volumeTotal = pedido.VolumeTotal;

            
            Assert.AreEqual(6000, volumeTotal); // 10 * 20 * 30 = 6000
        }

        [TestMethod]
        public void VolumeTotal_ComVariosProdutos_DeveSomarTodosVolumes()
        {
            
            var pedido = new Pedido();
            
            var produto1 = new Produto
            {
                Nome = "Produto 1",
                Altura = 10,
                Largura = 20,
                Comprimento = 30 // Volume: 6000
            };
            
            var produto2 = new Produto
            {
                Nome = "Produto 2",
                Altura = 5,
                Largura = 10,
                Comprimento = 15 // Volume: 750
            };
            
            var produto3 = new Produto
            {
                Nome = "Produto 3",
                Altura = 2,
                Largura = 3,
                Comprimento = 4 // Volume: 24
            };

            pedido.Produtos.Add(produto1);
            pedido.Produtos.Add(produto2);
            pedido.Produtos.Add(produto3);

            
            decimal volumeTotal = pedido.VolumeTotal;

            
            Assert.AreEqual(6774, volumeTotal); // 6000 + 750 + 24 = 6774
        }

        [TestMethod]
        public void VolumeTotal_SemProdutos_DeveRetornarZero()
        {
            
            var pedido = new Pedido();

            
            decimal volumeTotal = pedido.VolumeTotal;

            
            Assert.AreEqual(0, volumeTotal);
        }

        [TestMethod]
        public void VolumeTotal_ComProdutosSemVolume_DeveRetornarZero()
        {
            
            var pedido = new Pedido();
            
            var produto1 = new Produto
            {
                Nome = "Produto 1",
                Altura = 0,
                Largura = 20,
                Comprimento = 30
            };
            
            var produto2 = new Produto
            {
                Nome = "Produto 2",
                Altura = 5,
                Largura = 0,
                Comprimento = 15
            };

            pedido.Produtos.Add(produto1);
            pedido.Produtos.Add(produto2);

            
            decimal volumeTotal = pedido.VolumeTotal;

            
            Assert.AreEqual(0, volumeTotal); // Ambos têm volume 0
        }

        [TestMethod]
        public void DataCriacao_DeveSerInicializadaComDataAtual()
        {
            
            var dataAntes = DateTime.UtcNow;

            
            var pedido = new Pedido();
            var dataDepois = DateTime.UtcNow;

            
            Assert.IsTrue(pedido.DataCriacao >= dataAntes && pedido.DataCriacao <= dataDepois);
        }

        [TestMethod]
        public void Construtor_DeveInicializarListasVazias()
        {
  
            var pedido = new Pedido();

            
            Assert.IsNotNull(pedido.Produtos);
            Assert.AreEqual(0, pedido.Produtos.Count);
            Assert.IsNotNull(pedido.CaixasUtilizadas);
            Assert.AreEqual(0, pedido.CaixasUtilizadas.Count);
        }
    }
}
