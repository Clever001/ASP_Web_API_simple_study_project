using System.ComponentModel.DataAnnotations;

namespace WebApplication3.Dtos.Comment;

public class UpdateCommentDto {
    [Required]
    [Length(5, 1000)]
    public string Title { get; set; } = string.Empty;
    [Required]
    [Length(5, 1000)]
    public string Content { get; set; } = string.Empty;
}