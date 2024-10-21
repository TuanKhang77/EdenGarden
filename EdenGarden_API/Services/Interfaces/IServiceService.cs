using EdenGarden_API.Models.Entities;

namespace EdenGarden_API.Services.Interfaces
{
    public interface IServiceService
    {
        //Response<Service> GetByPage(GetByPageRequest request);
        Response Create(Service entry);
        Response Update(Service entry);
        Response Delete(int id);
        Service GetServiceById(int id);
        IEnumerable<Service> GetAll();
        //List<Service> GetByPageAssetsPrint(GetByPageRequest request);

    }
}
