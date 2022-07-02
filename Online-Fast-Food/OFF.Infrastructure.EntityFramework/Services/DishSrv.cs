using OFF.Domain.Common.Exceptions.Dish;
using OFF.Domain.Common.Models.Dish;
using OFF.Domain.Interfaces.Infrastructure;
using OFF.Infrastructure.EntityFramework.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OFF.Infrastructure.EntityFramework.Services;

public class DishSrv : IDishSrv
{
    private readonly OFFDbContext _dbContext;

    public DishSrv(OFFDbContext dbContext)
    {
        _dbContext=dbContext;
    }

    public DishDTO AddDish(AddDishDTO addDishDTO)
    {
        var isNameTaken = _dbContext.Dishes.FirstOrDefault(d => d.Name == addDishDTO.Name);
        if (isNameTaken != null) throw new NameTakenException();
        var dishToAdd = new Dish();
        var dishDTO = new DishDTO();
        //var categories = _dbContext.DishCategories...
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

        dishToAdd.Name = addDishDTO.Name;
        dishToAdd.Description = addDishDTO.Description;
        dishToAdd.Price = addDishDTO.Price;
        //dishToAdd.Categories = list

        _dbContext.Dishes.Add(dishToAdd);
        _dbContext.SaveChanges();

        dishDTO.Id = dishToAdd.Id;
        dishDTO.Name = dishToAdd.Name;
        dishDTO.Description = dishToAdd.Description;
        dishDTO.ProductImage = dishToAdd.ProductImage;
        dishDTO.Price = dishToAdd.Price;
        //dishDTO.Categories = list

        return dishDTO;
    }
}
