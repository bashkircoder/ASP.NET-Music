using Microsoft.AspNetCore.Mvc;
using Music.Common;
using Music.Data.Repositories.Interfaces;
using Music.Helpers;
using Music.Models;
using Music.ViewModels;

namespace Music.Controllers;

public class AlbumController(IAlbumRepository albumRepository) : Controller
{
    private const int _pageSize = 3;
    public async Task<IActionResult> Index(int artistId, SortedType sortedType = SortedType.None, string albumName = "", int page = 1)
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
        
        filteredAlbums = filteredAlbums.Skip((page - 1) * _pageSize).Take(_pageSize).ToList();

        var albumViewModel = new AlbumViewModel
        {
            PageViewModel = new PageViewModel(filteredAlbums.Count, page, _pageSize),
            ArtistId = artistId,
            Albums = filteredAlbums,
            SortedType = sortedType,
            AlbumName = albumName
        };

        return View(albumViewModel);
    }
    
}