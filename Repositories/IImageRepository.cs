using EcommerceWeb.Models.Domain;

namespace EcommerceWeb.Repositories
{
    public interface IImageRepository
    {
        Task<ProductImage> Upload(ProductImage image);
    }
}
