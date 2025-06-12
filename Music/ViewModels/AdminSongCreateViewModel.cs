using Music.Models;

namespace Music.ViewModels;

public class AdminSongCreateViewModel
{
    public Song? Song { get; set; } = null;
    public int AlbumId { get; set; } = 0;
    
    public int SongId { get; set; } = 0;
}