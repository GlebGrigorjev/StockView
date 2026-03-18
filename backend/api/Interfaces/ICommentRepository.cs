using api.DTOs.Stock;
using api.Models;

namespace api.Interfaces
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetAllCommentsAsync();
        Task<Comment?> GetCommentByIdAsync(int id);
        Task<Comment> CreateCommentAync(Comment comment);
        //Task<Comment> UpdateStockAync(int id, UpdateStockRequestDto updateStockDto);
        //Task<Comment?> DeletetockAync(int id);
    }
}
