using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OFF.Domain.Common.Models.Order;
public class MakeOrderDTO
{
    public int CustomerId { get; set; }
    public Dictionary<string, int> ProductIdAndQuantity { get; set; }
}
