using Microsoft.AspNetCore.Mvc;
using OFF.Domain.Common.Models.User;
using OFF.Domain.Interfaces.Facades;
using OFF.Domain.Interfaces.Infrastructure;

namespace Online_Fast_Food.UI.ASP.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IAccountSrv _accountSrv;
    public AccountController(IAccountSrv accountSrv)
    {
        _accountSrv = accountSrv;
    }

    [HttpPost("register")]
    public ActionResult RegisterUser([FromBody] RegisterDTO registerDTO)
    {
        return Ok(_accountSrv.Register(registerDTO));
    }

    [HttpPost("login")]
    public ActionResult LoginUser([FromBody] LoginDTO loginDTO)
    {
        return Ok(_accountSrv.Login(loginDTO));
    }
}
