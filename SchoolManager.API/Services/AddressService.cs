using SchoolManager.API.Models.DomainModels;
using SchoolManager.API.Models.DTOs;
using SchoolManager.API.Services.Repositories;

namespace SchoolManager.API.Services
{
    public class AddressService : IAddressService
    {
        private readonly IAddressRepository _addressRepository;
        public AddressService(IAddressRepository addressRepository)
        {
            _addressRepository = addressRepository;
        }

        public async Task<int> AddAddressAsync(AddressDTO dto)
        {
            var address = new Address
            {
                Street1 = dto.Street1,
                Street2 = dto.Street2,
                City = dto.City,
                State = dto.State,
                ZipCode = dto.ZipCode
            };

            await _addressRepository.AddAddressAsync(address);
            return address.AddressID;
        }

        public async Task<bool> DeleteAddressByIdAsync(int id)
        {
            return await _addressRepository.DeleteAddressByIdAsync(id);
        }

        public async Task<AddressDTO> GetAddressByIdAsync(int id)
        {
            var address = await _addressRepository.GetAddressByIdAsync(id);

            if (address != null)
            {
                return new AddressDTO
                {
                    Street1 = address.Street1,
                    Street2 = address.Street2,
                    City = address.City,
                    State = address.State,
                    ZipCode = address.ZipCode
                };
            } else
            {
                return null;
            }
        }

        public async Task<IEnumerable<AddressDTO>> GetAllAddressesAsync()
        {
            var address = await _addressRepository.GetAllAddressesAsync();

            return address.Select(address => new AddressDTO
            {
                Street1 = address.Street1,
                Street2 = address.Street2,
                City = address.City,
                State = address.State,
                ZipCode = address.ZipCode
            }).ToList();
        }
    }
}

