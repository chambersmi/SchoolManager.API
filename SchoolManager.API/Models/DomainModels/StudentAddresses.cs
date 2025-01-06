namespace SchoolManager.API.Models.DomainModels
{
    public class StudentAddress
    {
        public int StudentID { get; set; }
        public Student? Student { get; set; }

        public int AddressID { get; set; }
        public Address? Address { get; set; }
    }
}
