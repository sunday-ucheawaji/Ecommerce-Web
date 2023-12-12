using System.ComponentModel.DataAnnotations;

namespace EcommerceWeb.Models.DTO.Auth
{
    public class RegisterStaffDto
    {

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

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
