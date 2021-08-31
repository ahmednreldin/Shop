using Microsoft.AspNetCore.Mvc;
using Domain.Models.Products;
using Shop.Web.Models.Products;
using Application.Services.Fondations.FileManager;
using Application.Services.Fondations.Products;

namespace Shop.Web.Controllers
{
    public class ProductController : Controller
    {
        IFileManager fileManager;
        IProductService productService;

        public ProductController(IFileManager fileManager, IProductService productService)
        {
            this.fileManager = fileManager;
            this.productService = productService;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(ProductDTO productDTO)
        {
            try
            {
                Product product = MapProductDTO(productDTO);
                productService.AddProductAsync(product);
            }
            catch { }
            return View(productDTO);
        }

        private Product MapProductDTO(ProductDTO productDTO)
        {
            return new Product
            {
                Name = productDTO.Name,
                Salery = productDTO.Salery,
                Description = productDTO.Description,
                ImageUrl = fileManager.SaveImage(productDTO.ImageFile)
            };
        }
    }
}
