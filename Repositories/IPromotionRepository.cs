using EcommerceWeb.Models.Domain;

namespace EcommerceWeb.Repositories
{
    public interface IPromotionRepository
    {
        Task<List<Promotion>> GetAllAsync();

        Task<Promotion?> GetByIdAsync(Guid id);

        Task<Promotion> CreateAsync(Promotion promotion);

        Task<Promotion?> UpdateAsync(Guid id, Promotion promotion);

        Task<Promotion?> DeleteAsync(Guid id);
    }
}
