using SchoolManager.API.Models.DomainModels;

namespace SchoolManager.API.Services.Repositories
{
    public interface IStudentRepository
    {
        Task<Student> AddStudentAsync(Student student);
        Task<IEnumerable<Student>> GetAllStudentsAsync();
        Task<Student?> GetStudentByIdAsync(int id);
        Task<Student?> UpdateAsync(Student student);
        Task<bool> DeleteStudentByIdAsync(int id);
    }
}
