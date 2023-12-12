using EcommerceWeb.Models.Domain;

namespace EcommerceWeb.Repositories
{
    public interface ISupplierRepository
    {
        Task<List<Supplier>> GetAllAsync();

        Task<Supplier?> GetByIdAsync(Guid id);

        Task<Supplier> CreateAsync(Supplier supplier);

        Task<Supplier?> UpdateAsync(Guid id, Supplier supplier);

        Task<Supplier?> DeleteAsync(Guid id);
    }
}
