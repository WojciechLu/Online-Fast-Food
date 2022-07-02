using OFF.Infrastructure.EntityFramework.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OFF.Infrastructure.EntityFramework;

public class DbSeeder
{
    private readonly OFFDbContext _dbContext;

    public DbSeeder(OFFDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Seed()
    {
        if (_dbContext.Database.CanConnect())
        {
            if (!_dbContext.Roles.Any())
            {
                var roles = GetRoles();
                _dbContext.Roles.AddRange(roles);
                _dbContext.SaveChanges();
            }
            if (!_dbContext.Categories.Any())
            {
                var categories = GetCategories();
                _dbContext.Categories.AddRange(categories);
                _dbContext.SaveChanges();
            }
        }
    }

    private IEnumerable<Role> GetRoles()
    {
        var roles = new List<Role>
        {
            new()
            {
                Name = "User"
            },
            new()
            {
                Name = "Admin"
            }
        };
        return roles;
    }

    private IEnumerable<Category> GetCategories()
    {
        var categories = new List<Category>
        {
            new()
            {

                Name = "Vegetarian"
            },
            new()
            {
                Name = "Vegan"
            },
            new()
            {
                Name = "Meat"
            },
            new()
            {
                Name = "Main course"
            },
            new()
            {
                Name = "Soup"
            },
            new()
            {
                Name = "Pasta"
            },
            new()
            {
                Name = "Salad"
            },
            new()
            {
                Name = "BBQ food"
            },
            new()
            {
                Name = "Sandwiches"
            },
            new()
            {
                Name = "Cakes, cookies & pies"
            },
            new()
            {
                Name = "Snacks"
            },
            new()
            {
                Name = "Drinks"
            }
        };
        return categories;
    }
}