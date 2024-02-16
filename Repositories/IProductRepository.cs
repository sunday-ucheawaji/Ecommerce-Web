using EcommerceWeb.Models.Domain;

namespace EcommerceWeb.Repositories
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllAsync(
            string? filterOn = null, 
            string? filterQuery = null, 
            string? sortBy = null, 
            bool isAscending = true,
            int pageNumber = 1,
            int pageSize = 1000
            );

        Task<Product?> GetByIdAsync(Guid id);

        Task<Product> CreateAsync(Product product);

        Task<Product?> UpdateAsync(Guid productId, Product product, ICollection<Guid> productImageIDs);

        Task<Product?> DeleteAsync(Guid id);
    }
}
