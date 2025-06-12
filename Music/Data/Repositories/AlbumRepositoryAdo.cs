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

    public async Task<Song> GetSongDetailsByIdAsync(int id)
    {
        var song = await context.Songs.FirstAsync(x => x.Id == id);
        return song;
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
        
        var albums = album.Include(x => x.Users).Where(x => x.ArtistId == artistId);
        
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
        
        AlbumsCount = await albums.CountAsync();
 
        var paginationFilteredAlbums = await albums.ToPagedListAsync(page, pageSize);

        return paginationFilteredAlbums;
    }

    public async Task UpdateAsync(Album newAlbum)
    {
        var album = await context.Albums.FirstAsync(x => x.Id == newAlbum.Id);
        
        album.Id = newAlbum.Id;
        album.Name = newAlbum.Name;
        album.UrlImg = newAlbum.UrlImg;
        album.YearOfIssue = newAlbum.YearOfIssue;
        album.Songs = newAlbum.Songs;
        
        await context.SaveChangesAsync();
    }

    public async Task AddAsync(Album album)
    {
        await context.Albums.AddAsync(album);
        await context.SaveChangesAsync();
    }

    public async Task UpdateSongAsync(Song song)
    {
        var updateSong = await context.Songs.FirstAsync(x => x.Id == song.Id);
        updateSong.Name = song.Name;
        updateSong.UrlSong = song.UrlSong;
        await context.SaveChangesAsync();
    }

    public async Task AddSongAsync(int albumId, Song song)
    {
        var album = context.Albums.Include(x => x.Songs).First(x => x.Id == albumId);
        album.Songs.Add(song);
        await context.SaveChangesAsync();
    }

    public async Task DeleteSong(Song song)
    {
        context.Songs.Remove(song);
        await context.SaveChangesAsync();
    }

    public async Task Delete(Album albumToDelete)
    {
        context.Albums.Remove(albumToDelete);
        await context.SaveChangesAsync();
    }
}