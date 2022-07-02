using Microsoft.AspNetCore.Http;
using OFF.Domain.Common.Utils;
using OFF.Domain.Interfaces.Facades;
using OFF.Domain.Interfaces.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OFF.Domain.Facades.Middleware;

public class JwtMiddleware
{
    private readonly RequestDelegate _next;

    public JwtMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context, IJwtUtils jwtUtils, IAccountSrv accountSrv)
    {
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
        var userId = jwtUtils.ValidateToken(token);
        if (userId != null)
        {
            var user = accountSrv.GetById(userId);
            context.Items["User"] = user;
        }

        await _next(context);
    }
}