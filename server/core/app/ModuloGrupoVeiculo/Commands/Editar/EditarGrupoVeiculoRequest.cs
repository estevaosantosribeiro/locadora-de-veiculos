using FluentResults;
using MediatR;

namespace LocadoraDeVeiculos.Aplicacao.ModuloGrupoVeiculo.Commands.Editar;

public record EditarGrupoVeiculoPartalRequest(string Nome);

public record EditarGrupoVeiculoRequest(Guid Id, string Nome) : IRequest<Result<EditarGrupoVeiculoResponse>>;
