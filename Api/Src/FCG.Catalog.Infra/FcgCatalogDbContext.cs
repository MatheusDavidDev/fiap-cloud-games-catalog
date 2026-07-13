using FCG.Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FCG.Catalog.Infra;

public class FcgCatalogDbContext : DbContext
{
    public FcgCatalogDbContext(DbContextOptions<FcgCatalogDbContext> options) : base(options)
    {
    }

    public DbSet<Biblioteca> Bibliotecas { get; set; }
    public DbSet<JogoBiblioteca> JogosBiblioteca { get; set; }
    public DbSet<Jogo> Jogos { get; set; }
    public DbSet<Categoria> Categorias { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(FcgCatalogDbContext).Assembly);
    }
}
