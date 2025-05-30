using Microsoft.AspNetCore.Mvc;
using Music.Common;
using Music.Data.Repositories.Interfaces;
using Music.Helpers;
using Music.Models;
using Music.ViewModels;

namespace Music.Controllers;

public class AlbumController(IAlbumRepository albumRepository) : Controller
{
    
    public async Task<IActionResult> Index(int artistId, int pageQuantity = 1, SortedType sortedType = SortedType.None, string albumName = "",
        int page = 1)
    {

        var filteringAlbums = await albumRepository.FilteringAlbums(artistId, sortedType, albumName, page, pageQuantity);

        var albumsCount = albumRepository.AlbumsCount;

        var albumViewModel = new AlbumViewModel
        {
            PageViewModel = new PageViewModel(albumsCount, page, pageQuantity),
            ArtistId = artistId,
            Albums = filteringAlbums,
            SortedType = sortedType,
            AlbumName = albumName
        };

        return View(albumViewModel);
    }
}