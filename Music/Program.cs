using Microsoft.EntityFrameworkCore;
using Music.APIKeys;
using Music.Controllers;
using Music.Data.Repositories;
using Music.Data.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

var connection = builder.Configuration.GetConnectionString("MusicDbConnection");
builder.Services.AddDbContext<MusicDbContext>(options =>
    options.UseNpgsql(connection));

builder.Services.Configure<UploadcareKeys>(builder.Configuration.GetSection("UploadcareKeys"));

builder.Services.AddScoped<IAlbumRepository, AlbumRepositoryAdo>();
builder.Services.AddScoped<IArtistRepository, ArtistRepositoryAdo>();
builder.Services.AddScoped<IUserRepository, UserRepositoryAdo>();
builder.Services.AddScoped<IFavoritesRepository, FavoritesRepositoryAdo>();
builder.Services.AddSingleton<IPhotoRepository, PhotoRepository>();

builder.Services.AddControllersWithViews();

var app = builder.Build();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();