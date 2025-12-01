using LocadoraDeVeiculos.Aplicacao.ModuloGrupoVeiculo.Commands.Editar;
using LocadoraDeVeiculos.Aplicacao.ModuloGrupoVeiculo.Commands.Excluir;
using LocadoraDeVeiculos.Aplicacao.ModuloGrupoVeiculo.Commands.Inserir;
using LocadoraDeVeiculos.Aplicacao.ModuloGrupoVeiculo.Commands.SelecionarTodos;
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

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(EditarGrupoVeiculoResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> Editar(Guid id, EditarGrupoVeiculoPartalRequest request)
    {
        var editarRequest = new EditarGrupoVeiculoRequest(
            id,
            request.Nome
        );

        var resultado = await mediator.Send(editarRequest);

        return resultado.ToHttpResponse();
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(typeof(ExcluirGrupoVeiculoResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> Excluir(Guid id)
    {
        var excluirRequest = new ExcluirGrupoVeiculoRequest(id);

        var resultado = await mediator.Send(excluirRequest);

        return resultado.ToHttpResponse();
    }

    [HttpGet]
    [ProducesResponseType(typeof(SelecionarGrupoVeiculoResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> SelecionarTodos()
    {
        var resultado = await mediator.Send(new SelecionarGrupoVeiculoRequest());

        return resultado.ToHttpResponse();
    }
}
