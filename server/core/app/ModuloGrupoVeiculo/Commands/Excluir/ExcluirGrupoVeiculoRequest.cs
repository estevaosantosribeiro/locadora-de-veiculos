using FluentResults;
using MediatR;

namespace LocadoraDeVeiculos.Aplicacao.ModuloGrupoVeiculo.Commands.Excluir;

public record ExcluirGrupoVeiculoRequest(Guid Id) : IRequest<Result<ExcluirGrupoVeiculoResponse>>;
