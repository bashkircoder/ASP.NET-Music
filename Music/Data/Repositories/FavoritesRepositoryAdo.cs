using Microsoft.EntityFrameworkCore;
using Music.Data.Repositories.Interfaces;
using Music.Models;

namespace Music.Data.Repositories;

public class FavoritesRepositoryAdo(MusicDbContext context) : IFavoritesRepository
{
    public async Task<List<Album>> GetFavoritesAlbumsAsync(int userId)
    {
        var userFavoritesAlbums = await context.Users.Include(x => x.FavoriteAlbums).FirstAsync(x => x.UserId == userId);

        return userFavoritesAlbums.FavoriteAlbums;
    }

    public async Task<List<Artist>> GetFavoritesArtistsAsync(int userId)
    {
        var userFavoritesArtists = await context.Users.Include(x => x.FavoriteArtists).FirstAsync(x => x.UserId == userId);

        return userFavoritesArtists.FavoriteArtists;
    }

    public async Task RemoveFavoriteAlbumAsync(int albumId, int userId = 1)
    {
        var sqlQuery = $"DELETE FROM \"AlbumUser\" WHERE \"FavoriteAlbumsId\" = {albumId} AND \"UsersUserId\" = {userId};";
        
        await context.Database.ExecuteSqlRawAsync(sqlQuery);
    }

    public async Task RemoveFavoriteArtistAsync(int artistId, int userId = 1)
    {
        var sqlQuery = $"DELETE FROM \"ArtistUser\" WHERE \"FavoriteArtistsId\" = {artistId} AND \"UsersUserId\" = {userId};";
        
        await context.Database.ExecuteSqlRawAsync(sqlQuery);
    }
}