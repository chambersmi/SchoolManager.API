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
                .HasMany(a => a.Addresses)
                .WithMany(s => s.Students)
                .UsingEntity(j => j.ToTable("StudentAddresses"));

            modelBuilder.Entity<Student>()
                .HasMany(a => a.Addresses)
                .WithMany(s => s.Students)
                .UsingEntity<Dictionary<string, object>>(
                "StudentAddresses",
                j => j.HasOne<Address>().WithMany().HasForeignKey("AddressID").OnDelete(DeleteBehavior.Cascade),
                j => j.HasOne<Student>().WithMany().HasForeignKey("StudentID").OnDelete(DeleteBehavior.Cascade)
                );
        }
    }
}
