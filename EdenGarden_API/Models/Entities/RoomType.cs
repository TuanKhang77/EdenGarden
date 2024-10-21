namespace EdenGarden_API.Models.Entities
{
    public class RoomType
    {
        public int Id { get; set; }
        public string? TypeName { get; set; }
        public int? NumofPeople { get; set; }   
        public string? Description { get; set; }
        public string? Services { get; set; }
        public float? Price { get; set; }
        public string? Img { get; set; }
        public System.DateTime? CreatedDate { get; set; }
        public System.DateTime? ModifiedDate { get; set; }
    }
}
