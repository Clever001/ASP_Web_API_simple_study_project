using api.Data;
using api.Models;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Interfaces;

namespace WebApplication3.Repository;

public class CommentRepository : ICommentRepository {
    private readonly ApplicationDbContext _context;

    public CommentRepository(ApplicationDbContext context) {
        _context = context;
    }
    
    public async Task<List<Comment>> GetAll() {
        return await _context.Comments.ToListAsync();
    }

    public async Task<Comment?> GetById(int id) {
        return await _context.Comments.FindAsync(id);
    }
}