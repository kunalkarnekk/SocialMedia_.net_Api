using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Extension;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/portfolio")]
    [ApiController]
    public class PortFolioController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IStockRepository _stockRepo;
        private readonly IportfolioRepository _portFolioRepo;
        public PortFolioController(UserManager<AppUser> userManager , IStockRepository stockRepo, IportfolioRepository portFolioRepo)
        {
            _userManager = userManager;
            _stockRepo = stockRepo;
            _portFolioRepo = portFolioRepo;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserPortFolio()
        {
            var userName= User.getUserName();
            var appUser = await _userManager.FindByNameAsync(userName);
            var userPortFolio = await _portFolioRepo.GetUserPortFolio(appUser);
            return Ok(userPortFolio);

        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddPortfolio(string symbol)
        {
            var userName = User.getUserName();
            var appUser = await _userManager.FindByNameAsync(userName);
            var stock = await _stockRepo.GetBySymbolAsync(symbol);

            if(stock == null) return BadRequest("Stock not found");

            var userPortFolio = await _portFolioRepo.GetUserPortFolio(appUser);

            if(userPortFolio.Any(e => e.Symbol.ToLower() == symbol.ToLower())) return BadRequest("You cannot add same portfolio");

            var portFolioModel = new PortFolio
            {
                StockId = stock.Id,
                AppUserId = appUser.Id

            };

            await _portFolioRepo.CreateAsync(portFolioModel);

            if(portFolioModel == null)
            {
                return StatusCode( 500, "Couldent not created");
            }
            else
            {
                return Created();
            }
        }
        
    }
}