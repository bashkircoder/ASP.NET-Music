﻿namespace Music.Models;

public class Album
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required int YearOfIssue { get; set; }
    public required string UrlImg { get; set; }
    public required List<Song> Songs { get; set; }

    public List<User>? Users { get; set; } = [];
    
    public int? ArtistId { get; set; }
    
    public Artist Artist { get; set; }
}