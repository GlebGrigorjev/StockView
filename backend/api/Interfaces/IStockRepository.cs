using api.DTOs.Stock;
using api.Models;

namespace Api.Interfaces
{
    public interface IStockRepository
    {
        Task<List<Stock>> GetAllStocksAsync();
        Task<Stock?> GetStockByIdAsync(int id);
        Task<Stock> CreateStockAync(Stock stockModel);
        Task<Stock> UpdateStockAync(int id, UpdateStockRequestDto updateStockDto);
        Task<Stock?> DeletetockAync(int id);
        Task<bool> StockExistsAsync(int id);
    }
}
