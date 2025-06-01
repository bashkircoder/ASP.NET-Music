namespace Music.Models;

public class User
{
    
    public required int UserId { get; init; }

    public List<Artist> FavoriteArtists { get; set; } = [];

    public List<Album> FavoriteAlbums { get; set; } = [];

    public List<Song> FavoriteSongs { get; set; } = [];

}