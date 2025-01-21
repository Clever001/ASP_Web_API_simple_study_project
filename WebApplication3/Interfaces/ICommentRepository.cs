using api.Models;
using WebApplication3.Dtos.Comment;

namespace WebApplication3.Interfaces;

public interface ICommentRepository {
    Task<List<Comment>> GetAll();
    Task<Comment?> GetById(int id);
    Task<Comment> CreateComment(Comment stockDto);
    Task<Comment?> UpdateComment(int id, UpdateCommentDto commentDto);
    Task<Comment?> DeleteAsync(int id);
}