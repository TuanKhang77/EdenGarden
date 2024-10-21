using EdenGarden_API.Models.Entities;
using EdenGarden_API.Models.Response;
using EdenGarden_API.Repository.Interfaces;
using EdenGarden_API.Services.Interfaces;

namespace EdenGarden_API.Services
{
    public class StatisticalService : IStatisticalService
    {
        private readonly ICommonRepository _respository;
        public StatisticalService(ICommonRepository respository)
        {
            _respository = respository;
        }

        public IEnumerable<IncomeStatistical> GetAmountRecordsByMonth(int year)
        {
            var records = _respository.GetListByStore<IncomeStatistical>("[dbo].[Prc_AmountMonthStatistical]", new { Year = year });

            return records;
        }

        public IEnumerable<RoomStatistical> GetRoomRecordsByYear(int year)
        {
            var records = _respository.GetListByStore<RoomStatistical>("[dbo].[Prc_RoomStatistical]", new { Year = year });

            return records;
        }

    }
}
