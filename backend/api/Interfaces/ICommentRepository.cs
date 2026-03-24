using api.Models;

namespace api.Interfaces
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetAllCommentsAsync();
        Task<Comment?> GetCommentByIdAsync(int id);
        Task<Comment> CreateCommentAync(Comment comment);
        Task<Comment?> UpdateCommentAync(int id, Comment updateStockDto);
        Task<Comment?> DeleteCommentAsync(int id);
    }
}
