﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services.Hostory.Models;

namespace Services.Mapping
{
    public class MappingProfile : AutoMapper.Profile
    {
        public MappingProfile()
        {
            this.CreateMap<Domain.Models.History, HistoryViewModel>()
                .ForMember(x => x.Description, option => option.MapFrom(x => x.Description))
                .ForMember(x => x.Title, option => option.MapFrom(x => x.Title))
                .ForMember(x => x.HisFile, option => option.Ignore());

        }
    }
}
