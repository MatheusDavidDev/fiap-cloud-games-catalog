using FCG.Catalog.Application.Commands.OrdemCompraCommand.CriarOrdemCompraCommand;
using FCG.Catalog.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FCG.Catalog.Api.Controllers;


[ApiController]
[Route("api/ordens-compra")]
public class OrdemCompraController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IOrdemCompraQueryService _queryService;

    public OrdemCompraController(IMediator mediator , IOrdemCompraQueryService queryService)
    {
        _mediator = mediator;
        _queryService = queryService;
    }

    [HttpPost("comprar{id}")]
    public async Task<IActionResult> Comprar(Guid id, Guid idJogo)
    {
        await _mediator.Send(new CriarOrdemCompraCommand(id, idJogo));
        return NoContent();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> OrdemPorId(Guid id)
    {
        var result = await _queryService.ObterOrdemPorIdAsync(id, CancellationToken.None);
        return Ok(result);
    }

    [HttpGet("usuario/{id}")]
    public async Task<IActionResult> OrdensPorIdUsuarioAsync(Guid id)
    {
        var result = await _queryService.ObterOrdemUsuarioAsync(id, CancellationToken.None);
        return Ok(result);
    }


}
