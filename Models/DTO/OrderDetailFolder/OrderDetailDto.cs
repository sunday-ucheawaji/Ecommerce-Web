using EcommerceWeb.Models.DTO.Product;

namespace EcommerceWeb.Models.DTO.OrderDetailFolder
{
    public class OrderDetailDto
    {
        public Guid OrderDetailId { get; set; }
        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        // Foreign Keys
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }

        public Guid CustomerId { get; set; }

        // Navigation Properties
        public ProductBasicDto Product { get; set; }
    }
}
