using Microsoft.AspNetCore.Mvc;
using Music.Common;
using Music.Data.Repositories.Interfaces;
using Music.Filters;
using Music.Models;
using Music.ViewModels;

namespace Music.Controllers;

[AuthorizationFilter]
public class AdminAlbumController(IAlbumRepository albumRepository, IPhotoRepository photoRepository) : Controller
{
    private readonly string _controllerName = ControllerHelper.GetControllerName<AdminAlbumController>(); 
    public async Task<IActionResult> Index(int id)
    {
        var albums = await albumRepository.GetAlbumsByArtistIdAsync(id);
        
        var adminAlbumViewModel = new AdminAlbumViewModel()
        {
            Albums = albums,
            ArtistId = id
        };

        return View(adminAlbumViewModel);
    }
    
    public async Task<IActionResult> Update(int id)
    {
        var album = await albumRepository.GetDetailsByIdAsync(id);

        var adminAlbumCreateViewModel = new AdminAlbumCreateViewModel()
        {
            Album = album,
            ArtistId = album.ArtistId
        };
        
        return View(adminAlbumCreateViewModel);
    }
    
    [HttpPost]
    public async Task<IActionResult> Update(AdminAlbumCreateViewModel model)
    {
        if (model.Album != null) await albumRepository.UpdateAsync(model.Album);

        return RedirectToAction(nameof(AdminAlbumController.Index), _controllerName, new {id = model.ArtistId});
    }
    
    public IActionResult Create(AdminAlbumViewModel adminAlbumViewModel)
    {
        var adminAlbumCreateViewModel = new AdminAlbumCreateViewModel()
        {
            ArtistId = adminAlbumViewModel.ArtistId,
            Album = adminAlbumViewModel.Album
        };
        return View(adminAlbumCreateViewModel);
    }
    
    [HttpPost]
    public async Task<IActionResult> Create(AdminAlbumCreateViewModel adminAlbumCreateViewModel)
    {
        var urlAlbum = await photoRepository.UploadPhotoAsync(adminAlbumCreateViewModel.File);
            adminAlbumCreateViewModel.Album.ArtistId = adminAlbumCreateViewModel.ArtistId;
            adminAlbumCreateViewModel.Album.UrlImg = urlAlbum;
            await albumRepository.AddAsync(adminAlbumCreateViewModel.Album);
            
        return RedirectToAction(nameof(AdminAlbumController.Index), _controllerName,
            new { id = adminAlbumCreateViewModel.Album.ArtistId });
    }
    
    
    public async Task<IActionResult> Delete(int id)
    {
        var albumToDelete = await albumRepository.GetDetailsByIdAsync(id);
        var artistId = albumToDelete.ArtistId;
        await albumRepository.Delete(albumToDelete);
        Thread.Sleep(1500);
        return RedirectToAction(nameof(AdminAlbumController.Index), _controllerName, new {id = artistId});
    }

    
}