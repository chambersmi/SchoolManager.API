﻿using Azure;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Mvc;
using SchoolManager.API.Models.DomainModels;
using SchoolManager.API.Models.DTOs;
using SchoolManager.API.Models.Helpers;
using SchoolManager.API.Services.Repositories;

namespace SchoolManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : Controller
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IAddressRepository _addressRepository;
        private readonly ILogger<StudentController> _logger;

        public StudentController(IStudentRepository studentRepository, IAddressRepository addressRepository, ILogger<StudentController> logger)
        {
            _studentRepository = studentRepository;
            _addressRepository = addressRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetStudentsAsync()
        {
            var students = await _studentRepository.GetAllAsync();

            var response = new List<StudentDTO>();

            foreach (var student in students)
            {
                response.Add(new StudentDTO
                {
                    StudentID = student.StudentID,
                    FirstName = student.FirstName,
                    MiddleName = student.MiddleName,
                    LastName = student.LastName,
                    Birthdate = student.Birthdate,
                    SSN = student.SSN,
                    Addresses = student.Addresses.Select(x => new AddressDTO
                    {
                        AddressID = x.AddressID,
                        Street1 = x.Street1,
                        Street2 = x.Street2,
                        City = x.City,
                        State = x.State,
                        ZipCode = x.ZipCode
                    }).ToList()
                });
            }

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateStudentAsync([FromBody] CreateStudentRequestDTO request)
        {
            if (request != null)
            {
                var student = new Student
                {
                    FirstName = request.FirstName,
                    MiddleName = request.MiddleName,
                    LastName = request.LastName,
                    Birthdate = request.Birthdate,
                    SSN = request.SSN,
                    Addresses = new List<Address>()
                };

                if (request.Addresses != null)
                {
                    foreach (var addressDto in request.Addresses)
                    {
                        var address = new Address
                        {
                            Street1 = addressDto.Street1,
                            Street2 = addressDto.Street2,
                            City = addressDto.City,
                            State = addressDto.State,
                            ZipCode = addressDto.ZipCode
                        };

                        student.Addresses.Add(address);
                    }
                }

                await _studentRepository.CreateAsync(student);

                // Convert Domain Model to DTO
                var response = new StudentDTO
                {
                    StudentID = student.StudentID,
                    FirstName = student.FirstName,
                    MiddleName = student.MiddleName,
                    LastName = student.LastName,
                    Birthdate = student.Birthdate,
                    SSN = student.SSN,
                    Addresses = student.Addresses.Select(x => new AddressDTO
                    {
                        AddressID = x.AddressID,
                        Street1 = x.Street1,
                        Street2 = x.Street2,
                        City = x.City,
                        State = x.State,
                        ZipCode = x.ZipCode
                    }).ToList()
                };
                return Ok(response);

            } else
            {
                _logger.LogWarning("No address IDs provided in the request.");
                return NotFound();
            }
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> EditStudent([FromRoute] int id, UpdateStudentRequestDTO request)
        {            
            var student = new Student
            {
                StudentID = id,
                FirstName = request.FirstName,
                MiddleName = request.MiddleName,
                LastName = request.LastName,
                Birthdate = request.Birthdate,
                SSN = request.SSN
            };

            if (request.Addresses != null)
            {
                if(student.Addresses == null)
                {
                    student.Addresses = new List<Address>();
                }
                foreach(var addressDTO in request.Addresses)
                {
                    var address = new Address
                    {
                        Street1 = addressDTO.Street1,
                        Street2 = addressDTO.Street2,
                        City = addressDTO.City,
                        State = addressDTO.State,
                        ZipCode = addressDTO.ZipCode
                    };
                    student.Addresses.Add(address);
                }
            }

            if(student == null)
            {
                return NotFound();
            }

            await _studentRepository.UpdateAsync(student);

            var response = new StudentDTO
            {
                StudentID = student.StudentID,
                FirstName = student.FirstName,
                MiddleName = student.MiddleName,
                LastName = student.LastName,
                SSN = student.SSN,
                Birthdate = student.Birthdate,
                Addresses = student.Addresses?.Select(address => new AddressDTO
                {
                    Street1 = address.Street1,
                    Street2 = address.Street2,
                    City = address.City,
                    State = address.State,
                    ZipCode = address.ZipCode
                }).ToList() ?? new List<AddressDTO>()
            
            };

            return Ok(response);
        }
    }
}
