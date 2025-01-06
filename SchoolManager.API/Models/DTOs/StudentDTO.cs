using SchoolManager.API.Models.DomainModels;

namespace SchoolManager.API.Models.DTOs
{
    public class StudentDTO
    {
        public int StudentID { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public DateTime Birthdate { get; set; }
        public string? SSN { get; set; }
        //public List<AddressDTO> Addresses { get; set; } = new List<AddressDTO>();
    }
}
