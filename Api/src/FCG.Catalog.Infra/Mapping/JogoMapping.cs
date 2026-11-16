using FCG.Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FCG.Catalog.Infra.Mapping;

public class JogoMapping : IEntityTypeConfiguration<Jogo>
{
    public void Configure(EntityTypeBuilder<Jogo> builder)
    {
        builder.ToTable("Jogos");

        builder.Property(x => x.Nome)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(x => x.Preco)
            .HasPrecision(10, 2);

        builder.HasMany(x => x.JogoBibliotecas)
            .WithOne(x => x.Jogo)
            .HasForeignKey(x => x.IdJogo);

        builder.HasOne(x => x.Categoria)
            .WithMany(x => x.Jogos)
            .HasForeignKey(x => x.IdCategoria);
    }
}
