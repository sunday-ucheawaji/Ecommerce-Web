using System.ComponentModel.DataAnnotations.Schema;
using static EcommerceWeb.Models.Domain.Order;

namespace EcommerceWeb.Models.Domain
{
    public class Review
    {

        public enum RatingEnum
        {
            One=1,Two=2,Three=3,Four=4,Five=5,
        }
        public Guid ReviewId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime DateReview { get; set; } = DateTime.Now;

        public RatingEnum? Rating { get; set; }= RatingEnum.One;

        [NotMapped]
        public RatingEnum ReviewRatingEnum
        {
            get => (RatingEnum)Rating;
            set => Rating = value;
        }


        // Foreign key
        public Guid ProductId { get; set; }

        // Navigation Property
        public Product Product { get; set; }
    }
}
