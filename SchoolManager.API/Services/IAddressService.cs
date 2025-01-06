using SchoolManager.API.Models.DomainModels;
using SchoolManager.API.Models.DTOs;
using SchoolManager.API.Models.Helpers;

namespace SchoolManager.API.Services
{
    public interface IAddressService
    {
        Task<int> AddAddressAsync(CreateAddressRequestDTO dto);
        Task<IEnumerable<AddressDTO>> GetAllAddressesAsync();
        Task<AddressDTO> GetAddressByIdAsync(int id);
        Task<bool> DeleteAddressByIdAsync(int id);
        Task<Address> GetOrCreateAddressAsync(CreateAddressRequestDTO request);
    }
}
