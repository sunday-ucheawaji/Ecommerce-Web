using System.ComponentModel.DataAnnotations;

namespace EcommerceWeb.Models.DTO.Auth
{
    public class RegisterSupplierDto
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [StringLength(50, MinimumLength = 3)]
        public string CompanyName { get; set; }

        [StringLength(50, MinimumLength = 3)]
        public string ContactName { get; set; }
    }
}
