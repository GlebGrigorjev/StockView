using api.DTOs.Comment;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Api.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/comment")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        public readonly ICommentRepository _commentRepository;
        public readonly IStockRepository _stockRepository;

        public CommentController(ICommentRepository commentRepository, IStockRepository stockRepository)
        {
            _commentRepository = commentRepository;
            _stockRepository = stockRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<Comment> comments = await _commentRepository.GetAllCommentsAsync();
            List<CommentDto> commentsDto = comments.Select(x => x.ToCommentDto()).ToList();

            return Ok(commentsDto);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var comment = await _commentRepository.GetCommentByIdAsync(id);

            if (comment == null) return NotFound();

            return Ok(comment.ToCommentDto());
        }

        [HttpPost("{stockId}")]
        public async Task<IActionResult> Create([FromRoute] int stockId, [FromBody] CreateCommentDto createCommentDto)
        {
            if(!await _stockRepository.StockExistsAsync(stockId)) return NotFound($"Stock does not exist!");

            var commentModel = createCommentDto.ToCommentFromCreateComment(stockId);
            await _commentRepository.CreateCommentAync(commentModel);

            return CreatedAtAction(nameof(GetById), new { id = commentModel.Id }, commentModel.ToCommentDto());
        }
    }
}
