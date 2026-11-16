using FCG.Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FCG.Catalog.Infra.Mapping;

public class CategoriaMapping : IEntityTypeConfiguration<Categoria>
{
    public void Configure(EntityTypeBuilder<Categoria> builder)
    {
        builder.ToTable("Categorias");

        builder.Property(x => x.Nome)
            .IsRequired()
            .HasMaxLength(150);

        builder.HasMany(x => x.Jogos)
            .WithOne(x => x.Categoria)
            .HasForeignKey(x => x.IdCategoria);
    }
}



