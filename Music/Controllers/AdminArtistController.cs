using Microsoft.AspNetCore.Mvc;
using Music.Data.Repositories.Interfaces;
using Music.Models;

namespace Music.Controllers;

public class AdminArtistController(IArtistRepository artistRepository) : Controller
{
    public async Task<IActionResult> Index()
    {
        var artist = await artistRepository.GetAllAsync();

        return View(artist);
    }
    
    public async Task<IActionResult> Update(int id)
    {
        var artist = await artistRepository.GetDetailsByIdAsync(id);

        return View(artist);
    }
    
    [HttpPost]
    public async Task<IActionResult> Update(Artist artist)
    {
        await artistRepository.UpdateAsync(artist);

        return RedirectToAction("Index", "AdminArtist");
    }
    
    public IActionResult Create()
    {
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> Create(Artist artist)
    {
        await artistRepository.AddAsync(artist);
        return RedirectToAction("Index", "AdminArtist");
    }
    
    
    public async Task<IActionResult> Delete(int id)
    {
        var artistToDelete = await artistRepository.GetDetailsByIdAsync(id);
        artistRepository.Delete(artistToDelete);
        return RedirectToAction("Index", "AdminArtist");
    }
    
}