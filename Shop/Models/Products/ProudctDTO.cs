using Microsoft.AspNetCore.Http;

namespace Shop.Web.Models.Products
{
    public class ProudctDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Salery { get; set; }
        public IFormFile ImageFile { get; set; }
    }
}
