using EdenGarden_API.Models.Entities;
using EdenGarden_API.Models.Request;
using EdenGarden_API.Models.Response;

namespace EdenGarden_API.Services.Interfaces
{
    public interface IBillService
    {
        Response Update(Bill entry);
        Bill GetBillById(int id);
        IEnumerable<BillFullData> GetAll();
    }
}
