using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Persistence.Abstractions;

public static class PersistenceExtensions
{
    public static IServiceCollection AddPostgres(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        return services
           .AddDbContext<EnsekContext>(options => options
                .UseNpgsql(configuration.GetConnectionString("Database"))
                .UseSnakeCaseNamingConvention())
           .AddTransient<IEnsekRepository, EnsekRepository>();
    }

    public static IHealthChecksBuilder AddPostgres(this IHealthChecksBuilder builder)
        => builder.AddDbContextCheck<EnsekContext>();
}