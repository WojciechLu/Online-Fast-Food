using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OFF.Domain.Common.Models.Order;

public class OrderDTO
{
    public int OrderId { get; set; }
    public int CustomerId { get; set; }
    public List<ItemDTO> Dishes { get; set; }
}
