using LocadoraDeVeiculos.Infraestrutura.Orm.Compartilhado;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace LocadoraDeVeiculos.Testes.Integracao.Compartilhado;

public static class LocadoraDeVeiculosDbContextFactory
{
    public static LocadoraDeVeiculosDbContext CriarDbContext()
    {
        var configuracao = CriarConfiguracao();

        var connectionString = configuracao["SQL_CONNECTION_STRING"];

        var options = new DbContextOptionsBuilder<LocadoraDeVeiculosDbContext>()
            .UseSqlServer(connectionString)
            .Options;

        var dbContext = new LocadoraDeVeiculosDbContext(options);

        dbContext.Database.EnsureDeleted();
        dbContext.Database.EnsureCreated();

        return dbContext;
    }

    private static IConfiguration CriarConfiguracao()
    {
        var assembly = typeof(LocadoraDeVeiculosDbContextFactory).Assembly;

        return new ConfigurationBuilder()
            .AddUserSecrets(assembly)
            .Build();
    }
}
