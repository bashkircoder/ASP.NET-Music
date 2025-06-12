using Music.Models;

namespace Music.ViewModels;

public class AdminSongViewModel
{
    public Album? Album { get; set; } = null;
    
    public int SongId { get; set; } = 0;
    
    public Song? Song { get; set; } = null;
    
    public int AlbumId { get; set; } = 0;
}