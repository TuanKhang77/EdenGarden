namespace EdenGarden_API.Models.Response
{
    public class BillFullData
    {
        public int Id { get; set; } //id hoa don
        public float? TotalPrice { get; set; } //tổng bill
        public float? FirstPay { get; set; } //cọc
        public DateTime? PayDay { get; set; } //ngày thanh toán đầy đủ
        public string? Status { get; set; } //đã cọc, đã thanh toán, bỏ phòng
        public string? Note { get; set; }
        public string? PaymentType { get; set; } //hình thức
        public int BookingId { get; set; }
        //client
        public string ClientName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? CardId { get; set; }
        //Booking
        public System.DateTime? Bookday { get; set; }
    }
}
