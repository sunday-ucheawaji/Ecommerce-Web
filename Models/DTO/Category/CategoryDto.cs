using EcommerceWeb.Models.DTO.Product;

namespace EcommerceWeb.Models.DTO.Category
{
    public class CategoryDto
    {

        public Guid CategoryId { get; set; }

        public string CategoryName { get; set; }

        public string Description { get; set; }

        public List<ProductDto>? Products { get; } = new List<ProductDto>();
    }
}
