using EdenGarden_API.Models.Entities;

namespace EdenGarden_API.Services.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(Admin admin);
    }
}
