using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OFF.Domain.Common.Models.Dish;
public class GetMenuDTO
{
    public Dictionary<string, int> DishesByCategory { get; set; }
}
