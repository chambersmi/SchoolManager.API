using SchoolManager.API.Models.DomainModels;

namespace SchoolManager.API.Repos.Repositories.Students
{
    public interface IStudentWithAddressRepository
    {
        Task AddStudentWithAddressAsync(StudentAddress studentAddress);
    }
}
