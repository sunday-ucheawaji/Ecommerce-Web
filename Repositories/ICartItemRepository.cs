using EcommerceWeb.Models.Domain;

namespace EcommerceWeb.Repositories
{
    public interface ICartItemRepository
    {

        Task<List<CartItem>> GetAllAsync();
        Task<CartItem?> GetByIdAsync(Guid id);
        Task<CartItem> CreateAsync(CartItem cartItem);
        Task<CartItem?> UpdateAsync(Guid id, int quantity);
        Task<CartItem?> DeleteAsync(Guid id);

    }
}
