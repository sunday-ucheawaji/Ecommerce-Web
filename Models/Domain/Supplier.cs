namespace EcommerceWeb.Models.Domain
{
    public class Supplier
    {
        public Guid SupplierID { get; set; }

        public string CompanyName { get; set; } 
        public string ContactName { get; set; }

        public string Address { get; set; }

        // Foreign Key
        public Guid CustomUserId { get; set; }

        // Navigation Property

        public CustomUser CustomUser { get; set; }


    }
}
