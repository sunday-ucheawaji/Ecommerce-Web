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

        public async Task<List<Product>> GetAllAsync(
            string? filterOn = null, 
            string? filterQuery = null, 
            string? sortBy = null,
            bool isAscending = true,
            int pageNumber = 1,
            int pageSize = 1000
            )
        {
            var products = dbContext.Products
                .Include(ps => ps.ProductImages)
                .Include(pa => pa.Categories)
                .Include(p => p.ProductPromotions)
                .ThenInclude(pp => pp.Promotion)
                .AsQueryable();

            // Filtering
            if (string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
            {
                if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    products = products.Where(x => x.Name.Contains(filterQuery));

                }
            }

            // Sorting
            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if (sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    products = isAscending ? 
                    products.OrderBy(x => x.Name) 
                    : 
                    products.OrderByDescending(x => x.Name);

                }
            }

            // Pagination
            var skipResult = (pageNumber - 1) * pageSize;

            return await products.Skip(skipResult).Take(pageSize).ToListAsync();
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

            if (product.Name != null)
            {
                existingProduct.Name = product.Name;
            }

            if (product.Description != null) 
            { 
                existingProduct.Description = product.Description; 
            }

            if (product.Price != 0)
            {
                existingProduct.Price = product.Price;

            }

            if (product.StockQuantity != 0)
            {
                existingProduct.StockQuantity = product.StockQuantity;
            }

           
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
