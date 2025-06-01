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
                var artist = await artistRepository.GetDetailsByIdAsync(model.ArtistId);
                await userRepository.AddFavoriteArtist(artist);
            }
            else
            {
                var artist = await artistRepository.GetDetailsByIdAsync(model.ArtistId);
                
                await userRepository.RemoveFavoriteArtist(artist);
            }
        }
        
        var artists = await artistRepository.GetAllAsync();
      
        
        if (model.ArtistName != null)
        {
            var filteredArtists = artists.Where(a => a.Name.Contains(model.ArtistName, StringComparison.InvariantCultureIgnoreCase)).ToList();

            var viewModel = new HomeViewModel 
            {
                Artists = filteredArtists,
                ArtistName = model.ArtistName,
                IsFavorite = model.IsFavorite,
                ArtistId = model.ArtistId
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
                ArtistId = model.ArtistId
            };
        
            return View(viewModel);
        }
    }
}