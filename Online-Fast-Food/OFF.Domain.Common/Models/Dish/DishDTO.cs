using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OFF.Domain.Common.Models.Dish;

public class DishDTO
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public byte[] ProductImage { get; set; }
    public float Price { get; set; }
    public bool Avaible { get; set; }
    public ICollection<String> Categories { get; set; }
}
