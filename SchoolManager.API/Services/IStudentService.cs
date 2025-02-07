﻿using SchoolManager.API.Models.DTOs;
using SchoolManager.API.Models.Helpers;

namespace SchoolManager.API.Services
{
    public interface IStudentService
    {
        Task<int> AddStudentAsync(CreateStudentRequestDTO dto);
        Task<IEnumerable<StudentDTO>> GetAllStudentsAsync();
        Task<StudentDTO> GetStudentByIdAsync(int id);
        Task<bool> DeleteStudentByIdAsync(int studentId);
        Task<IEnumerable<StudentDTO>> GetAllStudentsWithAddressesAsync();
        Task AddStudentWithAddressAsync(CreateStudentRequestDTO studentDTO, CreateAddressRequestDTO addressDTO);
    }
}
