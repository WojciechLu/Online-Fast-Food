using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OFF.Infrastructure.EntityFramework.Entities;

public class Order
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public User Customer { get; set; }

    public ICollection<Dish> Dishes { get; set; }
}
