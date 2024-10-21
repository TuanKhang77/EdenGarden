namespace EdenGarden_API.Models.Request
{
    public class FindRoomTypeRequest
    {
        public DateOnly BeginDate { get; set; }
        public DateOnly EndDate { get; set; }
        public int NumOfPeople { get; set; }
    }
}
