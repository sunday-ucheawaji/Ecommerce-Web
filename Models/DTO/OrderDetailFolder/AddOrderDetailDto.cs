namespace EcommerceWeb.Models.DTO.OrderDetailFolder
{
    public class AddOrderDetailDto
    {
        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        // Foreign Keys
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
    }
}
