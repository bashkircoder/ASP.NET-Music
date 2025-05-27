using Microsoft.EntityFrameworkCore;
using Music.Data.Repositories.Interfaces;
using Music.Models;

namespace Music.Data.Repositories;

public class ArtistRepositoryAdo(MusicDbContext context) : IArtistRepository
{
    public async Task<List<Artist>> GetAllAsync()
    {
        var artists = await context.Artists.AsNoTracking().ToListAsync();

        return artists;
    }

    public async Task<Artist> GetDetailsByIdAsync(int id)
    {
        var artist = await context.Artists
            .AsNoTracking()
            .Include(a => a.Albums)
            .Where(x => x.Id == id).ToListAsync();

        return artist[0];
    }

    public async Task AddAsync(Artist newArtist)
    {
        var count = await context.Artists.CountAsync();
        var artist = new Artist()
        {
            Id = newArtist.Id + 2 + count,
            Name = newArtist.Name,
            UrlImg = newArtist.UrlImg,
            Albums = null
        };
        await context.Artists.AddAsync(artist);
        context.SaveChangesAsync();
    }
    
    public async Task UpdateAsync(Artist newArtist)
    {
        var artist = await context.Artists.FirstAsync(x => x.Id == newArtist.Id);
        
        artist.Id = newArtist.Id;
        artist.Name = newArtist.Name;
        artist.UrlImg = newArtist.UrlImg;
        artist.Albums = null;
        
        context.SaveChangesAsync();
    }
    
    public void Delete(Artist artist)
    {
        context.Artists.Remove(artist);
        context.SaveChanges();
    }
}