using SchoolManager.API.Data;
using SchoolManager.API.Models.DomainModels;

namespace SchoolManager.API.Repos.Repositories
{
    public class StudentWithAddressRepository : IStudentWithAddressRepository
    {
        private readonly AppDbContext _context;

        public StudentWithAddressRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddStudentWithAddressAsync(StudentAddress studentAddress)
        {

            _context.StudentAddresses?.Add(studentAddress);
            await _context.SaveChangesAsync();
        }
    }
}
