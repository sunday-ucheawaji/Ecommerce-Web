using EcommerceWeb.Models.Domain;
using EcommerceWeb.Models.DTO.CartItem;

namespace EcommerceWeb.Models.DTO.Cart
{
    public class UpdateCartDTo
    {
        public ICollection<CartItemDto>? CartItems { get; set; }
    }
}
