using SchoolManager.API.Models.DTOs;

namespace SchoolManager.API.Services
{
    public interface IAddressService
    {
        Task<int> AddAddressAsync(AddressDTO dto);
        Task<IEnumerable<AddressDTO>> GetAllAddressesAsync();
        Task<AddressDTO> GetAddressByIdAsync(int id);
        Task<bool> DeleteAddressByIdAsync(int id);
    }
}
