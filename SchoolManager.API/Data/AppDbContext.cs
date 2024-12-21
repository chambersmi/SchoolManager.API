using Microsoft.EntityFrameworkCore;
using SchoolManager.API.Models.DomainModels;

namespace SchoolManager.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Student> Students { get; set; } = null!;
        public DbSet<Address> Addresses { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>()
                .HasMany(s => s.Addresses)
                .WithMany(a => a.Students)
                .UsingEntity(j => j.ToTable("StudentAddresses"));
        }
    }
}
