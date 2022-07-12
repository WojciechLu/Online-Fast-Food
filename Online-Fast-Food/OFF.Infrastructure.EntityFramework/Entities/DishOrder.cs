using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OFF.Infrastructure.EntityFramework.Entities;

public class DishOrder
{
    public string DishId { get; set; }

    public Dish Dish { get; set; }
    public int OrderId { get; set; }
    public Order Order { get; set; }
    public int Quantity { get; set; }
}
