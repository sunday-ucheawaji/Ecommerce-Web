namespace EcommerceWeb.Models.Domain
{
    public class Cart
    {
        public Guid CartId { get; set; }

        public ICollection<CartItem> CartItems { get; set; }
    }
}
