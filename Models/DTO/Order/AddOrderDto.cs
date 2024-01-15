using EcommerceWeb.Models.DTO.CartItem;

namespace EcommerceWeb.Models.DTO.Order
{
    public class AddOrderDto
    {

        public ICollection<SimpleCartItemDto>? CartItems { get; set; }
        public Guid CustomerId { get; set; }
    }
}
