using api.Models;
using WebApplication3.Dtos.Comment;

namespace WebApplication3.Mappers;

public static class CommentMapper {
    public static CommentDto ToCommentDto(this Comment comment) {
        return new() {
            Id = comment.Id,
            Title = comment.Title,
            Content = comment.Content,
            CreatedOn = comment.CreatedOn,
            StockId = comment.StockId
        };
    }

    public static CommentShortDto ToCommentShortDto(this Comment comment) {
        return new() {
            Id = comment.Id,
            Title = comment.Title,
            Content = comment.Content,
            CreatedOn = comment.CreatedOn
        };
    }
}