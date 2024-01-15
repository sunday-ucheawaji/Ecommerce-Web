using EcommerceWeb.Models.Domain;

namespace EcommerceWeb.Repositories
{
    public interface IOrderDetailRepository
    {

        Task<List<OrderDetail>> getAllAsync();

        Task<OrderDetail?> GetByIdAsync(Guid id);

        Task<OrderDetail> CreateAsync(OrderDetail orderDetail);

        Task<OrderDetail?> UpdateAsync(Guid id, OrderDetail orderDetail);

        Task<OrderDetail?> DeleteAsync(Guid id);
    }
}
