using EcommerceWeb.Data;
using EcommerceWeb.Models.Domain;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

namespace EcommerceWeb.Repositories
{
    public class LocalImageRepository : IImageRepository
    {

        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly EcommerceWebDbContext _dbContext;

        public LocalImageRepository(
            IWebHostEnvironment webHostEnvironment, 
            IHttpContextAccessor httpContextAccessor, 
            EcommerceWebDbContext dbContext)
        {
            _webHostEnvironment = webHostEnvironment;
            _httpContextAccessor = httpContextAccessor;
            _dbContext = dbContext;
            
        }

        public async Task<ProductImage?> Delete(Guid productImageId)
        {
            var existingProductImage = await _dbContext.ProductImages.FirstOrDefaultAsync(x => x.ProductImageId == productImageId);
            if (existingProductImage == null)
            {
                return null;
            }
            _dbContext.ProductImages.Remove(existingProductImage);
            await _dbContext.SaveChangesAsync();
            return existingProductImage;
        }

        public async Task<List<ProductImage>> GetAll()
        {
            return await _dbContext.ProductImages.ToListAsync();
        }

        public async Task<ProductImage?> GetById(Guid productImageId)
        {
            return await _dbContext.ProductImages.FirstOrDefaultAsync(x => x.ProductImageId == productImageId);
        }

        public async Task<ProductImage?> Update(Guid productImageId, ProductImage image)
        {
            var existingProductImage = await _dbContext.ProductImages.FirstOrDefaultAsync(x=> x.ProductImageId == productImageId);
            if (existingProductImage == null)
            {
                return null;
            }

            if (image.BillBoardId != null) 
            { 
                existingProductImage.BillBoardId = image.BillBoardId;
            }

            if(image.ProductId != null)
            {
                existingProductImage.ProductId = image.ProductId;
            }

            if(image.FileDescription != null)
            {

                existingProductImage.FileDescription = image.FileDescription;
            }

            if (image.FileName != null)
            {

                existingProductImage.FileName = image.FileName;
            }

            await _dbContext.SaveChangesAsync();

            return existingProductImage;
        }

        public async Task<ProductImage?> UpdateProductId(Guid productImageId, Guid productId)
        {
            var existingProductImage = await _dbContext.ProductImages.FirstOrDefaultAsync(x => x.ProductImageId == productImageId);
            if (existingProductImage == null)
            {
                return null;
            }
            existingProductImage.ProductId = productId;
            await _dbContext.SaveChangesAsync();

            return existingProductImage;

        }

        public async Task<ProductImage?> UpdateBillBoardId(Guid productImageId, Guid billBoardId)
        {
            var existingProductImage = await _dbContext.ProductImages.FirstOrDefaultAsync(x => x.ProductImageId == productImageId);
            if (existingProductImage == null)
            {
                return null;
            }
            existingProductImage.BillBoardId = billBoardId;
            await _dbContext.SaveChangesAsync();

            return existingProductImage;

        }

        public async Task<ProductImage> Upload(ProductImage image)
        {
            var localFilePath = Path.Combine(_webHostEnvironment.ContentRootPath,
                "Images", $"{image.FileName}{image.FileExtension}");

            // Upload Image to Local Path
            using var stream = new FileStream(localFilePath, FileMode.Create);
            await image.File.CopyToAsync(stream);

            var urlFilePath = $"{_httpContextAccessor.HttpContext.Request.Scheme}" +
                $"://{_httpContextAccessor.HttpContext.Request.Host}" +
                $"{_httpContextAccessor.HttpContext.Request.PathBase}/Images/" +
                $"{image.FileName}{image.FileExtension}";

            image.FilePath = urlFilePath;
            // Add Image to the Images Table
            await _dbContext.ProductImages.AddAsync(image);
            await _dbContext.SaveChangesAsync();

            return image;
        }
    }
}
