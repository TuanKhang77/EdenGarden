using EdenGarden_API.Models.Entities;
using EdenGarden_API.Models.Request;
using EdenGarden_API.Models.Response;

namespace EdenGarden_API.Services.Interfaces
{
    public interface IBookingService
    {
        Response CreateBook(CreateBookingRequest2 bookingReq);
        Response Delete(int id);
        IEnumerable<BookingFullData> GetAll();
        IEnumerable<RoomAvailable> GetRoomAvailable(FindRoomTypeRequest findRoomTypeRequest);
        ResponseVnpay PaymentExecute(IQueryCollection collections);
        Boolean SaveBookingAfterPayment (Booking newBooking);
    }
}
