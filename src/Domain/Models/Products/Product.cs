using System;

namespace Domain.Models.Products
{
    public class Product
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string FullDescription { get; set; }
        public string Picture { get; set; }
        public double Price { get; set; }
        public int StockQuantity { get; set; }
        public bool ShowOnHomepage { get; set; } 
        public string Sku { get; set; }

    }
}
