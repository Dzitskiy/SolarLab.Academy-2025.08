using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SolarLab.Academy.AppServices.Services;
using SolarLab.Academy.Contracts.Adverts;
using SolarLab.Academy.Domain.Advert;

namespace SolarLab.Academy.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdvertController : ControllerBase
    {
        private readonly IAdvertService _advertService;
        private readonly IMapper _mapper;
        public AdvertController(IAdvertService advertService,IMapper mapper)
        {
           
            _advertService = advertService;
            _mapper= mapper;    
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
           
            var result = await _advertService.GetAll();
            List<AdvertInfoView> notess = _mapper.Map<List<AdvertDto>, List<AdvertInfoView>>(result);
            return Ok(result);
        }

    }
}
