using Microsoft.EntityFrameworkCore;
using LojaSeuManoel.WebApi.Models;

namespace LojaSeuManoel.WebApi.Data
{
    /// <summary>
    /// Contexto do banco de dados para a Loja do Seu Manoel
    /// </summary>
    public class LojaContext : DbContext
    {
        public LojaContext(DbContextOptions<LojaContext> options) : base(options)
        {
        }
        
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<TipoCaixa> TiposCaixa { get; set; }
        public DbSet<CaixaUtilizada> CaixasUtilizadas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
              // Configuração para Produto
            modelBuilder.Entity<Produto>(entity =>
            {
                entity.HasKey(e => e.Id);
                // Dimensões como inteiros (centímetros)
                entity.Property(e => e.Altura);
                entity.Property(e => e.Largura);
                entity.Property(e => e.Comprimento);
                
                entity.HasOne(e => e.Pedido)
                      .WithMany(p => p.Produtos)
                      .HasForeignKey(e => e.PedidoId);
            });
            
            // Configuração para Pedido
            modelBuilder.Entity<Pedido>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.DataCriacao).HasDefaultValueSql("GETUTCDATE()");
            });
              // Configuração para TipoCaixa
            modelBuilder.Entity<TipoCaixa>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nome).HasMaxLength(100).IsRequired();
                // Dimensões como inteiros (centímetros)
                entity.Property(e => e.Altura);
                entity.Property(e => e.Largura);
                entity.Property(e => e.Comprimento);
            });
            
            // Configuração para CaixaUtilizada
            modelBuilder.Entity<CaixaUtilizada>(entity =>
            {
                entity.HasKey(e => e.Id);
                
                entity.HasOne(e => e.Pedido)
                      .WithMany(p => p.CaixasUtilizadas)
                      .HasForeignKey(e => e.PedidoId);
                      
                entity.HasOne(e => e.TipoCaixa)
                      .WithMany()
                      .HasForeignKey(e => e.TipoCaixaId);
                      
                entity.HasMany(e => e.ProdutosDaCaixa)
                      .WithOne()
                      .HasForeignKey("CaixaUtilizadaId");
            });
            
            // Configuração para Usuario
            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nome).HasMaxLength(100).IsRequired();
                entity.Property(e => e.Email).HasMaxLength(200).IsRequired();
                entity.Property(e => e.SenhaHash).HasMaxLength(500).IsRequired();
                entity.Property(e => e.DataCriacao).HasDefaultValueSql("GETUTCDATE()");
                
                entity.HasIndex(e => e.Email).IsUnique();
            });
              // dados iniciais - Tipos de Caixa
            modelBuilder.Entity<TipoCaixa>().HasData(
                new TipoCaixa { Id = 1, Nome = "Caixa 1", Altura = 30, Largura = 40, Comprimento = 80 },
                new TipoCaixa { Id = 2, Nome = "Caixa 2", Altura = 80, Largura = 50, Comprimento = 40 },
                new TipoCaixa { Id = 3, Nome = "Caixa 3", Altura = 50, Largura = 80, Comprimento = 60 }
            );// usuario padrão para testes, registrado na inicialização
            modelBuilder.Entity<Usuario>().HasData(
                new Usuario 
                { 
                    Id = 1, 
                    Nome = "Administrador", 
                    Email = "admin",
                    // Hash da senha "admin123" 
                    SenhaHash = "$2a$11$0lHj0TfSfm2bsUD6BHmHXeXv4bmuupS2ffftkGZnyYDlY3jT4DZWi",
                    DataCriacao = DateTime.UtcNow
                }
            );
        }
    }
}
