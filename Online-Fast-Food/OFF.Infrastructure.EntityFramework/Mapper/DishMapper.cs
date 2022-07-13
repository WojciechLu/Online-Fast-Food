using OFF.Domain.Common.Models.Dish;
using OFF.Domain.Common.Models.Order;
using OFF.Infrastructure.EntityFramework.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OFF.Infrastructure.EntityFramework.Mapper;

public class DishMapper
{
    public DishDTO Map(Dish entity, ICollection<String> CategoryList)
    {
        return new DishDTO
        {
            Id = entity.Id,
            Name = entity.Name,
            Description = entity.Description,
            Price = entity.Price,
            ProductImage = entity.ProductImage,
            Avaible = entity.Avaible,
            Categories = CategoryList
        };
    }
    public DishDTO Map(Dish entity)
    {
        var categoryList = new List<String>();
        foreach (var category in entity.Categories)
        {
            categoryList.Add(category.Name);
        }
        return new DishDTO
        {
            Id = entity.Id,
            Name = entity.Name,
            Description = entity.Description,
            Price = entity.Price,
            ProductImage = entity.ProductImage,
            Avaible = entity.Avaible,
            Categories = categoryList
        };
    }

    public DishDTO Map(Dish entity, String category)
    {
        var categoryList = new List<String>();
        categoryList.Add(category);
        return new DishDTO
        {
            Id = entity.Id,
            Name = entity.Name,
            Description = entity.Description,
            Price = entity.Price,
            ProductImage = entity.ProductImage,
            Avaible = entity.Avaible,
            Categories = categoryList
        };
    }

    public Dish Map(DishDTO dish, byte[] Image)
    {
        return new Dish
        {
            Name = dish.Name,
            Description = dish.Description,
            Price = dish.Price
        };
    }

    public Dish Map(AddDishDTO addDishDTO)
    {
        return new Dish
        {
            Name = addDishDTO.Name,
            Description = addDishDTO.Description,
            Price = addDishDTO.Price,
            Avaible = true
        };
    }

    public List<ItemDTO> Map(Dictionary<Dish, int> orders)
    {
        var list = new List<ItemDTO>();
        foreach(var order in orders)
        {
            var i = new ItemDTO()
            {
                Dish = Map(order.Key),
                Quantity = order.Value,
            };
            list.Add(i);
        }
        return list;
    }
}
