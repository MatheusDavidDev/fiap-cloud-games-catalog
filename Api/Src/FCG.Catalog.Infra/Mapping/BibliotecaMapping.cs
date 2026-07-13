using FCG.Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FCG.Catalog.Infra.Mapping;

public class BibliotecaMapping : IEntityTypeConfiguration<Biblioteca>
{
    public void Configure(EntityTypeBuilder<Biblioteca> builder)
    {
        builder.ToTable("Bibliotecas");

        builder.Property(x => x.IdUsuario)
            .IsRequired();

        builder.HasIndex(x => x.IdUsuario)
            .IsUnique();

        builder.HasMany(x => x.JogosBiblioteca)
            .WithOne(x => x.Biblioteca)
            .HasForeignKey(x => x.IdBiblioteca);
    }
}
