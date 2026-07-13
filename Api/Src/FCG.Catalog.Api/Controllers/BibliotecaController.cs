using FCG.Catalog.Application.Commands.BibliotecaCommand.AdicionarJogoCommand;
using FCG.Catalog.Application.Commands.OrdemCompraCommand.CriarOrdemCompraCommand;
using FCG.Catalog.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FCG.Catalog.Api.Controllers;

[ApiController]
[Route("api/bibliotecas")]
public class BibliotecaController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IBibliotecaQueryService _queryService;

    public BibliotecaController(IMediator mediator , IBibliotecaQueryService queryService)
    {
        _mediator = mediator;
        _queryService = queryService;
    }

    [HttpPost("{id}")]
    public async Task<IActionResult> AdicionarJogo(Guid id, Guid idJogo)
    {
        await _mediator.Send(new CriarOrdemCompraCommand(id, idJogo));
        return NoContent();
    }

    [HttpGet("usuario/{id}")]
    public async Task<IActionResult> OrdensPorIdUsuarioAsync(Guid id)
    {
        var result = await _queryService.ObterBibliotecaUsuarioAsync(id, CancellationToken.None);
        return Ok(result);
    }
}
