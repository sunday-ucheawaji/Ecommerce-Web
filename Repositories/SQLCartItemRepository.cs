using EcommerceWeb.Data;
using EcommerceWeb.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace EcommerceWeb.Repositories
{
    public class SQLCartItemRepository:ICartItemRepository
    {
        private readonly EcommerceWebDbContext _dbContext;

        public SQLCartItemRepository(EcommerceWebDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<CartItem>> GetAllAsync()
        {
            return await _dbContext.CartItems.ToListAsync();
        }

        public async Task<CartItem?> GetByIdAsync(Guid id)
        {
            return await _dbContext.CartItems.FirstOrDefaultAsync(x => x.CartItemId == id);
        }

        public async Task<CartItem> CreateAsync(CartItem cartItem)
        {
            await _dbContext.AddAsync(cartItem);
            await _dbContext.SaveChangesAsync();
            return cartItem;
        }

        public async Task<CartItem?> UpdateAsync(Guid id, int quantity)
        {
            var existingCartItem = await _dbContext.CartItems.FirstOrDefaultAsync(x => x.CartItemId == id);
            if ( existingCartItem == null)
            {
                return null;
            }

            if (quantity != 0)
            {
                existingCartItem.Quantity = quantity;
            }
            await _dbContext.SaveChangesAsync();
            return existingCartItem;

        }



        public async Task<CartItem?> DeleteAsync(Guid id)
        {
            var cartItem = await _dbContext.CartItems.FirstOrDefaultAsync(x => x.CartItemId == id);
            if (cartItem == null)
            {
                return null;
            }

            _dbContext.CartItems.Remove(cartItem);
            await _dbContext.SaveChangesAsync();
            return cartItem;
        }


    }
}
