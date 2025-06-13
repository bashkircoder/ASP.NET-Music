using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Music.APIKeys;
using Music.Common;
using Music.Data.Repositories.Interfaces;
using Music.Models;
using Music.ViewModels;
using Uploadcare;
using Uploadcare.Models;
using Uploadcare.Upload;

namespace Music.Controllers;

public class AdminArtistController(IArtistRepository artistRepository, IPhotoRepository photoRepository) : Controller
{
    private readonly string _controllerName = ControllerHelper.GetControllerName<AdminArtistController>();
    
    
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

        return RedirectToAction(nameof(AdminArtistController.Index), _controllerName);
    }
    
    public IActionResult Create()
    {
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> Create(AdminArtistCreateViewModel model)
    {
        var urlArtist = await photoRepository.UploadPhotoAsync(model.File);

        var artist = new Artist()
        {
            Name = model.Name,
            UrlImg = urlArtist,
            Albums = []
        };
        await artistRepository.AddAsync(artist);
        
        return RedirectToAction(nameof(AdminArtistController.Index), _controllerName);
    }
    
    
    public async Task<IActionResult> Delete(int id)
    {
        var artistToDelete = await artistRepository.GetDetailsByIdAsync(id);
        await artistRepository.Delete(artistToDelete);
        return RedirectToAction(nameof(AdminArtistController.Index), _controllerName);
    }
    
}