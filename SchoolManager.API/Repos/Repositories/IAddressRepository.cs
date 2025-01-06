using SchoolManager.API.Data;
using SchoolManager.API.Models.DomainModels;

namespace SchoolManager.API.Services.Repositories
{
    public interface IAddressRepository
    {
        Task<Address> AddAddressAsync(Address address);
        Task<IEnumerable<Address>> GetAllAddressesAsync();
        Task<Address?> GetAddressByIdAsync(int id);
        Task<bool> DeleteAddressByIdAsync(int id);

    }
}
