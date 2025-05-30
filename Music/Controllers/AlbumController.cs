using Microsoft.AspNetCore.Mvc;
using Music.Common;
using Music.Data.Repositories.Interfaces;
using Music.Helpers;
using Music.Models;
using Music.ViewModels;

namespace Music.Controllers;

public class AlbumController(IAlbumRepository albumRepository) : Controller
{
    
    public async Task<IActionResult> Index(AlbumViewModel model)
    {

        var filteringAlbums = await albumRepository.FilteringAlbums(model.ArtistId, model.SortedType, model.AlbumName, model.PageNumber, model.PageQuantity);

        var albumsCount = albumRepository.AlbumsCount;

        var albumViewModel = new AlbumViewModel
        {
            PageViewModel = new PageViewModel(albumsCount, model.PageNumber, model.PageQuantity),
            ArtistId = model.ArtistId,
            Albums = filteringAlbums,
            SortedType = model.SortedType,
            AlbumName = model.AlbumName,
            PageNumber = model.PageNumber,
            PageQuantity = model.PageQuantity,
        };

        return View(albumViewModel);
    }
}