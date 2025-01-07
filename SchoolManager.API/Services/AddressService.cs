using SchoolManager.API.Models.DomainModels;
using SchoolManager.API.Models.DTOs;
using SchoolManager.API.Models.Helpers;
using SchoolManager.API.Services.Repositories;

namespace SchoolManager.API.Services
{
    public class AddressService : IAddressService
    {
        private readonly IAddressRepository _addressRepository;
        private readonly ILogger<AddressService> _logger;

        public AddressService(IAddressRepository addressRepository, ILogger<AddressService> logger)
        {
            _addressRepository = addressRepository;
            _logger = logger;
        }

        public async Task<int> AddAddressAsync(CreateAddressRequestDTO dto)
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

        public async Task<Address> GetOrCreateAddressAsync(CreateAddressRequestDTO request)
        {
            var existingAddress = await _addressRepository.GetByFieldsAsync(request);

            if(existingAddress != null)
            {
                return existingAddress;
            }

            var createAddress = new Address
            {
                Street1 = request.Street1,
                Street2 = request.Street2,
                City = request.City,
                State = request.State,
                ZipCode = request.ZipCode
            };
            
            return await _addressRepository.AddAddressAsync(createAddress);
        }
    }
}

