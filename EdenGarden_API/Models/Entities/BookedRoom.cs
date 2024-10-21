using System.Text.Json.Serialization;

namespace EdenGarden_API.Models.Entities
{
    public class BookedRoom
    {
        public int Id { get; set; } // id lịch sử đặt của 1 phòng 
        public DateTime? Checkin { get; set; }
        public DateTime? Checkout { get; set; }
        public string? Note { get; set; }
        public bool? IsChecked { get; set; }
        public int? BookingId { get; set; }
        public int? RoomID { get; set; }

        [JsonIgnore]
        public Booking? Booking { get; set; }
        public Room? Room { get; set; }
        public System.DateTime? CreatedDate { get; set; }
        public System.DateTime? ModifiedDate { get; set; }
    }
}
