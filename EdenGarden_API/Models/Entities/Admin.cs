using System.ComponentModel.DataAnnotations;

namespace EdenGarden_API.Models.Entities
{
    public class Admin
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Username { get; set; }
        public byte[]? PassWordHash { get; set; }
        public byte[]? PassWordSalt { get; set; }
    }
}
