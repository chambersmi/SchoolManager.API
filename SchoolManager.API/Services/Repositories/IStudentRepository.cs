using SchoolManager.API.Models.DomainModels;

namespace SchoolManager.API.Services.Repositories
{
    public interface IStudentRepository
    {
        Task<Student> CreateAsync(Student student);
        Task<IEnumerable<Student>> GetAllAsync();
        Task<Student?> GetByIdAsync(int id);
        Task<Student?> UpdateAsync(Student student);

    }
}
