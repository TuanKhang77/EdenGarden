using System.Text.Json.Serialization;

namespace EdenGarden_API.Models.Entities
{
    public class Service
    {
        public int Id { get; set; }
        public string? ServiceName { get; set; }
        public string? ServiceDescription { get; set; }
        public float? Price { get; set; }
        [JsonIgnore]
        public IEnumerable<Booking>? Booking { get; set; }
        public System.DateTime? CreatedDate { get; set; }
        public System.DateTime? ModifiedDate { get; set; }
    }
}
