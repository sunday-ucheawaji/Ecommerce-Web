using EcommerceWeb.Data;
using EcommerceWeb.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace EcommerceWeb.Repositories
{
    public class SQLAddressRepository : IAddressRepository
    {
        private readonly EcommerceWebDbContext _dbContext;

        public SQLAddressRepository(EcommerceWebDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Address>> GetAllAsync()
        {
            return await _dbContext.Addresses.ToListAsync();
        }

        public async Task<Address?> GetByIdAsync(Guid id)
        {
            return await _dbContext.Addresses.FirstOrDefaultAsync(x => x.AddressId == id);
        }
        public async Task<Address> CreateAsync(Address address)
        {
            await _dbContext.Addresses.AddAsync(address);
            await _dbContext.SaveChangesAsync();
            return address;
        }
        public async Task<Address?> UpdateAsync(Guid id, Address address)
        {
            var existingAddress = await _dbContext.Addresses.FirstOrDefaultAsync(x => x.AddressId == id);
            if (existingAddress == null)
            {
                return null;
            }
            if (address.Street != null)
            {
                existingAddress.Street = address.Street;
            }
            if (address.City != null)
            {
                existingAddress.City = address.City;
            }
            return existingAddress;
        }

        public async Task<Address?> DeleteAsync(Guid id)
        {
            var existingAddress = await _dbContext.Addresses.FirstOrDefaultAsync(x => x.AddressId == id);
            if (existingAddress == null)
            {
                return null;
            }

            _dbContext.Addresses.Remove(existingAddress);
            await _dbContext.SaveChangesAsync();
            return existingAddress;
        }

    

    }
}
