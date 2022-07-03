using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OFF.Domain.Common.Exceptions.Dish;

public class DishNotFoundException : Exception
{
    public DishNotFoundException() : base("Dish with this id was not found") { }
}
