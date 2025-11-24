using LocadoraDeVeiculos.Dominio.Compartilhado;

namespace LocadoraDeVeiculos.Dominio.ModuloGrupoVeiculo;

public class GrupoVeiculo : EntidadeBase
{
    public string Nome { get; set; }

    public GrupoVeiculo() { }

    public GrupoVeiculo(string Nome) : this()
    {
        this.Nome = Nome;
    }
}
