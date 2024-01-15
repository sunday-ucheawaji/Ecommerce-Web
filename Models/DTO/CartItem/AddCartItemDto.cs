namespace EcommerceWeb.Models.DTO.CartItem
{
    public class AddCartItemDto
    {
        public int Quantity { get; set; }

        // Foreign Keys
        public Guid CartId { get; set; }
        public Guid ProductId { get; set; }
    }
}
