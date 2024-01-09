using EcommerceWeb.Data;
using EcommerceWeb.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace EcommerceWeb.Repositories
{
    public class SQLProductRepository : IProductRepository
    {
        private readonly EcommerceWebDbContext dbContext;

        public SQLProductRepository(EcommerceWebDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<Product>> GetAllAsync()
        {
            return await dbContext.Products.Include(p => p.ProductPromotions).ThenInclude(pp => pp.Promotion).ToListAsync();       
        }

        public async Task<Product?> GetByIdAsync(Guid id)
        {
            return await dbContext.Products
                .Include(p => p.ProductPromotions)
                .ThenInclude(pp => pp.Promotion)
                .FirstOrDefaultAsync(x => x.ProductId == id);
          
        }

      
        public async Task<Product> CreateAsync(Product product)
        {
            //var promotionList = new List<ProductPromotion>();

            //foreach (var promotionId in promotionIds)
            //{
            //    promotionList.Add(
            //        new ProductPromotion
            //        {
            //            ProductId = product.ProductId,
            //            PromotionId = promotionId,
            //            ProductPromotionId = Guid.NewGuid(),
            //        }
            //    );
            //}
            //product.ProductPromotions = promotionList;
            await dbContext.Products.AddAsync(product);
            await dbContext.SaveChangesAsync();
            return product;
        }

        public async Task<Product?> UpdateAsync(Product product)
        {
            var existingProduct = await dbContext.Products.FirstOrDefaultAsync(x=>x.ProductId == product.ProductId);
            if (existingProduct == null)
            {
                return null;   
            }

            existingProduct.Name = product.Name != null ? product.Name : existingProduct.Name;
            existingProduct.Description = product.Description != null ? product.Description : existingProduct.Description;
            existingProduct.Price = product.Price != 0 ? product.Price : existingProduct.Price;
            existingProduct.StockQuantity = product.StockQuantity != 0 ? product.StockQuantity : existingProduct.StockQuantity;
            existingProduct.IsFeatured = product.IsFeatured != false ? product.IsFeatured : existingProduct.IsFeatured;
            existingProduct.IsArchived = product.IsArchived != false ? product.IsArchived : existingProduct.IsArchived;


            await dbContext.SaveChangesAsync();
            return existingProduct;

        }

        public async Task<Product?> DeleteAsync(Guid id)
        {
            var existingProduct = await dbContext.Products.FirstOrDefaultAsync(x=>x.ProductId == id);
            if (existingProduct == null)
            {
                return null;
            }
            dbContext.Products.Remove(existingProduct);
            await dbContext.SaveChangesAsync();
            return existingProduct;
        }

      

       
    }
}
