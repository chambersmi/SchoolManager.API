using SchoolManager.API.Data;
using SchoolManager.API.Models.DomainModels;
using SchoolManager.API.Models.Helpers;

namespace SchoolManager.API.Services.Repositories
{
    public interface IAddressRepository
    {
        Task<Address> AddAddressAsync(Address address);
        Task<IEnumerable<Address>> GetAllAddressesAsync();
        Task<Address?> GetAddressByIdAsync(int id);
        Task<bool> DeleteAddressByIdAsync(int id);
        Task<Address?> GetByFieldsAsync(CreateAddressRequestDTO request);

    }
}
