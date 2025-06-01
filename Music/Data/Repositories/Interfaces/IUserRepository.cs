using Music.Models;

namespace Music.Data.Repositories.Interfaces;

public interface IUserRepository
{
    public Task AddFavoriteArtist(Artist artist, int userId = 1);

    public Task RemoveFavoriteArtist(Artist artist, int userId = 1);
    
    public Task AddFavoriteAlbum(Album album, int userId = 1);

    public Task RemoveFavoriteAlbum(Album album, int userId = 1);
    
    public Task AddFavoriteSong(Song song, int userId = 1);

    public Task RemoveFavoriteSong(Song song, int userId = 1);

    public Task<List<User>> GetAllUsersAsync();
}