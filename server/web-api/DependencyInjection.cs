using LocadoraDeVeiculos.Dominio.Compartilhado;
using LocadoraDeVeiculos.Infraestrutura.Orm.Compartilhado;
using Microsoft.EntityFrameworkCore;

namespace LocadoraDeVeiculos.WebApi;

public static class DependencyInjection
{
    public static void ConfigureDbContext(
        this IServiceCollection services,
        IConfiguration config,
        IWebHostEnvironment environment
    )
    {
        var connectionString = config["SQL_CONNECTION_STRING"];

        if (connectionString == null)
            throw new ArgumentNullException("'SQL_CONNECTION_STRING' não foi fornecida para o ambiente.");

        services.AddDbContext<IContextoPersistencia, LocadoraDeVeiculosDbContext>(optionsBuilder =>
        {
            if (!environment.IsDevelopment())
                optionsBuilder.EnableSensitiveDataLogging(false);

            optionsBuilder.UseSqlServer(connectionString, dbOptions =>
            {
                dbOptions.EnableRetryOnFailure(3);
            });
        });
    }
}
