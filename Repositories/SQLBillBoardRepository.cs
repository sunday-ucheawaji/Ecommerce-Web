using EcommerceWeb.Data;
using EcommerceWeb.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace EcommerceWeb.Repositories
{
    public class SQLBillBoardRepository : IBillBoardRepository
    {

        private readonly EcommerceWebDbContext _dbContext;

        public SQLBillBoardRepository(EcommerceWebDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<BillBoard>> GetAllAsync()
        {
            return await _dbContext.BillBoards.ToListAsync();
        }

        public async Task<BillBoard?> GetById(Guid id)
        {
            return await _dbContext.BillBoards.FirstOrDefaultAsync(x => x.BillBoardId == id);
        }
        public async Task<BillBoard> CreateAsync(BillBoard billBoard)
        {
            await _dbContext.BillBoards.AddAsync(billBoard);
            await _dbContext.SaveChangesAsync();
            return billBoard;
        }
        public async Task<BillBoard?> UpdateAsync(Guid id, BillBoard billBoard)
        {
            var existingBillBoard = await _dbContext.BillBoards
                .FirstOrDefaultAsync(x => x.BillBoardId == id);

            if (existingBillBoard == null)
            {
                return null;
            }

            if (billBoard.BillBoardName != null)
            {
                existingBillBoard.BillBoardName = billBoard.BillBoardName;
            }
            await _dbContext.SaveChangesAsync();
            return billBoard;
        }

        public async Task<BillBoard?> DeleteAsync(Guid id)
        {
            var existingBillBoard = await _dbContext
                .BillBoards.FirstOrDefaultAsync(x=> x.BillBoardId==id);

            if (existingBillBoard == null)
            {
                return null;
            }

            _dbContext.BillBoards.Remove(existingBillBoard);
            await _dbContext.SaveChangesAsync();
            return existingBillBoard;
        }

    }
}
