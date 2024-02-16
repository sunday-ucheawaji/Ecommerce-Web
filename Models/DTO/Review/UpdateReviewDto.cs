namespace EcommerceWeb.Models.DTO.Review
{
    public class UpdateReviewDto
    {
        public enum RatingEnum
        {
            One = 1, Two = 2, Three = 3, Four = 4, Five = 5,
        }


        public string? Name { get; set; }

        public string? Description { get; set; }

        public RatingEnum? Rating { get; set; } = RatingEnum.One;


        // Foreign key
        public Guid? ProductId { get; set; }
    }
}
