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
    DishDTO EditDish(EditDishDTO editDishDTO);
    DishDTO GetDishById(DishIdDTO getDishDTO);
    DishesDTO GetDishesByCategory(GetDishCategoryDTO getDishDTO);
    DishesDTO GetDishes();
    DishesDTO GetAvaibleDishesByCategory(GetDishCategoryDTO getDishDTO);
    DishesDTO GetAvaibleDishes();

    DishDTO RemoveDishFromMenu(DishIdDTO dishId);
    DishDTO ReturnDishBackToMenu(DishIdDTO dishId);
}
