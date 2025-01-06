using Microsoft.AspNetCore.Mvc;
using SchoolManager.API.Models.DomainModels;
using SchoolManager.API.Models.DTOs;
using SchoolManager.API.Models.Helpers;
using SchoolManager.API.Repos.Repositories;
using SchoolManager.API.Services.Repositories;

namespace SchoolManager.API.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IStudentWithAddressRepository _studentAddressRepository;
        private readonly IAddressService _addressService;

        public StudentService(IStudentRepository studentRepository, IStudentWithAddressRepository studentAddressRepository, IAddressService addressService)
        {
            _studentRepository = studentRepository;
            _studentAddressRepository = studentAddressRepository;
            _addressService = addressService;
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
                Birthdate = student.Birthdate                
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
                };
            } else
            {
                return new StudentDTO();
            }
        }

        public async Task<bool> DeleteStudentByIdAsync(int studentId)
        {
            return await _studentRepository.DeleteStudentByIdAsync(studentId);
        }

        public async Task AddStudentWithAddressAsync(CreateStudentRequestDTO studentDTO, CreateAddressRequestDTO addressDTO)
        {
            var student = new Student
            {
                FirstName = studentDTO.FirstName,
                MiddleName = studentDTO.MiddleName,
                LastName = studentDTO.LastName,
                Birthdate = studentDTO.Birthdate,
                SSN = studentDTO.SSN
            };

            // Add student
            await _studentRepository.AddStudentAsync(student);

            // Add address
            var address = await _addressService.GetOrCreateAddressAsync(addressDTO);

            var studentAddress = new StudentAddress
            {
                StudentID = student.StudentID,
                AddressID = address.AddressID
            };

            await _studentAddressRepository.AddStudentWithAddressAsync(studentAddress);
        }

        public async Task<IEnumerable<StudentDTO>> GetAllStudentsWithAddressesAsync()
        {
            var studentsWithAddresses = await _studentRepository.GetAllStudentsWithAddressesAsync();

            var studentDTO = studentsWithAddresses.Select(student => new StudentDTO
            {
                StudentID = student.StudentID,
                FirstName = student.FirstName,
                MiddleName = student.MiddleName,
                LastName = student.LastName,
                Birthdate = student.Birthdate,
                SSN = student.SSN,
                Addresses = student.StudentAddresses
                .Where(sa => sa.Address != null)
                .Select(sa => new AddressDTO
                {
                    AddressID = sa.Address?.AddressID ?? 0,
                    Street1 = sa.Address?.Street1 ?? string.Empty,
                    Street2 = sa.Address?.Street2,
                    City = sa.Address?.City ?? string.Empty,
                    State = sa.Address?.State ?? string.Empty,
                    ZipCode = sa.Address?.ZipCode ?? string.Empty
                }).ToList()
            }).ToList();

            return studentDTO;

        }

    }
}
