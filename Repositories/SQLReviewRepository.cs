using EcommerceWeb.Data;
using EcommerceWeb.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace EcommerceWeb.Repositories
{
    public class SQLReviewRepository: IReviewRepository
    {
        private readonly EcommerceWebDbContext _dbContext;

        public SQLReviewRepository(EcommerceWebDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Review>> GetAllAsync()
        {
            return await _dbContext.Reviews.ToListAsync();
        }

        public async Task<Review?> GetByIdAsync(Guid id)
        {
            return await _dbContext.Reviews.FirstOrDefaultAsync(x => x.ReviewId == id);
        }
        public async Task<Review> CreateAsync(Review review)
        {
            await _dbContext.Reviews.AddAsync(review);
            await _dbContext.SaveChangesAsync();
            return review;
        }
        public async Task<Review?> UpdateAsync(Guid id, Review review)
        {
            var existingReview = await _dbContext.Reviews.FirstOrDefaultAsync(x => x.ReviewId == id);
            
            if (existingReview == null)
            {
                return null;
            }
            if (review.Name != null)
            {
                existingReview.Name = review.Name;
            }

            if (existingReview.Description != null)
            {
                existingReview.Description = review.Description;
            }

            if (existingReview.Rating != null)
            {
                existingReview.Rating = review.Rating;
            }

            await _dbContext.SaveChangesAsync();
           
            return existingReview;
        }

        public async Task<Review?> DeleteAsync(Guid id)
        {
            var existingReview = await _dbContext.Reviews.FirstOrDefaultAsync(x => x.ReviewId == id);
            if (existingReview == null)
            {
                return null;
            }

            _dbContext.Reviews.Remove(existingReview);
            await _dbContext.SaveChangesAsync();
            return existingReview;
        }

    }
}
