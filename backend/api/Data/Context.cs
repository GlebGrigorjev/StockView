using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    public class Context(DbContextOptions dbContextOptions) : IdentityDbContext<AppUser>(dbContextOptions)
    {
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Portfolio> Portfolios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Portfolio>(x => x.HasKey(p => new { p.AppUserId, p.StockId }));
            modelBuilder.Entity<Portfolio>().HasOne(p => p.AppUser).WithMany(u => u.Protfolios).HasForeignKey(p => p.AppUserId);
            modelBuilder.Entity<Portfolio>().HasOne(p => p.Stock).WithMany(s => s.Protfolios).HasForeignKey(p => p.StockId);

            List<IdentityRole> roles =
            [
                new IdentityRole { Id = "Admin", ConcurrencyStamp = "Admin", Name = "Admin", NormalizedName = "ADMIN" },
                new IdentityRole { Id = "User", ConcurrencyStamp = "User", Name = "User", NormalizedName = "USER" }
            ];
            modelBuilder.Entity<IdentityRole>().HasData(roles);
        }
    }
}
