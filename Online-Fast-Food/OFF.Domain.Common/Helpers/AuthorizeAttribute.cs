using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using OFF.Domain.Common.Models.User;
using Request.Body.Peeker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OFF.Domain.Common.Helpers;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AuthorizeAttribute : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();
        if (allowAnonymous)
            return;

        var user = (UserAuthorizeDTO)context.HttpContext.Items["User"];
        if (user == null)
            context.Result = new JsonResult(new { message = "Unauthorized" })
            { StatusCode = StatusCodes.Status401Unauthorized };

        var body = context.HttpContext.Request.PeekBody();
        //var authorId = JsonConvert.DeserializeObject<CreateConversationDTO>(body).AuthorId;

        //var senderId = JsonConvert.DeserializeObject<FriendDTO>(body).UserId;
        //if (authorId != user?.Id && senderId != user?.Id)
        //    context.Result = new JsonResult(new { message = "Unauthorized" })
        //    { StatusCode = StatusCodes.Status401Unauthorized };
    }
}