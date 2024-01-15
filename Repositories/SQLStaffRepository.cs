using EcommerceWeb.Data;
using EcommerceWeb.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace EcommerceWeb.Repositories
{
    public class SQLStaffRepository : IStaffRepository
    {
        private readonly EcommerceWebDbContext dbContext;

        public SQLStaffRepository(EcommerceWebDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<Staff>> GetAllAsync()
        {
            return await dbContext.Staff.ToListAsync();
        }

        public async Task<Staff?> GetByIdAsync(Guid id)
        {
            return await dbContext.Staff.FirstOrDefaultAsync(x=> x.StaffId == id);  
        }
        public async Task<Staff> CreateAsync(Staff staff)
        {
            await dbContext.Staff.AddAsync(staff);
            await dbContext.SaveChangesAsync();
            return staff;
        }
        public async Task<Staff?> UpdateAsync(Guid id, Staff staff)
        {
            var existingStaff = await dbContext.Staff.FirstOrDefaultAsync(x => x.StaffId == id);
            if (existingStaff ==  null)
            {
                return null;
            }
            existingStaff.OfficePhone = staff.OfficePhone;
            existingStaff.FirstName = staff.FirstName;
            existingStaff.LastName = staff.LastName;
            existingStaff.Department = staff.Department;
            existingStaff.Position = staff.Position;

            await dbContext.SaveChangesAsync();

            return existingStaff;

        }

        public async Task<Staff?> DeleteAsync(Guid id)
        {
            var existingStaff = await dbContext.Staff.FirstOrDefaultAsync(x => x.StaffId == id);
            if (existingStaff == null)
            {
                return null;
            }
            dbContext.Staff.Remove(existingStaff);
            await dbContext.SaveChangesAsync();
            return existingStaff;
        }
    }


}
