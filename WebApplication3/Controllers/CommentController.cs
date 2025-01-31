using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication3.Dtos.Comment;
using WebApplication3.Dtos.Stock;
using WebApplication3.Interfaces;
using WebApplication3.Mappers;

namespace WebApplication3.Controllers;

[Route("api/comment")]
[ApiController]
public class CommentController : ControllerBase {
    private readonly ICommentRepository _commentRepo;
    private readonly IStockRepository _stockRepo;

    public CommentController(ICommentRepository commentRepo, IStockRepository stockRepo) {
        _commentRepo = commentRepo;
        _stockRepo = stockRepo;
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetAll() {
        var comments = await _commentRepo.GetAll();

        return Ok(comments.Select(com => com.ToCommentDto()));
    }

    [HttpGet("{id:int}")]
    [Authorize]
    public async Task<IActionResult> GetById([FromRoute] int id) {
        var comment = await _commentRepo.GetById(id);
        if (comment is null) {
            return NotFound();
        }
        
        return Ok(comment.ToCommentDto());
    }

    [HttpPost("{stockId:int}")]
    [Authorize]
    public async Task<IActionResult> Create([FromRoute] int stockId, [FromBody] CreateCommentDto commentDto) {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        
        if (!await _stockRepo.StockExists(stockId)) {
            return BadRequest("Stock doesn't exist");
        }

        var comment = commentDto.ToCommentFromCreate(stockId);
        await _commentRepo.CreateComment(comment);
        return CreatedAtAction(nameof(GetById), new {id = comment.Id}, comment.ToCommentDto());
    }

    [HttpPut("{id:int}")]
    [Authorize]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCommentDto commentDto) {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        
        var comment = await _commentRepo.UpdateComment(id, commentDto);
        if (comment is null) {
            return NotFound();
        }
        return Ok(comment.ToCommentDto());
    }

    [HttpDelete("{id:int}")]
    [Authorize]
    public async Task<IActionResult> Delete([FromRoute] int id) {
        var comment = await _commentRepo.DeleteAsync(id);
        if (comment is null) {
            return NotFound();
        }
        return NoContent();
    }
}