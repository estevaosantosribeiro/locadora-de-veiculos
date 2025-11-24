using LocadoraDeVeiculos.Dominio.Compartilhado;
using LocadoraDeVeiculos.Dominio.ModuloGrupoVeiculo;
using LocadoraDeVeiculos.Infraestrutura.Orm.Compartilhado;

namespace LocadoraDeVeiculos.Infraestrutura.Orm.ModuloGrupoVeiculo;

public class RepositorioGrupoVeiculoEmOrm : RepositorioBase<GrupoVeiculo>, IRepositorioGrupoVeiculo
{
    public RepositorioGrupoVeiculoEmOrm(IContextoPersistencia context) : base(context)
    {
    }
}
