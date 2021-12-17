using Persistence.Data;
using Core.Repository;
namespace Services.Hostory.Repository
{
    public class HistoryRepository : Repository<DataContext,Domain.Models.History,int>
    {
        public HistoryRepository(DataContext context): base(context)
        {
        }

    }
}