using LocadoraDeVeiculos.Dominio.ModuloFuncionario;

namespace LocadoraDeVeiculos.Aplicacao.ModuloFuncionario.Commands.SelecionarTodos;

public record SelecionarFuncionariosDto(Guid Id, string Nome, decimal Salario, DateTime DataAdmissao);

public record SelecionarFuncionariosResponse
{ 
    public required int QuantidadeRegistros { get; init; }
    public required IEnumerable<SelecionarFuncionariosDto> Registros { get; init; }
}
