using FluentResults;

namespace LocadoraDeVeiculos.Aplicacao.ModuloGrupoVeiculo;

public class GrupoVeiculoErrorResults
{
    public static Error NomeDuplicadoError(string nome)
    {
        return new Error("Nome duplicado")
            .CausedBy($"Um grupo de veículos com o nome '{nome}' já foi cadastrado")
            .WithMetadata("ErrorType", "BadRequest");
    }
}
