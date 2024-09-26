using api.Interfaces;
using api.Mapper;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[Route("api/comment")]
[ApiController]
public class CommentController : ControllerBase
{
    private readonly ICommentRepository _commentRepository;

    public CommentController(ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var comments = await _commentRepository.GetAllAsync();

        var commentDto = comments
            .Select(x => x.ToCommentDto());
        
        return Ok(commentDto);
    }
}