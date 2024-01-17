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
            return await dbContext.Products
                .Include(ps => ps.ProductImages)
                .Include(pa => pa.Categories)
                .Include(p => p.ProductPromotions)
                .ThenInclude(pp => pp.Promotion).ToListAsync();       
        }

        public async Task<Product?> GetByIdAsync(Guid id)
        {
            return await dbContext.Products
                .Include(ps => ps.ProductImages)
                .Include(pa => pa.Categories)
                .Include(p => p.ProductPromotions)
                .ThenInclude(pp => pp.Promotion)
                .FirstOrDefaultAsync(x => x.ProductId == id);
          
        }

      
        public async Task<Product> CreateAsync(Product product)
        {
            
            await dbContext.Products.AddAsync(product);
            await dbContext.SaveChangesAsync();
            return product;
        }

        public async Task<Product?> UpdateAsync(Guid productId, Product product, ICollection<Guid> productImageIDs)
        {
            var existingProduct = await dbContext.Products
                .FirstOrDefaultAsync(x=>x.ProductId == productId);

            if (existingProduct == null)
            {
                return null;   
            }
            
            existingProduct.Name = product.Name;
            existingProduct.Description = product.Description;
            existingProduct.Price = product.Price;
            existingProduct.StockQuantity = product.StockQuantity;
            existingProduct.IsArchived = product.IsArchived;
            existingProduct.IsFeatured = product.IsFeatured;
            existingProduct.UpdatedAt = DateTime.Today;

            
            var allImagesToSetNull = await dbContext
                .ProductImages.Where(
                x=> x.ProductId == productId)
                .ToListAsync();

            foreach (var image in allImagesToSetNull)
            {
                if(image.ProductId != null)
                {
                    image.ProductId = null;
                }
            }

            if (product.ProductImages != null && !product.ProductImages.Any()) 
            {

                var selectedImages = await dbContext.ProductImages
                    .Where(
                    image => productImageIDs
                    .Contains(image.ProductImageId))
                    .ToListAsync();
                foreach(var selectedImage in selectedImages)
                {
                    selectedImage.ProductId = productId;
                }

            }

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
            var ProductImages = await dbContext.ProductImages.Where(x => x.ProductId == id).ToListAsync();

            if (ProductImages != null)
            {
                foreach (var image in ProductImages)
                {
                    image.ProductId = null;
                }
                await dbContext.SaveChangesAsync();
            }
            dbContext.Products.Remove(existingProduct);
            await dbContext.SaveChangesAsync();

            return existingProduct;
        }

      

       
    }
}
