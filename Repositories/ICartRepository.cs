using EcommerceWeb.Models.Domain;

namespace EcommerceWeb.Repositories
{
    public interface ICartRepository
    {
        Task<List<Cart>> GetAllAsync();
        Task<Cart?> GetByIdAsync(Guid id);
        Task<Cart> CreateAsync(Cart cart);
        Task<Cart?> UpdateAsync(Guid id, Cart cart);
        Task<Cart?> DeleteAsync(Guid id);
    }
}
