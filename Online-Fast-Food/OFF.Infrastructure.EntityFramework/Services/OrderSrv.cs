using Microsoft.EntityFrameworkCore;
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
    private readonly DishMapper _dishMapper;
    private readonly IStripeSrv _stripeSrv;

    public OrderSrv(OFFDbContext dbContext, OrderMapper orderMapper, IStripeSrv stripeSrv, DishMapper dishMapper)
    {
        _dbContext=dbContext;
        _orderMapper=orderMapper;
        _stripeSrv=stripeSrv;
        _dishMapper=dishMapper;
    }

    public OrderDTO CreateOrder(CreateOrderDTO createOrder)
    {
        var user = _dbContext.Users.FirstOrDefault(u => u.Id == createOrder.CustomerId);

        var order = _orderMapper.Create(createOrder, user);

        _dbContext.Orders.Add(order);
        _dbContext.SaveChanges();

        var emptyList = new List<ItemDTO>();

        var orderDTO = _orderMapper.Map(order, emptyList);
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

    public OrderDTO CompleteOrder(OrderIdDTO orderIdDTO)
    {
        var order = _dbContext.Orders.Include(o => o.Dishes).FirstOrDefault(o => o.Id == orderIdDTO.Id && o.Completed == false);
        order.Completed = true;

        var dishesOnOrder = _dbContext.DishOrders.Include(x => x.Dish).Where(x => x.OrderId == orderIdDTO.Id);
        var itemList = new Dictionary<Dish, int>();

        foreach(var dish in dishesOnOrder)
        {
            var i = _dbContext.Dishes.Include(d => d.Categories).FirstOrDefault(d => d.Id == dish.DishId);
            itemList.Add(i, dish.Quantity);
            order.TotalPrice += i.Price * dish.Quantity;
        }

        _dbContext.Orders.Update(order);
        _dbContext.SaveChanges();
        var itemListDTO = _dishMapper.Map(itemList);
        var orderDTO = _orderMapper.Map(order, itemListDTO);
        return orderDTO;
    }
}
