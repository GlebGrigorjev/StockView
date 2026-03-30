using api.Data;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class PortfolioRepository : IPortfolioRepository
    {
        private readonly Context _context;

        public PortfolioRepository(Context context)
        {
            _context = context;
        }

        public async Task<Portfolio> CreateAsync(Portfolio portfolio)
        {
            await _context.Portfolios.AddAsync(portfolio);
            await _context.SaveChangesAsync();

            return portfolio;
        }

        public async Task<Portfolio?> DeletePortfolioAsync(AppUser appUser, string symbol)
        {
            Portfolio? portfolio = await _context.Portfolios.FirstOrDefaultAsync(p => p.AppUserId == appUser.Id && p.Stock.Symbol.ToLower() == symbol.ToLower());

            if (portfolio == null)
                return null;

            _context.Portfolios.Remove(portfolio);
            await _context.SaveChangesAsync();

            return portfolio;
        }

        public async Task<List<Stock>> GetPortfolioByUserId(AppUser user)
        {
            return await _context.Portfolios.Where(x => x.AppUserId == user.Id)
                .Select(stock => new Stock
                {
                    Id = stock.Stock.Id,
                    Symbol = stock.Stock.Symbol,
                    CompanyName = stock.Stock.CompanyName,
                    Industry = stock.Stock.Industry,
                    MarketCap = stock.Stock.MarketCap,
                    Purchase = stock.Stock.Purchase,
                    LastDividend = stock.Stock.LastDividend
                }).ToListAsync();
        }
    }
}
