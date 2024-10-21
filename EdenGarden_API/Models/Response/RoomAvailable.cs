using EdenGarden_API.Models.Entities;

namespace EdenGarden_API.Models.Response
{
    public class RoomAvailable 
    {
        public int IdRoomType { get; set; }
        public string TypeName { get; set; }
        public int NumOfRoomLeft { get; set; }
    }
}
