using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using SchoolManager.API.Data;
using SchoolManager.API.Models.DomainModels;

namespace SchoolManager.API.Services.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<StudentRepository> _logger;

        public StudentRepository(AppDbContext context, ILogger<StudentRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Student> AddStudentAsync(Student student)
        {
            if(student == null)
            {
                throw new ArgumentNullException(nameof(Student), "Student cannot be found");
            }
            await _context.Students.AddAsync(student);
            await _context.SaveChangesAsync();

            return student;
        }

        public async Task<IEnumerable<Student>> GetAllStudentsAsync()
        {
            return await _context.Students.ToListAsync();
        }

        public async Task<IEnumerable<Student>> GetAllStudentsWithAddressesAsync()
        {
            var studentsWithAddresses = await _context.Students.Include(s => s.StudentAddresses).ThenInclude(sa => sa.Address).ToListAsync();

            if(studentsWithAddresses == null)
            {
                return Enumerable.Empty<Student>();
            }

            return studentsWithAddresses;
        }

        public async Task<Student?> GetStudentByIdAsync(int id)
        {
            return await _context.Students.FirstOrDefaultAsync(x => x.StudentID == id);
        }

        public async Task<bool> DeleteStudentByIdAsync(int studentId)
        {
            // Find the student along with their associated addresses via the StudentAddresses join table
            var student = await _context.Students
                .Include(s => s.StudentAddresses)  // Include the related StudentAddresses
                .ThenInclude(sa => sa.Address)     // Then include the related Address
                .FirstOrDefaultAsync(s => s.StudentID == studentId); // Filter by student ID

            if(student == null)
            {
                return false;
            }

            // Remove all StudentAddresses entries and associated addresses
            foreach(var studentAddress in student.StudentAddresses)
            {
                var isAddressUsedByOthers = await _context.StudentAddresses.AnyAsync(sa => sa.AddressID == studentAddress.AddressID && sa.StudentID != studentId);

                //Is this needed?
                if(!isAddressUsedByOthers && studentAddress.Address != null)
                {
                    _context.Addresses.Remove(studentAddress.Address);
                }

                _context.StudentAddresses.Remove(studentAddress);
            }

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
            
            return true;
        }

        public async Task<Student?> UpdateAsync(Student student)
        {
            var existingStudent = await _context.Students.FirstOrDefaultAsync(x => x.StudentID == student.StudentID);

            if (existingStudent != null)
            {
                _context.Entry(existingStudent).CurrentValues.SetValues(student);
                await _context.SaveChangesAsync();

                return student;
            }

            return null;
        }
    }
}
