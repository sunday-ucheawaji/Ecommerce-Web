using System.ComponentModel.DataAnnotations;

namespace EcommerceWeb.Models.Domain
{
    public class Staff
    {
        public Guid StaffId { get; set; }

        [StringLength(50, MinimumLength = 3)]
        public string FirstName { get; set; }

        [StringLength(50, MinimumLength = 3)]
        public string LastName { get; set; }

        [StringLength(50, MinimumLength = 3)]
        public string Department { get; set; }

        [StringLength(50, MinimumLength = 3)]
        public string Position { get; set; }    

        public string OfficePhone { get; set; }

        // Foreign Key
        public Guid CustomUserId { get; set; }

        // Navigation Property
        public CustomUser CustomUser { get; set; }

        public ICollection<Address> Addresses { get; set; }
    }
}
