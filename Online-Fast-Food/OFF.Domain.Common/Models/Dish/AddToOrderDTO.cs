using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OFF.Domain.Common.Models.Dish;

public class AddToOrderDTO
{
    public int CustomerId { get; set; }
    public int OrderId { get; set; }
    public string DishId { get; set; }
    public int? Quantity { get; set; }
}
