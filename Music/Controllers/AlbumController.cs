using Microsoft.AspNetCore.Mvc;
using Music.Data.Repositories.Interfaces;

namespace Music.Controllers;

public class AlbumController(IAlbumRepository albumRepository) : Controller
{
    public async Task<IActionResult> Index(int id)
    {
        var albums = await albumRepository.GetAlbumsByArtistIdAsync(id);

        return View(albums);
    }
    
}