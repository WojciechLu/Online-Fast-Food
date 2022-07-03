using OFF.Domain.Common.Models.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OFF.Domain.Interfaces.Infrastructure;

public interface IOrderSrv
{
    OrderDTO CreateOrder(CreateOrderDTO createOrder);
}
