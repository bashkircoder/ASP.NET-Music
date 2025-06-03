using Music.Common;
using Music.Models;

namespace Music.ViewModels;

public class FavoritesViewModel
{
    public List<Album> FavoriteAlbums { get; set; } = [];
    
    public List<Artist> FavoriteArtists { get; set; } = [];

    public int UserId = 1;
    
    public bool? IsFavorite { get; set; } = null;
    
    public int AlbumId { get; set; } = 0;
    
    public int ArtistId { get; set; } = 0;
}