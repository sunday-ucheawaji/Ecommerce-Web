namespace EcommerceWeb.Models.DTO.CartItem
{
    public class SimpleCartItemDto
    {
        public int Quantity { get; set; }

        // Foreign Keys
        public Guid ProductId { get; set; }
    }
}
