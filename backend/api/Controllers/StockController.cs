using api.DTOs.Stock;
using api.Helpers;
using api.Mappers;
using api.Models;
using Api.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly IStockRepository _stockRepository;

        public StockController(IStockRepository stockRepository)
        {
            _stockRepository = stockRepository;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject query)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            List<Stock> stocks = await _stockRepository.GetAllStocksAsync(query);
            List<StockDto> stocksDto = stocks.Select(stock => stock.ToStockDto()).ToList();

            return Ok(stocksDto);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetOneById([FromRoute] int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            Stock? stock = await _stockRepository.GetStockByIdAsync(id);

            if (stock == null) return NotFound();

            return Ok(stock.ToStockDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStockRequestDto createStockDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            Stock stockModel = createStockDto.ToStockFromCreateDto();

            await _stockRepository.CreateStockAync(stockModel);

            return CreatedAtAction(nameof(GetOneById), new { id = stockModel.Id }, stockModel.ToStockDto());
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequestDto updateStockDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            Stock? stockModel = await _stockRepository.UpdateStockAync(id, updateStockDto);

            if (stockModel == null) return NotFound();

            return Ok(stockModel.ToStockDto());
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            Stock? stockModel = await _stockRepository.DeletetockAync(id);

            if (stockModel == null) return NotFound();

            return NoContent();
        }
    }
}
