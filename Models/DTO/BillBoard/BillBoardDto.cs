using EcommerceWeb.Models.Domain;
using System.ComponentModel.DataAnnotations;

namespace EcommerceWeb.Models.DTO.BillBoard
{
    public class BillBoardDto
    {
        public Guid BillBoardId { get; set; }

        [StringLength(50, MinimumLength = 3)]
        public string BillBoardName { get; set; }

        // Navigation Properties 

        public ICollection<ProductImage> ProductImages { get; set; }
    }
}
