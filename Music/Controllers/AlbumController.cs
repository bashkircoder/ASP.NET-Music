using Microsoft.AspNetCore.Mvc;
using Music.Data.Repositories.Interfaces;
using Music.Helpers;
using Music.Models;
using Music.ViewModels;

namespace Music.Controllers;

public class AlbumController(IAlbumRepository albumRepository) : Controller
{
    
    public async Task<IActionResult> Index(int artistId, SortedType sortedType = SortedType.None, string albumName = "")
    {
        var albums = await albumRepository.GetAlbumsByArtistIdAsync(artistId);
         
        var filteredAlbums = albums.Where(a => a.Name.Contains(albumName, StringComparison.InvariantCultureIgnoreCase)).ToList();
        
        switch (sortedType)
        {
            case SortedType.CostAsk: filteredAlbums = filteredAlbums.OrderBy(x => x.YearOfIssue).ToList();
                break;
            case SortedType.CostDesc: filteredAlbums = filteredAlbums.OrderByDescending(x => x.YearOfIssue).ToList();
                break;
            case SortedType.None: 
                break;
        }

        var albumViewModel = new AlbumViewModel();
        albumViewModel.ArtistId = artistId;
        albumViewModel.Albums = filteredAlbums;
        albumViewModel.SortedType = sortedType;
        albumViewModel.AlbumName = albumName;
        
        return View(albumViewModel);
    }
    
}