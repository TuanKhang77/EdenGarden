using EdenGarden_API.Models.Entities;

namespace EdenGarden_API.Models.Request
{
    public class CreateBookingRequest2
    {
        public DateOnly BeginDate { get; set; }
        public DateOnly EndDate { get; set; }
        public int NumberOfPeople { get; set; }
        public Client BookedClient { get; set; }
        public IEnumerable<BookedRoomRequest> RoomRequests { get; set; } = new List<BookedRoomRequest>();
        public IEnumerable<int> ServiceIds { get; set; } = new List<int>();
        public int amount { get; set; }
    }
}
