using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using OFF.Domain.Common.Models.User;
using OFF.Domain.Common.Utils;
using OFF.Domain.Facades;
using OFF.Domain.Interfaces.Facades;
using OFF.Domain.Interfaces.Infrastructure;
using OFF.Infrastructure.EntityFramework;
using OFF.Infrastructure.EntityFramework.Entities;
using OFF.Infrastructure.EntityFramework.Services;
using System.Text;

namespace OFF.Domain.DI;

public static class DependencyInjector
{
    public static void AddDependency(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        serviceCollection.AddDbContext<OFFDbContext>(x => x.UseSqlServer(connectionString));
        serviceCollection.AddScoped<DbSeeder>();
        serviceCollection.AddScoped<IAccountFcd, AccountFcd>();
        serviceCollection.AddScoped<IAccountSrv, AccountSrv>();
        serviceCollection.AddScoped<IDishSrv, DishSrv>();
        serviceCollection.AddScoped<IJwtUtils, JwtUtils>();

        //JWT
        var authenticationSettings = new AuthenticationSettings();

        configuration.GetSection("Authentication").Bind(authenticationSettings);

        serviceCollection.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = "Bearer";
            options.DefaultScheme = "Bearer";
            options.DefaultChallengeScheme = "Bearer";
        }).AddJwtBearer(cfg =>
        {
            cfg.RequireHttpsMetadata = false;
            cfg.SaveToken = true;
            cfg.TokenValidationParameters = new TokenValidationParameters
            {
                ValidIssuer = authenticationSettings.JwtIssuer,
                ValidAudience = authenticationSettings.JwtIssuer,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSettings.JwtKey))
            };
        });

        serviceCollection.AddSingleton(authenticationSettings);
    }
}
