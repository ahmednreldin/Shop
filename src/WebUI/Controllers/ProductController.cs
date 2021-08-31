using Microsoft.AspNetCore.Mvc;
using Domain.Models.Products;
using Shop.Web.Models.Products;
using Application.Services.Fondations.FileManager;
using Application.Services.Fondations.Products;
using System;
using Domain.Models.Products.Exceptions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Threading.Tasks;

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
        public async ValueTask<ActionResult<Product>> Create(ProductViewModel ProductViewModel)
        {
            try
            {
                Product product = MapProductDTO(ProductViewModel);
                await productService.AddProductAsync(product);
                return RedirectToAction(nameof(Index));
            }
            catch(ProductValidationException productValidationException) 
            {
                ViewBag.Message = productValidationException.Message;
                ViewBag.innerException = productValidationException.InnerException.Message;
                return View();
            }
        }

        private Product MapProductDTO(ProductViewModel ProductViewModel)
        {
            return new Product
            {
                ProductId = Guid.NewGuid(),
                Name = ProductViewModel.Name,
                Price = ProductViewModel.Price,
                ShortDescription = ProductViewModel.ShortDescription,
                FullDescription = ProductViewModel.FullDescription,
                Sku = ProductViewModel.Sku,
                StockQuantity = ProductViewModel.StockQuantity,
                ShowOnHomepage = ProductViewModel.ShowOnHomePage,
                Picture = fileManager.SaveImage(ProductViewModel.ImageFile)
            };
        }
    }
}
