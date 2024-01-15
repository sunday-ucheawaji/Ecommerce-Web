using EcommerceWeb.Data;
using EcommerceWeb.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace EcommerceWeb.Repositories
{
    public class SQLOrderDetailRepository : IOrderDetailRepository
    {
        private readonly EcommerceWebDbContext _dbContext;

        public SQLOrderDetailRepository(EcommerceWebDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<OrderDetail>> getAllAsync()
        {
            return await _dbContext.OrderDetails.ToListAsync();
        }

        public async Task<OrderDetail?> GetByIdAsync(Guid id)
        {
            return await _dbContext.OrderDetails.FirstOrDefaultAsync(x => x.OrderDetailId == id);
        }
        public async Task<OrderDetail> CreateAsync(OrderDetail orderDetail)
        {
            await _dbContext.OrderDetails.AddAsync(orderDetail);
            await _dbContext.SaveChangesAsync();
            return orderDetail;
        }

        public async Task<OrderDetail?> UpdateAsync(Guid id, OrderDetail orderDetail)
        {
            var existingDomain = await _dbContext.OrderDetails.FirstOrDefaultAsync(x=> x.OrderDetailId == id);
            if (existingDomain == null)
            {
                return null;
            }
            existingDomain.Quantity = orderDetail.Quantity;
            await _dbContext.SaveChangesAsync();
            return existingDomain;
        }

        public async Task<OrderDetail?> DeleteAsync(Guid id)
        {
            var existingDomain = await _dbContext.OrderDetails.FirstOrDefaultAsync(x=> x.OrderDetailId == id);
            if (existingDomain == null)
            {
                return null;
            }
            _dbContext.OrderDetails.Remove(existingDomain);
            await _dbContext.SaveChangesAsync();
            return existingDomain;
        }
    }
}
