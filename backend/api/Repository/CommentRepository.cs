using api.Data;
using api.Helpers;
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

        public async Task<List<Comment>> GetAllCommentsAsync(CommentQueryObject queryObject)
        {
            IQueryable<Comment> comments = _context.Comments.Include(x => x.AppUser).AsQueryable();

            if (!string.IsNullOrEmpty(queryObject.Symbol))
                comments = comments.Where(x => x.Stock.Symbol == queryObject.Symbol);

            if (queryObject.IsDesc)
                comments = comments.OrderByDescending(x => x.DateCreated);
            else
                comments = comments.OrderBy(x => x.DateCreated);

            return await comments.ToListAsync();
        }

        public async Task<Comment?> GetCommentByIdAsync(int id)
        {
            Comment? comment = await _context.Comments.Include(x => x.AppUser).FirstOrDefaultAsync(x => x.Id == id);
            if (comment == null) return null;

            return comment;
        }

        public async Task<Comment> CreateCommentAync(Comment comment)
        {
            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();

            return comment;
        }

        public async Task<Comment?> DeleteCommentAsync(int id)
        {
            var commentModel = await _context.Comments.FirstOrDefaultAsync(x => x.Id == id);
            if (commentModel == null) return null;

            _context.Comments.Remove(commentModel);
            await _context.SaveChangesAsync();

            return commentModel;
        }

        public async Task<Comment?> UpdateCommentAync(int id, Comment comment)
        {
            var existingComment = await _context.Comments.FindAsync(id);

            if (existingComment == null) return null;

            existingComment.Title = comment.Title;
            existingComment.Content = comment.Content;

            await _context.SaveChangesAsync();

            return existingComment;
        }
    }
}
