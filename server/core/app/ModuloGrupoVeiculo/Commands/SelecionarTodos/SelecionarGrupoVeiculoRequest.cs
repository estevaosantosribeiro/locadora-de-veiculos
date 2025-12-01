using FluentResults;
using MediatR;

namespace LocadoraDeVeiculos.Aplicacao.ModuloGrupoVeiculo.Commands.SelecionarTodos;

public record SelecionarGrupoVeiculoRequest : IRequest<Result<SelecionarGrupoVeiculoResponse>>;
