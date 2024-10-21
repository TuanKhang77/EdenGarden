using EdenGarden_API.Data;
using EdenGarden_API.Models.Entities;
using EdenGarden_API.Models.Request;
using EdenGarden_API.Models.Response;
using EdenGarden_API.Repository.Interfaces;
using EdenGarden_API.Services.Interfaces;
using System.Linq;
namespace EdenGarden_API.Services
{
    public class BillService : IBillService
    {
        private readonly ICommonRepository _respository;
        private readonly DataContext _context;
        public BillService(ICommonRepository respository, DataContext context)
        {
            _respository = respository;
            _context = context;
        }


        public IEnumerable<BillFullData> GetAll()
        {
            var model = (from a in _context.Bill
                         join b in _context.Booking
                         on a.BookingId equals b.Id
                         join c in _context.Client
                         on b.Id equals c.BookingId
                         select new BillFullData
                         {
                            Id =  a.Id,
                            TotalPrice = a.TotalPrice,
                            FirstPay= a.FirstPay,
                            PayDay = a.PayDay,
                            Status = a.Status,
                            Note = a.Note,
                            PaymentType = a.PaymentType,
                            BookingId = a.BookingId,

                            ClientName=c.Name,
                            Email=c.Email,
                            PhoneNumber = c.PhoneNumber,
                            CardId = c.CardId,

                            Bookday = b.Bookday
                         }).ToList();
            return model;
        }

        public Bill? GetBillById(int id)
        {
            //return _respository.GetObjectByStore<Bill>("[dbo].[Prc_BillGetById]", new { Id = id });
            return _context.Bill.FirstOrDefault(x => x.Id == id);
        }

        public Response Update(Bill entry)
        {
            var arg = new
            {
                entry.Id,
                entry.Status,
                entry.PayDay,
                entry.Note
            };
            var response = _respository.GetObjectByStore<Response>("dbo.[Prc_BillUpdate]", arg);
            return response;
        }
    }
}
