namespace SchoolManager.API.Data.DomainModels
{
    public class Student
    {
        public int StudentID { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public DateTime Birthdate { get; set; }
        public string? SSN { get; set; }

        public ICollection<Address> Addresses { get; set; } = new List<Address>();
    }
}
