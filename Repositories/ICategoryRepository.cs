using EcommerceWeb.Models.Domain;

namespace EcommerceWeb.Repositories
{
    public interface ICategoryRepository
    {

        Task<List<Category>> GetAllAsync();
        Task<Category?> GetByIdAsync(Guid id);
        Task<Category> CreateAsync(Category category);
        Task<Category?> UpdateAsync(Guid id, Category category);
        Task<Category?> DeleteAsync(Guid id);
    }
}
