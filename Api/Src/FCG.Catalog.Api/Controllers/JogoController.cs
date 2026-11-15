using FCG.Catalog.Api.Controllers.Models;
using FCG.Catalog.Application.Commands.JogoCommand.AtualizarJogoCommand;
using FCG.Catalog.Application.Commands.JogoCommand.CadastrarJogoCommand;
using FCG.Catalog.Application.Interfaces;
using FCG.Catalog.Application.Queries;
using FCG.Catalog.Core.Repository;
using FCG.Catalog.Infra.OpenSearch.Services;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FCG.Catalog.Api.Controllers;

//[AllowAnonymous]
[ApiController]
[Route("api/jogos")]
public class JogoController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IJogoQueryService _queryService;
    private readonly IJogoDetalhesRepository _repository;
    private readonly IJogoSearchService _jogoSearchService;

    public JogoController(IMediator mediator, IJogoQueryService queryService, IJogoDetalhesRepository repository,
        IJogoSearchService jogoSearchService)
    {
        _mediator = mediator;
        _queryService = queryService;
        _repository = repository;
        _jogoSearchService = jogoSearchService;
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("cadastrar-jogo")]
    public async Task<IActionResult> Cadastrar(CadastrarJogoModel model, CancellationToken cancellationToken)
    {
        await _mediator.Send(new CadastrarJogoCommand(model.Nome, model.Preco, model.IdCategoria, model.Descricao,
            model.Desenvolvedora, model.Generos, model.Plataformas, model.RequisitosMinimos), cancellationToken);
        return Ok();
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> AtualizarJogo(Guid id, [FromBody] AtualizarJogoModel model, CancellationToken cancellationToken)
    {

        await _mediator.Send(new AtualizarJogoCommand(
            id,
            model.Nome,
            model.Preco,
            model.IdCategoria,
            model.Descricao,
            model.Desenvolvedora,
            model.Generos,
            model.Plataformas,
            model.RequisitosMinimos), cancellationToken);

        return NoContent();
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> JogoPorId(Guid id)
    {
        var result = await _queryService.ObterJogoPorIdAsync(id, CancellationToken.None);
        return Ok(result);
    }

    [HttpGet("{id:guid}/detalhes")]
    public async Task<IActionResult> ObterDetalhes(Guid id, CancellationToken cancellationToken)
    {
        var detalhes = await _repository.ObterPorJogoIdAsync(id, cancellationToken);

        if (detalhes is null)
            return NotFound();

        return Ok(detalhes);
    }

    [HttpGet("buscar")]
    [AllowAnonymous]
    public async Task<IActionResult> BuscarJogos([FromQuery] string pesquisa, CancellationToken cancellationToken)
    {
        var jogos = await _jogoSearchService.BuscarJogosAsync(pesquisa, cancellationToken);

        return Ok(jogos);
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> ObterJogos()
    {
        var result = await _queryService.ObterJogosAsync(CancellationToken.None);
        return Ok(result);
    }



}
