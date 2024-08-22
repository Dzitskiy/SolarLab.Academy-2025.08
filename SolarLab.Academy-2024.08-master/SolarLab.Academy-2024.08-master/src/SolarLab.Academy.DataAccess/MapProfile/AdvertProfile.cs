using AutoMapper;
using SolarLab.Academy.Contracts.Adverts;
using SolarLab.Academy.Domain.Advert;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarLab.Academy.DataAccess.MapProfile
{
    public class AdvertProfile:Profile
    {

        public AdvertProfile()
        {
          CreateMap<Advert,AdvertDto>().ReverseMap();
          CreateMap<AdvertDto, AdvertInfoView>().ReverseMap();

        }

    }
}
