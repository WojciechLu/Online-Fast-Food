using OFF.Domain.Common.Models.Order;
using OFF.Domain.Common.Models.Payment;
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
    private readonly IStripeSrv _stripeSrv;

    public OrderSrv(OFFDbContext dbContext, OrderMapper orderMapper, IStripeSrv stripeSrv)
    {
        _dbContext=dbContext;
        _orderMapper=orderMapper;
        _stripeSrv=stripeSrv;
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

    public async Task<CreateCheckoutSessionResponse> PayForOrder(OrderIdDTO orderIdDTO)
    {
        var dishOrder = _dbContext.DishOrders.Where(x => x.OrderId == orderIdDTO.Id).ToList();
        var payForOrder = new CreateCheckoutSessionRequest();
        payForOrder.DictionaryPrice = new Dictionary<string, int>();

        foreach (var order in dishOrder)
        {
            var i = _stripeSrv.GetProduct(order.DishId).DefaultPriceId;
            payForOrder.DictionaryPrice.Add(i, order.Quantity);
        }
        var result = await _stripeSrv.CreateCheckOutAsync(payForOrder);
        return result;
    }
}
