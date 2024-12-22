using SchoolManager.API.Models.DomainModels;

namespace SchoolManager.API.Models.DTOs
{
    public class AddressDTO
    {
        public int AddressID { get; set; }
        public string? Street1 { get; set; }
        public string? Street2 { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? ZipCode { get; set; }
        public List<StudentDTO> Students { get; set; } = new List<StudentDTO>();
    }
}
