using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using OFF.Domain.DI;
using OFF.Domain.Facades.Middleware;
using OFF.Infrastructure.EntityFramework;
using OFF.Infrastructure.EntityFramework.Entities;
using Stripe;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<OFFDbContext>(x => x.UseSqlServer(@"Server=(LocalDB)\MSSQLLocalDB;Database=OFFDb;Trusted_Connection=True;"));
builder.Services.AddDependency(builder.Configuration);
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<KestrelServerOptions>(options =>
{
    options.AllowSynchronousIO = true;
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//stripe api
//StripeConfiguration.ApiKey = "pk_live_51LHiyMA16oTjMyUCZfO6Z0KarukQx9OOA609shj5bNuTzJEHVPDzaozAEiRoHNAPY9nkaHvxtGRBTzmEeLMBNscy00oirSYRJf";
StripeConfiguration.ApiKey = "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";

using (var scope = app.Services.CreateScope())
{
    var seeder = scope.ServiceProvider.GetService<DbSeeder>();
    seeder.Seed();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<JwtMiddleware>();

app.MapControllers();

app.Run();
