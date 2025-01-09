using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Data;

public class ApplicationDbContext : DbContext {
    public ApplicationDbContext(DbContextOptions options) : base(options) { }

    public DbSet<Comment> Comments { get; set; } = null!;
    public DbSet<Stock> Stocks { get; set; } = null!;
}