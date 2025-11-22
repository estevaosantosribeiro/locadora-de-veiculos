using LocadoraDeVeiculos.Dominio.Compartilhado;
using LocadoraDeVeiculos.Infraestrutura.Orm.Compartilhado;
using LocadoraDeVeiculos.WebApi.Filters;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Text.Json.Serialization;

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

    public static void ConfigureControllersWithFilters(this IServiceCollection services)
    {
        services.AddControllers(options =>
        {
            options.Filters.Add<ResponseWrapperFilter>();
        }).AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
    }

    public static void ConfigureSerilog(this IServiceCollection services, ILoggingBuilder logging, IConfiguration config)
    {
        Log.Logger = new LoggerConfiguration()
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .WriteTo.NewRelicLogs(
                endpointUrl: "https://log-api.newrelic.com/log/v1",
                applicationName: "locadora-de-veiculos-api",
                licenseKey: config["NEWRELIC_LICENSE_KEY"]
            )
            .CreateLogger();

        logging.ClearProviders();

        services.AddLogging(builder => builder.AddSerilog(dispose: true));
    }
}
