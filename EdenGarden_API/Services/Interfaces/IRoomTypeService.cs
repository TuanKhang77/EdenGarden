using EdenGarden_API.Models.Entities;
using EdenGarden_API.Models.Request;

namespace EdenGarden_API.Services.Interfaces
{
    public interface IRoomTypeService
    {
        RoomType GetRoomTypeById(int id);
        IEnumerable<RoomType> GetAll();
        Response Create(RoomType entry);
        Response Update(RoomType entry);
        Response Delete(int id);
    }
}
