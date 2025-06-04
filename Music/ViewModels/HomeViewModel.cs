using Music.Models;

namespace Music.ViewModels;

public class HomeViewModel()
{
    public string ArtistName { get; init; } = "";
    public List<Artist>? Artists { get; init; } = [];
    public int ArtistId { get; init; }
    public bool? IsFavorite { get; set; } = null;
    
    public HashSet<Artist> FavoriteArtists { get; init; } = [];
    
}