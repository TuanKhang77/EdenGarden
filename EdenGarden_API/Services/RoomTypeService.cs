using EdenGarden_API.Models.Entities;
using EdenGarden_API.Models.Request;
using EdenGarden_API.Repository.Interfaces;
using EdenGarden_API.Services.Interfaces;

namespace EdenGarden_API.Services
{
    public class RoomTypeService : IRoomTypeService
    {
        private readonly ICommonRepository _respository;

        public RoomTypeService(ICommonRepository respository)
        {
            _respository = respository;
        }

        public IEnumerable<RoomType> GetAll()
        {
            var services = _respository.GetListByStore<RoomType>("dbo.[Prc_RoomTypeGetAll]", new { });

            return services;
        }

        public RoomType GetRoomTypeById(int id)
        {
            return _respository.GetObjectByStore<RoomType>("[dbo].Prc_RoomTypeGetById", new { Id = id });
        }
        public Response Create(RoomType entry)
        {
            var arg = new
            {
                entry.TypeName,
                entry.NumofPeople,
                entry.Description,
                entry.Services,
                entry.Price,
                entry.Img,
            };
            var response = _respository.GetObjectByStore<Response>("[dbo].[Prc_RoomTypeInsert]", arg);
            return response;
        }
        public Response Delete(int id)
        {
            var arg = new
            {
                Id = id
            };
            var response = _respository.GetObjectByStore<Response>("[dbo].[Prc_RoomTypeDelete]", arg);
            return response;
        }
        public Response Update(RoomType entry)
        {
            var arg = new
            {
                entry.Id,
                entry.TypeName,
                entry.NumofPeople,
                entry.Description,
                entry.Services,
                entry.Price,
                entry.Img,
            };
            var response = _respository.GetObjectByStore<Response>("[dbo].[Prc_RoomTypeUpdate]", arg);
            return response;
        }

    }
}
