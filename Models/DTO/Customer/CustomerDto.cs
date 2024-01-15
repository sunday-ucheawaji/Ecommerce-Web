using System.ComponentModel.DataAnnotations;

namespace EcommerceWeb.Models.DTO.Customer
{
    public class CustomerDto
    {
        public Guid CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }


    }
}
