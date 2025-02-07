﻿using System.ComponentModel.DataAnnotations;

namespace SchoolManager.API.Models.DomainModels
{
    public class Address
    {
        public int AddressID { get; set; }
        public string? Street1 { get; set; }
        public string? Street2 { get; set; }
        public string? City { get; set; }

        public string? State { get; set; }

        public string? ZipCode { get; set; }
        //public ICollection<Student>? Students { get; set; } = new List<Student>();
        public ICollection<StudentAddress> StudentAddresses { get; set; } = new List<StudentAddress>();


    }
}
