using Microsoft.AspNetCore.Http;
using OFF.Domain.Common.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OFF.Domain.Common.Models.Dish;

public class AddDishDTO
{
    [Required] public string Name { get; set; }
    public string? Description { get; set; }
    public IFormFile? ProductImage { get; set; }
    [Required] public float Price { get; set; }
    public ICollection<String>? CategoriesName { get; set; }
}
