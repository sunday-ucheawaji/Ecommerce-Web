using EcommerceWeb.Data;
using EcommerceWeb.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace EcommerceWeb.Repositories
{
    public class SQLOrderRepository : IOrderRepository
    {

        private readonly EcommerceWebDbContext _dbContext;

        public SQLOrderRepository(EcommerceWebDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Order>> getAllAsync()
        {
            return await _dbContext.Orders.ToListAsync();
        }

        public async Task<Order?> GetByIdAsync(Guid id)
        {
            return await _dbContext.Orders.FirstOrDefaultAsync(x => x.OrderId == id);
        }
        public async Task<Order> CreateAsync(Order order)
        {
            await _dbContext.Orders.AddAsync(order);
            await _dbContext.SaveChangesAsync();
            return order;
        }
        public Task<Order?> UpdateAsync(Guid id, Order order)
        {
            throw new NotImplementedException();
        }

        public Task<Order?> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }


    }
}
