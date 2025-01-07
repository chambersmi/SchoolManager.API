using SchoolManager.API.Data;
using SchoolManager.API.Models.DomainModels;

namespace SchoolManager.API.Repos.Repositories.Students
{
    public class StudentWithAddressRepository : IStudentWithAddressRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<StudentWithAddressRepository> _logger;

        public StudentWithAddressRepository(AppDbContext context, ILogger<StudentWithAddressRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task AddStudentWithAddressAsync(StudentAddress studentAddress)
        {

            _context.StudentAddresses?.Add(studentAddress);
            await _context.SaveChangesAsync();
        }
    }
}
