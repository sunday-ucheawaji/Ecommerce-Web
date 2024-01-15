using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceWeb.Models.DTO.Image
{
    public class ProductImageDto
    {
        public Guid ProductImageId { get; set; }

        public string FileName { get; set; }

        public string? FileDescription { get; set; }

        public string FileExtension { get; set; }

        public long FileSizeInBytes { get; set; }

        public string FilePath { get; set; }

        public Guid? ProductId { get; set; }

        public Guid? BillBoardId { get; set; }
    }
}
