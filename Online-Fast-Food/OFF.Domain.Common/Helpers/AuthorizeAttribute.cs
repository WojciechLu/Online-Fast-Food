using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using OFF.Domain.Common.Models.Dish;
using OFF.Domain.Common.Models.Order;
using OFF.Domain.Common.Models.User;
using Request.Body.Peeker;

namespace OFF.Domain.Common.Helpers;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AuthorizeAttribute : Attribute, IAuthorizationFilter
{
    public string Roles { get; set; }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var user = (UserAuthorizeDTO)context.HttpContext.Items["User"];
        if (user == null)
            context.Result = new JsonResult(new { message = "Unauthorized" })
            { StatusCode = StatusCodes.Status401Unauthorized };

        if (Roles != null)
        {
            if (!Roles.Contains(user?.Role))
                context.Result = new JsonResult(new { message = "Unauthorized" })
                { StatusCode = StatusCodes.Status401Unauthorized };
        }
        int adminId, customerId;
        var body = context.HttpContext.Request.PeekBody();
        if (checkIfFormData(body))
        {
            adminId = getIdFromForm(body, "AdminId");
            customerId = getIdFromForm(body, "CustomerId");
        }
        else
        {
            customerId = JsonConvert.DeserializeObject<CreateOrderDTO>(body).CustomerId;
            adminId = JsonConvert.DeserializeObject<AddDishDTO>(body).AdminId;
        }
        if (adminId != user?.Id && customerId != user?.Id)
            context.Result = new JsonResult(new { message = "Unauthorized" })
            { StatusCode = StatusCodes.Status401Unauthorized };
    }

    private bool checkIfFormData(string body)
    {
        if (body.Contains("----------------------------")) return true;
        return false;
    }

    private int getIdFromForm(string body, string cell)
    {
        string[] separators = new string[] { "\r", "\n", "Content-Disposition: form-data; name=", "\"" };
        string[] subs = body.Split(separators, StringSplitOptions.RemoveEmptyEntries);
        int index = Array.IndexOf(subs, cell);
        if (index != -1)
        {
            var stringId = subs[index + 1];
            int intId = Int32.Parse(stringId);
            return intId;
        }
        return -1;
    }
}
public class AllowAnonymousAttribute : Attribute
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();
        if (allowAnonymous)
            return;
    }
}

//public class FormDataMapper
//{
//    public bool checkIfFormData(string body)
//    {
//        if (body.Contains("----------------------------")) return true;
//        return false;
//    }

//    public int getIdFromForm(string body, string cell)
//    {
//        string[] separators = new string[] { "\r", "\n", "Content-Disposition: form-data; name=", "\"" };
//        string[] subs = body.Split(separators, StringSplitOptions.RemoveEmptyEntries);
//        int index = findIndex(subs, cell);
//        if (index != -1)
//        {
//            var stringId = subs[index + 1];
//            int intId = Int32.Parse(stringId);
//            return intId;
//        }
//        return -1;
    
//}