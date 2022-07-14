using Microsoft.EntityFrameworkCore;
using OFF.Domain.Common.Exceptions.Dish;
using OFF.Domain.Common.Models.Dish;
using OFF.Domain.Interfaces.Infrastructure;
using OFF.Infrastructure.EntityFramework.Entities;
using OFF.Infrastructure.EntityFramework.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OFF.Infrastructure.EntityFramework.Services;

public class DishSrv : IDishSrv
{
    private readonly OFFDbContext _dbContext;
    private readonly DishMapper _dishMapper;
    private readonly IStripeSrv _stripeSrv;

    public DishSrv(OFFDbContext dbContext, DishMapper dishMapper, IStripeSrv stripeSrv)
    {
        _dbContext=dbContext;
        _dishMapper=dishMapper;
        _stripeSrv=stripeSrv;
    }

    public DishDTO AddDish(AddDishDTO addDishDTO)
    {
        var isNameTaken = _dbContext.Dishes.FirstOrDefault(d => d.Name == addDishDTO.Name);
        if (isNameTaken != null) throw new NameTakenException();
        var dishToAdd = _dishMapper.Map(addDishDTO);

        var stripeProduct = _stripeSrv.CreateProduct(addDishDTO);

        dishToAdd.Id = stripeProduct.Id;
        //uploading image
        if (addDishDTO.ProductImage != null)
        {
            if (addDishDTO.ProductImage.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    addDishDTO.ProductImage.CopyTo(ms);
                    var fileBytes = ms.ToArray();
                    dishToAdd.ProductImage = fileBytes;
                }
            }
        }
        else
        {
            byte[] image = System.IO.File.ReadAllBytes("noimage.png");

            dishToAdd.ProductImage = image;
        }

        _dbContext.Dishes.Add(dishToAdd);
        _dbContext.SaveChanges();

        AddCategory(addDishDTO.CategoriesName, dishToAdd.Id);


        var dishDTO = _dishMapper.Map(dishToAdd, addDishDTO.CategoriesName);
        return dishDTO;
    }

    public DishDTO EditDish(EditDishDTO editDishDTO)
    {
        var dishToEdit = _dbContext.Dishes.Include(d => d.Categories).FirstOrDefault(d => d.Id == editDishDTO.DishId);
        if (dishToEdit == null) throw new DishNotFoundException();
        if (editDishDTO.Description != null) dishToEdit.Description = editDishDTO.Description;
        if(editDishDTO.ProductImage != null)
        {
            if (editDishDTO.ProductImage.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    editDishDTO.ProductImage.CopyTo(ms);
                    var fileBytes = ms.ToArray();
                    dishToEdit.ProductImage = fileBytes;
                }
            }
        }
        if (editDishDTO.Price != null) dishToEdit.Price = editDishDTO.Price.Value;
        if(editDishDTO.Categories != null)
        {
            //clearing
            foreach(var category in dishToEdit.Categories)
            {
                category.Dishes.Remove(dishToEdit);
            }
            dishToEdit.Categories.Clear();

            //adding new
            AddCategory(editDishDTO.Categories, editDishDTO.DishId);
        }
        _dbContext.SaveChanges();

        DishDTO editedDishDTO = _dishMapper.Map(dishToEdit);
        return editedDishDTO;
    }

    public DishesDTO GetAvailableDishes()
    {
        var list = GetDishes();
        var newList = CheckDishes(list, true);
        return newList;
    }

    public DishesDTO GetAvailableDishesByCategory(GetDishCategoryDTO getDishDTO)
    {
        var list = GetDishesByCategory(getDishDTO);
        var newList = CheckDishes(list, true);
        return newList;
    }

    public DishesDTO GetUnavailableDishes()
    {
        var list = GetDishes();
        var newList = CheckDishes(list, false);
        return newList;
    }

    public DishesDTO GetUnavailableDishesByCategory(GetDishCategoryDTO getDishDTO)
    {
        var list = GetDishesByCategory(getDishDTO);
        var newList = CheckDishes(list, false);
        return newList;
    }

    public DishDTO GetDishById(DishIdDTO getDishDTO)
    {
        var dish = _dbContext.Dishes.Include(d => d.Categories).FirstOrDefault(d => d.Id == getDishDTO.DishId);
        if (dish == null) throw new DishNotFoundException();
        var dishDTO = _dishMapper.Map(dish);
        return dishDTO;
    }

    public DishesDTO GetDishes()
    {
        var categories = _dbContext.Categories.Include(c => c.Dishes).ToArray();

        var listOfDishes = new DishesDTO();
        listOfDishes.Dishes = new List<DishDTO>();

        foreach(var category in categories)
        {
            foreach(var dish in category.Dishes)
            {
                var i = _dishMapper.Map(dish,category.Name);
                listOfDishes.Dishes.Add(i);
            }
        }
        return listOfDishes;
    }

    public DishesDTO GetDishesByCategory(GetDishCategoryDTO getDishDTO)
    {
        var list = _dbContext.Categories.Include(c => c.Dishes).FirstOrDefault(c => c.Name == getDishDTO.Name).Dishes;

        var listOfDishes = new DishesDTO();
        listOfDishes.Dishes = new List<DishDTO>();
        foreach (var dish in list)
        {
            var i = _dbContext.Dishes.Include(d => d.Categories).FirstOrDefault(d => d.Id == dish.Id);
            listOfDishes.Dishes.Add(_dishMapper.Map(i));
        }
        return listOfDishes;
    }

    public DishDTO RemoveDishFromMenu(DishIdDTO dishId)
    {
        var dish = _dbContext.Dishes.Include(d => d.Categories).FirstOrDefault(d => d.Id == dishId.DishId);
        if (dish == null) throw new DishNotFoundException();

        dish.Avaible = false;
        _dbContext.SaveChanges();

        var dishDTO = _dishMapper.Map(dish);
        return dishDTO;

    }

    public DishDTO ReturnDishBackToMenu(DishIdDTO dishId)
    {
        var dish = _dbContext.Dishes.Include(d => d.Categories).FirstOrDefault(d => d.Id == dishId.DishId);
        if (dish == null) throw new DishNotFoundException();

        dish.Avaible = true;
        _dbContext.SaveChanges();

        var dishDTO = _dishMapper.Map(dish);
        return dishDTO;
    }

    private void AddCategory(ICollection<String> Categories, string DishId)
    {
        var dish = _dbContext.Dishes.FirstOrDefault(d => d.Id == DishId);
        if (dish.Categories == null) dish.Categories = new List<Category>();
        foreach (var categoryName in Categories)
        {
            var i = _dbContext.Categories.FirstOrDefault(c => c.Name == categoryName);
            if (i.Dishes == null) i.Dishes = new List<Dish>();
            dish.Categories.Add(i);
            i.Dishes.Add(dish);
        }
        _dbContext.SaveChanges();
    }

    private DishesDTO CheckDishes(DishesDTO list, bool check)
    {
        var newList = new DishesDTO();
        newList.Dishes = new List<DishDTO>();
        foreach (var dish in list.Dishes)
        {
            if (dish.Avaible == check) newList.Dishes.Add(dish);
        }
        return newList;
    }

    public DishDTO AddToOrder(AddToOrderDTO addToOrder)
    {
        var order = _dbContext.Orders.Include(o => o.Dishes)
            .FirstOrDefault(o => o.Id == addToOrder.OrderId);
        var dish = _dbContext.Dishes.Include(d => d.Categories)
            .Include(d => d.Ordered)
            .FirstOrDefault(d => d.Id == addToOrder.DishId);

        var dishOrder = _dbContext.DishOrders.Include(x => x.Dish)
            .Include(x => x.Order)
            .FirstOrDefault(x => x.DishId == addToOrder.DishId && x.OrderId == addToOrder.OrderId);

        if (dishOrder == null)
        {
            dishOrder = new DishOrder
            {
                Dish = dish,
                DishId = dish.Id,
                Order = order,
                OrderId = order.Id,
                Quantity = addToOrder.Quantity ?? 1,
            };
            _dbContext.DishOrders.Add(dishOrder);
            order.Dishes.Add(dishOrder);
            dish.Ordered.Add(dishOrder);
        }
        else dishOrder.Quantity += 1;

        _dbContext.SaveChanges();

        var dishDTO = _dishMapper.Map(dish);
        return dishDTO;
    }
}
