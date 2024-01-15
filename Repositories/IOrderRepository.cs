using EcommerceWeb.Models.Domain;

namespace EcommerceWeb.Repositories
{
    public interface IOrderRepository
    {

        Task<List<Order>> getAllAsync();

        Task<Order?> GetByIdAsync(Guid id);

        Task<Order> CreateAsync(Order order);

        Task<Order?> UpdateAsync(Guid id, Order order);  
        
        Task <Order?> DeleteAsync(Guid id);

    }
}
