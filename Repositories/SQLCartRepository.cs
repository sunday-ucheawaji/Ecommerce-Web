using EcommerceWeb.Data;
using EcommerceWeb.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace EcommerceWeb.Repositories
{
    public class SQLCartRepository : ICartRepository
    {

        private readonly EcommerceWebDbContext _dbContext;

        public SQLCartRepository(EcommerceWebDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Cart>> GetAllAsync()
        {
            return await _dbContext.Carts.ToListAsync();
        }
        public async Task<Cart?> GetByIdAsync(Guid id)
        {

            return await _dbContext.Carts.FirstOrDefaultAsync(x => x.CartId == id);
        }
        public async Task<Cart> CreateAsync(Cart cart)
        {
            await _dbContext.Carts.AddAsync(cart);
            await _dbContext.SaveChangesAsync();
            return cart;
        }

        public async Task<Cart?> UpdateAsync(Guid id, Cart cart)
        {
            var existingCart = await _dbContext.Carts.FirstOrDefaultAsync(x=> x.CartId == id);
            if (existingCart == null)
            {
                return null;
            }

            return existingCart;

        }

        public async Task<Cart?> DeleteAsync(Guid id)
        {
            var cart = await _dbContext.Carts.FirstOrDefaultAsync(x=> x.CartId == id);

            if ( cart == null)
            {
                return null;
            }
            _dbContext.Carts.Remove(cart);
            await _dbContext.SaveChangesAsync();
            return cart ;
        }

        
    }
}
