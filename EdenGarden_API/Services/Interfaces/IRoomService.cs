using EdenGarden_API.Models.Entities;
using EdenGarden_API.Models.Response;

namespace EdenGarden_API.Services.Interfaces
{
    public interface IRoomService
    {
        //Response<Room> GetByPage(GetByPageRequest request);
        Response Create(Room entry);
        Response Update(Room entry);
        Response Delete(int id);
        Room GetRoomById(int id);
        IEnumerable<RoomFullData> GetAll();
        //List<Room> GetByPageAssetsPrint(GetByPageRequest request);
        //IEnumerable<AttachmentModel> GetAttachmentByChemicalLicenseId(int id);
    }
}
