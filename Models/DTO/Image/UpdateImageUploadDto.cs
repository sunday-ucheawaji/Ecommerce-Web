using System.ComponentModel.DataAnnotations;

namespace EcommerceWeb.Models.DTO.Image
{
    public class UpdateImageUploadDto
    {

        public Guid? ProductId { get; set; }
        public string? FileName { get; set; }
        public string? FileDescription { get; set; }
    }
}
