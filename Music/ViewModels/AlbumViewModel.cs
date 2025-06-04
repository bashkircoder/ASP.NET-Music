using Microsoft.AspNetCore.Mvc.Rendering;
using Music.Common;
using Music.Data.Repositories;
using Music.Helpers;
using Music.Models;

namespace Music.ViewModels;

public class AlbumViewModel
{
    public int ArtistId { get; init; }

    public int AlbumId { get; init; }

    public  SortedType SortedType { get; init; } = SortedType.None;

    public string? AlbumName { get; init; } = "";

    public List<Album>? Albums { get; init; } = [];

    public PageViewModel PageViewModel { get; init; }

    public int PageQuantity { get; init; } = 1;
    public int PageNumber { get; init; } = 1;
    
    public bool? IsFavorite { get; set; } = null;
    
    public HashSet<Album> FavoriteAlbums { get; set; } = [];
}