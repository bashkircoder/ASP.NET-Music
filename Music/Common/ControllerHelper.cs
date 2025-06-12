using Microsoft.AspNetCore.Mvc;

namespace Music.Common;

public class ControllerHelper
{
    public static string GetControllerName<T>()  where T : Controller
    {
        return typeof(T).Name.Replace(nameof(Controller), string.Empty);
    }
}