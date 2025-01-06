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

        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<int> AddStudentAsync(StudentDTO dto)
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
                return null;
            }
        }

        public async Task<bool> DeleteStudentByIdAsync(int id)
        {
            return await _studentRepository.DeleteStudentByIdAsync(id);
        }

    }
}
