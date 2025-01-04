using Microsoft.EntityFrameworkCore;
using SchoolManager.API.Data;
using SchoolManager.API.Models.DomainModels;

namespace SchoolManager.API.Services.Repositories
{
    public class AddressRepository : IAddressRepository
    {
        private readonly AppDbContext _context;

        public AddressRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Address> CreateAsync(Address address)
        {
            if(address == null)
            {
                throw new ArgumentNullException(nameof(address), "Address cannot be null.");
            }

            await _context.Addresses.AddAsync(address);
            await _context.SaveChangesAsync();

            return address;
        }

        public async Task<IEnumerable<Address>> GetAllAsync()
        {
            return await _context.Addresses.ToListAsync();
        }

        public async Task<Address?> GetByIdAsync(int id)
        {
            return await _context.Addresses.FirstOrDefaultAsync(x => x.AddressID == id);
        }

    }
}
