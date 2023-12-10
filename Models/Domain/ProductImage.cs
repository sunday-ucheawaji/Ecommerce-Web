using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceWeb.Models.Domain
{
    public class ProductImage
    {
        public Guid ProductImageId { get; set; }

        [NotMapped]
        public IFormFile File { get; set; }

        public string FileName { get; set; }

        public string? FileDescription { get; set; }

        public string FileExtension { get; set; }

        public long FileSizeInBytes { get; set; }

        public string FilePath { get; set; }

        public Guid? ProductId { get; set; }

        public Guid? BillBoardId { get; set; }


    }
}
