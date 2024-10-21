using EdenGarden_API.Models.Entities;
using EdenGarden_API.Models.Response;

namespace EdenGarden_API.Services.Interfaces
{
    public interface IBookedRoomService
    {
        Response Update(BookedRoom entry);
        Response Delete(int id);
        BookedRoom GetBookedRoomById(int id);
        IEnumerable<BookedRoomFullData> GetAll();
    }
}
