using Microsoft.AspNetCore.Http.HttpResults;
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

        public async Task<IEnumerable<Student>> GetAllStudentsWithAddressAsync()
        {
            return await _context.Students.Include(a => a.Addresses).ToListAsync();
        }

        public async Task<Student?> GetStudentByIdAsync(int id)
        {
            return await _context.Students.Include(a => a.Addresses).FirstOrDefaultAsync(x => x.StudentID == id);
        }

        public async Task<bool> DeleteStudentByIdAsync(int id)
        {
            var student = await _context.Students.Include(a => a.Addresses).FirstOrDefaultAsync(s => s.StudentID == id);

            if(student == null)
            {
                return false;
            }

            _context.Remove(student);
            await _context.SaveChangesAsync();

            return true;
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

        public async Task JoinStudentToAddressAsync(int studentId, int addressId)
        {
            var student = await _context.Students.FindAsync(studentId);
            var address = await _context.Addresses.FindAsync(addressId);

            if(student != null && address != null)
            {
                student.Addresses.Add(address);
                await _context.SaveChangesAsync();
            }
        }
    }
}
