using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Music.Common;
using Music.Data.Repositories;
using Music.Data.Repositories.Interfaces;
using Music.Models;
using Music.ViewModels;

namespace Music.Controllers;

public class AdminSongController(IAlbumRepository albumRepository) : Controller
{
    private string _controllerName = ControllerHelper.GetControllerName<AdminSongController>(); 
    public async Task<IActionResult> Index(int id)
    {
        var album = await albumRepository.GetDetailsByIdAsync(id);

        var songViewModel = new AdminSongViewModel()
        {
            Album = album,
            AlbumId = id
        };
        return View(songViewModel);
    }
    
    public async Task<IActionResult> Update(AdminSongViewModel adminSongViewModel)
    {
        var song = await albumRepository.GetSongDetailsByIdAsync(adminSongViewModel.SongId);

        var adminSongModel = new AdminSongCreateViewModel()
        {
            Song = song,
            AlbumId = adminSongViewModel.AlbumId,
            SongId = adminSongViewModel.SongId
        };
        
        return View(adminSongModel);
    }
    
    [HttpPost]
    public async Task<IActionResult> Update(AdminSongCreateViewModel model)
    {
        if (model.Song == null) return RedirectToAction("Index", "AdminSong", new { id = model.AlbumId });
        model.Song.Id = model.SongId;
        await albumRepository.UpdateSongAsync(model.Song);
        
        return RedirectToAction(nameof(AdminSongController.Index), _controllerName, new {id = model.AlbumId});
    }
    
    public async Task<IActionResult> Create(AdminSongViewModel adminSongViewModel)
    {
        var album = await albumRepository.GetDetailsByIdAsync(adminSongViewModel.AlbumId);
        adminSongViewModel.Album = album;
        return View(adminSongViewModel);
    }
    
    [HttpPost]
    public async Task<IActionResult> Create(AdminSongCreateViewModel adminSongCreateViewModel)
    {
        if (adminSongCreateViewModel.Song != null)
            await albumRepository.AddSongAsync(adminSongCreateViewModel.AlbumId, adminSongCreateViewModel.Song);
        return RedirectToAction(nameof(AdminSongController.Index), _controllerName, new {id = adminSongCreateViewModel.AlbumId});
    }
    
    
    public async Task<IActionResult> Delete(AdminSongViewModel adminSongViewModel)
    {
        var songToDelete = await albumRepository.GetSongDetailsByIdAsync(adminSongViewModel.SongId);
        await albumRepository.DeleteSong(songToDelete);
        return RedirectToAction(nameof(AdminSongController.Index), _controllerName, new {id = adminSongViewModel.AlbumId});
    }
}