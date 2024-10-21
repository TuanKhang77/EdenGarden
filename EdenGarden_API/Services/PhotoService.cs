using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using EdenGarden_API.Helper;
using EdenGarden_API.Services.Interfaces;
using Microsoft.Extensions.Options;

namespace EdenGarden_API.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly Cloudinary cloudinary;

        public PhotoService(IOptions<CloudinarySettings> config)
        {
            var account = new Account
            (
                config.Value.CloudName,
                config.Value.ApiKey,
                config.Value.ApiSecret
            );

            cloudinary = new Cloudinary(account);
        }

        public ImageUploadResult AddPhoto(IFormFile file)
        {
            var uploadResult = new ImageUploadResult();

            if (file.Length > 0)
            {
                using var stream = file.OpenReadStream();
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(file.FileName, stream),
                    Transformation = new Transformation().Height(500).Width(500).Crop("fill")
                };
                uploadResult = cloudinary.Upload(uploadParams);
            }

            return uploadResult;
        }

        public DeletionResult DeletePhoto(string photoUrl)
        {
            var deleteParams = new DeletionParams(photoUrl);
            var result = cloudinary.Destroy(deleteParams);

            return result;
        }
    }
}
