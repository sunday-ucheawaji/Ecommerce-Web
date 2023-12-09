namespace EcommerceWeb.Models.Domain
{
    public class Customer
    {
        public Guid CustomerId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public DateTime DateOfBirth { get; set; }

        // Foreign Key
        public Guid CustomUserId { get; set; }

        // Navigation Property
        public CustomUser CustomUser { get; set; }

    }
}
