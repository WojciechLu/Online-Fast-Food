using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OFF.Infrastructure.EntityFramework;
using OFF.Infrastructure.EntityFramework.Entities;

namespace OFF.Domain.DI;

public static class DependencyInjector
{
    public static void AddDependency(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        serviceCollection.AddDbContext<OFFDbContext>(x => x.UseSqlServer(connectionString));
        serviceCollection.AddScoped<DbSeeder>();
    }
}
