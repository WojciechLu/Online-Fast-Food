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
    GetMenuDTO GetDishesByCategory(GetDishCategoryDTO getDishDTO);
    GetMenuDTO GetDishes();
    GetMenuDTO GetAvailableDishesByCategory(GetDishCategoryDTO getDishDTO);
    GetMenuDTO GetAvailableDishes();
    GetMenuDTO GetUnavailableDishesByCategory(GetDishCategoryDTO getDishDTO);
    GetMenuDTO GetUnavailableDishes();

    DishDTO RemoveDishFromMenu(DishIdDTO dishId);
    DishDTO ReturnDishBackToMenu(DishIdDTO dishId);

    DishDTO AddToOrder(AddToOrderDTO addToOrder);
}
