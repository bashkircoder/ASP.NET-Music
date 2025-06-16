using System.Text;
using Microsoft.AspNetCore.Mvc.Filters;
using Music.Logger.Interfaces;

namespace Music.Filters;

public class ActionFilter(IMusicLogger logger) : Attribute, IActionFilter
{
    
    public void OnActionExecuting(ActionExecutingContext context)
    {
        logger.WriteLog($"Начало выполнения конечной точки: {context.ActionDescriptor.DisplayName}");
        if (context.ActionArguments.Count > 0)
        {
            var argsAndParams = new StringBuilder();
            foreach (var args in context.ActionArguments)
            {
             argsAndParams.Append($"{args.Key}={args.Value}&");   
            }
            logger.WriteLog(argsAndParams.ToString());
        }
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        logger.WriteLog($"Завершение выполнения конечной точки: {context.ActionDescriptor.DisplayName}");
    }
}