using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OFF.Domain.Facades;
using OFF.Domain.Interfaces.Facades;
using OFF.Domain.Interfaces.Infrastructure;
using OFF.Infrastructure.EntityFramework;
using OFF.Infrastructure.EntityFramework.Entities;
using OFF.Infrastructure.EntityFramework.Services;

namespace OFF.Domain.DI;

public static class DependencyInjector
{
    public static void AddDependency(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        serviceCollection.AddDbContext<OFFDbContext>(x => x.UseSqlServer(connectionString));
        serviceCollection.AddScoped<DbSeeder>();
        serviceCollection.AddScoped<IAccountSrv, AccountSrv>();
        serviceCollection.AddScoped<IAccountFcd, AccountFcd>();
    }
}
