using EdenGarden_API.Data;
using EdenGarden_API.Models.Entities;
using EdenGarden_API.Models.Response;
using EdenGarden_API.Repository.Interfaces;
using EdenGarden_API.Services.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace EdenGarden_API.Services
{
    public class BookedRoomService : IBookedRoomService
    {
        private readonly ICommonRepository _respository;
        private readonly DataContext _context;

        public BookedRoomService(ICommonRepository respository, DataContext dataContext)
        {
            _respository = respository;
            _context = dataContext;
        }
        public Response Update(BookedRoom entry)
        {
            var temp = new
            {
                entry.Id,
                entry.RoomID,
                entry.IsChecked
            };
            var response = _respository.GetObjectByStore<Response>("dbo.[Prc_BookedRoomUpdate]", temp);


            return response;
        }

        public Response Delete(int id)
        {
            var arg = new
            {
                Id = id
            };
            var response = _respository.GetObjectByStore<Response>("[dbo].[Prc_BookedRoomDelete]", arg);
            return response;
        }

        public IEnumerable<BookedRoomFullData> GetAll()
        {
            //_context.BookedRoom.Include(x => x.Booking)
            //    .Include(x => x.Room)
            //    .ToList();
            var model = (from a in _context.BookedRoom
                         join b in _context.Room
                         on a.RoomID equals b.Id
                         join c in _context.RoomType
                         on b.RoomTypeId equals c.Id
                         join d in _context.Client
                         on a.BookingId equals d.BookingId
                         select new BookedRoomFullData
                         {
                            Id = a.Id,
                            Checkin = a.Checkin,
                            Checkout = a.Checkout,
                            Note = a.Note,
                            IsChecked = a.IsChecked,
                            BookingId = a.BookingId,
                            RoomID = a.RoomID,
                            //room
                            RoomCode = b.RoomCode,
                            RoomFloor = b.Floor,
                            //roomtype
                            RoomTypeId = c.Id,
                            RoomTypeName = c.TypeName,
                            //client
                            ClientId = d.Id,
                            ClientName = d.Name,
                            ClientCardId = d.CardId,
                         }).ToList();
            return model;
        }

        public BookedRoom GetBookedRoomById(int id) 
        {
            return _context.BookedRoom.FirstOrDefault(x => x.Id == id);
        }

        
    }
}
