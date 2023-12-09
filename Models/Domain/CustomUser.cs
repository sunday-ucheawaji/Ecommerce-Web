using Microsoft.AspNetCore.Identity;

namespace EcommerceWeb.Models.Domain
{
    public class CustomUser:IdentityUser
    {
        public Customer? Customer { get; set; }

        public Supplier? Supplier { get; set; }

        public Staff? Staff { get; set;}
    }
}
