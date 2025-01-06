using SchoolManager.API.Models.DomainModels;
using SchoolManager.API.Models.DTOs;

namespace SchoolManager.API.Models.Helpers
{
    public class CreateStudentRequestDTO
    {
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public DateTime Birthdate { get; set; }
        public string? SSN { get; set; }
    }
}
