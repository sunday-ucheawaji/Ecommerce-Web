using EcommerceWeb.Models.Domain;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceWeb.Repositories
{
    public interface IStaffRepository
    {
        Task<List<Staff>> GetAllAsync();
        Task<Staff?> GetByIdAsync(Guid id);
        Task<Staff> CreateAsync(Staff staff);
        Task<Staff?> UpdateAsync(Guid id,Staff staff);
        Task<Staff?> DeleteAsync(Guid id);  
    }
}
