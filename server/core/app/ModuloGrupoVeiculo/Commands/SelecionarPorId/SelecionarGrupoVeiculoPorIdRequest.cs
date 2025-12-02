using FluentResults;
using MediatR;

namespace LocadoraDeVeiculos.Aplicacao.ModuloGrupoVeiculo.Commands.SelecionarPorId;

public record SelecionarGrupoVeiculoPorIdRequest(Guid Id) : IRequest<Result<SelecionarGrupoVeiculoPorIdResponse>>;
