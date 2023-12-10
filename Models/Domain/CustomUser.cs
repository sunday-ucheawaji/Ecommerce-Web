using Microsoft.AspNetCore.Identity;

namespace EcommerceWeb.Models.Domain
{
    public class CustomUser:IdentityUser<Guid>
    {
        public Customer? Customer { get; set; }

        public Supplier? Supplier { get; set; }

        public Staff? Staff { get; set;}
    }
}
