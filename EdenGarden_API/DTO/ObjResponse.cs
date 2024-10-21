using EdenGarden_API.Models.Entities;

namespace EdenGarden_API.DTO
{
    public class ObjResponse<T> : Response
    {
        public T Obj { get; set; }
    }
}
