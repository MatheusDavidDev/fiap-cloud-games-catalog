
using FCG.Catalog.Domain.Entities;
using Xunit;

namespace FCG.Catalog.Tests;

public class JogoTests
{
    [Fact]
    public void CriarJogo_DeveArmazenarDadosCorretamente()
    {
        // Arrange
        var nome = "Minecraft";
        var preco = 99.90m;
        var categoriaId = Guid.NewGuid();

        // Act
        var jogo = new Jogo(nome, preco, categoriaId);

        // Assert
        Assert.Equal(nome, jogo.Nome);
        Assert.Equal(preco, jogo.Preco);
        Assert.Equal(categoriaId, jogo.IdCategoria);
    }

    [Fact]
    public void CriarJogo_SemCategoria_DevePermitirIdNulo()
    {
        // Act
        var jogo = new Jogo("Jogo Teste", 50m, null);

        // Assert
        Assert.Null(jogo.IdCategoria);
    }

    [Fact]
    public void AtualizarJogo_DeveAlterarDadosCorretamente()
    {
        // Arrange
        var jogo = new Jogo("Nome Antigo", 30m, null);
        var novaCategoriaId = Guid.NewGuid();

        // Act
        jogo.Atualizar("Nome Novo", 75.50m, novaCategoriaId);

        // Assert
        Assert.Equal("Nome Novo", jogo.Nome);
        Assert.Equal(75.50m, jogo.Preco);
        Assert.Equal(novaCategoriaId, jogo.IdCategoria);
    }
}

