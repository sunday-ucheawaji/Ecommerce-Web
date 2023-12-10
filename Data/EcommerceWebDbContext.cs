using EcommerceWeb.Models.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace EcommerceWeb.Data
{
    public class EcommerceWebDbContext:IdentityDbContext<CustomUser, IdentityRole<Guid>,Guid>
    {
        public EcommerceWebDbContext(DbContextOptions<EcommerceWebDbContext> options): base(options)
        {
            
        }

        public DbSet<CustomUser> CustomUsers { get; set; }


        public DbSet<Customer>Customers { get; set; }

        public DbSet<Supplier> Suppliers { get; set; }

        public DbSet<Staff> Staff { get; set; }

        public DbSet<Address> Addresses { get; set; }

        public DbSet<BillBoard> BillBoards { get; set; }

        public DbSet<Cart> Carts { get; set; }

        public DbSet<CartItem> CartItems { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Promotion> Promotions { get; set; }
        public DbSet<Review> Reviews { get; set; }



        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Configure one-to-one relationship between CustomUser and Customer
            builder.Entity<CustomUser>()
                .HasOne(c => c.Customer)
                .WithOne(cu => cu.CustomUser)
                .HasForeignKey<Customer>(c => c.CustomUserId);


            // Configure one-to-one relationship between CustomUser and Customer
            builder.Entity<CustomUser>()
               .HasOne(s => s.Supplier)
               .WithOne(su => su.CustomUser)
               .HasForeignKey<Supplier>(s => s.CustomUserId);

            // Configure one-to-one relationship between CustomUser and Customer
            builder.Entity<CustomUser>()
                .HasOne(s => s.Staff)
                .WithOne(su => su.CustomUser)
                .HasForeignKey<Staff>(s => s.CustomUserId);

            builder.Entity<OrderDetail>()
                .Property(od => od.UnitPrice)
                .HasColumnType("decimal(18,2)"); // Adjust the precision and scale as needed

            builder.Entity<Product>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18,2)"); // Adjust the precision and scale as needed

        }


    }
}
