using api.Extensions;
using api.Interfaces;
using api.Models;
using Api.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PortfolioController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IStockRepository _stockRepository;
        private readonly IPortfolioRepository _portfolioRepository;
        private readonly IFMPService _fmpService;

        public PortfolioController(UserManager<AppUser> userManager, IStockRepository stockRepository, IPortfolioRepository portfolioRepository, IFMPService fmpService)
        {
            _userManager = userManager;
            _stockRepository = stockRepository;
            _portfolioRepository = portfolioRepository;
            _fmpService = fmpService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserPortfolio()
        {
            string username = User.GetUserName();
            AppUser? appUser = await _userManager.FindByNameAsync(username);
            List<Stock> userPortfolio = await _portfolioRepository.GetPortfolioByUserId(appUser);

            return Ok(userPortfolio);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddPortfolio(string symbol)
        {
            string username = User.GetUserName();
            AppUser? appUser = await _userManager.FindByNameAsync(username);
            Stock? stock = await _stockRepository.GetStockBySymbolAsync(symbol);

            if (stock == null)
            {
                stock = await _fmpService.FindStockBySymbolAsync(symbol);
                if (stock == null)
                    return NotFound($"Stock with symbol '{symbol}' does not exist.");
                else
                    await _stockRepository.CreateStockAync(stock);
            }

            if (stock == null)
                return NotFound($"Stock with symbol '{symbol}' not found.");

            var userPortfolio = await _portfolioRepository.GetPortfolioByUserId(appUser);

            if (userPortfolio.Any(s => s.Id == stock.Id))
                return BadRequest($"Stock with symbol '{symbol}' is already in the user's portfolio.");

            var portfolio = new Portfolio
            {
                AppUserId = appUser.Id,
                StockId = stock.Id
            };

            await _portfolioRepository.CreateAsync(portfolio);

            if (portfolio == null)
                return StatusCode(500, "Failed to add stock to portfolio.");
            else
                return Created();
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> RemovePortfolio(string symbol)
        {
            string username = User.GetUserName();
            AppUser? appUser = await _userManager.FindByNameAsync(username);
            List<Stock> userPortfolio = await _portfolioRepository.GetPortfolioByUserId(appUser);
            Stock? filteredStock = userPortfolio.FirstOrDefault(s => s.Symbol.Equals(symbol, StringComparison.OrdinalIgnoreCase));

            if (filteredStock != null)
                await _portfolioRepository.DeletePortfolioAsync(appUser, symbol);
            else
                return BadRequest("Stock not found in user's portfolio.");

            return Ok();
        }
    }
}
