using api.Models;

namespace WebApplication3.Interfaces;

public interface ICommentRepository {
    Task<List<Comment>> GetAll();
    Task<Comment?> GetById(int id);
}