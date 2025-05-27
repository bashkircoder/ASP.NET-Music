using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Music.Data.Repositories;
using Music.Data.Repositories.Interfaces;

namespace Music.Controllers;

public class AdminSongController(IAlbumRepository albumRepository) : Controller
{
    public async Task<IActionResult> Index(int id)
    {
        var album = await albumRepository.GetDetailsByIdAsync(id);
                            
        return View(album);
    }
}