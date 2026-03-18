using api.DTOs.Comment;
using api.Models;

namespace api.Mappers
{
    public static class CommentMapper
    {
        public static CommentDto ToCommentDto(this Comment comment) => new()
        {
            Id = comment.Id,
            Title = comment.Title,
            Content = comment.Content,
            DateCreated = comment.DateCreated,
            StockId = comment.StockId
        };

        public static Comment ToCommentFromCreateComment(this CreateCommentDto commentDto, int stockId) => new()
        {
            Title = commentDto.Title,
            Content = commentDto.Content,
            StockId = stockId
        };
    }
}
