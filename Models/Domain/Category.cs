﻿namespace EcommerceWeb.Models.Domain
{
    public class Category
    {
        public Guid CategoryId { get; set; }

        public string CategoryName { get; set; }

        public string Description { get; set; }

        public List<Product>? Products { get; } = new List<Product>();

    }
}
