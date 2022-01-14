using System.Collections.Generic;
using System.Threading.Tasks;
using Persistence.Data;

namespace Services.File.Repository
{
    public class FileRepository : Core.Repository.Repository<DataContext,Domain.Models.HistoryFiles,int>
    
    {
        private readonly DataContext _context;

        public FileRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public async Task CreateFiles(List<Domain.Models.HistoryFiles> model)
        {
            await _context.AddRangeAsync(model);
            await _context.SaveChangesAsync();
        }
        public async Task CreateFile(Domain.Models.HistoryFiles model)
        {
            await _context.AddRangeAsync(model);
            await _context.SaveChangesAsync();
        }
    }
}