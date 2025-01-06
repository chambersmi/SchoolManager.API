using Azure;
using Microsoft.AspNetCore.Hosting.Server.Features;
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
           if(request == null)
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
            }
            catch (Exception ex)
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
            if(student == null)
            {
                return NotFound(new
                {
                    Message = "Student Not Found."
                });
            }

            return Ok(student);
        }

        [HttpDelete]
        public async Task<IActionResult> DeletestudentByIdAsync(int id)
        {
            var student = await _studentService.DeleteStudentByIdAsync(id);
            return Ok(student);
        }

        [HttpPost]
        [Route("CreateStudentWithAddress")]
        public async Task<IActionResult> CreateStudentWithAddressAsync([FromBody] CreateStudentRequestDTO request)
        {
            var address = new AddressDTO
            {
                Street1 = request.Address.Street1,
                Street2 = request.Address.Street2,
                City = request.Address.City,
                State = request.Address.State,
                ZipCode = request.Address.ZipCode
            };

            var addressId = await _addressService.AddAddressAsync(address);

            var student = new StudentDTO
            {
                FirstName = request.FirstName,
                MiddleName = request.MiddleName,
                LastName = request.LastName,
                Birthdate = request.Birthdate,
                SSN = request.SSN
            };

            var studentId = _studentService.AddStudentAsync(student);

            return Ok(new
            {
                StudentID = studentId,
                AddressID = addressId
            });
        }
    }
}
