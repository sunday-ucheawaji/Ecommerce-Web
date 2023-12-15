using EcommerceWeb.Data;
using EcommerceWeb.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace EcommerceWeb.Repositories
{
    public class SQLPromotionRepository : IPromotionRepository
    {
        private readonly EcommerceWebDbContext dbContext;

        public SQLPromotionRepository(EcommerceWebDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<Promotion>> GetAllAsync()
        {
            return await dbContext.Promotions.ToListAsync();
        }

        public async Task<Promotion?> GetByIdAsync(Guid id)
        {
            return await dbContext.Promotions.FirstOrDefaultAsync(x => x.PromotionId == id);
        }
        public async Task<Promotion> CreateAsync(Promotion promotion)
        {
            await dbContext.Promotions.AddAsync(promotion);
            await dbContext.SaveChangesAsync();
            return promotion;
        }

        public async Task<Promotion?> UpdateAsync(Guid id, Promotion promotion)
        {
            var existingPromotion = await dbContext.Promotions.FirstOrDefaultAsync(x=> x.PromotionId == id);

            if (existingPromotion == null)
            {
                return null;
            }
            existingPromotion.StartDate = promotion.StartDate;
            existingPromotion.EndDate = promotion.EndDate;
            existingPromotion.DiscountPercentage = promotion.DiscountPercentage;
            existingPromotion.Title = promotion.Title;
            existingPromotion.Description = promotion.Description;

            await dbContext.SaveChangesAsync();

            return existingPromotion;
        }
        public async Task<Promotion?> DeleteAsync(Guid id)
        {
            var existingPromotion = await dbContext.Promotions.FirstOrDefaultAsync(x => x.PromotionId==id);
            if (existingPromotion == null)
            {
                return null;
            }
            dbContext.Promotions.Remove(existingPromotion);
            await dbContext.SaveChangesAsync();
            return existingPromotion;
        }
    }
}
