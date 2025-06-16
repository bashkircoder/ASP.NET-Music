using Microsoft.AspNetCore.Mvc;
using Music.Common;
using Music.Data.Repositories.Interfaces;
using Music.Filters;
using Music.ViewModels;

namespace Music.Controllers;

[ResourceFilter]
public class FavoritesController(IFavoritesRepository favoriteRepository) : Controller
{
    public async Task<IActionResult> Index(FavoritesViewModel favoritesViewModel)
    {
        if (favoritesViewModel.IsFavorite != null)
        {
            if (favoritesViewModel.ArtistId != 0)
            {
                await favoriteRepository.RemoveFavoriteArtistAsync(favoritesViewModel.ArtistId);
            }

            if (favoritesViewModel.AlbumId != 0)
            {
                await favoriteRepository.RemoveFavoriteAlbumAsync(favoritesViewModel.AlbumId);
            }
        }
        
        var albums = await favoriteRepository.GetFavoritesAlbumsAsync(1);
        var artists = await favoriteRepository.GetFavoritesArtistsAsync(1);

        var controller = new FavoritesViewModel()
        {
            FavoriteAlbums = albums,
            FavoriteArtists = artists,
            UserId = 1,
            IsFavorite = favoritesViewModel.IsFavorite
        };
        
        return View(controller);
    }
}
