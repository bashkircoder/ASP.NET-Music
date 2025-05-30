using Music.Helpers;
using Music.Models;

namespace Music.Data.Repositories.Interfaces;

public interface IAlbumRepository
{ 
    int AlbumsCount { get; set; }
    Task<List<Album>> GetAllAsync();
    Task<Album> GetDetailsByIdAsync(int id);

    Task<List<Album>> GetAlbumsByArtistIdAsync(int id);

    Task<List<Album>> FilteringAlbums(int artistId, SortedType sortedType, string? albumName, int page, int pageSize);
}