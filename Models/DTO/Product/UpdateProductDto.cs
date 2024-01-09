namespace EcommerceWeb.Models.DTO.Product
{
    public class UpdateProductDto
    {
        public string? Name { get; set; }

        public string? Description { get; set; }

        public decimal? Price { get; set; }

        public int? StockQuantity { get; set; }

        public bool? IsFeatured { get; set; }

        public bool? IsArchived { get; set; }

        // Navigation Properties
        public List<Guid>? PromotionIds { get; set; } = new List<Guid>();
        public ICollection<Guid>? ProductImageIds { get; set; } = new List<Guid>();
        public ICollection<Guid>? OrderDetailIds { get; set; } = new List<Guid>();
        public List<Guid>? CategoryIds { get; set; } = new List<Guid>();
    }
}
