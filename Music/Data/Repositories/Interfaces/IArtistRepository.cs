using Music.Models;

namespace Music.Data.Repositories.Interfaces;

public interface IArtistRepository
{
    Task<List<Artist>> GetAllAsync();
    
    Task<Artist> GetDetailsByIdAsync(int id);
    
    Task AddAsync(Artist artist);

    Task Delete(Artist artist);

    Task UpdateAsync(Artist newArtist);
    
    Task CreateArtistAlbumAsync(int artistId, Album album);
}