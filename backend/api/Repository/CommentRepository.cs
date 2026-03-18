using api.Data;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly Context _context;

        public CommentRepository(Context context)
        {
            _context = context;
        }

        public async Task<List<Comment>> GetAllCommentsAsync() => await _context.Comments.ToListAsync();

        public async Task<Comment?> GetCommentByIdAsync(int id)
        {
            Comment? comment = await _context.Comments.FirstOrDefaultAsync(x => x.Id == id);

            if (comment == null) return null;

            return comment;
        }

        public async Task<Comment> CreateCommentAync(Comment comment)
        {
            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();

            return comment;
        }
    }
}
