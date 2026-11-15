using FCG.Catalog.Api.Controllers.Models;
using FCG.Catalog.Application.Commands.JogoCommand.CadastrarJogoCommand;
using FCG.Catalog.Application.Interfaces;
using FCG.Catalog.Application.Queries;
using FCG.Catalog.Core.Repository;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FCG.Catalog.Api.Controllers;

[AllowAnonymous]
[ApiController]
[Route("api/jogos")]
public class JogoController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IJogoQueryService _queryService;
    private readonly IJogoDetalhesRepository _repository;


    public JogoController(IMediator mediator, IJogoQueryService queryService, IJogoDetalhesRepository repository)
    {
        _mediator = mediator;
        _queryService = queryService;
        _repository = repository;
    }

    //[Authorize(Roles = "Admin")]
    [HttpPost("cadastrar-jogo")]
    public async Task<IActionResult> Cadastrar(CadastrarJogoModel model)
    {
        await _mediator.Send(new CadastrarJogoCommand(model.Nome, model.Preco, model.IdCategoria, model.Descricao,
            model.Desenvolvedora, model.Generos, model.Plataformas, model.RequisitosMinimos));
        return Ok();
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

    [HttpGet]
    public async Task<IActionResult> ObterJogos()
    {
        var result = await _queryService.ObterJogosAsync(CancellationToken.None);
        return Ok(result);
    }



}
