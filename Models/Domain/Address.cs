using System.ComponentModel.DataAnnotations;

namespace EcommerceWeb.Models.Domain
{
    public class Address
    {
        public Guid AddressId {  get; set; }

        [StringLength(50, MinimumLength = 3)]
        public string City { get; set; }

        [StringLength(50, MinimumLength = 3)]
        public string Street { get; set; }

        // Foreign key
        public Guid? CustomerId {  get; set; }
        public Guid? SupplierId {  get; set; }

        public Guid? StaffId { get; set; }

    }
}
