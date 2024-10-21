using System.Text.Json.Serialization;

namespace EdenGarden_API.Models.Entities
{
    public class Booking
    {
        public int Id { get; set; } //id lich su dat cua 1 khach hang
        public System.DateTime? Bookday { get; set; }
        public string? Note { get; set; }
        public int? TotalAmount { get; set; }
        public int? NumOfPeople { get; set; }
        
        public Client? client { get; set; }
        public IEnumerable<Service>? UsedService { get; set; }
        public IEnumerable<BookedRoom>? BookedRooms { get; set; }
        public Bill? Bill { get; set; }
        public System.DateTime? CreatedDate { get; set; }
        public System.DateTime? ModifiedDate { get; set; }
    }
}
