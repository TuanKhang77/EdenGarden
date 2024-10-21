using EdenGarden_API.Models.Entities;
using EdenGarden_API.Repository.Interfaces;
using EdenGarden_API.Services.Interfaces;

namespace EdenGarden_API.Services
{
    public class ServiceService : IServiceService
    {
        private readonly ICommonRepository _respository;
        public ServiceService(ICommonRepository respository)
        {
            _respository = respository;
        }
        public Response Create(Service entry)
        {
            var arg = new
            {
                entry.ServiceName,
                entry.ServiceDescription,
                entry.Price
            };
            var response = _respository.GetObjectByStore<Response>("[dbo].[Prc_ServiceInsert]", arg);
            return response;
        }
        public Response Update(Service entry)
        {
            var arg = new
            {
                entry.Id,
                entry.ServiceName,
                entry.ServiceDescription,
                entry.Price
            };
            var response = _respository.GetObjectByStore<Response>("dbo.[Prc_ServiceUpdate]", arg);
            return response;
        }

        public Response Delete(int id)
        {
            var arg = new
            {
                Id = id
            };
            var response = _respository.GetObjectByStore<Response>("dbo.[Prc_ServiceDelete]", arg);
            return response;
        }

        public IEnumerable<Service> GetAll()
        {
            var services = _respository.GetListByStore<Service>("dbo.[Prc_ServiceGetAll]", new { });

            return services;
        }

        public Service GetServiceById(int id)
        {
            return _respository.GetObjectByStore<Service>("[dbo].Prc_ServiceGetById", new { Id = id });
        }

    }
}
