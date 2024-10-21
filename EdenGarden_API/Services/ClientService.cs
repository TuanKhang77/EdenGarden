using EdenGarden_API.Models.Entities;
using EdenGarden_API.Repository.Interfaces;
using EdenGarden_API.Services.Interfaces;

namespace EdenGarden_API.Services
{
    public class ClientService : IClientService
    {
        private readonly ICommonRepository _respository;
        public ClientService(ICommonRepository respository)
        {
            _respository = respository;
        }


        public IEnumerable<Client> GetAll()
        {
            var clients = _respository.GetListByStore<Client>("dbo.[Prc_ClientGetAll]", new { });

            return clients;
        }

        public Client GetClientById(int id)
        {
            return _respository.GetObjectByStore<Client>("[dbo].[Prc_ClientGetById]", new { Id = id });
        }

        public Response Update(Client entry)
        {
            var arg = new
            {
                entry.Id,
                entry.Name,
                entry.Email,
                entry.PhoneNumber,
                entry.CardId,
            };
            var response = _respository.GetObjectByStore<Response>("dbo.[Prc_ClientUpdate]", arg);
            return response;
        }
    }
}
