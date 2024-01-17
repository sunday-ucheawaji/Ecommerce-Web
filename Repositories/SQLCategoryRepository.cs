using EcommerceWeb.Data;
using EcommerceWeb.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace EcommerceWeb.Repositories
{
    public class SQLCategoryRepository : ICategoryRepository
    {

        private readonly EcommerceWebDbContext _dbContext;

        public SQLCategoryRepository(EcommerceWebDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Category>> GetAllAsync()
        {
            return await _dbContext.Categories.ToListAsync();
        }

        public async Task<Category?> GetByIdAsync(Guid id)
        {
            return await _dbContext.Categories.FirstOrDefaultAsync(x=> x.CategoryId == id);
        }
        public async Task<Category> CreateAsync(Category category)
        {
            await _dbContext.Categories.AddAsync(category);
            await _dbContext.SaveChangesAsync();
            return category;
        }
        public async Task<Category?> UpdateAsync(Guid id, Category category)
        {
            var existingDomain = await _dbContext.Categories.FirstOrDefaultAsync(x=> x.CategoryId ==id); 
            
            if (existingDomain == null) 
            { 
                return null;
            }

            if (category.CategoryName != null)
            {
                existingDomain.CategoryName = category.CategoryName;
            }

            if (category.Description != null)
            {
                existingDomain.Description = category.Description;
            }
            await _dbContext.SaveChangesAsync();

            return existingDomain;
        }

        public async Task<Category?> DeleteAsync(Guid id)
        {
            var category = await _dbContext.Categories.FirstOrDefaultAsync(x=> x.CategoryId == id);
            if (category == null)
            {
                return null;
            }

            _dbContext.Categories.Remove(category);
            await _dbContext.SaveChangesAsync();
            return category;
        }
        
    }
}
