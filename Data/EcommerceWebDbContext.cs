using EcommerceWeb.Models.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EcommerceWeb.Data
{
    public class EcommerceWebDbContext:IdentityDbContext
    {
        public EcommerceWebDbContext(DbContextOptions<EcommerceWebDbContext> options): base(options)
        {
            
        }

        public DbSet<Customer>Customers { get; set; }

        public DbSet<Supplier> Suppliers { get; set; }

        public DbSet<Staff> Staff { get; set; }


    }
}
