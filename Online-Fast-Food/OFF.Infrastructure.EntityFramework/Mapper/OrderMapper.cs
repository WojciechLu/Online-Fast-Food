using OFF.Domain.Common.Models.Order;
using OFF.Infrastructure.EntityFramework.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OFF.Infrastructure.EntityFramework.Mapper;

public class OrderMapper
{
    public OrderDTO Map(Order entity, List<ItemDTO> items)
    {

        return new OrderDTO
        {
            OrderId = entity.Id,
            CustomerId = entity.CustomerId,
            Dishes = items,
        };
    }

    public Order Create(CreateOrderDTO createOrder, User user)
    {
        return new Order
        {
            Customer = user,
            CustomerId = user.Id,
            Dishes = new List<DishOrder>(),
            Completed = false
        };
    }
    //public OrderDTO Map(Order entity, List<ItemDTO> items)
    //{

    //    return new OrderDTO
    //    {
    //        OrderId = entity.Id,
    //        CustomerId = entity.CustomerId,
    //        Dishes = items,
    //    };
    //}
}