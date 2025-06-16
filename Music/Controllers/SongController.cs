using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Music.Data.Repositories;
using Music.Data.Repositories.Interfaces;
using Music.Exceptions;
using Music.Filters;

namespace Music.Controllers;

[ResourceFilter]
[ExceptionFilter]
public class SongController(IAlbumRepository albumRepository) : Controller
{
    public async Task<IActionResult> Index(int id)
    {
        var album = await albumRepository.GetDetailsByIdAsync(id);

        //throw new NotFoundException("Page not found");
                            
        return View(album);
    }
}