using EcommerceWeb.Models.Domain;

namespace EcommerceWeb.Repositories
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllAsync();

        Task<Product?> GetByIdAsync(Guid id);

        Task<Product> CreateAsync(Product product);

        Task<Product?> UpdateAsync(Guid productId, Product product, ICollection<Guid> productImageIDs);

        Task<Product?> DeleteAsync(Guid id);
    }
}
