using EcommerceWeb.Models.Domain;

namespace EcommerceWeb.Repositories
{
    public interface IReviewRepository
    {
        Task<List<Review>> GetAllAsync();
        Task<Review?> GetByIdAsync(Guid id);
        Task<Review> CreateAsync(Review review);
        Task<Review?> UpdateAsync(Guid id, Review review);
        Task<Review?> DeleteAsync(Guid id);
    }
}
