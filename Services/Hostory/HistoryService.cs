using System;
using System.Collections.Generic;
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
            entity.CreateDate = DateTime.Now;
            await _repository.Create(entity);
        }


        public async Task<GetHistoryViewModel> GetHistory()
        {
            var model = _mapper.Map<GetHistoryViewModel>(await _repository.Get());
            return model;
        }

        public async Task UpdateHistory(UpdateHistoryViewModel history)
        {
            var model = await _repository.Get(x => x.Id == history.Id);
            model = _mapper.Map<Domain.Models.History>(history);
            await _repository.UpdateHistory(model);
        }

        public async Task<UpdateHistoryViewModel> GetPartnerById(int id)
        {
            
                var model =  _mapper.Map<UpdateHistoryViewModel>(await _repository.Get(x => x.Id.Equals(id)));
                return model;

        }
    }
}