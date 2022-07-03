using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OFF.Domain.Common.Models.Dish;

public class DishIdDTO
{
    [Required] public int UserId { get; set; }
    public int Id { get; set; }
}
