using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OFF.Domain.Common.Models.Dish;

public class EditDishDTO
{
    public int AdminId { get; set; }
    [Required] public int Id { get; set; }
    public string? Description { get; set; }
    public IFormFile? ProductImage { get; set; }
    public float? Price { get; set; }
    public ICollection<string>? Categories { get; set; }
}
