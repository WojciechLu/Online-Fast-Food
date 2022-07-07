using OFF.Domain.Common.Models.Order;
using OFF.Domain.Interfaces.Infrastructure;
using OFF.Infrastructure.EntityFramework.Entities;
using OFF.Infrastructure.EntityFramework.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OFF.Infrastructure.EntityFramework.Services;

public class OrderSrv : IOrderSrv
{
    private readonly OFFDbContext _dbContext;
    private readonly OrderMapper _orderMapper;

    public OrderSrv(OFFDbContext dbContext, OrderMapper orderMapper)
    {
        _dbContext=dbContext;
        _orderMapper=orderMapper;
    }

    public OrderDTO CreateOrder(CreateOrderDTO createOrder)
    {
        var user = _dbContext.Users.FirstOrDefault(u => u.Id == createOrder.CustomerId);

        var order = _orderMapper.Create(createOrder, user);

        _dbContext.Orders.Add(order);
        _dbContext.SaveChanges();

        var orderDTO = _orderMapper.Map(order);
        return orderDTO;
    }


}
