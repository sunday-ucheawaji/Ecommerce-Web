using EcommerceWeb.Models.Domain;

namespace EcommerceWeb.Repositories
{
    public interface IBillBoardRepository
    {
        Task<List<BillBoard>> GetAllAsync();

        Task<BillBoard?> GetById(Guid id);

        Task<BillBoard> CreateAsync(BillBoard billBoard);

        Task<BillBoard?> UpdateAsync(Guid id, BillBoard billBoard);

        Task<BillBoard?> DeleteAsync(Guid id);
    }
}
