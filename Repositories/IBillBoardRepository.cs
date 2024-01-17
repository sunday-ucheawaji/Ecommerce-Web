using EcommerceWeb.Models.Domain;

namespace EcommerceWeb.Repositories
{
    public interface IBillBoardRepository
    {
        Task<List<BillBoard>> GetAllAsync();
        Task<BillBoard?> GetByIdAsync(Guid id);
        Task<BillBoard> CreateAsync(BillBoard billboard);
        Task<BillBoard?> UpdateAsync(Guid id, BillBoard billboard);
        Task<BillBoard?> DeleteAsync(Guid id);
    }
}
