﻿using OFF.Domain.Common.Models.Dish;
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
    DishesDTO GetAvailableDishesByCategory(GetDishCategoryDTO getDishDTO);
    DishesDTO GetAvailableDishes();
    DishesDTO GetUnavailableDishesByCategory(GetDishCategoryDTO getDishDTO);
    DishesDTO GetUnavailableDishes();

    DishDTO RemoveDishFromMenu(DishIdDTO dishId);
    DishDTO ReturnDishBackToMenu(DishIdDTO dishId);

    DishDTO AddToOrder(AddToOrderDTO addToOrder);
}
