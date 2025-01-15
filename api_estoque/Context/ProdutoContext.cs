using api_estoque.Entities;
using Microsoft.EntityFrameworkCore;

namespace api_estoque.Context
{
    public class ProdutoContext : DbContext
    {
        public ProdutoContext(DbContextOptions<ProdutoContext> options) : base(options) 
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Produto>()
                .Property(p => p.DataAdicionado)
                .HasDefaultValueSql("GETDATE()");
        }

        public DbSet<Produto> produtos { get; set; }
        public DbSet<ProdutoSaida> ProdutosSaida { get; set; }
    }
}
