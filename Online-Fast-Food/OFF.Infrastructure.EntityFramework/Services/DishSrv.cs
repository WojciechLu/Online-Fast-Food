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

    public DishSrv(OFFDbContext dbContext, DishMapper dishMapper)
    {
        _dbContext=dbContext;
        _dishMapper=dishMapper;
    }

    public DishDTO AddDish(AddDishDTO addDishDTO)
    {
        var isNameTaken = _dbContext.Dishes.FirstOrDefault(d => d.Name == addDishDTO.Name);
        if (isNameTaken != null) throw new NameTakenException();
        var dishToAdd = _dishMapper.Map(addDishDTO);

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
        var dishToEdit = _dbContext.Dishes.Include(d => d.Categories).FirstOrDefault(d => d.Id == editDishDTO.Id);
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
            AddCategory(editDishDTO.Categories, editDishDTO.Id);
        }
        _dbContext.SaveChanges();

        DishDTO editedDishDTO = _dishMapper.Map(dishToEdit);
        return editedDishDTO;
    }

    public DishesDTO GetAvaibleDishes()
    {
        var list = GetDishes();
        var newList = AvaibleDishes(list);
        return newList;
    }

    public DishesDTO GetAvaibleDishesByCategory(GetDishCategoryDTO getDishDTO)
    {
        var list = GetDishesByCategory(getDishDTO);
        var newList = AvaibleDishes(list);
        return newList;
    }

    public DishDTO GetDishById(GetDishIdDTO getDishDTO)
    {
        var dish = _dbContext.Dishes.Include(d => d.Categories).FirstOrDefault(d => d.Id == getDishDTO.Id);
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

    private void AddCategory(ICollection<String> Categories, int DishId)
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

    private DishesDTO AvaibleDishes(DishesDTO list)
    {
        var newList = new DishesDTO();
        newList.Dishes = new List<DishDTO>();
        foreach (var dish in list.Dishes)
        {
            if (dish.Avaible == true) newList.Dishes.Add(dish);
        }
        return newList;
    }
}
