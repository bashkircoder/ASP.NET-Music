using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Music.Common;
using Music.Controllers;
using Music.Exceptions;

namespace Music.Filters;

public class ExceptionFilter : Attribute,IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is NotFoundException notFoundException)
        {
            context.Result = new NotFoundObjectResult(new
            {
                Status = 404,
                Title = "Not Found",
                Detail = notFoundException.Message,
                Timestamp = DateTime.UtcNow
            });
        }
        context.ExceptionHandled = true;
        var controllerName = ControllerHelper.GetControllerName<ErrorController>();
        context.Result = new RedirectToActionResult(nameof(ErrorController.NotFound),controllerName,"");
    }
}