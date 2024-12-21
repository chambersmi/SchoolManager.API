using Microsoft.AspNetCore.Mvc;
using SchoolManager.API.Models.DomainModels;
using SchoolManager.API.Models.DTOs;
using SchoolManager.API.Services.Repositories;

namespace SchoolManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : Controller
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IAddressRepository _addressRepository;

        public StudentController(IStudentRepository studentRepository, IAddressRepository addressRepository)
        {
            _studentRepository = studentRepository;
            _addressRepository = addressRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateStudent([FromBody] StudentDTO request)
        {
            var student = new Student
            {
                StudentID = request.StudentID,
                FirstName = request.FirstName,
                MiddleName = request.MiddleName,
                LastName = request.LastName,
                Birthdate = request.Birthdate,
                SSN = request.SSN,
                Addresses = new List<Address>()
            };

            foreach(var studentRequestID in request.Addresses)
            {
                var existingStudentAddress = await _addressRepository.GetByIdAsync(studentRequestID.AddressID);
                if(existingStudentAddress != null)
                {
                    student.Addresses.Add(existingStudentAddress);
                }
            }

            await _studentRepository.CreateAsync(student);

            // Return DTO
            var responseDTO = new StudentDTO
            {
                StudentID = student.StudentID,
                FirstName = student.FirstName,
                MiddleName = student.MiddleName,
                LastName = student.LastName,
                Birthdate = student.Birthdate,
                SSN = student.SSN,
                Addresses = student.Addresses.Select(a => new AddressDTO
                {
                    AddressID = a.AddressID,
                    Street1 = a.Street1,
                    Street2 = a.Street2,
                    City = a.City,
                    State = a.State,
                    ZipCode = a.ZipCode
                }).ToList()

            };

            return Ok(responseDTO);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStudentsAsync()
        {
            var students = await _studentRepository.GetAllAsync();

            var response = new List<StudentDTO>();

            foreach(var student in students)
            {
                response.Add(new StudentDTO
                {
                    StudentID = student.StudentID,
                    FirstName = student.FirstName,
                    MiddleName = student.MiddleName,
                    LastName = student.LastName,
                    Birthdate = student.Birthdate,
                    SSN = student.SSN,

                    Addresses = student.Addresses.Select(a => new AddressDTO
                    {
                        AddressID = a.AddressID,
                        Street1 = a.Street1,
                        Street2 = a.Street2,
                        City = a.City,
                        State = a.State,
                        ZipCode = a.ZipCode
                    }).ToList()
                });
            }

            return Ok(response);
        }
    }
}
