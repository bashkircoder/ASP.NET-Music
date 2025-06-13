using Microsoft.Extensions.Options;
using Music.APIKeys;
using Music.Data.Repositories.Interfaces;
using Uploadcare;
using Uploadcare.Upload;

namespace Music.Data.Repositories;

public class PhotoRepository(IOptions<UploadcareKeys> options) : IPhotoRepository
{
    private readonly UploadcareClient _uploadcareClient = new UploadcareClient(options.Value.PublicKey,options.Value.PrivateKey);
    
    public async Task<string> UploadPhotoAsync(IFormFile photo)
    {
        using var memoryStream = new MemoryStream();
        
        await photo?.CopyToAsync(memoryStream)!;
        
        var filBytes = memoryStream.ToArray();

        var fileUploader = new FileUploader(_uploadcareClient);
        
        var fileUploaded = await fileUploader.Upload(filBytes, photo.FileName);

        return fileUploaded.OriginalFileUrl;
    }
}