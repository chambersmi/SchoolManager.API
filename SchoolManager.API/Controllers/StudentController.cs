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

        [HttpGet]
        public async Task<IActionResult> GetStudentsAsync()
        {
            var students = await _studentRepository.GetAllAsync();

            var response = new List<StudentDTO>();

            foreach(var st in students)
            {
                response.Add(new StudentDTO
                {
                    StudentID = st.StudentID,
                    FirstName = st.FirstName,                    
                    Addresses = st.Addresses.Select(x => new AddressDTO
                    {
                        AddressID = x.AddressID,
                        Street1 = x.Street1
                    }).ToList()
                });
            }

            return Ok(response);
        }

    }
}