using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Stock;
using api.Helper;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IStockRepository _StockRepo;
        public StockController(ApplicationDbContext context, IStockRepository StockRepo)
        {
            _context = context;
            _StockRepo = StockRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject query)
        {
             if(!ModelState.IsValid)
                    return BadRequest(ModelState);

            var stocks = await _StockRepo.GetAllAsync(query);

            var stockDto = stocks.Select(s => s.ToStockDto());

            return Ok(stockDto);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
             if(!ModelState.IsValid)
                    return BadRequest(ModelState);

            var stock = await _StockRepo.GetByIdAsync(id);

            if(stock == null)
            {
                return NotFound();
            }

            return Ok(stock.ToStockDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStockRequestDto stockDto)
        {
             if(!ModelState.IsValid)
                    return BadRequest(ModelState);

            var stockModel = stockDto.toStockFromCreateDto();

            await _StockRepo.CreateAsync(stockModel);

            return CreatedAtAction(nameof(GetById), new {id = stockModel.Id} , stockModel.ToStockDto());
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id , [FromBody] UpdateStockRequestDto updateDto)
        {
             if(!ModelState.IsValid)
                    return BadRequest(ModelState);
            var stockModel = await _StockRepo.UpdateAsync(id , updateDto);
            if(stockModel == null)
            {
                return NotFound();
            }
            return Ok(stockModel);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
             if(!ModelState.IsValid)
                    return BadRequest(ModelState);
            var stockModel = await _StockRepo.DeleteAsync(id);
            if(stockModel == null)
            {
                return NotFound();
            }

            return NoContent();

        }
    }


}
