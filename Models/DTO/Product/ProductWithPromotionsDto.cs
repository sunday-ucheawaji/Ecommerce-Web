using EcommerceWeb.Models.DTO.Promotion;

namespace EcommerceWeb.Models.DTO.Product
{
    public class ProductWithPromotionsDto
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price {  get; set; }

        public int StockQuantity { get; set; }

        public bool IsFeatured { get; set; }

        public bool IsArchived { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set;}

        public List<PromotionDto>? Promotions { get; set; }
    }
}
