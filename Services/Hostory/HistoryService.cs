using System.Threading.Tasks;
using AutoMapper;
using Services.Hostory.Models;
using Services.Hostory.Repository;

namespace Services.Hostory
{
    public class HistoryService : IHistoryService
    {
        private readonly HistoryRepository _repository;
        private readonly IMapper _mapper;

        public HistoryService(HistoryRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task CreateHistory(HistoryViewModel model)
        {
            var entity = _mapper.Map<Domain.Models.History>(model);
            await _repository.Create(entity);
        }

    }
}