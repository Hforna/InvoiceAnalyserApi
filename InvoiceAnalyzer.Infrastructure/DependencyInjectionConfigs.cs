using InvoiceAnalyzer.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InvoiceAnalyzer.Infrastructure;

public static class DependencyInjectionConfigs
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        Persistence(services, configuration);
    }

    static void Persistence(IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<DataContext>(d => d.UseSqlServer(configuration.GetConnectionString("sqlserver")));
    }
}