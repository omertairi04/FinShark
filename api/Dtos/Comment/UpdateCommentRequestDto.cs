using System.ComponentModel.DataAnnotations;

namespace api.Dtos.Comment;

public class UpdateCommentRequestDto
{
    [Required]
    [MinLength(3, ErrorMessage = "Title must be longer than 3 characters.")]
    [MaxLength(280, ErrorMessage = "Title must be shorter than 280 characters.")]
    public string Title { get; set; } = string.Empty;
    [Required]
    [MinLength(1, ErrorMessage = "Content must be longer than 1 characters.")]
    [MaxLength(500, ErrorMessage = "Content must be shorter than 500 characters.")]
    public string Content { get; set; } = string.Empty;
}