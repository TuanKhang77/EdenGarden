using EdenGarden_API.Models.Entities;

namespace EdenGarden_API.Models.Response
{
    public class CreateBookingResponse
    {
        public string PaymentUrl { get; set; }

        public Booking Booking { get; set; }
    }
}
