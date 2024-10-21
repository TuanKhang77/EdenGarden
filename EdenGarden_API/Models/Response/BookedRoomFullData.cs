using EdenGarden_API.Models.Entities;

namespace EdenGarden_API.Models.Response
{
    public class BookedRoomFullData
    {
        public int Id { get; set; } // id lịch sử đặt của 1 phòng 
        public DateTime? Checkin { get; set; }
        public DateTime? Checkout { get; set; }
        public string? Note { get; set; }
        public bool? IsChecked { get; set; }
        public int? BookingId { get; set; }
        public int? RoomID { get; set; }
        //room
        public string? RoomCode { get; set; }
        public int? RoomFloor { get; set; }
        //roomtype
        public int? RoomTypeId { get; set; }
        public string? RoomTypeName { get; set; }
        //client
        public int? ClientId { get; set; }
        public string? ClientName { get; set; }
        public string? ClientCardId { get; set; }
    }
}
