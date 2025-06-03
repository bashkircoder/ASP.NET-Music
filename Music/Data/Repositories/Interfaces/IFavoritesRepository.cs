using Music.Models;

namespace Music.Data.Repositories.Interfaces;

public interface IFavoritesRepository
{
    Task<List<Album>> GetFavoritesAlbumsAsync(int userId);
    
    Task<List<Artist>> GetFavoritesArtistsAsync(int userId);
    
    Task RemoveFavoriteAlbumAsync(int albumId, int userId = 1);
    
    Task RemoveFavoriteArtistAsync(int artistId, int userId = 1);
}