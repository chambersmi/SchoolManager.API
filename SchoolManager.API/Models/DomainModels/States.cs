using System.ComponentModel.DataAnnotations;

namespace SchoolManager.API.Models.DomainModels
{
    public class States
    {
        [MaxLength(2)]
        public string? Abbreviation { get; set; }
        public string? Name { get; set; }
    }
}
