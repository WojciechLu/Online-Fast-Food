using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OFF.Domain.DI;
using OFF.Domain.Facades.Middleware;
using OFF.Infrastructure.EntityFramework;
using OFF.Infrastructure.EntityFramework.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<OFFDbContext>(x => x.UseSqlServer(@"Server=(LocalDB)\MSSQLLocalDB;Database=OFFDb;Trusted_Connection=True;"));
builder.Services.AddDependency(builder.Configuration);
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

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
