using System.ComponentModel.DataAnnotations;

namespace EcommerceWeb.Models.DTO.Address
{
    public class UpdateAddressDto
    {

        [StringLength(50, MinimumLength = 3)]
        public string? City { get; set; }

        [StringLength(50, MinimumLength = 3)]
        public string? Street { get; set; }
    }
}
