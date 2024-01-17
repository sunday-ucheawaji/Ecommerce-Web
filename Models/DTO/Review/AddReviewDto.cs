
using System.Text.Json.Serialization;

namespace EcommerceWeb.Models.DTO.Review
{
    public class AddReviewDto
    {
        public enum RatingEnum
        {
            One = 1, Two = 2, Three = 3, Four = 4, Five = 5,
        }
       

        public string Name { get; set; }

        public string Description { get; set; }

        [JsonPropertyName("rating")]
        public RatingEnum? Rating { get; set; } = RatingEnum.One;


        // Foreign key
        public Guid ProductId { get; set; }
    }
}
