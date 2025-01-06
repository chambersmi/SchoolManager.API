using Azure;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SchoolManager.API.Models.DomainModels;
using SchoolManager.API.Models.DTOs;
using SchoolManager.API.Models.Helpers;
using SchoolManager.API.Services;
using SchoolManager.API.Services.Repositories;
using System.Net;

namespace SchoolManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : Controller
    {
        private readonly IStudentService _studentService;
        private readonly IAddressService _addressService;

        private readonly ILogger<StudentController> _logger;

        public StudentController(IStudentService studentService, IAddressService addressService, ILogger<StudentController> logger)
        {
            _studentService = studentService;
            _addressService = addressService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStudentsAsync()
        {
            var students = await _studentService.GetAllStudentsAsync();
            return Ok(students);
        }

        // change create studentdto?
        [HttpPost]
        public async Task<IActionResult> AddStudentAsync([FromBody] StudentDTO request)
        {
            if (request == null)
            {
                return BadRequest("Request cannot be null.");
            }

            try
            {
                var studentId = await _studentService.AddStudentAsync(request);
                return Ok(new
                {
                    //Return success response
                    Message = "Student Created Successfully.",
                    StudentId = studentId
                });
            } catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Message = "An error has occured while creating the student.",
                    Error = ex.Message
                });
            }

        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetStudentByIdAsync(int id)
        {
            var student = await _studentService.GetStudentByIdAsync(id);
            if (student == null)
            {
                return NotFound(new
                {
                    Message = "Student Not Found."
                });
            }

            return Ok(student);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteStudentByIdAsync(int studentId)
        {            
            try
            {
                var isDeleted = await _studentService.DeleteStudentByIdAsync(studentId);

                if(!isDeleted)
                {
                    return NotFound(new
                    {
                        Message = "Student or associated address not found.",
                        StudentID = studentId
                    });
                }
                return Ok(new
                {
                    Message = "Student Deleted Successfully.",
                    StudentID = studentId
                });
            } catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Message = "An error has occured while deleting the student.",
                    ErrorDetails = ex.Message,
                    StackTrace = ex.StackTrace
                });
            }
        }

        [HttpPost]
        [Route("AddStudentWithAddress")]
        public async Task<IActionResult> CreateStudentWithAddressAsync([FromBody] AddStudentWithAddressDTO request)
        {
            if (request == null)
            {
                return BadRequest("Invalid request.");
            }

            await _studentService.AddStudentWithAddressAsync(
                new StudentDTO
                {
                    FirstName = request.FirstName,
                    MiddleName = request.MiddleName,
                    LastName = request.LastName,
                    Birthdate = request.Birthdate,
                    SSN = request.SSN
                },
                new AddressDTO
                {
                    Street1 = request.Street1,
                    Street2 = request.Street2,
                    City = request.City,
                    State = request.State,
                    ZipCode = request.ZipCode
                });

            return Ok(new
            {
                Message = "Student and Address successfully added."
            });
        }

        [HttpGet("StudentsWithAddresses")]
        public async Task<IActionResult> GetAllStudentsWithAddresses()
        {
            var students = await _studentService.GetAllStudentsWithAddressesAsync();

            if (students == null || !students.Any())
            {
                return NotFound(new
                {
                    Message = "No students found."
                });
            }

            return Ok(students);
        }
    }
}
