using api.Data;
using api.DTOs.Stock;
using api.Helpers;
using api.Models;
using Api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Api.Repository
{
    public class StockRepository : IStockRepository
    {
        private readonly Context _context;

        public StockRepository(Context context)
        {
            _context = context;
        }

        public async Task<List<Stock>> GetAllStocksAsync(QueryObject query)
        {
            var stocks = _context.Stocks.Include(x => x.Comments).ThenInclude(y => y.AppUser).AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.CompanyName))
                stocks = stocks.Where(x => x.CompanyName.Contains(query.CompanyName));

            if (!string.IsNullOrWhiteSpace(query.Symbol))
                stocks = stocks.Where(x => x.Symbol.Contains(query.Symbol));

            if (!string.IsNullOrWhiteSpace(query.SortBy))
                if (query.SortBy.Equals("Symbol", StringComparison.OrdinalIgnoreCase))
                    stocks = query.IsDesc ? stocks.OrderByDescending(x => x.Symbol) : stocks.OrderBy(x => x.Symbol);

            var skipNumber = (query.PageNumber - 1) * query.PageSize;

            return await stocks.Skip(skipNumber).Take(query.PageSize).ToListAsync();
        }

        public async Task<Stock?> GetStockByIdAsync(int id) => await _context.Stocks.Include(x => x.Comments).FirstOrDefaultAsync(x => x.Id == id);

        public async Task<Stock?> UpdateStockAync(int id, UpdateStockRequestDto updateStockDto)
        {
            Stock? existingStock = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);

            if (existingStock == null) return null;

            existingStock.Symbol = updateStockDto.Symbol;
            existingStock.CompanyName = updateStockDto.CompanyName;
            existingStock.Industry = updateStockDto.Industry;
            existingStock.MarketCap = updateStockDto.MarketCap;
            existingStock.Purchase = updateStockDto.Purchase;
            existingStock.LastDividend = updateStockDto.LastDividend;

            await _context.SaveChangesAsync();

            return existingStock;
        }

        public async Task<Stock> CreateStockAync(Stock stockModel)
        {
            await _context.Stocks.AddAsync(stockModel);
            await _context.SaveChangesAsync();

            return stockModel;
        }

        public async Task<Stock?> DeletetockAync(int id)
        {
            Stock? stockModel = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);

            if (stockModel == null) return null;

            _context.Stocks.Remove(stockModel);
            await _context.SaveChangesAsync();

            return stockModel;
        }

        public async Task<bool> StockExistsAsync(int id) => await _context.Stocks.AnyAsync(x => x.Id == id);

        public async Task<Stock?> GetStockBySymbolAsync(string symbol) => await _context.Stocks.FirstOrDefaultAsync(x => x.Symbol == symbol);
    }
}
