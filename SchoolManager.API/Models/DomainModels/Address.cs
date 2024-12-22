using System.ComponentModel.DataAnnotations;

namespace SchoolManager.API.Models.DomainModels
{
    public class Address
    {
        public int AddressID { get; set; }
        public string? Street1 { get; set; }
        public string? Street2 { get; set; }
        public string? City { get; set; }

        public string? ZipCode { get; set; }
        public ICollection<Student>? Students { get; set; }

        [MaxLength(2)]
        private string? _state { get; set; }

        public string State
        {
            get => _state;
            set => _state = value?.ToUpper();
        }
    }
}
