using FCG.Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FCG.Catalog.Infra.Mapping;

public class JogoBibliotecaMapping : IEntityTypeConfiguration<JogoBiblioteca>
{
    public void Configure(EntityTypeBuilder<JogoBiblioteca> builder)
    {
        builder.ToTable("JogosBibliotecas");

        builder.HasOne(x => x.Biblioteca)
            .WithMany(x => x.JogosBiblioteca)
            .HasForeignKey(x => x.IdBiblioteca);

        builder.HasOne(x => x.Jogo)
            .WithMany(x => x.JogoBibliotecas)
            .HasForeignKey(x => x.IdJogo);

    }
}


