using Microsoft.AspNetCore.Mvc;
using WebApplication3.Interfaces;
using WebApplication3.Mappers;

namespace WebApplication3.Controllers;

[Route("api/comment")]
[Controller]
public class CommentController : ControllerBase {
    private readonly ICommentRepository _commentRepo;

    public CommentController(ICommentRepository commentRepo) {
        _commentRepo = commentRepo;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() {
        var comments = await _commentRepo.GetAll();

        return Ok(comments.Select(com => com.ToCommentDto()));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id) {
        var comment = await _commentRepo.GetById(id);
        if (comment is null) {
            return NotFound();
        }
        
        return Ok(comment.ToCommentDto());
    }
}