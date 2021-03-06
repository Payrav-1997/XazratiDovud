using System.Threading.Tasks;
using Services.Hostory.Models;

namespace Services.Hostory
{
    public interface IHistoryService
    {
        public Task CreateHistory(HistoryViewModel model);
        Task<GetHistoryViewModel> GetHistory();
        Task UpdateHistory(UpdateHistoryViewModel model);
        Task<UpdateHistoryViewModel> GetPartnerById(int id);
    }
}