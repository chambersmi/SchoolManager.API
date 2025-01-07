using Microsoft.AspNetCore.Mvc;
using SchoolManager.API.Models.DomainModels;
using SchoolManager.API.Models.DTOs;
using SchoolManager.API.Models.Helpers;
using SchoolManager.API.Repos.Repositories.Addresses;

namespace SchoolManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : Controller
    {
        private readonly IAddressRepository _addressRepository;
        private readonly ILogger<AddressController> _logger;

        public AddressController(IAddressRepository addressRepository, ILogger<AddressController> logger)
        {
            _addressRepository = addressRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAddressAsync()
        {
            var addresses = await _addressRepository.GetAllAddressesAsync();

            var response = new List<AddressDTO>();

            foreach (var address in addresses)
            {
                response.Add(new AddressDTO
                {
                    AddressID = address.AddressID,
                    Street1 = address.Street1,
                    Street2 = address.Street2,
                    City = address.City,
                    State = address.State,
                    ZipCode = address.ZipCode
                });
            }

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAddressAsync([FromBody] CreateAddressRequestDTO request)
        {
            if (request != null)
            {
                var address = new Address
                {
                    Street1 = request.Street1,
                    Street2 = request.Street2,
                    City = request.City,
                    State = request.State,
                    ZipCode = request?.ZipCode                    
                };

                await _addressRepository.AddAddressAsync(address);

                var response = new AddressDTO
                {
                    AddressID = address.AddressID,
                    Street1 = address.Street1,
                    Street2 = address.Street2,
                    City = address.City,
                    State = address.State,
                    ZipCode = address.ZipCode
                };

                return Ok(response);
            } else
            {
                _logger.LogWarning("An error has occured in AddressController.");
                return NotFound();
            }
        }

    }
}
