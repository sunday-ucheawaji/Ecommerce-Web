namespace EcommerceWeb.Models.Domain
{
    public class ProductPromotion
    {
        public Guid ProductPromotionId { get; set; }
        public Guid ProductId { get; set; }

        public Product? Product { get; set; }

        public Guid PromotionId {  get; set; }

        public Promotion? Promotion { get; set; }
    }
}
