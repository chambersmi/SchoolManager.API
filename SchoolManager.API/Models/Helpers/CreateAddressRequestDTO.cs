using SchoolManager.API.Models.DomainModels;

namespace SchoolManager.API.Models.Helpers
{
    public class CreateAddressRequestDTO
    {
        public string? Street1 { get; set; }
        public string? Street2 { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? ZipCode { get; set; }
    }
}
