namespace Music.Models;

public class ArtistUser
{
    public int Id { get; set; }
    
    public int ArtistId { get; set; }
    
    public int UserId { get; set; }
}