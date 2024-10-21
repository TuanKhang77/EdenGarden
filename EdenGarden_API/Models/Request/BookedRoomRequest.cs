using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EdenGarden_API.Models.Request
{
    public class BookedRoomRequest
    {
        public int RoomTypeId { get; set; }
        public int Quantity { get; set; }
    }
}
