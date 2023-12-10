using System.ComponentModel.DataAnnotations;

namespace EcommerceWeb.Models.Domain
{
    public class Supplier
    {
        public Guid SupplierID { get; set; }

        [StringLength(50, MinimumLength = 3)]
        public string CompanyName { get; set; }

        [StringLength(50, MinimumLength = 3)]
        public string ContactName { get; set; }

        // Foreign Key
        public Guid CustomUserId { get; set; }

        // Navigation Property
        public CustomUser CustomUser { get; set; }

        public ICollection<Address> Addresses { get; set; }


    }
}
