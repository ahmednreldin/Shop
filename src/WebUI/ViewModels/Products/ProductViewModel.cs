using Microsoft.AspNetCore.Http;

namespace Shop.Web.Models.Products
{
    public class ProductViewModel
    {
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string FullDescription { get; set; }
        public double Price { get; set; }
        public int StockQuantity { get; set; }
        public bool ShowOnHomePage { get; set; }
        public string Sku { get; set; }
        public IFormFile ImageFile { get; set; } = null;
    }
}
