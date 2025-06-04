using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Music.Data.Repositories;
using Music.Data.Repositories.Interfaces;
using Music.Models;
using Music.ViewModels;

namespace Music.Controllers;

public class HomeController(IArtistRepository artistRepository, IUserRepository userRepository) : Controller
{
    public async Task<IActionResult> Index(HomeViewModel model)
    {
        
        if (model.IsFavorite != null)
        {
            if (model.IsFavorite == true)
            {
                await AddArtistToFavorites(model.ArtistId);
            }
            else
            {
                await RemoveArtistFromFavorites(model.ArtistId);
            }
        }
        
        var favoriteArtists = await userRepository.GetFavoriteArtistsAsync();
        
        var artists = await artistRepository.GetAllAsync();
      
        
        if (model.ArtistName != "")
        {
            var filteredArtists = artists.Where(a => a.Name.Contains(model.ArtistName, StringComparison.InvariantCultureIgnoreCase)).ToList();

            var viewModel = new HomeViewModel 
            {
                Artists = filteredArtists,
                ArtistName = model.ArtistName,
                IsFavorite = model.IsFavorite,
                ArtistId = model.ArtistId,
                FavoriteArtists = favoriteArtists
            };
            
            return View(viewModel);
        }
        else
        {
            var viewModel = new HomeViewModel
            {
                Artists = artists,
                ArtistName = model.ArtistName,
                IsFavorite = model.IsFavorite,
                ArtistId = model.ArtistId,
                FavoriteArtists = favoriteArtists
            };
        
            return View(viewModel);
        }
    }

    public async Task AddArtistToFavorites(int artistId)
    {
        var artist = await artistRepository.GetDetailsByIdAsync(artistId);
        await userRepository.AddFavoriteArtist(artist);
    }
    
    public async Task RemoveArtistFromFavorites(int artistId)
    {
        var artist = await artistRepository.GetDetailsByIdAsync(artistId);
        await userRepository.RemoveFavoriteArtist(artist);
    }
}