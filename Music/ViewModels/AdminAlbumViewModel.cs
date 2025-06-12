using Music.Models;

namespace Music.ViewModels;

public class AdminAlbumViewModel
{
    public Album? Album { get; set; } = null;
    public List<Album> Albums { get; set; } = [];
    public int ArtistId { get; set; } = 0;
}