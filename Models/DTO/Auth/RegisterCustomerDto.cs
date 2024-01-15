using System.ComponentModel.DataAnnotations;

namespace EcommerceWeb.Models.DTO.Auth
{
    public class RegisterCustomerDto
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email {  get; set; }

        [Required]
        [DataType (DataType.Password)]
        public string Password { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string LastName { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }


    }
}
