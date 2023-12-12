using System.ComponentModel.DataAnnotations;

namespace EcommerceWeb.Models.DTO.Supplier
{
    public class SupplierDto
    {
        public Guid SupplierId { get; set; }

        [StringLength(50, MinimumLength = 3)]
        public string CompanyName { get; set; }

        [StringLength(50, MinimumLength = 3)]
        public string ContactName { get; set; }
        
    }
}
