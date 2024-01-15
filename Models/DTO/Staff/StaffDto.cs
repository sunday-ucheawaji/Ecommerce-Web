using System.ComponentModel.DataAnnotations;

namespace EcommerceWeb.Models.DTO.Staff
{
    public class StaffDto
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
    }
}
