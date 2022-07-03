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
    public OrderDTO Map(Order entity)
    {
        return new OrderDTO
        {
            Id = entity.Id,
            CustomerId = entity.CustomerId,
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
}