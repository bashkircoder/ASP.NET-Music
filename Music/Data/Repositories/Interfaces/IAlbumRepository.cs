using Music.Helpers;
using Music.Models;

namespace Music.Data.Repositories.Interfaces;

public interface IAlbumRepository
{ 
    int AlbumsCount { get; set; }
    Task<List<Album>> GetAllAsync();
    Task<Album> GetDetailsByIdAsync(int id);
    
    Task<Song> GetSongDetailsByIdAsync(int id);

    Task<List<Album>> GetAlbumsByArtistIdAsync(int id);

    Task<List<Album>> FilteringAlbums(int artistId, SortedType sortedType, string? albumName, int page, int pageSize);
    Task UpdateAsync(Album album);
    Task AddAsync(Album album);
    Task UpdateSongAsync(Song song);
    Task AddSongAsync(int albumId, Song song);
    Task DeleteSong(Song song);
    Task Delete(Album albumToDelete);
}