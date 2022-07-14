using OFF.Domain.Common.Models.Order;
using OFF.Domain.Common.Models.Payment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OFF.Domain.Interfaces.Infrastructure;

public interface IOrderSrv
{
    OrderDTO CreateOrder(CreateOrderDTO createOrder);
    Task<CreateCheckoutSessionResponse> PayForOrder(OrderIdDTO orderIdDTO);
    OrderDTO CompleteOrder(OrderIdDTO orderIdDTO);
    OrderDTO MakeOrder(MakeOrderDTO makeOrderDTO);
}
