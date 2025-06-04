using Music.Models;

namespace Music.Data.Repositories.Interfaces;

public interface IUserRepository
{
    Task AddFavoriteArtist(Artist artist, int userId = 1);

    Task RemoveFavoriteArtist(Artist artist, int userId = 1);
    
    Task AddFavoriteAlbum(Album album, int userId = 1);

    Task RemoveFavoriteAlbum(Album album, int userId = 1);
    
    Task AddFavoriteSong(Song song, int userId = 1);

    Task RemoveFavoriteSong(Song song, int userId = 1);

    Task<List<User>> GetAllUsersAsync();

    Task<HashSet<Album>> GetFavoriteAlbums(int userid = 1);

    Task<HashSet<Artist>> GetFavoriteArtistsAsync(int userId = 1);
}