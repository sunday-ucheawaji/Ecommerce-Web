using EcommerceWeb.Models.Domain;
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

        public DbSet<Customer>Customers { get; set; }

        public DbSet<Supplier> Suppliers { get; set; }

        public DbSet<Staff> Staff { get; set; }


    }
}
