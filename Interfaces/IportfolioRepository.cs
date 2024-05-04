using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Interfaces
{
    public interface IportfolioRepository
    {
        Task<List<Stock>> GetUserPortFolio(AppUser user);

        Task<PortFolio> CreateAsync(PortFolio portFolio);
    }
}