using Microsoft.EntityFrameworkCore;
using SchoolManager.API.Data;
using SchoolManager.API.Models.DomainModels;

namespace SchoolManager.API.Services.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly AppDbContext _context;

        public StudentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Student> CreateAsync(Student student)
        {
            if(student == null)
            {
                throw new ArgumentNullException(nameof(Student), "Student cannot be found");
            }
            await _context.Students.AddAsync(student);
            await _context.SaveChangesAsync();

            return student;
        }

        public async Task<IEnumerable<Student>> GetAllAsync()
        {
            return await _context.Students.Include(a => a.Addresses).ToListAsync();
        }

        public async Task<Student?> GetByIdAsync(int id)
        {
            return await _context.Students.Include(a => a.Addresses).FirstOrDefaultAsync(x => x.StudentID == id);
        }

        public async Task<Student?> UpdateAsync(Student student)
        {
            var existingStudent = await _context.Students.Include(x => x.Addresses).FirstOrDefaultAsync(x => x.StudentID == student.StudentID);

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
