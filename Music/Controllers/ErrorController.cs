using Microsoft.AspNetCore.Mvc;

namespace Music.Controllers;

public class ErrorController : Controller
{
    public IActionResult NotFound()
    {
        return View();
    }
}