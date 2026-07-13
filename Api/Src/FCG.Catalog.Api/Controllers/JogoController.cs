using FCG.Catalog.Api.Controllers.Models;
using FCG.Catalog.Application.Commands.JogoCommand.CadastrarJogoCommand;
using FCG.Catalog.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FCG.Catalog.Api.Controllers;

[ApiController]
[Route("Jogos")]
public class JogoController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IJogoQueryService _queryService;

    public JogoController(IMediator mediator, IJogoQueryService queryService)
    {
        _mediator = mediator;
        _queryService = queryService;
    }
    //[Authorize(Roles = "Admin")]
    [HttpPost("cadastrar-jogo")]
    public async Task<IActionResult> Cadastrar(CadastrarJogoModel model)
    {
        await _mediator.Send(new CadastrarJogoCommand(model.Nome, model.Preco, model.IdCategoria));
        return Ok();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> JogoPorId(Guid id)
    {
        var result = await _queryService.ObterJogoPorIdAsync(id, CancellationToken.None);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> ObterJogos()
    {
        var result = await _queryService.ObterJogosAsync(CancellationToken.None);
        return Ok(result);
    }



}
