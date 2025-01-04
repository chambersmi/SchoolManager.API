using Azure;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Mvc;
using SchoolManager.API.Models.DomainModels;
using SchoolManager.API.Models.DTOs;
using SchoolManager.API.Models.Helpers;
using SchoolManager.API.Services;
using SchoolManager.API.Services.Repositories;

namespace SchoolManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : Controller
    {
        private readonly IStudentService _studentService;
        private readonly IAddressRepository _addressRepository;
        private readonly ILogger<StudentController> _logger;

        public StudentController(IStudentService studentService, IAddressRepository addressRepository, ILogger<StudentController> logger)
        {
            _studentService = studentService;
            _addressRepository = addressRepository;
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
        public async Task<IActionResult> AddStudentAsync([FromBody] CreateStudentRequestDTO request)
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
    }
}
