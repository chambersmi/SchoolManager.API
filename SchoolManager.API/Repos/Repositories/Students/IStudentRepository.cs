using SchoolManager.API.Models.DomainModels;
using SchoolManager.API.Models.DTOs;

namespace SchoolManager.API.Repos.Repositories.Students
{
    public interface IStudentRepository
    {
        Task<Student> AddStudentAsync(Student student);
        Task<IEnumerable<Student>> GetAllStudentsAsync();
        Task<Student?> GetStudentByIdAsync(int id);
        Task<Student?> UpdateAsync(Student student);
        //Task<bool> DeleteStudentByIdAsync(int id);
        Task<IEnumerable<Student>> GetAllStudentsWithAddressesAsync();
        Task<bool> DeleteStudentByIdAsync(int studentId);
    }
}
