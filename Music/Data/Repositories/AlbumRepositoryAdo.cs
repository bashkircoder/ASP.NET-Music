using Microsoft.EntityFrameworkCore;
using Music.Data.Repositories.Interfaces;
using Music.Extensions;
using Music.Helpers;
using Music.Models;

namespace Music.Data.Repositories;

public class AlbumRepositoryAdo(MusicDbContext context) : IAlbumRepository
{
    public int AlbumsCount { get; set; }

    public async Task<List<Album>> GetAllAsync()
    {
        var albums = await context.Albums.AsNoTracking().ToListAsync();

        return albums;
    }

    public async Task<Album> GetDetailsByIdAsync(int id)
    {
        var album = await context.Albums
            .AsNoTracking()
            .Include(album => album.Songs)
            .FirstAsync(x => x.Id == id);

        return album;
    }
    
    public async Task<List<Album>> GetAlbumsByArtistIdAsync(int id)
    {
        var artist = await context.Artists
            .Where(x => x.Id == id).Select(x => x.Albums).ToListAsync();
        
        return artist[0];
    }

    public async Task<List<Album>> FilteringAlbums(int artistId, SortedType sortedType, string? albumName, int page, int pageSize)
    {
        var album = context.Albums.AsQueryable();
        
        var albums = album.Where(x => x.ArtistId == artistId);
        
        if (!string.IsNullOrEmpty(albumName))
        {
            albums = albums.Where(
                x => x.Name.ToLower().Contains(
                    albumName.ToLower()));
        }
 
        if (sortedType != SortedType.None)
        {
            if (sortedType == SortedType.CostAsk)
            {
                albums = albums.OrderBy(x => x.YearOfIssue);
            }
            else
            {
                albums = albums.OrderByDescending(x => x.YearOfIssue);
            }
        }
        
        AlbumsCount = albums.Count();
 
        var paginationFilteredAlbums = await albums.ToPagedListAsync(page, pageSize);

        return paginationFilteredAlbums;
    }
}