using System;

namespace Shop.Models.Products
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public double Salery { get; set; }
    }
}
