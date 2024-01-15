using EcommerceWeb.Models.Domain;

namespace EcommerceWeb.Repositories
{
    public interface IImageRepository
    {
        Task<ProductImage> Upload(ProductImage image);

        Task<List<ProductImage>> GetAll();

        Task<ProductImage> GetById(Guid productImageId);

        Task<ProductImage?> Update(Guid productImageId, ProductImage image);

        Task<ProductImage?> UpdateProductId(Guid productImageId, Guid productId);

        Task<ProductImage?> Delete(Guid productImageId);
    }
}
