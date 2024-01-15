using EcommerceWeb.Models.DTO.Product;

namespace EcommerceWeb.Models.DTO.CartItem
{
    public class CartItemDto
    {

        public Guid CartItemId { get; set; }
        public int Quantity { get; set; }

        // Foreign Keys
        public Guid CartId { get; set; }
        public Guid ProductId { get; set; }

      
        public ProductBasicDto? Product { get; set; }
    }
}
