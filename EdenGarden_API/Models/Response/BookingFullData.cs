using EdenGarden_API.Models.Entities;

namespace EdenGarden_API.Models.Response
{
    public class BookingFullData
    {
        public int Id { get; set; } //id lich su dat cua 1 khach hang
        public System.DateTime? Bookday { get; set; }
        public string? Note { get; set; }
        public int? TotalAmount { get; set; }
        public int? FirstPay { get; set; }
        public string? CombinedServiceNames { get; set; } //cộng chuỗi

        public int? ClientId { get; set; }
        public string? ClientName { get; set; }
        public string? CardId { get; set; }
        public int? NumOfPeople { get; set; }

        public string? CombinedTypeRoomNames { get; set; } //cộng chuỗi

        
    }
}
