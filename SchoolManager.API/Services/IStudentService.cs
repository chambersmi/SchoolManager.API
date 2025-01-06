using SchoolManager.API.Models.DTOs;
using SchoolManager.API.Models.Helpers;

namespace SchoolManager.API.Services
{
    public interface IStudentService
    {
        Task<int> AddStudentAsync(StudentDTO dto);
        Task<IEnumerable<StudentDTO>> GetAllStudentsAsync();
        Task<StudentDTO> GetStudentByIdAsync(int id);
        Task<bool> DeleteStudentByIdAsync(int studentId);
        Task AddStudentWithAddressAsync(StudentDTO studentDTO, AddressDTO addressDTO);
        Task<IEnumerable<StudentDTO>> GetAllStudentsWithAddressesAsync();
    }
}
