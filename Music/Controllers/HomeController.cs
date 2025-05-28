using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Music.Data.Repositories;
using Music.Data.Repositories.Interfaces;

namespace Music.Controllers;

public class HomeController(IArtistRepository artistRepository) : Controller
{
    public async Task<IActionResult> Index(string? artistName = null)
    {
        var artists = await artistRepository.GetAllAsync();
        
        if (artistName != null)
        {
            var filteredArtists = artists.Where(a => a.Name.Contains(artistName, StringComparison.InvariantCultureIgnoreCase)).ToList();
            return View(filteredArtists);
        }
        
        return View(artists);
    }
}