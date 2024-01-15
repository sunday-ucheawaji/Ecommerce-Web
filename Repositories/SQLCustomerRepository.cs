using EcommerceWeb.Data;
using EcommerceWeb.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace EcommerceWeb.Repositories
{
    public class SQLCustomerRepository : ICustomerRepository
    {
        private readonly EcommerceWebDbContext dbContext;

        public SQLCustomerRepository(EcommerceWebDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<Customer>> GetAllAsync()
        {
            return await dbContext.Customers.ToListAsync();

        }
        public async Task<Customer?> GetByIdAsync(Guid id)
        {
            return await dbContext.Customers.FirstOrDefaultAsync(x=> x.CustomerId == id);
        }
        public async Task<Customer> CreateAsync(Customer customer)
        {
            await dbContext.Customers.AddAsync(customer);
            await dbContext.SaveChangesAsync();
            return customer;
        }

        public async Task<Customer?> UpdateAsync(Guid id, Customer customer)
        {
            var existingCustomer = await dbContext.Customers.FirstOrDefaultAsync(x=> x.CustomerId == id);
            if (existingCustomer == null)
            {
                return null;
            }
            existingCustomer.FirstName = customer.FirstName;
            existingCustomer.LastName = customer.LastName;  
            existingCustomer.DateOfBirth = customer.DateOfBirth;

            await dbContext.SaveChangesAsync();
            return existingCustomer;
        }

        public async Task<Customer?> DeleteAsync(Guid id)
        {
            var existingCustomer = dbContext.Customers.FirstOrDefault(x=> x.CustomerId == id);
            if (existingCustomer == null)
            {
                return null;
            }
            dbContext.Customers.Remove(existingCustomer);
            await dbContext.SaveChangesAsync();
            return existingCustomer;
        }



    }
}
