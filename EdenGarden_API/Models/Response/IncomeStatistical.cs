using EdenGarden_API.Models.Entities;

namespace EdenGarden_API.Models.Response
{
    public class IncomeStatistical : Statistical
    {
        public int? month { get; set; }
        public int? amount { get; set; }
    }
}
