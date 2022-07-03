using OFF.Domain.Common.Models.Dish;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OFF.Domain.Common.Models.Order;

public class ItemDTO
{
    public DishDTO Dish { get; set; }
    public int Quantity { get; set; }
}
