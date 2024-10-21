using Newtonsoft.Json;

namespace EdenGarden_API.Models.Entities
{
    public class Bill
    {
        public int Id { get; set; } //id hoa don
        public int? TotalPrice { get; set; } //tổng bill
        public int? FirstPay { get; set; } //cọc
        public DateTime? PayDay { get; set; } //ngày thanh toán đầy đủ
        public string? Status { get; set; } //đã cọc, đã thanh toán, bỏ phòng
        public string? Note { get; set; }
        public string? PaymentType { get; set; } //hình thức
        public int BookingId { get; set; }
        public System.DateTime? CreatedDate { get; set; }
        public System.DateTime? ModifiedDate { get; set; }


    }
}
