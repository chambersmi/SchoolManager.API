namespace SchoolManager.API.Models.Helpers
{
    public class UpdateStudentRequestDTO
    {
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public DateTime Birthdate { get; set; }
        public string? SSN { get; set; }
        public List<CreateAddressRequestDTO>? Addresses { get; set; }
    }
}
