﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OFF.Domain.Common.Models.Dish;

public class GetDishCategoryDTO
{
    public int CustomerId { get; set; }
    public string CategoryName { get; set; }
}
