using Microsoft.EntityFrameworkCore;
using Music.Data.Repositories.Interfaces;
using Music.Models;

namespace Music.Data.Repositories;

public class UserRepositoryAdo(MusicDbContext context) : IUserRepository
{
    public async Task AddFavoriteArtist(Artist artist, int userId = 1)
    {
        var sqlQuery = $"INSERT INTO \"ArtistUser\" (\"FavoriteArtistsId\", \"UsersUserId\") VALUES ({artist.Id}, 1);";
        
        await context.Database.ExecuteSqlRawAsync(sqlQuery);
    }
    
    public async Task RemoveFavoriteArtist(Artist artist, int userId = 1)
    {
        
        var sqlQuery = $"DELETE FROM \"ArtistUser\" WHERE \"FavoriteArtistsId\" = {artist.Id} AND \"UsersUserId\" = 1;";
        
       await context.Database.ExecuteSqlRawAsync(sqlQuery);
    }

    public async Task AddFavoriteAlbum(Album album, int userId = 1)
    {
        var sqlQuery = $"INSERT INTO \"AlbumUser\" (\"FavoriteAlbumsId\", \"UsersUserId\") VALUES ({album.Id}, 1);";
        
        await context.Database.ExecuteSqlRawAsync(sqlQuery);
    }

    public async Task RemoveFavoriteAlbum(Album album, int userId = 1)
    {
        var sqlQuery = $"DELETE FROM \"AlbumUser\" WHERE \"FavoriteAlbumsId\" = {album.Id} AND \"UsersUserId\" = 1;";
        
        await context.Database.ExecuteSqlRawAsync(sqlQuery);
    }

    public async Task AddFavoriteSong(Song song, int userId = 1)
    {
        var users = await context.Users.AsNoTracking().ToListAsync();
        var user = users.First(x => x.UserId == userId);
        
        user.FavoriteSongs.Add(song);
        context.Users.Update(user);
        await context.SaveChangesAsync();
    }

    public async Task RemoveFavoriteSong(Song song, int userId = 1)
    {
        var sqlQuery = $"DELETE FROM \"SongUser\" WHERE \"FavoriteSongsId\" = {song.Id} AND \"UsersUserId\" = 1;";
        
        await context.Database.ExecuteSqlRawAsync(sqlQuery);
    }

    public async Task<List<User>> GetAllUsersAsync()
    {
        var users = await context.Users.Include(x => x.FavoriteArtists).AsNoTracking().ToListAsync();
        return users;
    }

    public async Task<HashSet<Album>> GetFavoriteAlbums(int userid = 1)
    {
        var user = await context.Users.Include(x => x.FavoriteAlbums).FirstAsync(x => x.UserId == userid);
        return [..user.FavoriteAlbums];
    }
    
    public async Task<HashSet<Artist>> GetFavoriteArtistsAsync(int userId = 1)
    {
        var user = await context.Users.Include(x => x.FavoriteArtists).FirstAsync(x => x.UserId == userId);
        
        return [..user.FavoriteArtists];
    }
}