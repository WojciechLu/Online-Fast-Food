using Microsoft.AspNetCore.Mvc;
using OFF.Domain.Common.Helpers;
using OFF.Domain.Common.Models.Order;
using OFF.Domain.Interfaces.Infrastructure;

namespace Online_Fast_Food.UI.ASP.Controllers;

[Route("api/[controller]")]
[ApiController]
//[Authorize(Roles = "Admin, User")]
[Authorize]
public class OrderController : ControllerBase
{
    private readonly IOrderSrv _orderSrv;
    public OrderController(IOrderSrv orderSrv)
    {
        _orderSrv=orderSrv;
    }

    [HttpPost("createOrder")]
    public ActionResult CreateOrder([FromBody] CreateOrderDTO createOrderDTO)
    {
        return Ok(_orderSrv.CreateOrder(createOrderDTO));
    }

    [HttpPost("payForOrder")]
    public async Task<ActionResult> PayForOrder([FromBody] OrderIdDTO orderIdDTO)
    {
        var result = await _orderSrv.PayForOrder(orderIdDTO);
        return Ok(result);
    }
}
