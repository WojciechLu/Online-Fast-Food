using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace OFF.Infrastructure.EntityFramework.Entities;

public class OFFDbContext : DbContext
{
    public OFFDbContext(DbContextOptions options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Dish> Dishes { get; set; }
    //public DbSet<DishCategory> DishCategories { get; set; }
    public DbSet<Category> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .Property(x => x.Email)
            .IsRequired();
        modelBuilder.Entity<User>()
            .Property(x => x.FirstName)
            .IsRequired();
        modelBuilder.Entity<User>()
            .Property(x => x.LastName)
            .IsRequired();
        modelBuilder.Entity<User>()
            .Property(x => x.PasswordHash)
            .IsRequired();

        modelBuilder.Entity<Dish>()
            .Property(x => x.Name)
            .IsRequired();
        modelBuilder.Entity<Dish>()
            .Property(x => x.Price)
            .IsRequired();

        modelBuilder.Entity<Category>()
            .Property(x => x.Name)
            .IsRequired();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        var connectionString = configuration.GetConnectionString("DefaultConnection");
        optionsBuilder.UseSqlServer(connectionString);
    }
}
