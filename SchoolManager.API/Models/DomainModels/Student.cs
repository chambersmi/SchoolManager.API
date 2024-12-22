using System.ComponentModel.DataAnnotations;

namespace SchoolManager.API.Models.DomainModels
{
    public class Student
    {
        public int StudentID { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Birthdate { get; set; }
        public string? SSN { get; set; }
        public ICollection<Address>? Addresses { get; set; }
    }
}
