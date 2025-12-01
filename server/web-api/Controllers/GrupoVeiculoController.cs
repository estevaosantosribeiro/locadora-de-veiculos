using LocadoraDeVeiculos.Aplicacao.ModuloGrupoVeiculo.Commands.Inserir;
using LocadoraDeVeiculos.WebApi.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LocadoraDeVeiculos.WebApi.Controllers;

[ApiController]
[Authorize]
[Route("api/grupo-veiculo")]
public class GrupoVeiculoController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(InserirGrupoVeiculoResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> Inserir(InserirGrupoVeiculoRequest request)
    {
        var resultado = await mediator.Send(request);

        return resultado.ToHttpResponse();
    }
}
