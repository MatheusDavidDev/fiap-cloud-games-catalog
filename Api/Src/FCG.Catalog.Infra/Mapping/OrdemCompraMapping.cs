using FCG.Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FCG.Catalog.Infra.Mapping;

public class OrdemCompraMapping : IEntityTypeConfiguration<OrdemCompra>
{
    public void Configure(EntityTypeBuilder<OrdemCompra> builder)
    {
        builder.ToTable("OrdensCompra");

        builder.Property(x => x.IdUsuario)
            .IsRequired();

        builder.Property(x => x.IdJogo)
            .IsRequired();

        builder.HasOne(x => x.Jogo)
           .WithMany()
           .HasForeignKey(x => x.IdJogo);

        builder.Property(x => x.Valor)
            .HasPrecision(10, 2);

        builder.Property(x => x.Status)
            .IsRequired()
            .HasConversion<string>();
    }
}
