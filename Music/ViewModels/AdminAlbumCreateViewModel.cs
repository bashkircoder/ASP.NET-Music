using Music.Models;

namespace Music.ViewModels;

public class AdminAlbumCreateViewModel
{
    public Album? Album { get; set; } = null;
    public int? ArtistId { get; set; } = 0;
    
    public IFormFile File { get; set; } = null;
}