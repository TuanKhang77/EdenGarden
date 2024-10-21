namespace EdenGarden_API.Models.Request
{
    public class CreateRoomTypeRequest
    {
        public int Id { get; set; }
        public string? TypeName { get; set; }
        public int? NumofPeople { get; set; }
        public string? Description { get; set; }
        public string? Services { get; set; }
        public float? Price { get; set; }
        public IFormFile? Img { get; set; }
    }
}
