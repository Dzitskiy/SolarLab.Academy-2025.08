using SolarLab.Academy.Contracts.Adverts;
using SolarLab.Academy.Domain.Advert;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarLab.Academy.AppServices.Repository
{
    public interface IAdvertRepository
    {


        Task<List<Advert>> GetAll();
    }
}
