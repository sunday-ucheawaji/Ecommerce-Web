namespace EcommerceWeb.Models.Domain
{
    public class Product
    {
        public Guid ProductId { get; set; } 
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public int StockQuantity { get; set; }

        public bool IsFeatured { get; set; }

        public bool IsArchived { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;





        // Navigation Properties
        public ICollection<ProductPromotion>? ProductPromotions { get; set; } = new List<ProductPromotion>();
        public ICollection<ProductImage> ProductImages { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }

        public List<Category> Categories { get; } = new();
       

    }
}
