using System.ComponentModel.DataAnnotations;

namespace EcommerceWeb.Models.Domain
{
    public class Customer
    {
        public Guid CustomerId { get; set; }

        [StringLength(50, MinimumLength = 3)]
        public string FirstName { get; set; }

        [StringLength(50, MinimumLength = 3)]
        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        // Foreign Key
        public Guid CustomUserId { get; set; }

        // Navigation Property
        public CustomUser CustomUser { get; set; }
        public IList<Order> Orders { get; set; }
        public ICollection<Address> Addresses { get; set; }


    }
}
