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
            if (!_dbContext.Roles.Any())
            {
                var roles = GetRoles();
                _dbContext.Roles.AddRange(roles);
                _dbContext.SaveChanges();
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
}