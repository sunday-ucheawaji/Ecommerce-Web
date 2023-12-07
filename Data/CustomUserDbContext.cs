using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EcommerceWeb.Data
{
    public class CustomUserDbContext:IdentityDbContext
    {
        public CustomUserDbContext(DbContextOptions<CustomUserDbContext> options): base(options)
        {
            
        }
    }
}
