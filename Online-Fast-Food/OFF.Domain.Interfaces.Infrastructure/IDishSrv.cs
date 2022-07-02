using OFF.Domain.Common.Models.Dish;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OFF.Domain.Interfaces.Infrastructure;

public interface IDishSrv
{
    DishDTO AddDish(AddDishDTO addDishDTO);
}
