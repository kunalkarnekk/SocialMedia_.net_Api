using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class PortFolioRepository : IportfolioRepository
    {
        private readonly ApplicationDbContext _context;
        public PortFolioRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PortFolio> CreateAsync(PortFolio portFolio)
        {
            await _context.PortFolios.AddAsync(portFolio);
            await _context.SaveChangesAsync();
            return  portFolio;

        }

        public async Task<List<Stock>> GetUserPortFolio(AppUser user)
        {
            return await _context.PortFolios.Where(u => u.AppUserId == user.Id)
                .Select(Stock => new Stock{
                    Id = Stock.StockId,
                    Symbol = Stock.Stock.Symbol,
                    CompanyName = Stock.Stock.CompanyName,
                    Purchase = Stock.Stock.Purchase,
                    LastDev = Stock.Stock.LastDev,
                    Industry = Stock.Stock.Industry,
                    MarcketCap = Stock.Stock.MarcketCap

                }).ToListAsync();
        }
    }
}