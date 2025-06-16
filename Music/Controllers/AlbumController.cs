using Microsoft.AspNetCore.Mvc;
using Music.Common;
using Music.Data.Repositories.Interfaces;
using Music.Filters;
using Music.Helpers;
using Music.Models;
using Music.ViewModels;

namespace Music.Controllers;

[ResourceFilter]
public class AlbumController(IAlbumRepository albumRepository, IUserRepository userRepository) : Controller
{
    
    public async Task<IActionResult> Index(AlbumViewModel model)
    {
        
        if (model.IsFavorite != null)
        {
            if (model.IsFavorite == true)
            {
                await AddAlbumToFavorites(model.AlbumId);
            }
            else
            {
                await RemoveAlbumFromFavorites(model.AlbumId);
            }
        }
        
        var favoriteAlbums = await userRepository.GetFavoriteAlbums(1);

        var filteringAlbums = await albumRepository.FilteringAlbums(model.ArtistId, model.SortedType, model.AlbumName, model.PageNumber, model.PageQuantity);

        var albumsCount = albumRepository.AlbumsCount;

        var albumViewModel = new AlbumViewModel
        {
            PageViewModel = new PageViewModel(albumsCount, model.PageNumber, model.PageQuantity),
            ArtistId = model.ArtistId,
            IsFavorite = model.IsFavorite,
            AlbumId = model.AlbumId,
            Albums = filteringAlbums,
            SortedType = model.SortedType,
            AlbumName = model.AlbumName,
            PageNumber = model.PageNumber,
            PageQuantity = model.PageQuantity,
            FavoriteAlbums = favoriteAlbums
        };

        return View(albumViewModel);
    }

    public async Task AddAlbumToFavorites(int albumId)
    {
        var album = await albumRepository.GetDetailsByIdAsync(albumId);
        await userRepository.AddFavoriteAlbum(album);
    }
    
    public async Task RemoveAlbumFromFavorites(int albumId)
    {
        var album = await albumRepository.GetDetailsByIdAsync(albumId);
        await userRepository.RemoveFavoriteAlbum(album);
    }
}