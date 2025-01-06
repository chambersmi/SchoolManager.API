using SchoolManager.API.Models.DomainModels;

namespace SchoolManager.API.Repos.Repositories
{
    public interface IStudentWithAddressRepository
    {
        Task AddStudentWithAddressAsync(StudentAddress studentAddress);
    }
}
