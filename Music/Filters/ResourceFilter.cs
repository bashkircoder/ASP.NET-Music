using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Music.Filters;

public class ResourceFilter : Attribute, IResourceFilter
{
    private readonly Dictionary<string, IActionResult> _cache = [];
    
    public void OnResourceExecuting(ResourceExecutingContext context)
    {
        var path = context.HttpContext.Request.Path;
        if (_cache.TryGetValue(path, out var cacheResult))
        {
            context.Result = cacheResult;
        }
    }

    public void OnResourceExecuted(ResourceExecutedContext context)
    {
        var path = context.HttpContext.Request.Path;
        if (HttpMethods.IsGet(context.HttpContext.Request.Method) && context.Result != null)
        {
            if (!_cache.ContainsKey(path))
            {
                _cache[path] = context.Result;
            }
        }
    }
}