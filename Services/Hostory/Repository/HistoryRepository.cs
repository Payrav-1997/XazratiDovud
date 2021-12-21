using System;
using System.Threading.Tasks;
using Persistence.Data;
using Core.Repository;
namespace Services.Hostory.Repository
{
    public class HistoryRepository : Repository<DataContext,Domain.Models.History,int>
    {
        public HistoryRepository(DataContext context): base(context)
        {
        }

        public async Task<Domain.Models.History> UpdateHistory(Domain.Models.History history)
        {
            var model = context.Histories.Find(history.Id);
            model.Description = history.Description;
            model.Title = history.Title;
            model.UpdateDate = DateTime.Now;
            context.Update(model);
            await context.SaveChangesAsync();
            return model;
        }

    }
}