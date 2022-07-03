using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OFF.Infrastructure.EntityFramework.Entities;

public class Dish
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public byte[] ProductImage { get; set; }
    public float Price { get; set; }
    public ICollection<Category> Categories { get; set; } 
    public ICollection<Order> Ordered { get; set; }
}
