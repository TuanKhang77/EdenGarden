using System.Text.Json.Serialization;

namespace EdenGarden_API.Models.Entities
{
    public class Room
    {
        public int Id { get; set; } //id phòng
        public string? RoomCode { get; set; }
        public int? Floor { get; set; }
        public int? RoomTypeId { get; set; }
        public string? RoomStatus { get; set; } // vacant, Occupied , Cleaning, Maintained 
        [JsonIgnore]
        public RoomType? RoomType { get; set; }
        public System.DateTime? CreatedDate { get; set; }
        public System.DateTime? ModifiedDate { get; set; }
    }
}
