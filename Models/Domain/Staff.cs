namespace EcommerceWeb.Models.Domain
{
    public class Staff
    {
        public Guid StaffId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Department { get; set; }

        public string Position { get; set; }    

        public string OfficeAddress { get; set; }

        public string OfficePhone { get; set; }

        // Foreign Key
        public Guid CustomUserId { get; set; }

        // Navigation Property
        public CustomUser CustomUser { get; set; }
    }
}
