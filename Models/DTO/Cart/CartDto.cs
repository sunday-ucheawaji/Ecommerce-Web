using EcommerceWeb.Models.Domain;
using EcommerceWeb.Models.DTO.CartItem;

namespace EcommerceWeb.Models.DTO.Cart
{
    public class CartDto
    {
        public Guid CartId { get; set; }

        public ICollection<CartItemDto>? CartItems { get; set; }
    }
}