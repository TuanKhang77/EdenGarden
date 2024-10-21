using EdenGarden_API.Models.Entities;

namespace EdenGarden_API.Models.Response
{
    public class RoomFullData
    {
        public int Id { get; set; } //id phòng
        public string? RoomCode { get; set; }
        public int? Floor { get; set; }
        public string? Service { get; set; }
        public float? Price { get; set; }
        public int? RoomTypeId { get; set; }
        public string? RoomStatus { get; set; }
        public string? Img { get; set; }
        public string? TypeName { get; set; }
        public int? NumofPeople { get; set; }
        public string? Description { get; set; }
    }
}
