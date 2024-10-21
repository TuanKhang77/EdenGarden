using EdenGarden_API.Models.Entities;

namespace EdenGarden_API.Services.Interfaces
{
    public interface IClientService
    {
        Response Update(Client entry);
        Client GetClientById(int id);
        IEnumerable<Client> GetAll();
    }
}
