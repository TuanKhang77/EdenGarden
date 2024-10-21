using EdenGarden_API.Data;
using EdenGarden_API.Models.Entities;
using EdenGarden_API.Models.Response;
using EdenGarden_API.Repository.Interfaces;
using EdenGarden_API.Services.Interfaces;

namespace EdenGarden_API.Services
{
    public class RoomService : IRoomService
    {
        private readonly ICommonRepository _respository;
        private readonly DataContext _context;
        public RoomService(ICommonRepository respository, DataContext dataContext)
        {
            _respository = respository;
            _context = dataContext;
        }

        public Response Create(Room entry)
        {
            var arg = new
            {
                entry.RoomCode,
                entry.Floor,
                entry.RoomTypeId
            };
            var response = _respository.GetObjectByStore<Response>("[dbo].[Prc_RoomInsert]", arg);
            return response;
        }
        public Response Update(Room entry)
        {
            var arg = new
            {
                entry.Id,
                entry.RoomCode,
                entry.Floor,
                entry.RoomTypeId,
                entry.RoomStatus
            };
            var response = _respository.GetObjectByStore<Response>("[dbo].[Prc_RoomUpdate]", arg);
            return response;
        }

        public Response Delete(int id)
        {
            var arg = new
            {
                Id = id
            };
            var response = _respository.GetObjectByStore<Response>("[dbo].[Prc_RoomDelete]", arg);
            return response;
        }

        public IEnumerable<RoomFullData> GetAll()
        {
            var rooms = _respository.GetListByStore<RoomFullData>("[dbo].[Prc_RoomGetAll]", new { });

            return rooms;
        }

        public Room GetRoomById(int id)
        {
            return _respository.GetObjectByStore<Room>("[dbo].[Prc_RoomGetById]", new { Id = id });
        }

        public void SetRoomStatus(int id, string roomstatus)
        {
            // Tìm phòng trong danh sách có id tương ứng
            Room roomToUpdate = _context.Room.FirstOrDefault(r => r.Id == id);

            // Nếu phòng tồn tại, gán trạng thái mới
            roomToUpdate.RoomStatus = roomstatus;
            
        }
    }
}
