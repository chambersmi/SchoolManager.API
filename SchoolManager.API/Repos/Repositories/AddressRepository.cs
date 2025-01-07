using Microsoft.EntityFrameworkCore;
using SchoolManager.API.Data;
using SchoolManager.API.Models.DomainModels;
using SchoolManager.API.Models.DTOs;
using SchoolManager.API.Models.Helpers;
using System.Reflection.Metadata.Ecma335;

namespace SchoolManager.API.Services.Repositories
{
    public class AddressRepository : IAddressRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<AddressRepository> _logger;

        public AddressRepository(AppDbContext context, ILogger<AddressRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Address> AddAddressAsync(Address address)
        {
            if(address == null)
            {
                throw new ArgumentNullException(nameof(address), "Address cannot be null.");
            }

            await _context.Addresses.AddAsync(address);
            await _context.SaveChangesAsync();

            return address;
        }

        public async Task<IEnumerable<Address>> GetAllAddressesAsync()
        {
            return await _context.Addresses.ToListAsync();
        }

        public async Task<Address?> GetAddressByIdAsync(int id)
        {
            return await _context.Addresses.FirstOrDefaultAsync(x => x.AddressID == id);
        }

        public async Task<bool> DeleteAddressByIdAsync(int id)
        {
            var address = await _context.Addresses.FirstOrDefaultAsync(a => a.AddressID == id);

            if(address == null)
            {
                return false;
            }

            _context.Remove(address);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<Address?> GetByFieldsAsync(CreateAddressRequestDTO request)
        {
            return await _context.Addresses.FirstOrDefaultAsync(a =>
                a.Street1 == request.Street1 &&
                a.Street2 == request.Street2 &&
                a.City == request.City &&
                a.State == request.Street2 &&
                a.ZipCode == request.ZipCode);
        }

    }
}
