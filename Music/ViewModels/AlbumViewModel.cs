using Microsoft.AspNetCore.Mvc.Rendering;
using Music.Common;
using Music.Data.Repositories;
using Music.Helpers;
using Music.Models;

namespace Music.ViewModels;

public class AlbumViewModel
{
    const int PageSize = 20;
    public int ArtistId { get; set; }

    public  SortedType SortedType { get; set; } = 0;

    public  string AlbumName { get; set; } = "";

    public  List<Album> Albums { get; set; } = [];
    
    
    public required PageViewModel PageViewModel { get; init; }
    
    
}