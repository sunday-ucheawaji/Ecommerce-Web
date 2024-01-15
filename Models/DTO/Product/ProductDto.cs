using EcommerceWeb.Models.Domain;
using EcommerceWeb.Models.DTO.Image;
using EcommerceWeb.Models.DTO.Promotion;

namespace EcommerceWeb.Models.DTO.Product
{
    public class ProductDto
    {
        public Guid ProductId { get; set; }
        public string? Name { get; set; }
        
        public string? Description { get; set; }

        public decimal? Price { get; set; }

        public int? StockQuantity { get; set; }

        public bool? IsFeatured { get; set; }

        public bool? IsArchived { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;



        // Navigation Properties

        public List<PromotionDto>? Promotions { get; set; } = new List<PromotionDto>();

        //public ICollection<ProductPromotionDto>? ProductPromotionDto { get; set; } = new List<ProductPromotionDto>();  

        public List<ProductImageDto>? ProductImages { get; set; } = new List<ProductImageDto>();

        public ICollection<OrderDetail>? OrderDetails { get; set; }
        public ICollection<Category>? Categories { get; set; }


    }
}
