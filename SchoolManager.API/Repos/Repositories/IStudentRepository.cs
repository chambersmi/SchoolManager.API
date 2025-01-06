using SchoolManager.API.Models.DomainModels;
using SchoolManager.API.Models.DTOs;

namespace SchoolManager.API.Services.Repositories
{
    public interface IStudentRepository
    {
        Task<Student> AddStudentAsync(Student student);
        Task<IEnumerable<Student>> GetAllStudentsAsync();
        Task<Student?> GetStudentByIdAsync(int id);
        Task<Student?> UpdateAsync(Student student);
        Task<bool> DeleteStudentByIdAsync(int id);
        Task JoinStudentToAddressAsync(int studentId, int addressId);
    }
}
