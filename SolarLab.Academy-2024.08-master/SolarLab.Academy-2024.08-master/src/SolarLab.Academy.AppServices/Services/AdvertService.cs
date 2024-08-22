using AutoMapper;


using SolarLab.Academy.AppServices.Repository;
using SolarLab.Academy.Contracts.Adverts;
using SolarLab.Academy.Domain.Advert;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace SolarLab.Academy.AppServices.Services
{
    public class AdvertService :IAdvertService
    {
        private readonly IAdvertRepository _advertRepository;
        private readonly IMapper _mapper;

        public async Task<List<AdvertDto>> GetAll()
        {
            var advert =await _advertRepository.GetAll();
            List<AdvertDto> notess = _mapper.Map<List<Advert>, List<AdvertDto>>(advert);
            return notess;

        }
        

    }
}