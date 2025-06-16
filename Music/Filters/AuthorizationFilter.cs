using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Music.Common;
using Music.Controllers;

namespace Music.Filters;


public class AuthorizationFilter : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        if (context.HttpContext.User.Identity.IsAuthenticated)
        {
            var controllerName = ControllerHelper.GetControllerName<HomeController>();
            context.Result = new RedirectResult($"{controllerName}/{nameof(HomeController.Index)}");
        }
    }
}
