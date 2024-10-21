using EdenGarden_API.Models.Response;

namespace EdenGarden_API.Services.Interfaces
{
    public interface IStatisticalService
    {
        IEnumerable<RoomStatistical> GetRoomRecordsByYear(int year);

        IEnumerable<IncomeStatistical> GetAmountRecordsByMonth(int year);
    }
}
