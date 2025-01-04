using Microsoft.AspNetCore.Mvc;
using SchoolManager.API.Models.DomainModels;
using SchoolManager.API.Models.DTOs;
using SchoolManager.API.Models.Helpers;
using SchoolManager.API.Services.Repositories;

namespace SchoolManager.API.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IAddressRepository _addressRepository;

        public StudentService(IStudentRepository studentRepository, IAddressRepository addressRepository)
        {
            _studentRepository = studentRepository;
            _addressRepository = addressRepository;
        }

        public async Task<int> AddStudentAsync(CreateStudentRequestDTO dto)
        {
            var student = new Student
            {
                FirstName = dto.FirstName,
                MiddleName = dto.MiddleName,
                LastName = dto.LastName,
                Birthdate = dto.Birthdate,
                SSN = dto.SSN
            };

            await _studentRepository.AddStudentAsync(student);
            return student.StudentID;
        }

        public async Task<IEnumerable<StudentDTO>> GetAllStudentsAsync()
        {
            var students = await _studentRepository.GetAllStudentsAsync();

            return students.Select(student => new StudentDTO
            {
                StudentID = student.StudentID,
                FirstName = student.FirstName,
                MiddleName = student.MiddleName,
                LastName = student.LastName,
                SSN = student.SSN,
                Birthdate = student.Birthdate,
                Addresses = student.Addresses.Select(address => new AddressDTO
                {
                    Street1 = address.Street1,
                    Street2 = address.Street2,
                    City = address.City,
                    State = address.State,
                    ZipCode = address.ZipCode
                }).ToList()
            }).ToList();
        }

        public async Task<StudentDTO> GetStudentByIdAsync(int id)
        {
            var student = await _studentRepository.GetStudentByIdAsync(id);
            
            if(student != null)
            {
                return new StudentDTO
                {
                    StudentID = student.StudentID,
                    FirstName = student.FirstName,
                    MiddleName = student.MiddleName,
                    LastName = student.LastName,
                    Birthdate = student.Birthdate,
                    SSN = student.SSN,
                    Addresses = student.Addresses.Select(address => new AddressDTO
                    {
                        Street1 = address.Street1,
                        Street2 = address.Street2,
                        City = address.City,
                        State = address.State,
                        ZipCode = address.ZipCode
                    }).ToList()
                };
            } else
            {
                return null;
            }
        }

        public async Task<bool> DeleteStudentByIdAsync(int id)
        {
            return await _studentRepository.DeleteStudentByIdAsync(id);
        }
        

        //link student to address?
    }
}
