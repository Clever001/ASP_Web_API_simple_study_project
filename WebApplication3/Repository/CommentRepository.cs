using api.Data;
using api.Models;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Dtos.Comment;
using WebApplication3.Interfaces;
using WebApplication3.Mappers;

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

    public async Task<Comment> CreateComment(Comment comment) {
        await _context.Comments.AddAsync(comment);
        await _context.SaveChangesAsync();
        return comment;
    }

    public async Task<Comment?> UpdateComment(int id, UpdateCommentDto commentDto) {
        var comment = await _context.Comments.FindAsync(id);
        if (comment is null) return null;

        comment.Update(commentDto);
        await _context.SaveChangesAsync();
        return comment;
    }

    public async Task<Comment?> DeleteAsync(int id) {
        var comment = await _context.Comments.FindAsync(id);
        if (comment is null) return null;

        _context.Comments.Remove(comment);
        await _context.SaveChangesAsync();
        return comment;
    }
}