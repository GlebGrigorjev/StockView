using api.DTOs.Comment;
using api.Extensions;
using api.Helpers;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Api.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/comment")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IStockRepository _stockRepository;
        private readonly IFMPService _fmpService;
        private readonly UserManager<AppUser> _userManager;

        public CommentController(ICommentRepository commentRepository, IStockRepository stockRepository, IFMPService fmpService, UserManager<AppUser> userManager)
        {
            _commentRepository = commentRepository;
            _stockRepository = stockRepository;
            _fmpService = fmpService;
            _userManager = userManager;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll([FromQuery] CommentQueryObject queryObject)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            List<Comment> comments = await _commentRepository.GetAllCommentsAsync(queryObject);
            List<CommentDto> commentsDto = comments.Select(x => x.ToCommentDto()).ToList();

            return Ok(commentsDto);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var comment = await _commentRepository.GetCommentByIdAsync(id);

            if (comment == null) return NotFound();

            return Ok(comment.ToCommentDto());
        }

        [HttpPost]
        [Route("{symbol:alpha}")]
        public async Task<IActionResult> Create([FromRoute] string symbol, [FromBody] CreateCommentDto createCommentDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            Stock? stock = await _stockRepository.GetStockBySymbolAsync(symbol);
            if (stock == null)
            {
                stock = await _fmpService.FindStockBySymbolAsync(symbol);
                if (stock == null)
                    return NotFound($"Stock with symbol '{symbol}' does not exist.");
                else
                    await _stockRepository.CreateStockAync(stock);
            }

            string userName = User.GetUserName();
            AppUser? user = await _userManager.FindByNameAsync(userName);

            Comment commentModel = createCommentDto.ToCommentFromCreateComment(stock.Id);
            commentModel.AppUserId = user!.Id;
            await _commentRepository.CreateCommentAync(commentModel);

            return CreatedAtAction(nameof(GetById), new { id = commentModel.Id }, commentModel.ToCommentDto());
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var comment = await _commentRepository.DeleteCommentAsync(id);

            if (comment == null) return NotFound(); ;

            return NoContent();
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCommentDto updateCommentDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            Comment? commentModel = await _commentRepository.UpdateCommentAync(id, updateCommentDto.ToCommentFromUpdate(id));

            if (commentModel == null) return NotFound();

            return Ok(commentModel.ToCommentDto());
        }
    }
}
