using CloudinaryDotNet.Actions;

namespace EdenGarden_API.Services.Interfaces
{
    public interface IPhotoService
    {
        ImageUploadResult AddPhoto(IFormFile file);
        DeletionResult DeletePhoto(string photoUrl);
    }
}
