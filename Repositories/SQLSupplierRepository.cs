using EcommerceWeb.Data;
using EcommerceWeb.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace EcommerceWeb.Repositories
{
    public class SQLSupplierRepository : ISupplierRepository
    {
        private readonly EcommerceWebDbContext dbContext;

        public SQLSupplierRepository(EcommerceWebDbContext dbContext) {
            this.dbContext = dbContext;
        }

        public async Task<List<Supplier>> GetAllAsync()
        {
            return await dbContext.Suppliers.ToListAsync();
        }

        public async Task<Supplier?> GetByIdAsync(Guid id)
        {
            return await dbContext.Suppliers
                .FirstOrDefaultAsync(x => x.SupplierID == id);
        }
        public async Task<Supplier> CreateAsync(Supplier supplier)
        {
            await dbContext.Suppliers.AddAsync(supplier);
            await dbContext.SaveChangesAsync();
            return supplier;
        }

        public async Task<Supplier?> UpdateAsync(Guid id, Supplier supplier)
        {
            var existingSupplier = await dbContext.Suppliers
                .FirstOrDefaultAsync(x => x.SupplierID == id);

            if (existingSupplier == null) 
            {
                return null;
            };

            existingSupplier.CompanyName = supplier.CompanyName;
            existingSupplier.ContactName = supplier.ContactName;
            existingSupplier.CustomUserId = supplier.CustomUserId;

            await dbContext.SaveChangesAsync();
            return existingSupplier;
        }
        public async Task<Supplier?> DeleteAsync(Guid id)
        {
            var existingSupplier = await dbContext.Suppliers
             .FirstOrDefaultAsync(x => x.SupplierID == id);

            if (existingSupplier == null)
            {
                return null;
            };
            
            dbContext.Suppliers.Remove(existingSupplier);
            await dbContext.SaveChangesAsync();
            return existingSupplier;
        }

    }
}
