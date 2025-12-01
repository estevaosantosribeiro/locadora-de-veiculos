namespace LocadoraDeVeiculos.Aplicacao.ModuloGrupoVeiculo.Commands.SelecionarTodos;

public record SelecionarGrupoVeiculoDto(
    Guid Id,
    string Nome
);

public record SelecionarGrupoVeiculoResponse
{
    public required int QuantidadeRegistros { get; init; }
    public required IEnumerable<SelecionarGrupoVeiculoDto> Registros { get; init; }
}
