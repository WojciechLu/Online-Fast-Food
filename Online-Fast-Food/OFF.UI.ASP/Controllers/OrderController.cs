using Microsoft.AspNetCore.Mvc;
using OFF.Domain.Interfaces.Infrastructure;

namespace Online_Fast_Food.UI.ASP.Controllers;

public class OrderController : ControllerBase
{
    private readonly IOrderSrv _orderSrv;
    public OrderController(IOrderSrv orderSrv)
    {
        _orderSrv=orderSrv;
    }
}
