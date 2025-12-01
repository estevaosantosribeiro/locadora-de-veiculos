using FluentResults;
using MediatR;

namespace LocadoraDeVeiculos.Aplicacao.ModuloGrupoVeiculo.Commands.Inserir;

public record InserirGrupoVeiculoRequest(string nome) : IRequest<Result<InserirGrupoVeiculoResponse>>;
