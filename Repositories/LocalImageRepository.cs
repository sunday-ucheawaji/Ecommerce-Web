using EcommerceWeb.Data;
using EcommerceWeb.Models.Domain;

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
