using Music.Models;

namespace Music.ViewModels;

public class AdminArtistCreateViewModel
{
    public string Name { get; set; } = "";
    public IFormFile? File { get; set; } = null; 
}