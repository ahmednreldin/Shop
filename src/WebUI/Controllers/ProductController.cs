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
        public IActionResult Create(ProductViewModel ProductViewModel)
        {
            try
            {
                Product product = MapProductDTO(ProductViewModel);
                productService.AddProductAsync(product);
            }
            catch { }
            return View(ProductViewModel);
        }

        private Product MapProductDTO(ProductViewModel ProductViewModel)
        {
            return new Product
            {
                Name = ProductViewModel.Name,
                Price = ProductViewModel.Salery,
                ShortDescription = ProductViewModel.Description,
                FullDescription = ProductViewModel.Description,
                Picture = fileManager.SaveImage(ProductViewModel.ImageFile)
            };
        }
    }
}
