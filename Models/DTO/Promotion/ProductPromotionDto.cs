using EcommerceWeb.Models.DTO.Product;

namespace EcommerceWeb.Models.DTO.Promotion
{
    public class ProductPromotionDto
    {
        public Guid ProductPromotionId { get; set; }
        public Guid ProductId { get; set; }
        public Guid PromotionId { get; set; }

        public ProductDto? ProductDto { get; set; }

        public PromotionDto? PromotionDto { get; set; }

    }
}
