using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace api.Data;

public class ApplicationDbContext : IdentityDbContext<AppUser> {
    public ApplicationDbContext(DbContextOptions options) : base(options) { }

    public DbSet<Comment> Comments { get; set; } = null!;
    public DbSet<Stock> Stocks { get; set; } = null!;
    public DbSet<Portfolio> Portfolios { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder builder) {
        base.OnModelCreating(builder);

        builder.Entity<Portfolio>().HasKey(p => new { p.StockId, p.AppUserId });
        builder.Entity<Portfolio>()
            .HasOne(p => p.Stock)
            .WithMany(s => s.Portfolios)
            .HasForeignKey(p => p.StockId);
        builder.Entity<Portfolio>()
            .HasOne(p => p.AppUser)
            .WithMany(u => u.Portfolios)
            .HasForeignKey(p => p.AppUserId);

        var roles = new List<IdentityRole> {
            new() {
                Name = "Admin",
                NormalizedName = "ADMIN"
            },
            new() {
                Name = "User",
                NormalizedName = "USER"
            },
        };
        builder.Entity<IdentityRole>().HasData(roles);
    }
}