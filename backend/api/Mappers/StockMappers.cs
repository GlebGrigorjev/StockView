using api.DTOs.Stock;
using api.Models;

namespace api.Mappers
{
    public static class StockMappers
    {
        public static StockDto ToStockDto(this Stock stock) => new()
        {
            Id = stock.Id,
            Symbol = stock.Symbol,
            CompanyName = stock.CompanyName,
            Industry = stock.Industry,
            MarketCap = stock.MarketCap,
            Purchase = stock.Purchase,
            LastDividend = stock.LastDividend,
            Comments = stock.Comments.Select(c => c.ToCommentDto()).ToList()
        };

        public static Stock ToStockFromCreateDto(this CreateStockRequestDto stockDto) => new()
        {
            Symbol = stockDto.Symbol,
            CompanyName = stockDto.CompanyName,
            Industry = stockDto.Industry,
            MarketCap = stockDto.MarketCap,
            Purchase = stockDto.Purchase,
            LastDividend = stockDto.LastDividend
        };

        public static Stock ToStockFromFMP(this FMPStock fmpStock) => new()
        {
            Symbol = fmpStock.symbol,
            CompanyName = fmpStock.companyName,
            Industry = fmpStock.industry,
            MarketCap = fmpStock.marketCap,
            Purchase = (decimal)fmpStock.price,
            LastDividend = (decimal)fmpStock.lastDividend
        };
    }
}
