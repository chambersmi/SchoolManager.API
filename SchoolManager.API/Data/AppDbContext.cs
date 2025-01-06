using Microsoft.EntityFrameworkCore;
using SchoolManager.API.Models.DomainModels;

namespace SchoolManager.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<StudentAddress> StudentAddresses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StudentAddress>()
                .HasKey(sa => new { sa.StudentID, sa.AddressID });

            // Define relationships
            modelBuilder.Entity<StudentAddress>()
                .HasOne(sa => sa.Student)
                .WithMany(s => s.StudentAddresses)
                .HasForeignKey(sa => sa.StudentID);

            modelBuilder.Entity<StudentAddress>()
                .HasOne(sa => sa.Address)
                .WithMany(a => a.StudentAddresses)
                .HasForeignKey(sa => sa.AddressID);                
        }
    }
}
