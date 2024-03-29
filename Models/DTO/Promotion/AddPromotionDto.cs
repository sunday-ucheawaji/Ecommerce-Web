﻿using System.ComponentModel.DataAnnotations;

namespace EcommerceWeb.Models.DTO.Promotion
{
    public class AddPromotionDto
    {
        [StringLength(100, MinimumLength = 3)]
        public string Title { get; set; }

        [StringLength(100, MinimumLength = 3)]
        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        [Range(0, 100)]
        public int DiscountPercentage { get; set; }
    }
}
