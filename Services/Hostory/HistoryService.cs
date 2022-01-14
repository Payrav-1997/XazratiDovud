using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Services.File.Repository;
using Services.Hostory.Models;
using Services.Hostory.Repository;

namespace Services.Hostory
{
    public class HistoryService : IHistoryService
    {
        private readonly HistoryRepository _repository;
        private readonly IMapper _mapper;
        private readonly FileRepository _fileRepository;

        public HistoryService(HistoryRepository repository, IMapper mapper,FileRepository fileRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _fileRepository = fileRepository;
        }

        public async Task CreateHistory(HistoryViewModel model)
        {
            var entity = _mapper.Map<Domain.Models.History>(model);
            entity.CreateDate = DateTime.Now;
            var history =  await _repository.Create(entity);
            if (model.Files != null)
            {
                await CreateHistoryImage(model.Files, history.Id);
            }
            
        }


        public async Task<GetHistoryViewModel> GetHistory()
        {
            var model = _mapper.Map<GetHistoryViewModel>(await _repository.Entities.Include(x=>x.HistoryFiles).FirstOrDefaultAsync());
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

        private async Task CreateHistoryImage(IFormFile[] image, int id)
        {
            var images = new List<Domain.Models.HistoryFiles>();
            if (image != null)
            {
                foreach (var img in image)
                {
                    string path = Path.GetFullPath("wwwroot/images/historyImage");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    var fileExtension = Path.GetExtension(img.FileName);
                    var fileName = $"{Guid.NewGuid()}{fileExtension}";
                    var finalFileName = Path.Combine(path, fileName);

                    using (var steram = System.IO.File.Create(finalFileName))
                    {
                        await img.CopyToAsync(steram);
                    }
                    
                    images.Add(new Domain.Models.HistoryFiles
                    {
                        Name = fileName,
                        HistoryId = id,
                        CreateDate = DateTime.Now,
                        FileSize = (img.Length / 1024).ToString()
                    });

                }
                await _fileRepository.CreateFiles(images);
            }
        }
    }
}