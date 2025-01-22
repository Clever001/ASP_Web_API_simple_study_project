using System.ComponentModel.DataAnnotations;

namespace WebApplication3.Dtos.Comment;

public class CreateCommentDto {
    [Required]
    [Length(5, 1000)]
    public string Title { get; set; }
    [Required]
    [Length(5, 1000)]
    public string Content { get; set; }
}