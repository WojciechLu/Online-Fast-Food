﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OFF.Domain.Common.Models;

public class EditDishDTO
{
    [Required] public int Id { get; set; }
    public string? Description { get; set; }
    public IFormFile? ProductImage { get; set; }
    public float? Price { get; set; }
    public ICollection<String>? Categories { get; set; }
}
