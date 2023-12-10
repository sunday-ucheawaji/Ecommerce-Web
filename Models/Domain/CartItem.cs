namespace EcommerceWeb.Models.Domain
{
    public class CartItem
    {
        public Guid CartItemId { get; set; }
        public int Quantity { get; set; }   

        // Foreign Keys
        public Guid CartId { get; set; }
        public Guid ProductId { get; set; }

        // Navigation Properties
        public Cart Cart { get; set; }
        public Product Product { get; set; }

    }
}
